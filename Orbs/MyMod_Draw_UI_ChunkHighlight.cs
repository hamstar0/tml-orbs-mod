using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	public partial class OrbsMod : Mod {
		private void DrawCurrentOrbableChunk_If( SpriteBatch sb ) {
			if( Main.LocalPlayer.dead ) {
				return;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<OrbsPlayer>();

			if( !myplayer.CurrentTargettedOrbableChunkGridPosition.HasValue ) {
				return;
			}

			//

			int chunkTileSize = OrbItemBase.ChunkTileSize;

			(int x, int y) chunkTile = myplayer.CurrentTargettedOrbableChunkGridPosition.Value;
			Vector2 chunkWldPos = new Vector2(chunkTile.x * chunkTileSize, chunkTile.y * chunkTileSize );
			Vector2 chunkScrPos = (chunkWldPos * 16f) - Main.screenPosition;
//DebugLibraries.Print( "orbrect", $"chunkTile: {chunkTile}, chunkScrPos: {chunkScrPos}" );

			//

			var topRect = new Rectangle(
				x: (int)chunkScrPos.X,
				y: (int)chunkScrPos.Y,
				width: chunkTileSize * 16,
				height: 2
			);

			var botRect = new Rectangle(
				x: (int)chunkScrPos.X,
				y: (int)chunkScrPos.Y + (chunkTileSize * 16),
				width: chunkTileSize * 16,
				height: 2
			);

			var leftRect = new Rectangle(
				x: (int)chunkScrPos.X,
				y: (int)chunkScrPos.Y,
				width: 2,
				height: chunkTileSize * 16
			);

			var rightRect = new Rectangle(
				x: (int)chunkScrPos.X + (chunkTileSize * 16),
				y: (int)chunkScrPos.Y,
				width: 2,
				height: chunkTileSize * 16
			);

			//

			float pulse = (float)Main.mouseTextColor / 255f;
			Color bgColor = Color.Black * 0.5f;
			Color fgColor = Color.White * pulse * pulse * pulse * pulse * 0.5f;

			//

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: topRect,
				color: bgColor
			);
			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: botRect,
				color: bgColor
			);
			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: leftRect,
				color: bgColor
			);
			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: rightRect,
				color: bgColor
			);

			//

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: topRect,
				color: fgColor
			);
			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: botRect,
				color: fgColor
			);
			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: leftRect,
				color: fgColor
			);
			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: rightRect,
				color: fgColor
			);
		}
	}
}