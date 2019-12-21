using System;
using Microsoft.Xna.Framework;
using Orbs.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs {
	partial class OrbsNPC : GlobalNPC {
		public Color? Tint = null;

		public Action<NPC> OrbAI = null;
		public Func<NPC, Projectile, bool> PreProjectileHit = null;
		public Func<NPC, Player, Item, bool> PreItemHit = null;
		public Action<NPC, Player, int, bool> HitPlayer = null;


		////////////////

		public override bool InstancePerEntity => true;



		////////////////

		public override void SetDefaults( NPC npc ) {
			this.SetPseudoBiomeEffects( npc );
		}


		////////////////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			switch( type ) {
			case NPCID.GoblinTinkerer:
				shop.item[nextSlot] = new Item();
				shop.item[nextSlot].SetDefaults( ModContent.ItemType<CyanOrbTopFragmentItem>() );
				break;
			case NPCID.Mechanic:
				shop.item[nextSlot] = new Item();
				shop.item[nextSlot].SetDefaults( ModContent.ItemType<CyanOrbRightFragmentItem>() );
				break;
			case NPCID.WitchDoctor:
				shop.item[nextSlot] = new Item();
				shop.item[nextSlot].SetDefaults( ModContent.ItemType<CyanOrbLeftFragmentItem>() );
				break;
			}
			nextSlot++;
		}


		////////////////

		public override void NPCLoot( NPC npc ) {
			if( npc.modNPC != null ) {
				if( OrbsConfig.Instance.PinkOrbDropsViaTrickster > 0 ) {
					if( ModLoader.GetMod( "TheTrickster" ) != null ) {
						this.TricksterModLoot( npc );
					}
				}
				if( OrbsConfig.Instance.RedOrbDropsViaBossWithoutSoG > 0 ) {
					if( ModLoader.GetMod( "StaffOfGaia" ) != null ) {
						this.StaffOfGaiaModLoot( npc );
					}
				}
			}
		}


		////////////////

		public override bool PreAI( NPC npc ) {
			this.OrbAI?.Invoke( npc );
			return base.PreAI( npc );
		}


		public override bool? CanBeHitByProjectile( NPC npc, Projectile projectile ) {
			return this.PreProjectileHit?.Invoke(npc, projectile) ?? base.CanBeHitByProjectile( npc, projectile );
		}

		public override bool? CanBeHitByItem( NPC npc, Player player, Item item ) {
			return this.PreItemHit?.Invoke( npc, player, item ) ?? base.CanBeHitByItem( npc, player, item );
		}


		public override void OnHitPlayer( NPC npc, Player target, int damage, bool crit ) {
			this.HitPlayer?.Invoke( npc, target, damage, crit );
		}



		////////////////

		public override void DrawEffects( NPC npc, ref Color drawColor ) {
			if( this.Tint.HasValue ) {
				this.ApplyTint( npc, ref drawColor );
			}
		}
	}
}
