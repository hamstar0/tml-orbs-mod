using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

		public static IReadOnlyDictionary<OrbColorCode, Color> ColorValues { get; private set; }

		public static IReadOnlyDictionary<OrbColorCode, string> TexturePaths { get; private set; }

		public static IReadOnlyDictionary<int, OrbColorCode> ItemTypeColorCodes { get; private set; }

		public static Texture2D GlowTex { get; private set; }


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

			var textures = new Dictionary<OrbColorCode, string> {
				{ OrbColorCode.Blue, "Items/BlueOrbItem" },
				{ OrbColorCode.Cyan, "Items/CyanOrbItem" },
				{ OrbColorCode.Green, "Items/GreenOrbItem" },
				{ OrbColorCode.Pink, "Items/PinkOrbItem" },
				{ OrbColorCode.Purple, "Items/PurpleOrbItem" },
				{ OrbColorCode.Red, "Items/RedOrbItem" },
				{ OrbColorCode.Brown, "Items/BrownOrbItem" },	//Color.LightSeaGreen
				{ OrbColorCode.White, "Items/WhiteOrbItem" },
				{ OrbColorCode.Yellow, "Items/YellowOrbItem" },
			};
			OrbItemBase.TexturePaths = new ReadOnlyDictionary<OrbColorCode, string>( textures );
		}


		internal static void Initialize() {
			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				OrbItemBase.GlowTex = OrbsMod.Instance.GetTexture( "Items/OrbBase/OrbGlow" );

				OrbsMod.PremultiplyTexture( OrbItemBase.GlowTex );
			}

			//

			var itemColorCodes = new Dictionary<int, OrbColorCode> {
				{ ModContent.ItemType<BlueOrbItem>(), OrbColorCode.Blue },
				{ ModContent.ItemType<CyanOrbItem>(), OrbColorCode.Cyan },
				{ ModContent.ItemType<GreenOrbItem>(), OrbColorCode.Green },
				{ ModContent.ItemType<PinkOrbItem>(), OrbColorCode.Pink },
				{ ModContent.ItemType<PurpleOrbItem>(), OrbColorCode.Purple },
				{ ModContent.ItemType<RedOrbItem>(), OrbColorCode.Red },
				{ ModContent.ItemType<BrownOrbItem>(), OrbColorCode.Brown },	//Color.LightSeaGreen
				{ ModContent.ItemType<WhiteOrbItem>(), OrbColorCode.White },
				{ ModContent.ItemType<YellowOrbItem>(), OrbColorCode.Yellow },
			};
			OrbItemBase.ItemTypeColorCodes = new ReadOnlyDictionary<int, OrbColorCode>( itemColorCodes );
		}

		////

		internal static void Uninstall() {
			OrbItemBase.GlowTex = null;
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
			
			if( bl.Contains( TileID.GetUniqueKey(TileID.Ebonstone) )
					&& bl.Contains( TileID.GetUniqueKey(TileID.Crimstone) )
					&& bl.Contains( TileID.GetUniqueKey(TileID.Pearlstone) ) ) {
				string evilInfo = WorldGen.crimson ? "crimson" : "corruption";
				string goodInfo = Main.hardMode ? " or hallow" : "";
				var tip1 = new TooltipLine(
					this.mod,
					"OrbsSpiritedTilesTip1",
					$"Does not affect {evilInfo}{goodInfo} stone tiles"
				);

				tooltips.Add( tip1 );
			}

			var tip2 = new TooltipLine(
				this.mod,
				"OrbsSpiritedTilesTip2",
				"Use fullscreen map's toggle button to see orb-resonant tiles by color"
			);

			tooltips.Add( tip2 );
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
			var config = OrbsConfig.Instance;

			if( !config.Get<bool>( nameof(config.EnableOrbUseUponTiles) ) ) {
				string disabledMessage = config.Get<string>( nameof(config.DisabledOrbMessage) );

				if( !string.IsNullOrEmpty(disabledMessage) ) {
					Main.NewText( disabledMessage, Color.Yellow );
				}

				//

				return false;
			}

			//

			var myplayer = player.GetModPlayer<OrbsPlayer>();
			if( !myplayer.CanUseOrbsWithoutSettings() ) {
				return false;
			}
			
			//

			(int X, int Y) chunkGridPos = myplayer.CurrentTargettedOrbableChunkGridPosition.Value;
			if( !OrbItemBase.CanActivateOrbForChunk(chunkGridPos.X, chunkGridPos.Y) ) {
				return false;
			}

			//

			OrbItemBase.ActivateOrbUponTileChunk( chunkGridPos.X, chunkGridPos.Y );

			myplayer.ClearCurrentTargetOrbChunk();

			//

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				OrbActivateProtocol.BroadcastFromClientToEveryone( this.ColorCode, chunkGridPos.X, chunkGridPos.Y );
			}

			//

			return true;
		}
	}
}