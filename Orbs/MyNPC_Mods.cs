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
					OrbsConfig.Instance.PinkOrbDropsViaTrickster
				);
				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
			}
		}


		////////////////

		private void StaffOfGaiaModLoot( NPC npc ) {
			if( Main.netMode == 1 ) { return; }
			if( !npc.boss ) { return; }

			var sogNpc = npc.GetGlobalNPC<StaffOfGaia.StaffOfGaiaNPC>();
			if( sogNpc == null ) {
				throw new ArgumentException( "StaffOfGaiaModLoot " + npc );
			}

			if( sogNpc.StaffOfGaiaHits == 0 ) {
				this.StaffOfGaiaModLootWithoutStaffHit( npc );
			}
		}

		////

		private void StaffOfGaiaModLootWithoutStaffHit( NPC npc ) {
			string npcKey = NPCID.GetUniqueKey( npc.netID );
			bool hasKills = false;

			for( int i = 0; i < 255; i++ ) {
				if( !npc.playerInteraction[i] ) { continue; }

				var myplayer = TmlHelpers.SafelyGetModPlayer<OrbsPlayer>( Main.player[i] );
				if( myplayer == null ) { continue; }

				if( myplayer.NoSoGKillCount.ContainsKey(npcKey) ) {
					myplayer.NoSoGKillCount[npcKey] = 1;
				} else {
					myplayer.NoSoGKillCount[npcKey]++;
					hasKills = true;
				}
			}

			if( !hasKills ) {
				int itemWho = Item.NewItem(
					npc.getRect(),
					ModContent.ItemType<RedOrbItem>(),
					OrbsConfig.Instance.RedOrbDropsViaBossWithoutSoG
				);
				if( Main.netMode == 2 ) {
					NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
				}
			}
		}
	}
}
