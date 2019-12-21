using Orbs.Items;
using Orbs.Items.Materials;
using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs.Recipes {
	class WhiteOrbRecipe : ModRecipe {
		public WhiteOrbRecipe() : base( OrbsMod.Instance ) {
			this.AddRecipeGroup( "Orbs:CobaltOrPalladiumBars", 15 );
			this.AddRecipeGroup( "Orbs:MythrilOrOrichalcumBars", 12 );
			this.AddRecipeGroup( "Orbs:AdamantiteOrTitaniumBars", 8 );
			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), 1 );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<WhiteOrbItem>(), OrbsConfig.Instance.WhiteOrbCraftStack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.WhiteOrbCraftStack > 0;
		}
	}
}
