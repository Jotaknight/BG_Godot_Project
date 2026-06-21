using Godot;

public partial class CardImageResolver : RefCounted
{
	public static Texture2D GetTexture(CardData cardData)
	{
		string levelText = cardData.Level?.ToString() ?? "_";
		string cardFilename = $"{cardData.Name}{levelText}{cardData.Class}";
		string path = $"res://Resources/Images/Cards/Player/{cardFilename}.jpg";
		return GD.Load<Texture2D>(path);
	}
}
