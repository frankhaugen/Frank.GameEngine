namespace Frank.GameEngine.Core;

/// <summary>
/// Simulates a game loop and allows for the simulation speed to be adjusted and the simulation to be stopped
/// </summary>
public class Simulator
{
    private bool _stopSimulation;

    /// <summary>
    /// Creates a new instance of the Simulator class
    /// </summary>
    /// <param name="action">The action to run each iteration</param>
    public Simulator(Action<TimeSpan> action)
    {
        Action = action;
    }

    /// <summary>
    /// The total running time of the simulation
    /// </summary>
    public TimeSpan TotalRunningTime { get; private set; } = TimeSpan.Zero;

    /// <summary>
    /// The maximum running time of the simulation
    /// </summary>
    public TimeSpan MaxRunningTime { get; set; } = TimeSpan.FromMinutes(1);

    /// <summary>
    /// The speed of the simulation (1 = realtime), 0 and below is infinite speed
    /// </summary>
    public float SimulationSpeed { get; set; } = 5;

    /// <summary>
    /// The time between each iteration
    /// </summary>
    public TimeSpan TimeIncrement { get; set; } = TimeSpan.FromSeconds(1);
    
    /// <summary>
    /// The action to run each iteration
    /// </summary>
    public Action<TimeSpan> Action { get; }

    /// <summary>
    /// Stops the simulation
    /// </summary>
    public void Stop() => _stopSimulation = true;

    /// <summary>
    /// Runs the simulation for the specified number of iterations
    /// </summary>
    /// <param name="iterations"></param>
    public void Run(int iterations)
    {
        for (var i = 0; i < iterations; i++)
        {
            if (_stopSimulation)
            {
                _stopSimulation = false;
                break;
            }
            Tick();
        }
    }

    /// <summary>
    /// Runs the simulation for the specified time
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <param name="action"></param>
    public void Start()
    {
        while (TotalRunningTime < MaxRunningTime)
        {
            if (_stopSimulation)
            {
                _stopSimulation = false;
                break;
            }
            Tick();
        }
    }

    /// <summary>
    /// Runs one iteration of the simulation and increments the time by the TimeIncrement
    /// </summary>
    /// <param name="action"></param>
    public void Tick()
    {
        TotalRunningTime += TimeIncrement;
        if (SimulationSpeed > 0)
        {
            Task.Delay(TimeIncrement / SimulationSpeed).Wait();
        }
        Action.Invoke(TotalRunningTime);
    }
}