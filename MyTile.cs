using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Services.AnimatedColor;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsTile : GlobalTile {
		private static int _CurrentCoordCode = -1;
		private static OrbColorCode _CurrentCoordColorCode = 0;

		internal static (int X, int Y)? CurrentTargetTileChunk = null;



		////////////////
		
		public static bool IsOrbTileType( int tileType ) {
			return tileType == TileID.ObsidianBrick
				|| TileGroupIdentityHelpers.VanillaEarthTiles.Contains(tileType);
		}

		////////////////

		public static OrbColorCode GetTileColorCode( int i, int j ) {
			int coordCode = ( i / 16 ) + ( ( j / 16 ) << 16 );
			if( OrbsTile._CurrentCoordCode == coordCode ) {
				return OrbsTile._CurrentCoordColorCode;
			}

			var mycustomplr = CustomPlayerData.GetPlayerData<OrbsPlayerData>( Main.LocalPlayer.whoAmI );
			if( mycustomplr == null ) {
				return 0;
			}

			OrbColorCode tileColorCode = OrbItemBase.GetRandomColorCode( mycustomplr.WorldCode + coordCode );

			OrbsTile._CurrentCoordCode = coordCode;
			OrbsTile._CurrentCoordColorCode = tileColorCode;

			return OrbsTile._CurrentCoordColorCode;
		}



		////////////////

		public override void DrawEffects(
					int i,
					int j,
					int type,
					SpriteBatch sb,
					ref Color drawColor,
					ref int nextSpecialDrawIndex ) {
			Tile tile = Main.tile[i, j];

			if( !tile.active() || tile.inActive() || !OrbsTile.IsOrbTileType(type) ) {
				return;
			}

			if( Main.LocalPlayer.HeldItem?.active != true ) {
				OrbsTile.CurrentTargetTileChunk = null;
				return;
			}

			//var myplayer = TmlHelpers.SafelyGetModPlayer<OrbsPlayer>( Main.LocalPlayer );
			Item heldItem = Main.LocalPlayer.HeldItem;
			bool isBinoculars = heldItem.type == ItemID.Binoculars;

			var myorb = heldItem.modItem as OrbItemBase;
			if( myorb == null ) {
				OrbsTile.CurrentTargetTileChunk = null;

				if( !isBinoculars && !OrbsConfig.Instance.DebugModeTheColorsDuke ) {
					return;
				}
			}

			if( OrbsTile.CurrentTargetTileChunk.HasValue ) {
				bool isWithinUseRange = OrbItemBase.IsTileChunkWithinUseRange(
					Main.LocalPlayer.Center,
					OrbsTile.CurrentTargetTileChunk.Value
				);

				if( !isWithinUseRange ) {
					OrbsTile.CurrentTargetTileChunk = null;
				}
			}

			OrbColorCode tileColorCode = OrbsTile.GetTileColorCode( i, j );
			OrbColorCode plrColorCode = myorb?.ColorCode ?? (OrbColorCode)0;
			bool canSeeColor = tileColorCode == plrColorCode
				|| isBinoculars
				|| OrbsConfig.Instance.DebugModeTheColorsDuke;

			if( canSeeColor ) {
				if( tileColorCode == plrColorCode && OrbsTile.CurrentTargetTileChunk == null ) {
					if( OrbItemBase.IsTileWithinUseRange(i, j) ) {
						OrbsTile.CurrentTargetTileChunk = ((i >> 4) << 4, (j >> 4) << 4);
					}
				}

				bool isWithinCurrentChunk = false;
				if( OrbsTile.CurrentTargetTileChunk != null ) {
					isWithinCurrentChunk = i >= OrbsTile.CurrentTargetTileChunk.Value.X
						&& i < OrbsTile.CurrentTargetTileChunk.Value.X + 16
						&& j >= OrbsTile.CurrentTargetTileChunk.Value.Y
						&& j < OrbsTile.CurrentTargetTileChunk.Value.Y + 16;
				}

				this.ApplyTileColor( i, j, tileColorCode, isWithinCurrentChunk, ref drawColor );
			}

			/*int biomeRadiusSqr = OrbsConfig.Instance.OrbPseudoBiomeTileRadius;
			biomeRadiusSqr *= biomeRadiusSqr;

			foreach( (int x, int y) in myplayer.NearbyOrbs ) {
				int diffX = i - x;
				int diffY = j - y;
				int distSqr = (diffX*diffX) + (diffY*diffY);

				if( distSqr < biomeRadiusSqr ) {
					ModTile rawMyTile = ModContent.GetModTile( Main.tile[x, y].type );
					if( rawMyTile == null || !(rawMyTile is OrbTileBase) ) { continue; }

					var mytile = (OrbTileBase)rawMyTile;
					Color color = mytile.PrimaryColor;

					drawColor.R = (byte)((float)drawColor.R * ((float)color.R / 255f));
					drawColor.G = (byte)((float)drawColor.G * ((float)color.G / 255f));
					drawColor.B = (byte)((float)drawColor.B * ((float)color.B / 255f));
				}
			}*/
		}


		////

		public void ApplyTileColor( int i, int j, OrbColorCode tileColorCode, bool isWithinUseRange, ref Color drawColor ) {
			Tile tile = Main.tile[i, j];

			Color color = OrbItemBase.ColorValues[ (OrbColorCode)tileColorCode ];
			if( isWithinUseRange ) {
				float oscillate = ( (float)AnimatedColors.Air.CurrentColor.R / 255f );
				oscillate *= oscillate;

				color *= oscillate;
			}

			float lightness = 0.15f;

			float r = (float)color.R / 255f;
			float g = (float)color.G / 255f;
			float b = (float)color.B / 255f;
			if( !isWithinUseRange ) {
				r += (1f - r) * lightness;
				g += (1f - g) * lightness;
				b += (1f - b) * lightness;
			}

			bool neighborHalfBrick = Main.tile[ (i == 0 ? i : i - 1), j ].halfBrick()
				 || Main.tile[ (i >= Main.maxTilesX - 1 ? i : i + 1), j ].halfBrick();

			if( tile.slope() == 0 && !tile.halfBrick() && !neighborHalfBrick ) {
				drawColor.R = (byte)( (float)drawColor.R * r );
				drawColor.G = (byte)( (float)drawColor.G * g );
				drawColor.B = (byte)( (float)drawColor.B * b );
			} else {
				drawColor.R = (byte)( ( (float)drawColor.R + ( (float)drawColor.R * r ) ) * 0.5f );
				drawColor.G = (byte)( ( (float)drawColor.G + ( (float)drawColor.G * g ) ) * 0.5f );
				drawColor.B = (byte)( ( (float)drawColor.B + ( (float)drawColor.B * b ) ) * 0.5f );
			}

			if( isWithinUseRange ) {
				drawColor.R = Math.Max( (byte)16, drawColor.R );
				drawColor.G = Math.Max( (byte)16, drawColor.G );
				drawColor.B = Math.Max( (byte)16, drawColor.B );
			}
		}
	}
}
