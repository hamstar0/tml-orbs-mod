using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class WhiteOrbRecipe : ModRecipe {
		public WhiteOrbRecipe() : base( OrbsMod.Instance ) {
			var config = OrbsConfig.Instance;
			int ingredientCount = config.Get<int>( nameof( config.WhiteOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = config.Get<int>( nameof( config.WhiteOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			//this.AddRecipeGroup( "Orbs:CobaltOrPalladiumBars", 15 );
			//this.AddRecipeGroup( "Orbs:MythrilOrOrichalcumBars", 12 );
			//this.AddRecipeGroup( "Orbs:AdamantiteOrTitaniumBars", 8 );
			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ItemID.GuideVoodooDoll, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<WhiteOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.WhiteOrbRecipeStack) ) > 0;
		}
	}
}
