using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Tiles;
using ModLibsGeneral.Services.AnimatedColor;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsTile : GlobalTile {
		public static void ApplyOrbChunkTintColorForTile(
					int tileX,
					int tileY,
					OrbColorCode tileColorCode,
					bool isWithinUseRange,
					ref Color drawColor ) {
			Tile tile = Main.tile[ tileX, tileY ];
			Color targetColor = OrbItemBase.ColorValues[ (OrbColorCode)tileColorCode ];

			if( isWithinUseRange ) {
				float oscillate = (float)AnimatedColors.Air.CurrentColor.R / 255f;
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

			bool neighborHalfBrick = Main.tile[ (tileX == 0 ? tileX : tileX - 1), tileY ].halfBrick()
				 || Main.tile[ (tileX >= Main.maxTilesX - 1 ? tileX : tileX + 1), tileY ].halfBrick();

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
		}


		////////////////

		public static bool IsTileOrbable( int tileX, int tileY ) {
			Tile tile = Main.tile[ tileX, tileY ];
			return tile?.active() == true
				&& !tile.inActive()
				&& OrbsTile.IsTileTypeOrbable( tile.type );
		}

		public static bool IsTileTypeOrbable( int tileType ) {
			var config = OrbsConfig.Instance;

			if( config.Get<bool>( nameof(config.OrbAffectsOnlyVanillaEarthTiles) ) ) {
				if( !TileCommonGroupsLibraries.VanillaEarthTiles.Contains(tileType) ) {
					return false;
				}
			}

			string uid = TileID.GetUniqueKey( tileType );
			var orbTileBl = config.Get<HashSet<string>>( nameof(config.OrbAffectedTilesBlacklist) );

			return !orbTileBl.Contains( uid );
		}



		////////////////

		public override void DrawEffects(
					int tileX,
					int tileY,
					int tileType,
					SpriteBatch sb,
					ref Color drawColor,
					ref int nextSpecialDrawIndex ) {
			if( !OrbsTile.IsTileOrbable(tileX, tileY) ) {
				return;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<OrbsPlayer>();
			(int ChunkGridX, int ChunkGridY)? targettedChunkGridPos = myplayer.CurrentTargettedOrbableChunkGridPos;

			bool isTarget = targettedChunkGridPos.HasValue
				&& targettedChunkGridPos.Value == OrbItemBase.GetChunk( tileX, tileY );
			
			if( !isTarget && !OrbsPlayer.CanViewAllOrbChunks(myplayer.player) ) {
				return;
			}

			var orbWld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode tileColorCode = orbWld.GetColorCodeOfOrbChunkOfTile( tileX, tileY );
			if( tileColorCode == 0 ) {
				return;
			}
			
			OrbsTile.ApplyOrbChunkTintColorForTile(
				tileX: tileX,
				tileY: tileY,
				tileColorCode: tileColorCode,
				isWithinUseRange: isTarget,
				drawColor: ref drawColor
			);
		}
	}
}
