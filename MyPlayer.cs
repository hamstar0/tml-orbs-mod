using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Orbs.Tiles.Base;


namespace Orbs {
	class OrbsPlayer : ModPlayer {
		internal IList<(int TileX, int TileY)> NearbyOrbs = new List<(int, int)>();



		////////////////

		/*public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.UpdateOrbsNearby();
			}

			if( OrbsConfig.Instance.DebugModeCheatCreate ) {
				if( Main.mouseRight && Main.mouseRightRelease ) {
					int x = (int)( ( Main.screenPosition.X + Main.mouseX ) / 16 );
					int y = (int)( ( Main.screenPosition.Y + Main.mouseY ) / 16 );

					OrbTileBase.CreateTile( x, y );

					var myworld = ModContent.GetInstance<OrbsWorld>();
					myworld.GetOrbs().Set2D( x, y );
				}
			}
		}*/


		/*private void UpdateOrbsNearby() {
			var myworld = ModContent.GetInstance<OrbsWorld>();

			int scrTiles = Main.screenWidth / 16;
			int scrTileX = (int)( Main.screenPosition.X / 16f );
			int scrTileY = (int)( Main.screenPosition.Y / 16f );

			int biomeRadiusSqr = OrbsConfig.Instance.OrbPseudoBiomeTileRadius + scrTiles;
			biomeRadiusSqr *= biomeRadiusSqr;

			this.NearbyOrbs.Clear();

			foreach( (int tileX, ISet<int> tileYs) in myworld.GetOrbs() ) {
				foreach( int tileY in tileYs ) {
					int diffX = scrTileX - tileX;
					int diffY = scrTileY - tileY;
					int distSqr = ( diffX * diffX ) + ( diffY * diffY );

					if( distSqr < biomeRadiusSqr ) {
						this.NearbyOrbs.Add( (tileX, tileY) );
					}
				}
			}
		}*/
	}
}
