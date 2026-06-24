using Godot.Collections;
using Godot;

public partial class CardData : RefCounted
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Class { get; set; }
	public int? Level { get; set; }
	public string Type { get; set; }
	public int? Cost { get; set; }
	public Skills Skills { get; set; }

	public void SetupFromDB(Godot.Collections.Dictionary<string, Variant> item)
	{
		Id = (int)item["CARD_ID"];
		Name = item["CARD_NAME"].AsString();
		Class = item["CARD_CLASS"].AsString();

		Level = item["CARD_LEVEL"].VariantType == Variant.Type.Nil
			? null
			: (int)item["CARD_LEVEL"];

		Type = item["CARD_TYPE"].AsString();

		Cost = item["CARD_COST"].VariantType == Variant.Type.Nil
			? null
			: (int)item["CARD_COST"];

		Skills = new Skills();
		Skills.Willpower = (int)item["SKILL_WILLPOWER"];
		Skills.Intellect = (int)item["SKILL_INTELLECT"];
		Skills.Combat = (int)item["SKILL_COMBAT"];
		Skills.Agility = (int)item["SKILL_AGILITY"];
		Skills.Wild = (int)item["SKILL_WILD"];
	}
}
