using System;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class YellowOrbRecipe : ModRecipe {
		public YellowOrbRecipe() : base( OrbsMod.Instance ) {
			var config = OrbsConfig.Instance;
			int ingredientCount = config.Get<int>( nameof( config.YellowOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = config.Get<int>( nameof( config.YellowOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			//this.AddRecipeGroup( "Orbs:CopperOrTinBars", 15 );
			//this.AddRecipeGroup( "Orbs:IronOrLeadBars", 15 );
			//this.AddRecipeGroup( "Orbs:SilverOrTungstenBars", 12 );
			//this.AddRecipeGroup( "Orbs:GoldOrPlatinumBars", 10 );
			//this.AddRecipeGroup( "Orbs:StrangePlants", 1 );
			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddIngredient( ItemID.BeeWax, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<YellowOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.YellowOrbRecipeStack) ) > 0;
		}
	}
}
