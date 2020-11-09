using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class BrownOrbRecipe : ModRecipe {
		/*private static int GetManaCrystalShardItemType() {
			return ModContent.ItemType<FindableManaCrystals.Items.ManaCrystalShardItem>();
		}*/



		////////////////

		public BrownOrbRecipe() : base( OrbsMod.Instance ) {
			var config = OrbsConfig.Instance;
			int ingredientCount = config.Get<int>( nameof( config.BrownOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = config.Get<int>( nameof( config.BrownOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			int ingredType = ItemID.FossilOre;
			//if( ModLoader.GetMod("FindableManaCrystals") != null ) {
			//	ingredType = BrownOrbRecipe.GetManaCrystalShardItemType();
			//}

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ingredType, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<BrownOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.BrownOrbRecipeStack ) ) > 0;
		}
	}
}
