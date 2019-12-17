using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orbs.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Orbs.Tiles.Base {
	class OrbPlayer : ModPlayer {
		public override void PreUpdate() {
			if( Main.mouseRight && Main.mouseRightRelease ) {
				OrbTile.CreateTile(
					(int)((Main.screenPosition.X + Main.mouseX) / 16),
					(int)((Main.screenPosition.Y + Main.mouseY) / 16)
				);
			}
		}
	}





	abstract class OrbTile : ModTile {
		public static void CreateTile( int x, int y ) {
			ushort orbTileType = (ushort)ModContent.TileType<BlueOrbTile>();

			Main.tile[x - 1, y - 1].active( true );
			Main.tile[x - 1, y - 1].type = orbTileType;
			Main.tile[x - 1, y - 1].frameX = 0;
			Main.tile[x - 1, y - 1].frameY = 0;
			Main.tile[x, y - 1].active( true );
			Main.tile[x, y - 1].type = orbTileType;
			Main.tile[x, y - 1].frameX = (short)( 18 );
			Main.tile[x, y - 1].frameY = 0;
			Main.tile[x - 1, y].active( true );
			Main.tile[x - 1, y].type = orbTileType;
			Main.tile[x - 1, y].frameX = 0;
			Main.tile[x - 1, y].frameY = 18;
			Main.tile[x, y].active( true );
			Main.tile[x, y].type = orbTileType;
			Main.tile[x, y].frameX = (short)( 18 );
			Main.tile[x, y].frameY = 18;
		}



		////////////////

		public abstract string MyName { get; }
		public abstract Color PrimaryColor { get; }



		////////////////

		public override void SetDefaults() {
			Main.tileFrameImportant[this.Type] = true;
			Main.tileLavaDeath[this.Type] = false;
			TileObjectData.newTile.CopyFrom( TileObjectData.Style2x2 );
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = default( AnchorData );
			TileObjectData.addTile( this.Type );
			this.dustType = 7;
			this.disableSmartCursor = false;

			ModTranslation name = this.CreateMapEntryName();
			name.SetDefault( this.MyName );

			this.AddMapEntry( this.PrimaryColor, name );
		}


		////////////////

		public override void ModifyLight( int i, int j, ref float r, ref float g, ref float b ) {
			float flicker = (float)Main.rand.Next( -5, 6 ) * 0.0025f;
			r = ((float)this.PrimaryColor.R / 255f) + flicker * 2f;
			g = ((float)this.PrimaryColor.G / 255f) + flicker;
			b = ((float)this.PrimaryColor.B / 255f);
		}

		public override void DrawEffects( int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex ) {
			var pos = new Vector2( j, i ) * 16;

			Dust.NewDust( pos, 16, 16, 14, 0f, 0f, 100, default(Color), 1f );
		}


		////////////////

		public override void NearbyEffects( int i, int j, bool closer ) {
			// if close to player, spawn guardian + passive effects
		}
	}
}
