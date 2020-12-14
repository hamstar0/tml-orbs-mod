using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsPlayer : ModPlayer {
		public static bool IsTargettingOrbChunkOfTile( Player player, int tileX, int tileY ) {
			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode tileColorCode = orbWld.GetColorCodeOfOrbChunkOfTile( tileX, tileY );
			if( tileColorCode == 0 ) {
				return false;
			}

			Item heldItem = player.HeldItem;
			OrbItemBase myorb = heldItem?.modItem as OrbItemBase;
			if( myorb == null ) {
				return false;
			}

			OrbColorCode plrColorCode = myorb?.ColorCode ?? (OrbColorCode)0;
			bool canSeeColor = tileColorCode == plrColorCode;// || canSeeAllColors;
			if( !canSeeColor ) {
				return false;
			}

/*var myplayer = player.GetModPlayer<OrbsPlayer>();
DebugHelpers.Print( "orb",
	"i:"+tileX+", j:"+tileY
	+", plr:"+(player.Center/16).ToShortString()
	+", dist:"+((player.Center/16)-(new Vector2(tileX, tileY))).Length().ToString("N2")
	+" - "+tileColorCode.ToString()
	+", match?"+(tileColorCode == plrColorCode)
	+", within?"+(OrbItemBase.IsTileWithinUseRange(player, tileX, tileY)) );*/
			if( !OrbItemBase.IsTileWithinUseRange(player, tileX, tileY) ) {
				return false;
			}

			return true;
		}



		////////////////

		public (int ChunkX, int ChunkY)? CurrentTargetOrbChunkGridPos { get; private set; } = null;



		////////////////

		public override void PreUpdate() {
			if( !this.IsTargetOrbChunkAvailable() ) {
				this.CurrentTargetOrbChunkGridPos = null;
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

		public bool CanViewAllOrbChunks() {
			if( OrbsConfig.Instance.Get<bool>( nameof( OrbsConfig.DebugModeTheColorsDuke ) ) ) {
				return true;
			}

			Item heldItem = this.player.HeldItem;
			return heldItem?.active == true && heldItem.type == ItemID.Binoculars;
		}


		////////////////

		private bool IsTargetOrbChunkAvailable() {
			(int x, int y)? chunkGridPos = this.CurrentTargetOrbChunkGridPos;
			if( !chunkGridPos.HasValue ) {
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
			bool isWithinRange = OrbItemBase.IsOrbChunkWithinUseRange( this.player.Center, chunkGridPos.Value );
			
			return isWithinRange;
		}

		////

		/// <summary>
		/// Gets the orb chunk of a given tile, if the player is targetting it.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <returns></returns>
		public (int ChunkX, int ChunkY)? GetOrbChunkIfTargetted( int tileX, int tileY ) {
			int chunkTileSize = OrbItemBase.ChunkTileSize;

			if( !this.CurrentTargetOrbChunkGridPos.HasValue ) {
				if( OrbsPlayer.IsTargettingOrbChunkOfTile(this.player, tileX, tileY) ) {
					this.CurrentTargetOrbChunkGridPos = (tileX / chunkTileSize, tileY / chunkTileSize);
				}
			} else {
				if( this.CurrentTargetOrbChunkGridPos.Value.ChunkX != (tileX / chunkTileSize) ) {
					return null;
				}
				if( this.CurrentTargetOrbChunkGridPos.Value.ChunkY != (tileY / chunkTileSize) ) {
					return null;
				}
			}
			return this.CurrentTargetOrbChunkGridPos;
		}

		////////////////

		public void ClearTargetOrbChunk() {
			this.CurrentTargetOrbChunkGridPos = null;
		}
	}
}
