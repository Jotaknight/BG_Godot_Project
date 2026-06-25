using Godot;

public static class PreviewPositionCalculator
{
	public static Vector2 Calculate(Vector2 mousePosition, Vector2 previewSize, Vector2 screenSize)
	{
		var newPosition = mousePosition;

		if (newPosition.Y < screenSize.Y / 2)
		{
			float overflowY = Mathf.Max(0, newPosition.Y + previewSize.Y - screenSize.Y);
			newPosition.Y -= overflowY;
		}
		else
		{
			newPosition.Y = Mathf.Max(0, newPosition.Y - previewSize.Y);
		}
		return newPosition;
	}
}
