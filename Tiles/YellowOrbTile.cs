using System;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Orbs.Items;
using Orbs.Tiles.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs.Tiles {
	class YellowOrbTile : OrbTileBase {
		public override string MyName => "Yellow Orb";

		public override Color PrimaryColor => new Color( 208, 208, 72 );



		////////////////

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			Item.NewItem(
				X: i * 16,
				Y: j * 16,
				Width: 24,
				Height: 24,
				Type: ModContent.ItemType<YellowOrbItem>(),
				Stack: 4
			);
		}


		////////////////

		public override void NearbyEffects( int i, int j, bool closer ) {
			base.NearbyEffects( i, j, closer );
		}


		////////////////

		public override void ApplyPseudoBiomeToNPC( NPC npc ) {
			var mynpc = npc.GetGlobalNPC<OrbsNPC>();
			if( mynpc.OrbAI != null ) {
				return;
			}

			int hitItemType = -1;
			bool isHit = false;

			mynpc.Tint = this.PrimaryColor;

			mynpc.OrbAI = ( orbNpc ) => {
				if( hitItemType != -1 && !Main.projectile[hitItemType].active ) {
					hitItemType = -1;
				}
				this.PseudoBiomeNpcAI( orbNpc );
			};

			mynpc.HitPlayer = ( orbNpc, player, damage, crit ) => {
				player.AddBuff( BuffID.OnFire, 60 * 5 );
			};

			mynpc.PreItemHit = ( orbNpc, player, item ) => {
				if( hitItemType == -1 ) {
					hitItemType = item.type;
					isHit = TmlHelpers.SafelyGetRand().NextBool();
				}
				return isHit;
			};
			//TODO: Check correctness
		}


		private void PseudoBiomeNpcAI( NPC npc ) {
			for( int i=0; i<npc.buffType.Length; i++ ) {
				if( npc.buffTime[i] > 0 ) {
					npc.buffTime[i] = 0;
				}
			}
		}
	}
}
