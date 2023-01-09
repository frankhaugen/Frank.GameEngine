namespace Frank.GameEngine.Core._2D.Experimental;

public class CollissionsEventArgs : EventArgs
{
	public List<Collission> Collissions { get; set; }

	public CollissionsEventArgs(List<Collission> collissions)
	{
		Collissions = collissions;
	}
}