using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsPlayer : ModPlayer {
		public static IList<int> GetInventoryOrbIndexes( Player player ) {
			var indexes = new List<int>();

			for( int i=0; i<player.inventory.Length; i++ ) {
				Item item = player.inventory[i];
				if( item?.active != true ) {
					continue;
				}

				if( OrbItemBase.ItemTypeColorCodes.ContainsKey(item.type) ) {
					indexes.Add( i );
				}
			}

			return indexes;
		}
	}
}
