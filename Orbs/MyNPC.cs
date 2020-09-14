using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items.Materials;



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
			var config = OrbsConfig.Instance;

			switch( type ) {
			case NPCID.GoblinTinkerer:
				if( config.Get<int>( nameof(OrbsConfig.CyanOrbCraftStack) ) > 0 ) {
					shop.item[nextSlot] = new Item();
					shop.item[nextSlot].SetDefaults( ModContent.ItemType<CyanOrbTopFragmentItem>() );
				}
				break;
			case NPCID.Mechanic:
				if( config.Get<int>( nameof(OrbsConfig.CyanOrbCraftStack) ) > 0 ) {
					shop.item[nextSlot] = new Item();
					shop.item[nextSlot].SetDefaults( ModContent.ItemType<CyanOrbRightFragmentItem>() );
				}
				break;
			case NPCID.WitchDoctor:
				if( config.Get<int>( nameof(OrbsConfig.CyanOrbCraftStack) ) > 0 ) {
					shop.item[nextSlot] = new Item();
					shop.item[nextSlot].SetDefaults( ModContent.ItemType<CyanOrbLeftFragmentItem>() );
				}
				break;
			case NPCID.Dryad:
				if( config.Get<bool>( nameof(OrbsConfig.IsGeoResonantOrbSoldByDryad) ) ) {
					shop.item[nextSlot] = new Item();
					shop.item[nextSlot].SetDefaults( ModContent.ItemType<GeoResonantOrbItem>() );
				}
				break;
			}
			nextSlot++;
		}


		////////////////

		public override void NPCLoot( NPC npc ) {
			if( npc.modNPC != null ) {
				if( OrbsConfig.Instance.Get<int>( nameof(OrbsConfig.PinkOrbDropsViaTrickster) ) > 0 ) {
					if( ModLoader.GetMod( "TheTrickster" ) != null ) {
						this.TricksterModLoot( npc );
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
