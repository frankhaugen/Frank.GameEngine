namespace Frank.GameEngine.Core;

public class CollissionsEventArgs : EventArgs
{
    public List<Collission> Collissions { get; set; }

    public CollissionsEventArgs(List<Collission> collissions)
    {
        Collissions = collissions;
    }
}