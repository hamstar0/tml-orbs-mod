using Orbs.Items;
using Orbs.Items.Materials;
using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs.Recipes {
	class BlueOrbRecipe : ModRecipe {
		public BlueOrbRecipe() : base( OrbsMod.Instance ) {
			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), 1 );
			this.AddIngredient( ModContent.ItemType<FindableManaCrystals.Items.ManaCrystalShardItem>(), 1 );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<BlueOrbItem>(), OrbsConfig.Instance.BlueOrbCraftStack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.BlueOrbCraftStack > 0;
		}
	}
}
