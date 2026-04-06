using System.Numerics;
using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Core;
using Frank.GameEngine.Input;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.RayLib;

var game = new BattleshipGame();
game.Setup();

var (scene, cellViews) = CreateBattleScene(game);
var engine = new GameEngine2D(new SilentAudioPlayer(), new NullInputSource());
engine.Scene2DManager.GameScenes.Add(scene);
engine.Scene2DManager.SelectScene(scene.Id);

var renderer = new RayLibRenderer2D(960, 640, "Battleship — AI vs AI (1s / move)");
engine.Initialize(renderer);

var actionTimer = 0f;
try
{
    while (!renderer.ShouldClose)
    {
        if (!game.IsOver)
        {
            actionTimer += renderer.FrameDeltaSeconds;
            while (actionTimer >= BattleshipLayout.ActionIntervalSeconds && !game.IsOver)
            {
                actionTimer -= BattleshipLayout.ActionIntervalSeconds;
                game.StepAiTurn();
            }
        }

        UpdateCellTints(game, cellViews);
        engine.Draw();
    }
}
finally
{
    engine.Dispose();
}

static (Scene2D scene, CellView[] cells) CreateBattleScene(BattleshipGame game)
{
    var camera2D = new Camera2D
    {
        Target = Vector2.Zero,
        Zoom = 1f,
        ViewportWidth = 960,
        ViewportHeight = 640
    };

    var scene = new Scene2D("Battleship", camera2D)
    {
        BackgroundColor = new Rgba32(18, 22, 32)
    };

    var cells = new List<CellView>();
    var z = 0;

    void AddPanel(float originX, float originY, PanelKind kind, int playerIndex)
    {
        for (var r = 0; r < BattleshipLayout.RowsPerPlayer; r++)
        for (var c = 0; c < BattleshipLayout.Columns; c++)
        {
            var worldR = kind switch
            {
                PanelKind.OwnFleet => PlayerRowStart(playerIndex) + r,
                PanelKind.Radar => PlayerRowStart(1 - playerIndex) + r,
                _ => 0
            };
            var worldC = c;
            var go = new GameObject2D
            {
                Name = $"{kind} P{playerIndex + 1} ({r},{c})",
                ZOrder = z++,
                Transform = new Transform2D
                {
                    Position = new Vector2(
                        originX + c * (BattleshipLayout.CellSize + BattleshipLayout.CellGap) + BattleshipLayout.CellSize * 0.5f,
                        originY + r * (BattleshipLayout.CellSize + BattleshipLayout.CellGap) + BattleshipLayout.CellSize * 0.5f)
                },
                Sprite = new Sprite2D
                {
                    Size = new Vector2(BattleshipLayout.CellSize, BattleshipLayout.CellSize),
                    Tint = Rgba32.Black
                }
            };
            scene.GameObjects.Add(go);
            cells.Add(new CellView(go, kind, playerIndex, worldR, worldC));
        }
    }

    const float leftX = -420f;
    const float rightX = -80f;
    const float topY = -130f;
    const float bottomY = 80f;

    AddPanel(leftX, topY, PanelKind.OwnFleet, 0);
    AddPanel(rightX, topY, PanelKind.Radar, 0);
    AddPanel(leftX, bottomY, PanelKind.OwnFleet, 1);
    AddPanel(rightX, bottomY, PanelKind.Radar, 1);

    return (scene, cells.ToArray());
}

static int PlayerRowStart(int playerIndex) =>
    playerIndex == 0 ? BattleshipLayout.Player1RowMin : BattleshipLayout.Player2RowMin;

static void UpdateCellTints(BattleshipGame game, CellView[] views)
{
    foreach (var v in views)
    {
        var pos = new BoardPosition(v.WorldRow, v.WorldCol);
        v.Object.Sprite.Tint = v.Kind switch
        {
            PanelKind.OwnFleet => TintOwnFleet(game, v.PlayerIndex, pos),
            PanelKind.Radar => TintRadar(game, v.PlayerIndex, pos),
            _ => Rgba32.Black
        };
    }
}

