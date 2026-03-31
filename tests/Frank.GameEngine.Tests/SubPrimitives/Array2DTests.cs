using System;
using System.Linq;
using Frank.GameEngine.Primitives.SubPrimitives;
using FluentAssertions;
using JetBrains.Annotations;
using TUnit.Core;

namespace Frank.GameEngine.Tests.SubPrimitives;

[TestSubject(typeof(Array2D<>))]
public class Array2DTests
{
    [Test]
    public void Constructor_WithWidthAndHeight_ShouldSetPropertiesCorrectly()
    {
        var array = new Array2D<int>(5, 5);
        array.Width.Should().Be(5);
        array.Height.Should().Be(5);
    }

    [Test]
    public void GetRow_WithValidRow_ShouldReturnRow()
    {
        var array = new Array2D<TestRecord?>(5, 5);
        array.Set(new ArrayPosition2D(1, 1), new TestRecord("Frank", 10));
        array.Set(new ArrayPosition2D(2, 2), new TestRecord("Bob", 20));
        array.Set(new ArrayPosition2D(3, 3), new TestRecord("John", 30));
        array.Set(new ArrayPosition2D(4, 4), new TestRecord("Jane", 40));
        TestContext.Current!.Output.WriteLine(array.ToString());
        TestContext.Current.Output.WriteLine(array.GetMap());
        array.GetRow(2).Should().HaveCount(5);
    }

    private record TestRecord(string Name, int Age);

    [Test]
    public void GetRow_WithInvalidRow_ShouldThrowException()
    {
        var array = new Array2D<int>(5, 5);
        var act = () => array.GetRow(6);
        act.Should().Throw<IndexOutOfRangeException>();
    }

    [Test]
    public void GetColumn_WithValidColumn_ShouldReturnColumn()
    {
        var array = new Array2D<int>(5, 5);
        array.GetColumn(2).Should().HaveCount(5);
    }

    [Test]
    public void GetColumn_WithInvalidColumn_ShouldThrowException()
    {
        var array = new Array2D<int>(5, 5);
        var act = () => array.GetColumn(6);
        act.Should().Throw<IndexOutOfRangeException>();
    }

    [Test]
    public void Set_WithValidValues_ShouldSetValuesCorrectly()
    {
        var array = new Array2D<int>(5, 5);
        array.Set(new ArrayPosition2D(2, 2), 10);
        array[new ArrayPosition2D(2, 2)].Should().Be(10);
    }

    [Test]
    public void FindInRow_WithValidPredicate_ShouldFindValuesInRow()
    {
        var array = new Array2D<int>(5, 5);
        array.Set(new ArrayPosition2D(1, 1), 10);
        array.Set(new ArrayPosition2D(2, 2), 20);
        array.FindInRow(1, x => x == 10).Should().HaveCount(1);
    }

    [Test]
    public void FindInColumn_WithValidPredicate_ShouldFindValuesInColumn()
    {
        var array = new Array2D<int>(5, 5);
        array.Set(new ArrayPosition2D(1, 1), 10);
        array.Set(new ArrayPosition2D(2, 2), 20);
        array.FindInColumn(2, x => x == 20).Should().HaveCount(1);
    }

    [Test]
    public void Slice_WithValidValues_ShouldReturnNewArray2D()
    {
        var array = new Array2D<int>(5, 5);
        array.Set(new ArrayPosition2D(1, 1), 10);
        var slice = array.Slice(1, 1, 3, 3);
        slice.Should().NotBeNull();
        slice.Width.Should().Be(3);
    }
}
