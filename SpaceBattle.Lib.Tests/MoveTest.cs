using System;
using Moq;

namespace SpaceBattle.Lib.Tests;

public class MovableTests
{

    [Fact]
    public void MoveCommand_Well()
    {
        var movable = new Mock<IMovable>();

        movable.SetupGet(m => m.Position).Returns(new Vector(12, 5)).Verifiable();
        movable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3)).Verifiable();

        ICommand mc = new MoveCommand(movable.Object);

        mc.Execute();

        movable.VerifySet(m => m.Position = new Vector(5, 8), Times.Once);
        movable.VerifyAll();
    }

    [Fact]
    public void MoveCommand_Without_Position()
    {
        var movable = new Mock<IMovable>();

        movable.SetupGet(m => m.Position).Throws(new NotImplementedException());

        ICommand mc = new MoveCommand(movable.Object);

        Assert.Throws<NotImplementedException>(mc.Execute);
    }

    [Fact]
    public void MoveCommand_Without_Velocity()
    {
        var movable = new Mock<IMovable>();

        movable.SetupGet(m => m.Position).Returns(new Vector(0, 1)).Verifiable();
        movable.SetupGet(m => m.Velocity).Throws(new NotImplementedException());

        ICommand mc = new MoveCommand(movable.Object);

        Assert.Throws<NotImplementedException>(mc.Execute);
    }

    [Fact]
    public void MoveCommand_Cant_Set_Position()
    {
        var movable = new Mock<IMovable>();

        movable.SetupGet(m => m.Position).Returns(new Vector(0, 1)).Verifiable();
        movable.SetupGet(m => m.Velocity).Returns(new Vector(1, 2)).Verifiable();
        movable.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws(new NotImplementedException());

        ICommand mc = new MoveCommand(movable.Object);

        Assert.Throws<NotImplementedException>(mc.Execute);
        movable.VerifyAll();
    }
}
