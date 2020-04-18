using System;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Items;
using Orbs.Items;


namespace Orbs.Recipes {
	class YellowOrbRecipe : ModRecipe {
		public YellowOrbRecipe() : base( OrbsMod.Instance ) {
			this.AddRecipeGroup( "Orbs:CopperOrTinBars", 15 );
			this.AddRecipeGroup( "Orbs:IronOrLeadBars", 15 );
			this.AddRecipeGroup( "Orbs:SilverOrTungstenBars", 12 );
			this.AddRecipeGroup( "Orbs:GoldOrPlatinumBars", 10 );
			this.AddRecipeGroup( "Orbs:StrangePlants", 1 );
			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), 1 );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<YellowOrbItem>(), OrbsConfig.Instance.YellowOrbCraftStack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.YellowOrbCraftStack > 0;
		}
	}
}
