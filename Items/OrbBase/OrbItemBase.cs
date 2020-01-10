using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Orbs.Protocols;
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




	public abstract partial class OrbItemBase : ModItem {
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


		////////////////

		public override bool ConsumeItem( Player player ) {
			if( !OrbsTile.CurrentTargetTileChunk.HasValue ) {
				return false;
			}

			if( OrbItemBase.CanActivateOrb() ) {
				if( Main.netMode == 0 ) {
					OrbItemBase.ActivateOrb( OrbsTile.CurrentTargetTileChunk.Value.X, OrbsTile.CurrentTargetTileChunk.Value.Y );
					OrbsTile.CurrentTargetTileChunk = null;
				} else if( Main.netMode == 1 ) {
					OrbActivateProtocol.Broadcast(
						this.ColorCode,
						OrbsTile.CurrentTargetTileChunk.Value.X,
						OrbsTile.CurrentTargetTileChunk.Value.Y
					);

					OrbItemBase.ActivateOrb( OrbsTile.CurrentTargetTileChunk.Value.X, OrbsTile.CurrentTargetTileChunk.Value.Y );
				}
				return true;
			}

			return false;
		}
	}
}