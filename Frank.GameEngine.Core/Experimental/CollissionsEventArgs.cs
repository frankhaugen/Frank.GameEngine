namespace Frank.GameEngine.Core.Experimental;

public class CollissionsEventArgs : EventArgs
{
	public List<Collission> Collissions { get; set; }

	public CollissionsEventArgs(List<Collission> collissions)
	{
		Collissions = collissions;
	}
}