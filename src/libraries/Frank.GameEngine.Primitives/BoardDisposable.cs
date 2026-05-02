namespace Frank.GameEngine.Primitives;

internal class BoardDisposable<T> : IDisposable
{
    private List<IObserver<T>> _observers;
    private IObserver<T> _observer;

    public BoardDisposable(List<IObserver<T>> observers, IObserver<T> observer)
    {
        this._observers = observers;
        this._observer = observer;
    }

    public void Dispose()
    {
        if (_observer != null && _observers.Contains(_observer))
        {
            _observers.Remove(_observer);
        }
    }
}