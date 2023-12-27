using FluentAssertions;
using Frank.GameEngine.Primitives;
using JetBrains.Annotations;

namespace Frank.GameEngine.Tests.Primitives;

[TestSubject(typeof(BoardPosition))]
public class BoardPositionTests
{
    [Fact]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumn()
    {
        var boardPosition = new BoardPosition(1, 1);
        boardPosition.Should().NotBeNull();
    }
    
    [Fact]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings()
    {
        var boardPosition = new BoardPosition("a", 0);
        boardPosition.Column.Should().Be(0);
        boardPosition.Row.Should().Be(0);
    }
    
    [Fact]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings2()
    {
        var boardPosition = new BoardPosition("a", "b");
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(0);
    }
    
    [Fact]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings3()
    {
        var boardPosition = new BoardPosition(0, "b");
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(0);
    }
    
    [Fact]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings4()
    {
        var boardPosition = new BoardPosition("a", 1);
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(0);
    }
    
    [Fact]
    public void BoardPosition_ShouldInitializeWithProvidedRowAndColumnAsStrings5()
    {
        var boardPosition = new BoardPosition("b", 1);
        boardPosition.Column.Should().Be(1);
        boardPosition.Row.Should().Be(1);
    }

    [Theory]
    [InlineData(-1, "a", typeof(IndexOutOfRangeException))]
    [InlineData("a", -1, typeof(IndexOutOfRangeException))]
    [InlineData("0", "a", typeof(ArgumentException))]
    [InlineData("a", "0", typeof(ArgumentException))]
    public void BoardPosition_ShouldThrowExceptionForInvalidIndices(object row, object col, Type expectedException)
    {
        Action action = () =>
        {
            var val = new BoardPosition((dynamic)row, (dynamic)col);
        };

        Assert.Throws(expectedException, action);
    }
}