// See https://aka.ms/new-console-template for more information

using Frank.GameEngine.Primitives;

Console.WriteLine("Hello, World!");

var game = new BattleshipGame();

game.Board.SetupBoard(game.Player1);

game.Board.SetupBoard(game.Player2);

Console.WriteLine(game.Board.ToString());




public class BattleshipGame
{
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    
    public BattleshipGame()
    {
        Player1 = new Player(PlayerType.Human);
        Player2 = new Player(PlayerType.Computer);
        Board = new Board<ShipType>(20, 10);
    }

    public Board<ShipType> Board { get; set; }
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

public static class BoardExtensions
{
    public static void SetupBoard(this Board<ShipType> board, Player player)
    {
        var offset = player.PlayerType == PlayerType.Human ? 0 : 10;
        
        var carrier = new Ship {PlayerType = player.PlayerType, ShipType = ShipType.Carrier};
        var battleship = new Ship {PlayerType = player.PlayerType, ShipType = ShipType.Battleship};
        var destroyer = new Ship {PlayerType = player.PlayerType, ShipType = ShipType.Destroyer};
        var submarine = new Ship {PlayerType = player.PlayerType, ShipType = ShipType.Submarine};
        var patrolBoat = new Ship {PlayerType = player.PlayerType, ShipType = ShipType.PatrolBoat};
        board.AddShip(carrier, new BoardPosition(GetRandomNumber(offset, board.RowCount - offset), 0), ShipDirection.Horizontal);
        board.AddShip(battleship, new BoardPosition(GetRandomNumber(offset, board.RowCount - offset), 0), ShipDirection.Horizontal);
        board.AddShip(destroyer, new BoardPosition(GetRandomNumber(offset, board.RowCount - offset), 0), ShipDirection.Horizontal);
        board.AddShip(submarine, new BoardPosition(GetRandomNumber(offset, board.RowCount - offset), 0), ShipDirection.Horizontal);
        board.AddShip(patrolBoat, new BoardPosition(GetRandomNumber(offset, board.RowCount - offset), 0), ShipDirection.Horizontal);
        player.Ships.Add(carrier);
        player.Ships.Add(battleship);
        player.Ships.Add(destroyer);
        player.Ships.Add(submarine);
        player.Ships.Add(patrolBoat);
    }
    
    private static int GetRandomNumber(int min, int max)
    {
        return Random.Shared.Next(min, max);
    }
    
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
        {
            board.Set(position, ship.ShipType);
        }
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

public class Ship
{
    public PlayerType PlayerType { get; set; }
    
    public ShipType ShipType { get; set; }
    
    public List<BoardPosition> Positions { get; set; }
    
    public int Length => ShipType.GetLength();
    
    public int Hits { get; set; }
    public bool IsSunk => Hits >= Length;
}

public enum PlayerType
{
    Human,
    Computer
}

public class Player
{
    public PlayerType PlayerType { get; set; }
    
    public List<Ship> Ships { get; set; }
    
    public Player(PlayerType playerType)
    {
        PlayerType = playerType;
        Ships = new List<Ship>();
    }
}