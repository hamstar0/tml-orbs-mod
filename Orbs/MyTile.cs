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
		public static Color GetOrbChunkTintColorForTile(
					int i,
					int j,
					Color drawColor,
					OrbColorCode tileColorCode,
					bool isWithinUseRange ) {
			Tile tile = Main.tile[i, j];

			Color targetColor = OrbItemBase.ColorValues[ (OrbColorCode)tileColorCode ];

			if( isWithinUseRange ) {
				float oscillate = ( (float)AnimatedColors.Air.CurrentColor.R / 255f );
				oscillate *= oscillate;

				targetColor *= oscillate;
			}

			float rTargScale = (float)targetColor.R / 255f;
			float gTargScale = (float)targetColor.G / 255f;
			float bTargScale = (float)targetColor.B / 255f;

			if( !isWithinUseRange ) {
				float lightness = 0.15f;

				rTargScale += (1f - rTargScale) * lightness;
				gTargScale += (1f - gTargScale) * lightness;
				bTargScale += (1f - bTargScale) * lightness;
			}

			bool neighborHalfBrick = Main.tile[ (i == 0 ? i : i - 1), j ].halfBrick()
				 || Main.tile[ (i >= Main.maxTilesX - 1 ? i : i + 1), j ].halfBrick();

			float newR = (float)drawColor.R * rTargScale;
			float newG = (float)drawColor.G * gTargScale;
			float newB = (float)drawColor.B * bTargScale;

			if( tile.slope() == 0 && !tile.halfBrick() && !neighborHalfBrick ) {
				drawColor.R = (byte)newR;
				drawColor.G = (byte)newG;
				drawColor.B = (byte)newB;
			} else {
				drawColor.R = (byte)( ((float)drawColor.R + newR) * 0.5f );
				drawColor.G = (byte)( ((float)drawColor.G + newG) * 0.5f );
				drawColor.B = (byte)( ((float)drawColor.B + newB) * 0.5f );
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
			(int x, int y)? targettedChunkGridPos = myplayer.GetOrbChunkIfTargetted( i, j );

			if( targettedChunkGridPos.HasValue || myplayer.CanViewAllOrbChunks() ) {
				drawColor = OrbsTile.GetOrbChunkTintColorForTile( i, j, drawColor, tileColorCode, targettedChunkGridPos.HasValue );
			}
		}
	}
}
