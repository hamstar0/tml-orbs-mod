using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.TModLoader;
using Orbs.Items;
using Orbs.Tiles.Base;


namespace Orbs.Tiles {
	class BrownOrbTile : OrbTileBase {
		public override string MyName => "Brown Orb";

		public override Color PrimaryColor => new Color( 72, 224, 142 );



		////////////////

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			Item.NewItem(
				X: i * 16,
				Y: j * 16,
				Width: 24,
				Height: 24,
				Type: ModContent.ItemType<BrownOrbItem>(),
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

			int projTimer = 1;
			int hitProjIdx = -1;
			bool isHit = false;

			mynpc.Tint = this.PrimaryColor;
			mynpc.OrbAI = ( orbNpc ) => {
				if( hitProjIdx != -1 && !Main.projectile[hitProjIdx].active ) {
					hitProjIdx = -1;
				}
				this.PseudoBiomeNpcAI( orbNpc, ref projTimer );
			};

			mynpc.PreProjectileHit = ( orbNpc, projectile ) => {
				if( hitProjIdx == -1 ) {
					hitProjIdx = projectile.whoAmI;
					isHit = TmlHelpers.SafelyGetRand().NextBool();
				}
				return isHit;
			};
		}


		private void PseudoBiomeNpcAI( NPC npc, ref int projTimer ) {
			if( Main.netMode == 1 ) {
				return;
			}

			var rand = TmlHelpers.SafelyGetRand();

			if( projTimer-- <= 0 ) {
				projTimer = 60 * rand.Next( 10, 30 );

				if( !npc.HasNPCTarget && npc.target >= 0 ) {
					Player player = Main.player[npc.target];
					Vector2 aim = player.Center - npc.Center;
					aim.Normalize();

					int projIdx = Projectile.NewProjectile(
						position: npc.Center,
						velocity: aim * 3f,
						Type: ProjectileID.PoisonSeedPlantera,
						Damage: 1,
						KnockBack: 5f
					);
					Main.projectile[projIdx].friendly = false;
					Main.projectile[projIdx].hostile = true;
					Main.projectile[projIdx].netUpdate = true;

					if( Main.netMode != 0 ) {
						NetMessage.SendData( MessageID.SyncProjectile, -1, -1, null, projIdx );
					}
				}
			}
		}
	}
}
