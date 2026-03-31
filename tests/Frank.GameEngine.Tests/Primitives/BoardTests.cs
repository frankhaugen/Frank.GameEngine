using FluentAssertions;
using Frank.GameEngine.Primitives;
using JetBrains.Annotations;
using Moq;

namespace Frank.GameEngine.Tests.Primitives
{
    [TestSubject(typeof(Board<string>))]
    public class BoardTests
    {
        [Test]
        public void Board_ShouldInitializeWithProvidedDimensions()
        {
            var board = new Board<string>(5, 5);
            board.Should().NotBeNull();
        }

        [Test]
        [Arguments(-1, "a", typeof(IndexOutOfRangeException))]
        [Arguments("a", -1, typeof(IndexOutOfRangeException))]
        [Arguments("0", "a", typeof(ArgumentException))]
        [Arguments("a", "0", typeof(ArgumentException))]
        public void Board_ShouldThrowExceptionForInvalidIndices(object row, object col, Type expectedException)
        {
            var board = new Board<string>(5, 5);
            Action action = () =>
            {
                var val = board[new BoardPosition((dynamic)row, (dynamic)col)];
            };

            action.Should().Throw<Exception>().Which.GetType().Should().Be(expectedException);
        }

        [Test]
        public void Board_Dispose_ShouldUnsubscribeObserver()
        {
            var observerMock = new Mock<IObserver<string>>();
            var board = new Board<string>(2, 2);
            var disposable = board.Subscribe(observerMock.Object);
            disposable.Dispose();
            board[new BoardPosition(1, 1)] = "value";
            observerMock.Verify(x => x.OnNext(It.IsAny<string>()), Times.Never);
        }
    }
}