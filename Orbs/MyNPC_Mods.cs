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
				throw new ArgumentException( "TricksterModLoot "+npc );
			}

			if( !tricksterNpc.HasAttacked ) {
				int itemWho = Item.NewItem(
					npc.getRect(),
					ModContent.ItemType<PinkOrbItem>(),
					OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.PinkOrbDropsViaTrickster) )
				);
				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
			}
		}
	}
}
