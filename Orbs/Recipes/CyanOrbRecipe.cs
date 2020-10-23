using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class CyanOrbRecipe : ModRecipe {
		public CyanOrbRecipe() : base( OrbsMod.Instance ) {
			var config = OrbsConfig.Instance;
			int ingredientCount = config.Get<int>( nameof( config.CyanOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = config.Get<int>( nameof( config.CyanOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ItemID.Feather, ingredientCount );
			//this.AddIngredient( ModContent.ItemType<CyanOrbTopFragmentItem>(), 1 );
			//this.AddIngredient( ModContent.ItemType<CyanOrbRightFragmentItem>(), 1 );
			//this.AddIngredient( ModContent.ItemType<CyanOrbLeftFragmentItem>(), 1 );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<CyanOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.CyanOrbRecipeStack) ) > 0;
		}
	}
}
