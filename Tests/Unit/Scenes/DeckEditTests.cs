using NUnit.Framework;
using Godot;
namespace Tests.Unit.Scenes;

[TestFixture]
public class DeckEditTests
{
    [SetUp]
    public void SetUp()
    {

    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void TopHalfPreviewFits()
    {
        var mouse = new Vector2(100, 100);
        var preview = new Vector2(300, 300);
        var screen = new Vector2(500, 500);
        var result = PreviewPositionCalculator.Calculate(mouse, preview, screen);
        Assert.That(result, Is.EqualTo(new Vector2(100, 100)));
    }

    [Test]
    public void BottomHalfPreviewFits()
    {
        var mouse = new Vector2(100, 400);
        var preview = new Vector2(300, 300);
        var screen = new Vector2(500, 500);
        var result = PreviewPositionCalculator.Calculate(mouse, preview, screen);
        Assert.That(result, Is.EqualTo(new Vector2(100, 100)));
    }

    [Test]
    public void TopHalfPreviewRunsOff()
    {
        var mouse = new Vector2(50, 50);
        var preview = new Vector2(180, 180);
        var screen = new Vector2(300, 200);
        var result = PreviewPositionCalculator.Calculate(mouse, preview, screen);
        Assert.That(result, Is.EqualTo(new Vector2(50, 20)));
    }

    [Test]
    public void BottomHalfPreviewRunsOff()
    {
        var mouse = new Vector2(40, 101);
        var preview = new Vector2(150, 150);
        var screen = new Vector2(300, 200);
        var result = PreviewPositionCalculator.Calculate(mouse, preview, screen);
        Assert.That(result, Is.EqualTo(new Vector2(40, 0)));
    }
}