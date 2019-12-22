using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;


namespace Orbs.Items.Base {
	public enum OrbColorCode : int {
		Blue=1,
		Cyan=2,
		Green=3,
		Pink=4,
		Purple=5,
		Red=6,
		Teal=7,
		White=8,
		Yellow=9
	}




	public abstract class OrbItemBase : ModItem {
		public const int MaxTileChunkUseRange = 16;

		////

		public static IReadOnlyDictionary<OrbColorCode, Color> ColorValues;


		////////////////

		static OrbItemBase() {
			var colors = new Dictionary<OrbColorCode, Color> {
				{ OrbColorCode.Blue, Color.Blue },
				{ OrbColorCode.Cyan, Color.Cyan },
				{ OrbColorCode.Green, Color.Lime },
				{ OrbColorCode.Pink, Color.HotPink },
				{ OrbColorCode.Purple, Color.DarkMagenta },
				{ OrbColorCode.Red, Color.Red },
				{ OrbColorCode.Teal, Color.LightSeaGreen },
				{ OrbColorCode.White, Color.White },
				{ OrbColorCode.Yellow, Color.Yellow },
			};
			OrbItemBase.ColorValues = new ReadOnlyDictionary<OrbColorCode, Color>( colors );
		}


		////////////////

		public static bool IsTileWithinUseRange( int i, int j ) {
			Player plr = Main.LocalPlayer;

			int diffX = (int)( plr.Center.X * 0.0625 ) - i;
			int diffY = (int)( plr.Center.Y * 0.0625 ) - j;
			int distSqr = ( diffX * diffX ) + ( diffY * diffY );
			return distSqr <= 256;
		}


		public static bool IsTileChunkWithinUseRange( Vector2 worldPos, (int i, int j) tileChunkPos ) {
			int maxRange = OrbItemBase.MaxTileChunkUseRange;
			var rect = new Rectangle(
				(tileChunkPos.i - maxRange) << 4,
				(tileChunkPos.j - maxRange) << 4,
				(maxRange << 4) * 3,
				(maxRange << 4) * 3
			);

			return rect.Contains( (int)worldPos.X, (int)worldPos.Y );
		}


		public static OrbColorCode GetRandomColorCode( int randSeed ) {
			var rand = new Random( randSeed );
			int maxColors = Enum.GetValues( typeof(OrbColorCode) ).Length;

			int tileColorCode = rand.Next( maxColors + 1 );

			if( tileColorCode >= maxColors ) {
				tileColorCode = (int)OrbColorCode.White;
			} else {
				tileColorCode += 1;
			}

			return (OrbColorCode)tileColorCode;
		}



		////////////////

		public abstract OrbColorCode ColorCode { get; }



		////////////////

		public override void SetDefaults() {
			this.item.maxStack = 99;
			this.item.width = 16;
			this.item.height = 16;
			this.item.consumable = true;
			this.item.useStyle = 4;
			this.item.useTime = 30;
			this.item.useAnimation = 30;
			//this.item.UseSound = SoundID.Item108;
			this.item.value = Item.buyPrice( 0, 5, 0, 0 );
			this.item.rare = 3;
		}


		////////////////

		public override bool UseItem( Player player ) {
			if( player.itemAnimation > 0 && player.itemTime == 0 ) {
				player.itemTime = item.useTime;
				return true;
			}
			return base.UseItem( player );
		}
	}
}