static Rgba32 TintOwnFleet(BattleshipGame game, int ownerIndex, BoardPosition pos)
{
    var board = game.Board;
    var occupant = board[pos];
    var isShip = occupant != ShipType.None;

    var attackerIndex = 1 - ownerIndex;
    if (game.TryGetShotResult(attackerIndex, pos, out var incoming))
        return incoming ? Rgba32.Red : new Rgba32(160, 180, 200);

    return isShip ? new Rgba32(90, 95, 110) : new Rgba32(25, 60, 110);
}

static Rgba32 TintRadar(BattleshipGame game, int shooterIndex, BoardPosition pos)
{
    if (!game.TryGetShotResult(shooterIndex, pos, out var hit))
        return new Rgba32(35, 40, 55);

    return hit ? Rgba32.Red : new Rgba32(130, 140, 160);
}

enum PanelKind
{
    OwnFleet,
    Radar
}

readonly record struct CellView(GameObject2D Object, PanelKind Kind, int PlayerIndex, int WorldRow, int WorldCol);

public sealed class BattleshipGame
{
    private readonly HashSet<(int Row, int Column)> _p1Shots = new();
    private readonly HashSet<(int Row, int Column)> _p2Shots = new();
    private readonly Dictionary<(int Row, int Column), bool> _p1Results = new();
    private readonly Dictionary<(int Row, int Column), bool> _p2Results = new();

    public Player Player1 { get; }
    public Player Player2 { get; }
    public Board<ShipType> Board { get; }
    public int CurrentPlayerIndex { get; private set; }
    public bool IsOver { get; private set; }
    public int? WinnerIndex { get; private set; }

    public BattleshipGame()
    {
        Player1 = new Player(PlayerType.Computer);
        Player2 = new Player(PlayerType.Computer);
        Board = new Board<ShipType>(BattleshipLayout.RowsPerPlayer * 2, BattleshipLayout.Columns);
    }

    public void Setup()
    {
        PlaceAllShips(Board, Player1, BattleshipLayout.Player1RowMin, BattleshipLayout.Player1RowMax);
        PlaceAllShips(Board, Player2, BattleshipLayout.Player2RowMin, BattleshipLayout.Player2RowMax);
    }

    public bool TryGetShotResult(int shooterIndex, BoardPosition target, out bool hit)
    {
        var key = (target.Row, target.Column);
        if (shooterIndex == 0)
            return _p1Results.TryGetValue(key, out hit);
        return _p2Results.TryGetValue(key, out hit);
    }

    public void StepAiTurn()
    {
        if (IsOver)
            return;

        var defender = CurrentPlayerIndex == 0 ? Player2 : Player1;
        var (rowMin, rowMax) = CurrentPlayerIndex == 0
            ? (BattleshipLayout.Player2RowMin, BattleshipLayout.Player2RowMax)
            : (BattleshipLayout.Player1RowMin, BattleshipLayout.Player1RowMax);
        var shots = CurrentPlayerIndex == 0 ? _p1Shots : _p2Shots;
        var results = CurrentPlayerIndex == 0 ? _p1Results : _p2Results;

        BoardPosition shot;
        var guard = 0;
        do
        {
            var r = Random.Shared.Next(rowMin, rowMax);
            var c = Random.Shared.Next(0, BattleshipLayout.Columns);
            shot = new BoardPosition(r, c);
            if (++guard > 10_000)
                return;
        } while (!shots.Add((shot.Row, shot.Column)));

        var occupant = Board[shot];
        var isHit = occupant != ShipType.None;
        results[(shot.Row, shot.Column)] = isHit;

        if (isHit)
        {
            foreach (var ship in defender.Ships)
            {
                if (!ship.Positions.Any(p => p.Row == shot.Row && p.Column == shot.Column))
                    continue;
                ship.HitCells.Add((shot.Row, shot.Column));
                break;
            }
        }

        if (defender.Ships.All(s => s.IsSunk))
        {
            IsOver = true;
            WinnerIndex = CurrentPlayerIndex;
            return;
        }

        CurrentPlayerIndex = 1 - CurrentPlayerIndex;
    }

