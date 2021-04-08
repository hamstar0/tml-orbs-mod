using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Protocols;


namespace Orbs.Items.Base {
	public enum OrbColorCode : int {
		Blue=1,
		Cyan=2,
		Green=3,
		Pink=4,
		Purple=5,
		Red=6,
		Brown=7,
		White=8,
		Yellow=9
	}




	public abstract partial class OrbItemBase : ModItem {
		public const int ChunkTileSize = 16;

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
				{ OrbColorCode.Brown, Color.Peru },	//Color.LightSeaGreen
				{ OrbColorCode.White, Color.White },
				{ OrbColorCode.Yellow, Color.Yellow },
			};
			OrbItemBase.ColorValues = new ReadOnlyDictionary<OrbColorCode, Color>( colors );
		}



		////////////////

		public static (int ChunkGridX, int ChunkGridY) GetChunk( int tileX, int tileY ) =>
			(tileX / OrbItemBase.ChunkTileSize, tileY / OrbItemBase.ChunkTileSize);

		public static (int tileX, int tileY) GetTopLeftTile( int chunkGridX, int chunkGridY ) =>
			(chunkGridX * OrbItemBase.ChunkTileSize, chunkGridY * OrbItemBase.ChunkTileSize);



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

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			var config = OrbsConfig.Instance;
			var bl = config.Get<HashSet<string>>( nameof(config.OrbAffectedTilesBlacklist) );
			
			if( !bl.Contains( TileID.GetUniqueKey(TileID.Ebonstone) )
					&& !bl.Contains( TileID.GetUniqueKey(TileID.Crimstone) )
					&& !bl.Contains( TileID.GetUniqueKey(TileID.Pearlstone) ) ) {
				string evilInfo = WorldGen.crimson ? "crimson" : "corruption";
				string goodInfo = Main.hardMode ? " or hallow" : "";
				var tip = new TooltipLine(
					this.mod,
					"OrbsSpiritedTilesTip",
					"Does not affect "+evilInfo+goodInfo+" stone tiles"
				);
				tooltips.Add( tip );
			}
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
			var myplayer = player.GetModPlayer<OrbsPlayer>();
			if( !myplayer.CurrentTargettedOrbableChunkGridPos.HasValue ) {
				return false;
			}

			var config = OrbsConfig.Instance;

			if( !config.Get<bool>( nameof(config.EnableOrbUseUponTiles) ) ) {
				string disabledMessage = config.Get<string>( nameof(config.DisabledOrbMessage) );

				if( !string.IsNullOrEmpty(disabledMessage) ) {
					Main.NewText( disabledMessage, Color.Yellow );
				}

				return false;
			}

			(int X, int Y) chunkGridPos = myplayer.CurrentTargettedOrbableChunkGridPos.Value;
			if( !OrbItemBase.CanActivateOrbForChunk(chunkGridPos.X, chunkGridPos.Y) ) {
				return false;
			}

			if( Main.netMode == 1 ) {
				OrbActivateProtocol.Broadcast( this.ColorCode, chunkGridPos.X, chunkGridPos.Y );
			}
			OrbItemBase.ActivateOrbUponTileChunk( chunkGridPos.X, chunkGridPos.Y );
			myplayer.ClearTargetOrbChunk();

			return true;
		}
	}
}