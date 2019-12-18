using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Orbs.Tiles.Base {
	abstract class OrbTileBase : ModTile {
		public static void CreateTile( int x, int y ) {
			ushort orbTileType = (ushort)ModContent.TileType<TealOrbTile>();

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

		public static int GetCodeFromTileType( int tileType ) {
			if( tileType == ModContent.TileType<BlueOrbTile>() ) {
				return 1;
			}
			if( tileType == ModContent.TileType<PinkOrbTile>() ) {
				return 2;
			}
			if( tileType == ModContent.TileType<TealOrbTile>() ) {
				return 3;
			}
			if( tileType == ModContent.TileType<YellowOrbTile>() ) {
				return 4;
			}
			throw new ArgumentException();
		}

		public static int GetTileTypeFromCode( int code ) {
			switch( code ) {
			case 1:
				return ModContent.TileType<BlueOrbTile>();
			case 2:
				return ModContent.TileType<PinkOrbTile>();
			case 3:
				return ModContent.TileType<TealOrbTile>();
			case 4:
				return ModContent.TileType<YellowOrbTile>();
			}
			throw new ArgumentException();
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


		////////////////

		public abstract void ApplyPseudoBiomeToNPC( NPC npc );
	}
}
