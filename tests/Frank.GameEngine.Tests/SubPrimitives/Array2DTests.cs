using System;
using System.Linq;
using Frank.GameEngine.Primitives.SubPrimitives;
using JetBrains.Annotations;
using Xunit;
using FluentAssertions;
using Frank.Libraries.Extensions;
using Xunit.Abstractions;

namespace Frank.GameEngine.Tests.SubPrimitives
{
    [TestSubject(typeof(Array2D<>))]
    public class Array2DTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public Array2DTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Constructor_WithWidthAndHeight_ShouldSetPropertiesCorrectly()
        {
            var array = new Array2D<int>(5, 5);
            array.Width.Should().Be(5);
            array.Height.Should().Be(5);
        }

        [Fact]
        public void GetRow_WithValidRow_ShouldReturnRow()
        {
            var array = new Array2D<TestRecord?>(5, 5);
            array.Set(new ArrayPosition2D(1, 1), new TestRecord("Frank", 10));
            array.Set(new ArrayPosition2D(2, 2), new TestRecord("Bob", 20));
            array.Set(new ArrayPosition2D(3, 3), new TestRecord("John", 30));
            array.Set(new ArrayPosition2D(4, 4), new TestRecord("Jane", 40));
            _outputHelper.WriteLine(array.ToString());
            _outputHelper.WriteLine(array.GetMap());
            array.GetRow(2).Should().HaveCount(5);
        }
        
        private record TestRecord(string Name, int Age);

        [Fact]
        public void GetRow_WithInvalidRow_ShouldThrowException()
        {
            var array = new Array2D<int>(5, 5);
            Assert.Throws<IndexOutOfRangeException>(() => array.GetRow(6));
        }

        [Fact]
        public void GetColumn_WithValidColumn_ShouldReturnColumn()
        {
            var array = new Array2D<int>(5, 5);
            array.GetColumn(2).Should().HaveCount(5);
        }

        [Fact]
        public void GetColumn_WithInvalidColumn_ShouldThrowException()
        {
            var array = new Array2D<int>(5, 5);
            Assert.Throws<IndexOutOfRangeException>(() => array.GetColumn(6));
        }

        [Fact]
        public void Set_WithValidValues_ShouldSetValuesCorrectly()
        {
            var array = new Array2D<int>(5, 5);
            array.Set(new ArrayPosition2D(2, 2), 10);
            array[new ArrayPosition2D(2, 2)].Should().Be(10);
        }

        [Fact]
        public void FindInRow_WithValidPredicate_ShouldFindValuesInRow()
        {
            var array = new Array2D<int>(5, 5);
            array.Set(new ArrayPosition2D(1, 1), 10);
            array.Set(new ArrayPosition2D(2, 2), 20);
            array.FindInRow(1, x => x == 10).Should().HaveCount(1);
        }

        [Fact]
        public void FindInColumn_WithValidPredicate_ShouldFindValuesInColumn()
        {
            var array = new Array2D<int>(5, 5);
            array.Set(new ArrayPosition2D(1, 1), 10);
            array.Set(new ArrayPosition2D(2, 2), 20);
            array.FindInColumn(2, x => x == 20).Should().HaveCount(1);
        }

        [Fact]
        public void Slice_WithValidValues_ShouldReturnNewArray2D()
        {
            var array = new Array2D<int>(5, 5);
            array.Set(new ArrayPosition2D(1, 1), 10);
            var slice = array.Slice(1, 1, 3, 3);
            slice.Should().NotBeNull();
            slice.Width.Should().Be(3);
        }
    }
}