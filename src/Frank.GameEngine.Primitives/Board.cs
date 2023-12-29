using System.Text;

namespace Frank.GameEngine.Primitives;

public class Board<T> : IObservable<T?>, ICloneable
{
    private readonly T?[,] _board;
    private readonly List<IObserver<T>> _observers;

    public Board(int rows, int columns)
    {
        _board = new T[rows, columns];
        _observers = new List<IObserver<T>>();
        
    }

    public static BoardHistory<T> History { get; } = new();
    
    public int ColumnCount => _board.GetLength(1);
    
    public int RowCount => _board.GetLength(0);

    public T this[BoardPosition boardPosition]
    {
        get => _board[boardPosition.Row, boardPosition.Column];
        set => _board[boardPosition.Row, boardPosition.Column] = value;
    }

    public void Set(BoardPosition position, T value)
    {
        _board[position.Row, position.Column] = value;
        OnBoardUpdated(value);
    }

    public void UnSet(BoardPosition position)
    {
        _board[position.Row, position.Column] = default;
        OnBoardUpdated(default);
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer)) _observers.Add(observer);
        return new BoardDisposable<T>(_observers, observer);
    }

    public object Clone()
    {
        var newBoard = new Board<T>(_board.GetLength(0), _board.GetLength(1));
        for (var i = 0; i < _board.GetLength(0); i++)
        for (var j = 0; j < _board.GetLength(1); j++)
            newBoard[new BoardPosition(i, j)] = _board[i, j];
        return newBoard;
    }

    public T[] GetRow(int row)
    {
        var rowArray = new T[_board.GetLength(1)];
        for (var i = 0; i < _board.GetLength(1); i++) rowArray[i] = _board[row, i]; // Here, use the row as is, because we are assuming index starts from 0
        return rowArray;
    }

    public T[] GetColumn(int column)
    {
        var columnArray = new T[_board.GetLength(0)];
        for (var i = 0; i < _board.GetLength(0); i++) columnArray[i] = _board[i, column]; // Here, use the column as is, because we are assuming index starts from 0
        return columnArray;
    }

    private void NotifyObservers(T? value)
    {
        if (value == null)
            foreach (var observer in _observers)
                observer.OnCompleted();
        else
            foreach (var observer in _observers)
                observer.OnNext(value);
    }

    private void OnBoardUpdated(T? value)
    {
        NotifyObservers(value);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _board.GetLength(0); i++)
        {
            for (var j = 0; j < _board.GetLength(1); j++) sb.Append(_board[i, j]);
            sb.AppendLine();
        }

        return sb.ToString();
    }
}