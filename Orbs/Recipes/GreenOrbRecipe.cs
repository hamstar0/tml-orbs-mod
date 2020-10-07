using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class GreenOrbRecipe : ModRecipe {
		public GreenOrbRecipe() : base( OrbsMod.Instance ) {
			int ingredientCount = OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.GreenOrbRecipeUniqueIngredientCount) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = OrbsConfig.Instance.Get<int>( nameof( OrbsConfig.GreenOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ItemID.JungleSpores, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<GreenOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.GreenOrbRecipeStack) ) > 0;
		}
	}
}
