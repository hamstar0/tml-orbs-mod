using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	public partial class OrbsMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.gameMenu ) {
				return;
			}

			int layerIdx = layers.FindIndex( layer => layer.Name.Equals("Vanilla: Mouse Over") );
			if( layerIdx == -1 ) {
				return;
			}

			//

			var binocsLayer = new LegacyGameInterfaceLayer(
				"Orbs: Mouse Icon",
				() => {
					if( Main.gameMenu ) {
						return true;
					}

					this.DrawMouseOrb_If( Main.spriteBatch );
					this.DrawCurrentOrbableChunk_If( Main.spriteBatch );

					return true;
				},
				InterfaceScaleType.Game
			);

			//

			layers.Insert( layerIdx + 1, binocsLayer );
		}


		////////////////

		private void DrawMouseOrb_If( SpriteBatch sb ) {
			Item heldItem = Main.LocalPlayer.HeldItem;
			if( heldItem?.active != true || heldItem.type != ItemID.Binoculars ) {
				return;
			}

			//

			int mouseTileX = (int)Main.MouseWorld.X / 16;
			int mouseTileY = (int)Main.MouseWorld.Y / 16;

			var myworld = ModContent.GetInstance<OrbsWorld>();
			OrbColorCode code = myworld.GetColorCodeOfOrbChunkOfTile( mouseTileX, mouseTileY );

			Texture2D orbTex = this.GetTexture( OrbItemBase.TexturePaths[code] );
			Vector2 pos = Main.MouseScreen + new Vector2( -16f, -4f );
			float pulse = (float)Main.mouseTextColor / 255f;
			Color color = Color.White * pulse * pulse;

			//

			sb.Draw( orbTex, pos, color );
		}


		////////////////

		private void DrawCurrentOrbableChunk_If( SpriteBatch sb ) {
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