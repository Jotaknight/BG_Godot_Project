using Godot;

public partial class CardPreview : Control
{
	private TextureRect _textureRect;

	public override void _Ready()
	{
		_textureRect = GetNode<TextureRect>("TextureRect");
	}

	public void Setup(CardData cardData)
	{
		_textureRect.Texture = CardImageResolver.GetTexture(cardData);
	}

	public Vector2 GetTextureSize()
	{
		return _textureRect.Texture?.GetSize() ?? Vector2.Zero;
	}
}
