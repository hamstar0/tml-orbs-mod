using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Orbs.Items.Base;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Services.AnimatedColor;


namespace Orbs {
	partial class OrbsTile : GlobalTile {
		private static int _CurrentCoordCode = -1;
		private static OrbColorCode _CurrentCoordColorCode = 0;



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

			if( !tile.active() || !TileGroupIdentityHelpers.VanillaEarthTiles.Contains(type) ) {
				return;
			}

			if( Main.LocalPlayer.HeldItem?.active != true ) {
				return;
			}

			//var myplayer = TmlHelpers.SafelyGetModPlayer<OrbsPlayer>( Main.LocalPlayer );
			var myorb = Main.LocalPlayer.HeldItem.modItem as OrbItemBase;
			if( myorb == null ) {
				if( !OrbsConfig.Instance.DebugModeTheColorsDuke ) {
					return;
				}
			}

			OrbColorCode tileColorCode = OrbsTile.GetTileColorCode( i, j );
			OrbColorCode plrColorCode = myorb?.ColorCode ?? (OrbColorCode)0;

			if( tileColorCode == plrColorCode || OrbsConfig.Instance.DebugModeTheColorsDuke ) {
				this.ApplyTileColor( i, j, tileColorCode, ref drawColor );
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

		public void ApplyTileColor( int i, int j, OrbColorCode tileColorCode, ref Color drawColor ) {
			Tile tile = Main.tile[i, j];
			Color color = OrbItemBase.ColorValues[(OrbColorCode)tileColorCode];
			color *= ( (float)AnimatedColors.Air.CurrentColor.R / 255f );

			float r = (float)color.R / 255f;
			float g = (float)color.G / 255f;
			float b = (float)color.B / 255f;

			bool neighborHalfBrick = Main.tile[( i == 0 ? i : i - 1 ), j].halfBrick()
				|| Main.tile[( i >= Main.maxTilesX - 1 ? i : i + 1 ), j].halfBrick();

			if( tile.slope() == 0 && !tile.halfBrick() && !neighborHalfBrick ) {
				drawColor.R = (byte)( (float)drawColor.R * r );
				drawColor.G = (byte)( (float)drawColor.G * g );
				drawColor.B = (byte)( (float)drawColor.B * b );
			} else {
				drawColor.R = (byte)( ( (float)drawColor.R + ( (float)drawColor.R * r ) ) * 0.5f );
				drawColor.G = (byte)( ( (float)drawColor.G + ( (float)drawColor.G * g ) ) * 0.5f );
				drawColor.B = (byte)( ( (float)drawColor.B + ( (float)drawColor.B * b ) ) * 0.5f );
			}
		}
	}
}
