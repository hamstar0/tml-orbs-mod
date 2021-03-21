using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


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
				tiles: new (int?, int?)[0],
				alsoUndergroundChests: config.Get<bool>( nameof(OrbsConfig.OnlyGenOrbsInUndergroundChests) ),
				alsoDungeonAndTempleChests: true
			);

			IList<Chest> modifiedChests = WorldChestHelpers.AddToWorldChests( def, chestDef );

			foreach( Chest chest in modifiedChests ) {
				LogHelpers.Log( "Implanted orb in chest at "+chest.x+", "+chest.y );
				break;
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