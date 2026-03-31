using FluentAssertions;
using Frank.GameEngine.Primitives;
using JetBrains.Annotations;

namespace Frank.GameEngine.Tests.Primitives;

[TestSubject(typeof(BoardPosition))]
public class BoardPositionTests
{
    [Test]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumn()
    {
        var boardPosition = new BoardPosition(1, 1);
        boardPosition.Should().NotBeNull();
    }
    
    [Test]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings()
    {
        var boardPosition = new BoardPosition("a", 0);
        boardPosition.Column.Should().Be(0);
        boardPosition.Row.Should().Be(0);
    }
    
    [Test]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings2()
    {
        var boardPosition = new BoardPosition("a", "b");
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(0);
    }
    
    [Test]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings3()
    {
        var boardPosition = new BoardPosition(0, "b");
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(0);
    }
    
    [Test]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings4()
    {
        var boardPosition = new BoardPosition("a", 1);
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(0);
    }
    
    [Test]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings5()
    {
        var boardPosition = new BoardPosition("b", 1);
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(1);
    }

    [Test]
    [Arguments(-1, "a", typeof(IndexOutOfRangeException))]
    [Arguments("a", -1, typeof(IndexOutOfRangeException))]
    [Arguments("0", "a", typeof(ArgumentException))]
    [Arguments("a", "0", typeof(ArgumentException))]
    public void BoardPosition_ShouldThrowExceptionForInvalidIndices(object row, object col, Type expectedException)
    {
        Action action = () =>
        {
            var val = new BoardPosition((dynamic)row, (dynamic)col);
        };

        action.Should().Throw<Exception>().Which.GetType().Should().Be(expectedException);
    }
}