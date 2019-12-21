using Orbs.Items;
using Orbs.Items.Materials;
using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs.Recipes {
	class CyanOrbRecipe : ModRecipe {
		public CyanOrbRecipe() : base( OrbsMod.Instance ) {
			this.AddIngredient( ModContent.ItemType<CyanOrbTopFragmentItem>(), 1 );
			this.AddIngredient( ModContent.ItemType<CyanOrbRightFragmentItem>(), 1 );
			this.AddIngredient( ModContent.ItemType<CyanOrbLeftFragmentItem>(), 1 );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<CyanOrbItem>(), OrbsConfig.Instance.CyanOrbCraftStack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.CyanOrbCraftStack > 0;
		}
	}
}
