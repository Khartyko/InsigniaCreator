using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

#pragma warning disable CS0219, CS8600, CS8601, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Entity;

public class LinkTests
{
    #region Head

    [Fact]
    public void Head_Get_Succeeds()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);

        var link = new Link(head, tail);

        Assert.Equal(head, link.Head);
    }

    [Fact]
    public void Head_Set_Succeeds()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);
        Node updatedHead = new(Vector2.One * 2);

        var link = new Link(head, tail);

        Assert.Equal(head, link.Head);

        link.Head = updatedHead;

        Assert.NotEqual(head, link.Head);
        Assert.Equal(updatedHead, link.Head);
    }

    [Fact]
    public void Head_Set_NullValue_Fails()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);
        Node nullNode = null;

        var link = new Link(head, tail);

        Assert.Throws<ArgumentNullException>(() => link.Head = nullNode);
        Assert.NotNull(link.Head);
        Assert.Equal(head, link.Head);
    }

    [Fact]
    public void Head_Set_MatchesTail_Fails()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);
        Node duplicate = new(tail);

        var link = new Link(head, tail);

        Assert.Throws<ArgumentException>(() => link.Head = duplicate);
        Assert.NotEqual(duplicate, link.Head);
        Assert.Equal(head, link.Head);
    }

    #endregion Head

    #region Tail

    [Fact]
    public void Tail_Get_Succeeds()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);

        var link = new Link(head, tail);

        Assert.Equal(tail, link.Tail);
    }

    [Fact]
    public void Tail_Set_Succeeds()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);
        Node updatedTail = new(Vector2.One * 2);

        var link = new Link(head, tail);

        Assert.Equal(tail, link.Tail);

        link.Tail = updatedTail;

        Assert.NotEqual(tail, link.Tail);
        Assert.Equal(updatedTail, link.Tail);
    }

    [Fact]
    public void Tail_Set_NullValue_Fails()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);
        Node nullNode = null;

        var link = new Link(head, tail);

        Assert.Throws<ArgumentNullException>(() => link.Tail = nullNode);
        Assert.NotNull(link.Tail);
        Assert.Equal(tail, link.Tail);
    }

    [Fact]
    public void Tail_Set_MatchesHead_Fails()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);
        Node duplicate = new(head);

        var link = new Link(head, tail);

        Assert.Throws<ArgumentException>(() => link.Tail = duplicate);
        Assert.NotEqual(duplicate, link.Tail);
        Assert.Equal(tail, link.Tail);
    }

    #endregion Tail

    #region Constructor

    [Fact]
    public void Construct_FromNodes_Succeeds()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);

        var link = new Link(head, tail);

        Assert.Equal(head, link.Head);
        Assert.Equal(tail, link.Tail);
    }

    [Fact]
    public void Construct_FromNodes_NullNodes_Fails()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);
        Node nullNode = null;

        Assert.Throws<ArgumentNullException>(() => new Link(nullNode, tail));
        Assert.Throws<ArgumentNullException>(() => new Link(head, nullNode));
        Assert.Throws<ArgumentNullException>(() => new Link(nullNode, nullNode));
    }

    [Fact]
    public void Construct_FromNodes_MatchingNodes_Fails()
    {
        var head = new Node(Vector2.Zero);
        var tail = new Node(Vector2.Zero);

        Assert.Throws<ArgumentException>(() => new Link(head, tail));
    }

    [Fact]
    public void Construct_FromExistingLink_Succeeds()
    {
        Node head = new(Vector2.Zero);
        Node tail = new(Vector2.One);

        var originalLink = new Link(head, tail);

        Assert.Equal(head, originalLink.Head);
        Assert.Equal(tail, originalLink.Tail);

        var duplicateLink = new Link(originalLink);

        Assert.Equal(head, duplicateLink.Head);
        Assert.Equal(tail, duplicateLink.Tail);
        Assert.Equal(originalLink.Head, duplicateLink.Head);
        Assert.Equal(originalLink.Tail, duplicateLink.Tail);
    }

    [Fact]
    public void Construct_FromExistingLink_Fails()
    {
        Link nullLink = null;

        Assert.Throws<ArgumentNullException>(() => new Link(nullLink));
    }

    #endregion Constructor

    #region Reversed

    [Fact]
    public void Reversed_Succeeds()
    {
        var head = new Node(Vector2.Zero);
        var tail = new Node(Vector2.One);

        var link = new Link(head, tail);
        var reversedLink = link.Reversed();

        Assert.Equal(head, reversedLink.Tail);
        Assert.Equal(tail, reversedLink.Head);
        Assert.Equal(link.Head, reversedLink.Tail);
        Assert.Equal(link.Tail, reversedLink.Head);
    }

    #endregion Reversed

    #region Contains

    [Fact]
    public void Contains_Succeeds()
    {
        var head = new Node(Vector2.Zero);
        var tail = new Node(Vector2.One);

        var link = new Link(head, tail);

        Assert.True(link.Contains(head));
        Assert.True(link.Contains(tail));

        var duplicateHead = new Node(head);
        var duplicateTail = new Node(tail);

        Assert.True(link.Contains(duplicateHead));
        Assert.True(link.Contains(duplicateTail));
    }

    [Fact]
    public void Contains_Fails()
    {
        var head = new Node(Vector2.Zero);
        var tail = new Node(Vector2.One);

        var testNode = new Node(Vector2.One * 2);
        Node nullNode = null;

        var link = new Link(head, tail);

        Assert.False(link.Contains(testNode));
        Assert.False(link.Contains(nullNode));
    }

    #endregion Contains

    #region Equals

    [Fact]
    public void Equals_Succeeds()
    {
        var head0 = new Node(Vector2.Zero);
        var tail0 = new Node(Vector2.One);

        var link0 = new Link(head0, tail0);
        var duplicateLink0 = new Link(head0, tail0);

        Assert.True(link0.Equals(duplicateLink0));
        Assert.True(duplicateLink0.Equals(link0));

        var head1 = new Node(Vector2.Zero);
        var tail1 = new Node(Vector2.One);

        var link1 = new Link(head1, tail1);

        Assert.True(link0.Equals(link1));
        Assert.True(link1.Equals(link0));
    }

    [Fact]
    public void Equals_Fails()
    {
        var head0 = new Node(Vector2.Zero);
        var tail0 = new Node(Vector2.One);

        var link0 = new Link(head0, tail0);
        Link nullLink = null;

        Assert.False(link0.Equals(nullLink));

        var head1 = new Node(Vector2.One * 2);
        var tail1 = new Node(Vector2.One * 3);

        var link1 = new Link(head1, tail1);

        Assert.False(link0.Equals(link1));
        Assert.False(link1.Equals(link0));
    }

    #endregion Equals

    #region ToString

    [Fact]
    public void ToString_Succeeds()
    {
        var head = new Node(Vector2.Zero);
        var tail = new Node(Vector2.One);
        const string expectedString = "{ Head: { activated: False, position: { x: 0, y: 0 } }, Tail: { activated: False, position: { x: 1, y: 1 } } }";

        var link = new Link(head, tail);

        Assert.Equal(expectedString, link.ToString());
    }

    #endregion ToString
}