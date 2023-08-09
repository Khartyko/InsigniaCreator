using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Testing.Utility;

#pragma warning disable CS8600, CS8601, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Entity;

public class NodeTests
{
    #region Activated

    [Fact]
    public void Node_Activated_Get_Succeeds()
    {
        Node subject = DataGenerator.GenerateRandomNode();
        
        Assert.False(subject.Activated);
    }

    [Fact]
    public void Node_Activated_Set_Succeeds()
    {
        Node subject = DataGenerator.GenerateRandomNode();

        subject.Activated = true;
        
        Assert.True(subject.Activated);

        subject.Activated = false;
        
        Assert.False(subject.Activated);
    }
    
    #endregion Activated

    #region Position

    [Fact]
    public void Node_Position_Get_Succeeds()
    {
        Vector2 initialPosition = DataGenerator.GenerateRandomVector2();
        var subject = new Node(initialPosition);
        
        Assert.Equal(initialPosition, subject.Position);
    }

    [Fact]
    public void Node_Position_Set_Succeeds()
    {
        Vector2 initialPosition = Vector2.Zero;
        Vector2 positionUpdate = Vector2.One;

        var subject = new Node(initialPosition);
        
        Assert.NotEqual(positionUpdate, subject.Position);

        subject.Position = positionUpdate;
        
        Assert.Equal(positionUpdate, subject.Position);
        Assert.NotEqual(initialPosition, positionUpdate);
    }

    [Fact]
    public void Node_Position_Set_Fails()
    {
        Node subject = DataGenerator.GenerateRandomNode();

        Vector2 nullVector = null;
        Vector2 priorPosition = subject.Position;
        
        Assert.Throws<ArgumentNullException>(() => subject.Position = nullVector);
        Assert.Equal(priorPosition, subject.Position);
        Assert.NotEqual(nullVector, subject.Position);
    }

    #endregion Position

    #region Constructors
    
    #region Construct From Doubles
    
    [Fact]
    public void ConstructFromDoubles_Succeeds()
    {
        var x = DataGenerator.GenerateRandomDouble();
        var y = DataGenerator.GenerateRandomDouble();

        var node = new Node(x, y);
        
        Assert.Equal(x, node.Position.X);
        Assert.Equal(y, node.Position.Y);
    }

    [Theory]
    [InlineData(double.NaN, 0.0)]
    [InlineData(double.PositiveInfinity, 0.0)]
    [InlineData(double.NegativeInfinity, 0.0)]
    [InlineData(0.0, double.NaN)]
    [InlineData(0.0, double.PositiveInfinity)]
    [InlineData(0.0, double.NegativeInfinity)]
    public void ConstructFromDoubles_BadValues_Fails(double x, double y)
    {
        Assert.Throws<ArgumentException>(() => new Node(x, y));
    }

    #endregion Construct From Doubles

    #region Construct From Vector2

    [Fact]
    public void ConstructFromVector2_Succeeds()
    {
        Vector2 position = DataGenerator.GenerateRandomVector2();

        var node = new Node(position);
        
        Assert.Equal(position, node.Position);
    }

    [Fact]
    public void ConstructFromVector2_Fails()
    {
        Vector2 nullVector = null;
        
        Assert.Throws<ArgumentNullException>(() => new Node(nullVector));
    }

    #endregion Construct From Vector2

    #region Construct From Existing

    [Fact]
    public void ConstructFromExistingNode_Succeeds()
    {
        Vector2 position = DataGenerator.GenerateRandomVector2();
        var existingNode = new Node(position);

        var duplicate = new Node(existingNode);
        
        Assert.Equal(position, duplicate.Position);
    }

    [Fact]
    public void ConstructFromExistingNode_Fails()
    {
        Node nullNode = null;

        Assert.Throws<ArgumentNullException>(() => new Node(nullNode));
    }

    #endregion Construct From Existing
    
    #endregion Constructors

    #region Equals
    
    [Fact]
    public void Equals_Succeeds()
    {
        Vector2 position = DataGenerator.GenerateRandomVector2();

        var node0 = new Node(position);
        var node1 = new Node(position);
        
        Assert.True(node0.Equals(node1));
        
        // ReSharper disable EqualExpressionComparison
        Assert.True(node0.Equals(node0));
        Assert.True(node1.Equals(node1));
        // ReSharper restore EqualExpressionComparison
    }

    [Fact]
    public void Equals_Fails()
    {
        var node0 = new Node(Vector2.Zero);
        var node1 = new Node(Vector2.One);
        
        Assert.False(node0.Equals(node1));
        Assert.False(node0.Equals(null));
        Assert.False(node1.Equals(null));
    }
    
    #endregion Equals

    #region ToString
    
    /*
     * ToString():
     * - Valid Only
     */

    [Fact]
    public void ToString_Succeeds()
    {
        var node = new Node(Vector2.One);
        const string expectedWhenNotActivated = "{ activated: False, position: { x: 1, y: 1 } }";
        const string expectedWhenActivated = "{ activated: True, position: { x: 1, y: 1 } }";
        
        Assert.Equal(expectedWhenNotActivated, node.ToString());

        node.Activated = true;
        
        Assert.Equal(expectedWhenActivated, node.ToString());
    }
    
    #endregion ToString
}