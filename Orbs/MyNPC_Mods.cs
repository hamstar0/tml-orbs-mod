using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.TModLoader;
using Orbs.Items;


namespace Orbs {
	partial class OrbsNPC : GlobalNPC {
		private void TricksterModLoot( NPC npc ) {
			if( Main.netMode == 1 ) { return; }

			var tricksterNpc = npc.modNPC as TheTrickster.NPCs.TricksterNPC;
			if( tricksterNpc == null ) {
				return;
			}

			if( !tricksterNpc.HasAttacked ) {
				int stack = OrbsConfig.Instance.Get<int>( nameof( OrbsConfig.PinkOrbDropsViaTrickster ) );

				if( stack > 0 ) {
					int itemWho = Item.NewItem( npc.getRect(), ModContent.ItemType<PinkOrbItem>(), stack );
					NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
				}
			}
		}
	}
}
