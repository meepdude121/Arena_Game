using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
	namespace Item
	{
		public class ItemData
		{
			public Color GetRarityColor(Rarity rarity)
			{
				switch (rarity)
				{
					case Rarity.common:
						return new Color32(170, 170, 170, 255);
					case Rarity.uncommon:
						return new Color32(122, 218, 122, 255);
					case Rarity.rare:
						return new Color32(63, 122, 212, 255);
					case Rarity.epic:
						return new Color32(209, 100, 221, 255);
					case Rarity.legendary:
						return new Color32(255, 184, 56, 255);
					case Rarity.mythic:
						return new Color32(255, 109, 109, 255);
					case Rarity.ultimate:
						// special case maybe have a shader for this one i think
						// oh well i dont need to worry about htis for a long long time
						return new Color32(0, 0, 0, 255);
				}
				// if text is transparent something bad happened
				return new Color32(255, 255, 255, 125);
			}
			public enum Rarity
			{
				common,
				uncommon,
				rare,
				epic,
				legendary,
				mythic,
				ultimate
			}
		}
	}
}
