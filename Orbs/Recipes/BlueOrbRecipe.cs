using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items.Materials;
using Orbs.Items;


namespace Orbs.Recipes {
	class BlueOrbRecipe : ModRecipe {
		public BlueOrbRecipe() : base( OrbsMod.Instance ) {
			int ingredientCount = OrbsConfig.Instance.Get<int>( nameof( OrbsConfig.BlueOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = OrbsConfig.Instance.Get<int>( nameof( OrbsConfig.BlueOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ItemID.WaterCandle, ingredientCount );
			//this.AddIngredient( ModContent.ItemType<FindableManaCrystals.Items.ManaCrystalShardItem>(), 1 );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<BlueOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.BlueOrbRecipeStack) ) > 0;
		}
	}
}
