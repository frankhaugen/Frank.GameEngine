namespace Frank.GameEngine.Lagacy._2d._2D.Experimental;

public class CollissionsEventArgs : EventArgs
{
	public List<Collission> Collissions { get; set; }

	public CollissionsEventArgs(List<Collission> collissions)
	{
		Collissions = collissions;
	}
}