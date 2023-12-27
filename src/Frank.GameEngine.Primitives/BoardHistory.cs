namespace Frank.GameEngine.Primitives;

public class BoardHistory<T>
{
    private readonly Dictionary<DateTime, Board<T>> history;

    public BoardHistory()
    {
        history = new Dictionary<DateTime, Board<T>>();
    }

    public void SaveState(Board<T> boardState)
    {
        history[DateTime.Now] = (Board<T>)boardState.Clone();
    }

    public IEnumerable<KeyValuePair<DateTime, Board<T>>> GetHistory()
    {
        return history.OrderByDescending(kvp => kvp.Key);
    }

    public Board<T> GetLatest()
    {
        if (history.Count == 0)
        {
            throw new InvalidOperationException("No history available.");
        }

        return history[history.Keys.Max()];
    }

    public Board<T> GetStateAt(DateTime timestamp)
    {
        if (history.TryGetValue(timestamp, out Board<T> boardState))
        {
            return boardState;
        }
        else
        {
            throw new KeyNotFoundException("No board state found for the given timestamp.");
        }
    }
}