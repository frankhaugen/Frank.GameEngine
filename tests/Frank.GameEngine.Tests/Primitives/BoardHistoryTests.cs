using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class BoardHistoryTests
{
    [Fact]
    public void GetLatest_Throws_WhenEmpty()
    {
        var history = new BoardHistory<char>();

        history.Invoking(h => h.GetLatest()).Should().Throw<InvalidOperationException>()
            .WithMessage("*No history*");
    }

    [Fact]
    public void SaveState_ThenGetLatest_ReturnsClone()
    {
        var board = new Board<char>(2, 2);
        board[new BoardPosition(0, 0)] = 'X';
        var history = new BoardHistory<char>();

        history.SaveState(board);
        var latest = history.GetLatest();

        latest.Should().NotBeSameAs(board);
        latest[new BoardPosition(0, 0)].Should().Be('X');
    }

    [Fact]
    public void GetStateAt_ReturnsBoard_WhenTimestampExists()
    {
        var board = new Board<int>(1, 1);
        board[new BoardPosition(0, 0)] = 7;
        var history = new BoardHistory<int>();
        var before = DateTime.UtcNow;
        history.SaveState(board);
        var after = DateTime.UtcNow;

        var keys = history.GetHistory().Select(kvp => kvp.Key).ToList();
        var key = keys.Should().ContainSingle().Subject;

        var restored = history.GetStateAt(key);
        restored[new BoardPosition(0, 0)].Should().Be(7);
    }

    [Fact]
    public void GetStateAt_Throws_WhenMissing()
    {
        var history = new BoardHistory<int>();

        history.Invoking(h => h.GetStateAt(DateTime.UtcNow)).Should().Throw<KeyNotFoundException>();
    }
}
