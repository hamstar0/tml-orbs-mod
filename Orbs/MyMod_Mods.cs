using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;


namespace Orbs {
	public partial class OrbsMod : Mod {
		private void LoadForChestImplants() {
			ChestImplants.ChestImplantsAPI.AddCustomImplanter( "Random Orb", ( context, chest ) => {
				var config = OrbsConfig.Instance;

				if( config.Get<bool>( nameof(OrbsConfig.OnlyGenOrbsInUndergroundChests) ) ) {
					if( !ChestImplants.ChestImplanter.IsChestMatch( context, "Vanilla Underground World Chest" ) ) {
						return;
					}
				}

				UnifiedRandom rand = TmlHelpers.SafelyGetRand();
				if( rand.NextFloat() >= config.Get<float>( nameof(OrbsConfig.AnyOrbPercentChancePerChest) ) ) {
					return;
				}

				int orbItemType = -1;

				float totalWeight;
				IEnumerable<(float, int)> weights = config.GetOrbChestWeights( out totalWeight );
				float weightRand = rand.NextFloat() * totalWeight;

				float countedWeight = 0f;
				foreach( (float weight, int checkOrbItemType) in weights ) {
					if( (countedWeight + weight) < weightRand ) {
						countedWeight += weight;
						continue;
					}

					orbItemType = checkOrbItemType;
					break;
				}

				if( orbItemType == -1 ) {
					LogHelpers.Warn( "Could not pick random orb to implant in chest." );
					return;
				}

				for( int i=0; i<chest.item.Length; i++ ) {
					Item item = chest.item[i];
					if( item?.active == true || !item.IsAir ) {
						continue;
					}

					chest.item[i] = new Item();
					chest.item[i].SetDefaults( orbItemType );
					chest.item[i].stack = 1;

					LogHelpers.Log( "Implanted "+chest.item[i].Name+" in chest." );
					break;
				}
			} );
		}


		////
		
		/*private void LoadRecipesForFindableManaCrystals() {
			var blueOrbRecipe = new BlueOrbRecipe();
			if( blueOrbRecipe.RecipeAvailable() ) {
				blueOrbRecipe.AddRecipe();
			}
		}*/
	}
}