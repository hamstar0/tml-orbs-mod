using System;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Recipes;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class RedOrbRecipe : ModRecipe {
		public RedOrbRecipe() : base( OrbsMod.Instance ) {
			int ingredientCount = OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.RedOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = OrbsConfig.Instance.Get<int>( nameof( OrbsConfig.RedOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ItemID.Hellstone, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<RedOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.RedOrbRecipeStack ) ) > 0;
		}
	}
}
