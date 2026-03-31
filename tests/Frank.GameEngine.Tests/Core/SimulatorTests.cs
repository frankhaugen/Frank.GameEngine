using FluentAssertions;
using Frank.GameEngine.Core;

namespace Frank.GameEngine.Tests.Core;

public class SimulatorTests
{
    [Test]
    public void Run_InvokesActionPerIteration_WithSimulationSpeedZero_SkipsDelay()
    {
        var ticks = new List<TimeSpan>();
        var sim = new Simulator(t => ticks.Add(t))
        {
            TimeIncrement = TimeSpan.FromMilliseconds(10),
            SimulationSpeed = 0,
            MaxRunningTime = TimeSpan.MaxValue
        };

        sim.Run(4);

        ticks.Should().HaveCount(4);
        ticks[0].Should().Be(TimeSpan.FromMilliseconds(10));
        ticks[1].Should().Be(TimeSpan.FromMilliseconds(20));
        ticks[2].Should().Be(TimeSpan.FromMilliseconds(30));
        ticks[3].Should().Be(TimeSpan.FromMilliseconds(40));
    }

    [Test]
    public void Run_StopsEarly_WhenStopIsCalled()
    {
        var count = 0;
        Simulator sim = null!;
        sim = new Simulator(_ =>
        {
            count++;
            if (count == 2)
                sim.Stop();
        })
        {
            TimeIncrement = TimeSpan.FromTicks(1),
            SimulationSpeed = 0
        };

        sim.Run(100);

        count.Should().Be(2);
    }

    [Test]
    public void Tick_IncrementsTotalRunningTime_AndInvokesAction()
    {
        TimeSpan? last = null;
        var sim = new Simulator(t => last = t)
        {
            TimeIncrement = TimeSpan.FromSeconds(2),
            SimulationSpeed = 0
        };

        sim.Tick();
        sim.TotalRunningTime.Should().Be(TimeSpan.FromSeconds(2));
        last.Should().Be(TimeSpan.FromSeconds(2));

        sim.Tick();
        sim.TotalRunningTime.Should().Be(TimeSpan.FromSeconds(4));
        last.Should().Be(TimeSpan.FromSeconds(4));
    }

    [Test]
    public void Start_RespectsMaxRunningTime_WithSimulationSpeedZero()
    {
        var iterations = 0;
        var sim = new Simulator(_ => iterations++)
        {
            TimeIncrement = TimeSpan.FromSeconds(1),
            SimulationSpeed = 0,
            MaxRunningTime = TimeSpan.FromSeconds(3)
        };

        sim.Start();

        iterations.Should().Be(3);
        sim.TotalRunningTime.Should().Be(TimeSpan.FromSeconds(3));
    }
}
