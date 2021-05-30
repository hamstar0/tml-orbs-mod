using System;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Recipes;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class PurpleOrbRecipe : ModRecipe {
		public PurpleOrbRecipe() : base( OrbsMod.Instance ) {
			var config = OrbsConfig.Instance;
			int ingredientCount = config.Get<int>( nameof( config.PurpleOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = config.Get<int>( nameof( config.PurpleOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			string ingredGrpName = RecipeCommonGroupsLibraries.Groups
				.First( kv => kv.Value == RecipeCommonGroupsLibraries.EvilBiomeBossDrops )
				.Key;

			this.AddIngredient( ModContent.ItemType<GeoResonantOrbItem>(), stack );
			this.AddRecipeGroup( ingredGrpName, ingredientCount );
			this.AddTile( TileID.WorkBenches );
			this.SetResult( ModContent.ItemType<PurpleOrbItem>(), stack );
		}


		public override bool RecipeAvailable() {
			return OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.PurpleOrbRecipeStack ) ) > 0;
		}
	}
}
