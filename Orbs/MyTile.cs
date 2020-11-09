using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Services.AnimatedColor;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsTile : GlobalTile {
		public static Color GetOrbChunkTintColorForTile( int i, int j, OrbColorCode tileColorCode, bool isWithinUseRange ) {
			Color drawColor = default( Color );
			Tile tile = Main.tile[i, j];

			Color color = OrbItemBase.ColorValues[(OrbColorCode)tileColorCode];
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

			return drawColor;
		}


		////////////////

		public static bool IsTileTypeOrbable( int tileType ) {
			return tileType == TileID.ObsidianBrick
				|| tileType == TileID.HellstoneBrick
				|| TileGroupIdentityHelpers.VanillaEarthTiles.Contains(tileType);
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
			if( tile?.active() != true || tile.inActive() || !OrbsTile.IsTileTypeOrbable(type) ) {
				return;
			}

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode tileColorCode = orbWld.GetColorCodeOfOrbChunkOfTile( i, j );
			if( tileColorCode == 0 ) {
				return;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<OrbsPlayer>();
			(int x, int y)? targettedChunk = myplayer.GetOrbChunkIfTargetted( i, j );

			if( targettedChunk.HasValue || myplayer.CanViewAllOrbChunks() ) {
				drawColor = OrbsTile.GetOrbChunkTintColorForTile( i, j, tileColorCode, targettedChunk.HasValue );
			}
		}
	}
}
