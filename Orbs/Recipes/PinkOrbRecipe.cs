using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class PinkOrbRecipe : ModRecipe {
		public PinkOrbRecipe() : base( OrbsMod.Instance ) {
			int ingredientCount = OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.PinkOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = OrbsConfig.Instance.Get<int>( nameof( OrbsConfig.PinkOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ItemID.PinkGel, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<PinkOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.PinkOrbRecipeStack ) ) > 0;
		}
	}
}
