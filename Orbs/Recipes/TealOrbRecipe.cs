using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class TealOrbRecipe : ModRecipe {
		/*private static int GetManaCrystalShardItemType() {
			return ModContent.ItemType<FindableManaCrystals.Items.ManaCrystalShardItem>();
		}*/



		////////////////

		public TealOrbRecipe() : base( OrbsMod.Instance ) {
			var config = OrbsConfig.Instance;
			int ingredientCount = config.Get<int>( nameof( config.TealOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = config.Get<int>( nameof( config.TealOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			int ingredType = ItemID.FossilOre;
			//if( ModLoader.GetMod("FindableManaCrystals") != null ) {
			//	ingredType = TealOrbRecipe.GetManaCrystalShardItemType();
			//}

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ingredType, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<TealOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.TealOrbRecipeStack ) ) > 0;
		}
	}
}
