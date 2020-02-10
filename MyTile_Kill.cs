using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Hooks.ExtendedHooks;
using Orbs.Items;


namespace Orbs {
	class OrbsExtendedTileHooks : ILoadable {
		private static void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem, bool isNonGameplay ) {
			if( Main.gameMenu && Main.netMode != 2 ) {
				return;
			}
			if( fail || effectOnly || isNonGameplay ) {
				return;
			}
			//if( Main.netMode != 2 && !LoadHelpers.IsCurrentPlayerInGame() ) {
			//	return;
			//}
			if( !LoadHelpers.IsWorldBeingPlayed() ) {
				return;
			}

			if( OrbsConfig.Instance.HardmodeBreakableDirt && Main.hardMode ) {
				if( type == TileID.Dirt ) {
					return;
				}
			}

			if( !OrbsTile.IsKillable( type ) ) {
				if( !OrbsConfig.Instance.CanDestroyActuatedTiles
					|| (OrbsConfig.Instance.CanDestroyActuatedTiles && !Main.tile[i, j].inActive()) ) {
					fail = true;
					effectOnly = true;
					noItem = true;
				}
			}
		}


		private static void KillMultiTile( int i, int j, int type, bool isNonGameplay ) {
			if( type != TileID.ShadowOrbs ) { return; }
			if( isNonGameplay ) { return; }
			if( OrbsConfig.Instance.PurpleOrbDropsViaShadowOrb == 0 ) { return; }
			
			int itemWho = Item.NewItem(
				new Rectangle( i << 4, j << 4, 32, 32 ),
				ModContent.ItemType<PurpleOrbItem>()
			);
		}



		////////////////

		private OrbsExtendedTileHooks() { }

		void ILoadable.OnModsLoad() {
		}

		void ILoadable.OnModsUnload() {
		}

		void ILoadable.OnPostModsLoad() {
			if( OrbsConfig.Instance.PurpleOrbDropsViaShadowOrb > 0 ) {
				ExtendedTileHooks.AddKillMultiTileHook( OrbsExtendedTileHooks.KillMultiTile );
			}

			var killTileHook = new ExtendedTileHooks.KillTileDelegate( OrbsExtendedTileHooks.KillTile );
			ExtendedTileHooks.AddSafeKillTileHook( killTileHook );
		}
	}



	
	partial class OrbsTile : GlobalTile {
		public static bool IsKillable( int tileType ) {
			return OrbsConfig.Instance.TileKillWhitelist.Contains( TileID.GetUniqueKey( tileType ) );
		}



		////////////////

		/*public override bool CanKillTile( int i, int j, int type, ref bool blockDamaged ) {
			bool fail = false, effectOnly = false, noItem = false;
			this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			return !fail;
		}*/


		/*public override bool Slope( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return false;
		}*/

		public override bool CreateDust( int i, int j, int type, ref int dustType ) {
			if( Main.gameMenu || !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return OrbsTile.IsKillable( type );
			//bool fail=false, effectOnly=false, noItem=false;
			//this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			//return !fail || effectOnly;
		}

		/*public override bool KillSound( int i, int j, int type ) {
			if( !LoadHelpers.IsCurrentPlayerInGame() ) {
				return true;
			}

			return AdventureModeTile.IsKillable( type );
			//bool fail = false, effectOnly = false, noItem = false;
			//this.KillTile( i, j, type, ref fail, ref effectOnly, ref noItem );
			//return !fail || effectOnly;
		}*/
	}
}
