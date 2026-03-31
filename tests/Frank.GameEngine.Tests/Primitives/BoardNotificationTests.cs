using FluentAssertions;
using Frank.GameEngine.Primitives;
using Moq;

namespace Frank.GameEngine.Tests.Primitives;

public class BoardNotificationTests
{
    [Test]
    public void Set_NotifiesObserver_WithValue()
    {
        var board = new Board<int>(2, 2);
        var observer = new Mock<IObserver<int>>();
        board.Subscribe(observer.Object);

        board.Set(new BoardPosition(0, 0), 42);

        observer.Verify(o => o.OnNext(42), Times.Once);
    }

    [Test]
    public void UnSet_ForReferenceType_CallsOnCompleted_WhenCellClearedToNull()
    {
        var board = new Board<string>(2, 2);
        var observer = new Mock<IObserver<string>>();
        board.Subscribe(observer.Object);
        board.Set(new BoardPosition(0, 0), "x");

        board.UnSet(new BoardPosition(0, 0));

        observer.Verify(o => o.OnCompleted(), Times.Once);
    }

    [Test]
    public void GetRow_ReturnsValuesAlongRow()
    {
        var board = new Board<int>(2, 3);
        board.Set(new BoardPosition(0, 0), 1);
        board.Set(new BoardPosition(0, 1), 2);
        board.Set(new BoardPosition(0, 2), 3);

        board.GetRow(0).Should().Equal(1, 2, 3);
    }

    [Test]
    public void GetColumn_ReturnsValuesAlongColumn()
    {
        var board = new Board<int>(3, 2);
        board.Set(new BoardPosition(0, 1), 10);
        board.Set(new BoardPosition(1, 1), 20);
        board.Set(new BoardPosition(2, 1), 30);

        board.GetColumn(1).Should().Equal(10, 20, 30);
    }

    [Test]
    public void Clone_CopiesCellContents()
    {
        var board = new Board<string>(2, 2);
        board.Set(new BoardPosition(0, 0), "a");
        board.Set(new BoardPosition(1, 1), "b");

        var clone = (Board<string>)board.Clone();

        clone[new BoardPosition(0, 0)].Should().Be("a");
        clone[new BoardPosition(1, 1)].Should().Be("b");
    }
}