    private static void PlaceAllShips(Board<ShipType> board, Player player, int rowMin, int rowMaxExclusive)
    {
        var fleet = new[]
        {
            ShipType.Carrier,
            ShipType.Battleship,
            ShipType.Destroyer,
            ShipType.Submarine,
            ShipType.PatrolBoat
        };

        foreach (var shipType in fleet)
        {
            var placed = false;
            for (var attempt = 0; attempt < 400 && !placed; attempt++)
            {
                var horizontal = Random.Shared.Next(2) == 0;
                var len = shipType.GetLength();
                int startRow;
                int startCol;
                if (horizontal)
                {
                    startRow = Random.Shared.Next(rowMin, rowMaxExclusive);
                    startCol = Random.Shared.Next(0, BattleshipLayout.Columns - len + 1);
                }
                else
                {
                    startRow = Random.Shared.Next(rowMin, rowMaxExclusive - len + 1);
                    startCol = Random.Shared.Next(0, BattleshipLayout.Columns);
                }

                var direction = horizontal ? ShipDirection.Horizontal : ShipDirection.Vertical;
                if (!CanPlace(board, startRow, startCol, len, direction))
                    continue;

                var ship = new Ship { PlayerType = player.PlayerType, ShipType = shipType };
                board.AddShip(ship, new BoardPosition(startRow, startCol), direction);
                player.Ships.Add(ship);
                placed = true;
            }

            if (!placed)
                throw new InvalidOperationException($"Could not place {shipType} for player after many attempts.");
        }
    }

    private static bool CanPlace(Board<ShipType> board, int startRow, int startCol, int length, ShipDirection direction)
    {
        for (var i = 0; i < length; i++)
        {
            var r = direction == ShipDirection.Horizontal ? startRow : startRow + i;
            var c = direction == ShipDirection.Horizontal ? startCol + i : startCol;
            if (board[new BoardPosition(r, c)] != ShipType.None)
                return false;
        }

        return true;
    }
}

public enum ShipType
{
    None,
    Carrier,
    Battleship,
    Destroyer,
    Submarine,
    PatrolBoat
}

public static class BattleshipBoardExtensions
{
    public static void AddShip(this Board<ShipType> board, Ship ship, BoardPosition startingPosition, ShipDirection direction)
    {
        var positions = new List<BoardPosition>();
        for (var i = 0; i < ship.Length; i++)
        {
            var position = direction switch
            {
                ShipDirection.Horizontal => new BoardPosition(startingPosition.Row, startingPosition.Column + i),
                ShipDirection.Vertical => new BoardPosition(startingPosition.Row + i, startingPosition.Column),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            positions.Add(position);
        }

        ship.Positions = positions;
        foreach (var position in positions)
            board.Set(position, ship.ShipType);
    }
}

public enum ShipDirection
{
    Horizontal,
    Vertical
}

public static class ShipTypeExtensions
{
    public static int GetLength(this ShipType shipType)
    {
        return shipType switch
        {
            ShipType.Carrier => 5,
            ShipType.Battleship => 4,
            ShipType.Destroyer => 3,
            ShipType.Submarine => 3,
            ShipType.PatrolBoat => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(shipType), shipType, null)
        };
    }
}

public sealed class Ship
{
    public PlayerType PlayerType { get; init; }

    public ShipType ShipType { get; init; }

    public List<BoardPosition> Positions { get; set; } = new();

    public HashSet<(int Row, int Column)> HitCells { get; } = new();

    public int Length => ShipType.GetLength();

    public bool IsSunk => HitCells.Count >= Length;
}

public enum PlayerType
{
    Human,
    Computer
}

public sealed class Player
{
    public PlayerType PlayerType { get; }

    public List<Ship> Ships { get; } = new();

    public Player(PlayerType playerType)
    {
        PlayerType = playerType;
    }
}

internal static class BattleshipLayout
{
    public const int Columns = 10;
    public const int RowsPerPlayer = 10;
    public const int Player1RowMin = 0;
    public const int Player1RowMax = RowsPerPlayer;
    public const int Player2RowMin = RowsPerPlayer;
    public const int Player2RowMax = RowsPerPlayer * 2;
    public const float ActionIntervalSeconds = 1f;
    public const float CellSize = 22f;
    public const float CellGap = 2f;
}
