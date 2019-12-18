using System;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Orbs.Items;
using Orbs.Tiles.Base;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace Orbs.Tiles {
	class BlueOrbTile : OrbTileBase {
		public static void AnimateInvincibleFx( Vector2 position, float radius, int particles ) {
			UnifiedRandom rand = TmlHelpers.SafelyGetRand();

			for( int i = 0; i < particles; i++ ) {
				Vector2 dir = new Vector2( rand.NextFloat() - 0.5f, rand.NextFloat() - 0.5f );
				dir.Normalize();
				Vector2 dustPos = position + ( dir * rand.NextFloat() * radius );

				int dustIdx = Dust.NewDust(
					Position: dustPos,
					Width: 1,
					Height: 1,
					Type: 269,
					SpeedX: 0f,
					SpeedY: 0f,
					Alpha: 0,
					newColor: Color.White,
					Scale: 1f
				);
				Dust dust = Main.dust[dustIdx];
				dust.noGravity = true;
			}
		}



		////////////////

		public override string MyName => "Blue Orb";

		public override Color PrimaryColor => new Color( 72, 72, 224 );



		////////////////

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			Item.NewItem(
				X: i * 16,
				Y: j * 16,
				Width: 24,
				Height: 24,
				Type: ModContent.ItemType<BlueOrbItem>(),
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

			int healTimer = 1;
			bool hasDefended = false;

			mynpc.Tint = this.PrimaryColor;
			mynpc.OrbAI = ( orbNpc ) => {
				this.PseudoBiomeNpcAI( orbNpc, ref hasDefended, ref healTimer );
			};
		}


		private void PseudoBiomeNpcAI( NPC npc, ref bool hasDefended, ref int healTimer ) {
			if( healTimer-- <= 0 ) {
				healTimer = 15;

				if( npc.life < npc.lifeMax ) {
					npc.life += 1;
					CombatText.NewText( npc.getRect(), CombatText.HealLife, 1 );
				}

				if( hasDefended ) {
					npc.dontTakeDamage = false;
				}
			}

			if( !hasDefended ) {
				hasDefended = true;

				if( npc.life < ( npc.lifeMax / 5 ) ) {
					healTimer = 60 * 3;
					npc.dontTakeDamage = true;
				}
			}

			if( hasDefended && npc.dontTakeDamage ) {
				int radius = ( npc.width + npc.height ) / 2;
				BlueOrbTile.AnimateInvincibleFx( npc.Center, radius, radius / 10 );
			}
		}
	}
}
