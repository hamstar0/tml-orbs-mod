using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.Items.Attributes;


namespace Orbs {
	class OrbsItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			string modName = "[c/FFFF88"+OrbsMod.Instance.DisplayName+":] ";
			TooltipLine tip;

			switch( item.type ) {
			case ItemID.Binoculars:
				tip = new TooltipLine( this.mod, "OrbsBinoculars", modName+"Reveals patches of orb-resonant terrain (by color)" );
				ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				break;
			default:
				if( item.pick > 0 ) {
					tip = new TooltipLine( this.mod, "OrbsPick", modName+"Able to break ores, plants, gems, sand, snow, silt, obsidian, and wood" );
					ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
				}
				break;
			}
		}


		////////////////

		public override bool Shoot(
					Item item,
					Player player,
					ref Vector2 position,
					ref float speedX,
					ref float speedY,
					ref int type,
					ref int damage,
					ref float knockBack ) {
			if( item.type == ItemID.PurificationPowder && !Main.hardMode ) {
				var mousePos = new Vector2( Main.mouseX, Main.mouseY );
				mousePos.X += Main.screenPosition.X;
				mousePos.Y += Main.screenPosition.Y;
				var offset = mousePos - player.Center;
				offset.Normalize();
				offset *= 3 * 16;

				var tileArea = new Rectangle(
					((int)(player.Center.X + offset.X) >> 4) - 6,
					((int)(player.Center.Y + offset.Y) >> 4) - 6,
					12,
					12
				);

				this.PurificationPowderToSand( item.whoAmI + (int)Main.time + player.whoAmI, tileArea );
			}

			return base.Shoot( item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack );
		}


		////

		private void PurificationPowderToSand( int timerCode, Rectangle tileArea ) {
/*int count = 80;
Timers.SetTimer( "rect", 10, false, () => {
	Dust.QuickDust( new Point(tileArea.X, tileArea.Y), Color.Lime );
	Dust.QuickDust( new Point(tileArea.X, tileArea.Y+tileArea.Height), Color.Lime );
	Dust.QuickDust( new Point(tileArea.X+tileArea.Width, tileArea.Y), Color.Lime );
	Dust.QuickDust( new Point(tileArea.X+tileArea.Width, tileArea.Y+tileArea.Height), Color.Lime );
	return count-- > 0;
} );*/
			var evilTiles = new List<(int, int)>();

			int maxX = tileArea.X + tileArea.Width;
			int maxY = tileArea.Y + tileArea.Height;
			for( int i = tileArea.X; i < maxX; i++ ) {
				for( int j = tileArea.Y; j < maxY; j++ ) {
					Tile tile = Main.tile[i, j];
					if( tile?.active() != true || (tile.type != TileID.Ebonstone && tile.type != TileID.Crimstone) ) {
						continue;
					}
					evilTiles.Add( (i, j) );
				}
			}

			Timers.SetTimer( "OrbsPurificationPowder_"+(timerCode + tileArea.GetHashCode()),
				60 * 3,
				false,
				() => {
					foreach( (int x, int y) in evilTiles ) {
						Tile tile = Main.tile[x, y];
						if( tile?.active() != true || tile.type != TileID.Stone ) {
							continue;
						}

						tile.type = TileID.Sand;
						WorldGen.SquareTileFrame( x, y );
					}
					return false;
				}
			);
		}
	}
}
