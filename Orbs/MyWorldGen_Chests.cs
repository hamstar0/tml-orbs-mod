using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World.Chests;


namespace Orbs {
	partial class OrbsWorldGen : GenPass {
		private void ApplyChestOrbs() {
			var config = OrbsConfig.Instance;

			float perChestChance = config.Get<float>( nameof(OrbsConfig.AnyOrbPercentChancePerChest) );
			if( perChestChance <= 0f ) {
				return;
			}

			float totalWeight;
			IEnumerable<(float weight, int itemType)> weights = config.GetOrbChestWeights( out totalWeight );

			var def = new ChestFillDefinition(
				any: weights.Select( w=>(w.weight, new ChestFillItemDefinition(w.itemType)) ).ToArray(),
				percentChance: perChestChance
			);
			var chestDef = new ChestTypeDefinition(
				anyOfTiles: new (int?, int?)[0],
				alsoUndergroundChests: config.Get<bool>( nameof(OrbsConfig.OnlyGenOrbsInUndergroundChests) ),
				alsoDungeonAndTempleChests: true
			);
			
			IList<Chest> modifiedChests = WorldChestLibraries.AddToWorldChests( def, chestDef );

			if( modifiedChests.Count >= 1 ) {
				int i = 0;
				foreach( Chest chest in modifiedChests ) {
					if( OrbsConfig.Instance.DebugModeInfo ) {
						LogLibraries.Log( "Implanted orb in chest at " + chest.x + ", " + chest.y
							+ " (" + i + " of " + modifiedChests.Count + ")"
						);
					}
					i++;
				}
			} else {
				LogLibraries.Log( "Could not implant orbs into any chests." );
			}
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