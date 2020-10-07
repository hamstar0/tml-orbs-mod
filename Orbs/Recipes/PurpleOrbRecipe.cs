﻿using System;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Recipes;
using Orbs.Items;
using Orbs.Items.Materials;


namespace Orbs.Recipes {
	class PurpleOrbRecipe : ModRecipe {
		public PurpleOrbRecipe() : base( OrbsMod.Instance ) {
			int ingredientCount = OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.PurpleOrbRecipeUniqueIngredientCount ) );
			if( ingredientCount == 0 ) {
				return;
			}
			int stack = OrbsConfig.Instance.Get<int>( nameof( OrbsConfig.PurpleOrbRecipeStack ) );
			if( stack == 0 ) {
				return;
			}

			string ingredGrpName = RecipeGroupHelpers.Groups
				.First( kv => kv.Value == RecipeGroupHelpers.EvilBiomeBossDrops )
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
