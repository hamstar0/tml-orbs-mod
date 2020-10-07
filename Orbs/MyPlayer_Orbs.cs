using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsPlayer : ModPlayer {
		public static (int x, int y)? GetTileChunkIfValidTarget( Player player, int tileX, int tileY ) {
			Item heldItem = player.HeldItem;
			bool isBinoculars = heldItem?.active == true && heldItem.type == ItemID.Binoculars;
			bool canSeeAllColors = isBinoculars || OrbsConfig.Instance.DebugModeTheColorsDuke;

			var myorb = heldItem.modItem as OrbItemBase;
			if( myorb == null ) {
				if( !isBinoculars && !OrbsConfig.Instance.DebugModeTheColorsDuke ) {
					return null;
				}
			}

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode tileColorCode = orbWld.GetTileColorCode( tileX, tileY );
			if( tileColorCode == 0 ) {
				return null;
			}

			OrbColorCode plrColorCode = myorb?.ColorCode ?? (OrbColorCode)0;
			bool canSeeColor = tileColorCode == plrColorCode || canSeeAllColors;
			if( !canSeeColor ) {
				return null;
			}

			//if( this.CurrentTargetTileChunk.HasValue ) {
			//	return null;
			//}

/*var myplayer = player.GetModPlayer<OrbsPlayer>();
DebugHelpers.Print( "orb",
	"i:"+tileX+", j:"+tileY
	+", plr:"+(player.Center/16).ToShortString()
	+", dist:"+((player.Center/16)-(new Vector2(tileX, tileY))).Length().ToString("N2")
	+" - "+tileColorCode.ToString()
	+", match?"+(tileColorCode == plrColorCode)
	+", within?"+(OrbItemBase.IsTileWithinUseRange(player, tileX, tileY)) );*/
			if( !canSeeAllColors && !OrbItemBase.IsTileWithinUseRange(player, tileX, tileY) ) {
				return null;
			}

			return ((tileX/16) * 16, (tileY/16) * 16);
		}



		////////////////

		public (int X, int Y)? CurrentTargetTileChunk { get; private set; } = null;



		////////////////

		public override void PreUpdate() {
			if( !this.IsTargetTileChunkAvailable() ) {
				this.CurrentTargetTileChunk = null;
			}

			/*if( this.player.whoAmI == Main.myPlayer ) {
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
			}*/
		}


		////

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


		////////////////

		private bool IsTargetTileChunkAvailable() {
			(int x, int y)? chunk = this.CurrentTargetTileChunk;
			if( !chunk.HasValue ) {
				return false;
			}

			// Are we holding any item?
			if( this.player.HeldItem?.active != true ) {
				return false;
			}

			Item heldItem = this.player.HeldItem;

			// Are we still holding our orb? Binoculars? Cheats?
			var myorb = heldItem.modItem as OrbItemBase;
			if( myorb == null ) {
				bool isBinoculars = heldItem.type == ItemID.Binoculars;
				if( !isBinoculars && !OrbsConfig.Instance.DebugModeTheColorsDuke ) {
					return false;
				}
			}

			// Is nearby targetted orb still within range?
			bool isWithinRange = OrbItemBase.IsTileChunkWithinUseRange( this.player.Center, chunk.Value );

			return isWithinRange;
		}

		////

		//private int _PrevTargetCheckTileChunkCode = 0;

		public (int x, int y)? GetTargetTileChunk( int tileX, int tileY ) {
			if( !this.CurrentTargetTileChunk.HasValue ) {
				//int code = OrbsWorld.GetTileChunkCode( tileX, tileY );
				//if( code == this._PrevTargetCheckTileChunkCode ) {
				//	return;
				//}
				//this._PrevTargetCheckTileChunkCode = code;

				this.CurrentTargetTileChunk = OrbsPlayer.GetTileChunkIfValidTarget( this.player, tileX, tileY );
			}
			return this.CurrentTargetTileChunk;
		}

		////////////////

		public void ClearTargetChunk() {
			this.CurrentTargetTileChunk = null;
		}
	}
}
