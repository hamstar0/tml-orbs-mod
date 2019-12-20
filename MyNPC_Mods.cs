using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;
using TheTrickster.NPCs;


namespace Orbs {
	partial class OrbsNPC : GlobalNPC {
		private void TricksterLoot( NPC npc ) {
			var tricksterNpc = npc.modNPC as TricksterNPC;
			if( tricksterNpc == null ) {
				throw new ArgumentException();
			}

			if( !tricksterNpc.HasAttacked ) {
				int itemWho = Item.NewItem( npc.getRect(), ModContent.ItemType<PinkOrbItem>(), OrbsConfig.Instance.TricksterPinkOrbs );
				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
			}
		}
	}
}
