using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Orbs.Items;


namespace Orbs {
	/*partial class OrbsTile : GlobalTile {
		public override void DrawEffects(
					int i,
					int j,
					int type,
					SpriteBatch sb,
					ref Color drawColor,
					ref int nextSpecialDrawIndex ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<OrbsPlayer>( Main.LocalPlayer );
			int biomeRadiusSqr = OrbsConfig.Instance.OrbPseudoBiomeTileRadius;
			biomeRadiusSqr *= biomeRadiusSqr;

			foreach( (int x, int y) in myplayer.NearbyOrbs ) {
				int diffX = i - x;
				int diffY = j - y;
				int distSqr = (diffX*diffX) + (diffY*diffY);

				if( distSqr < biomeRadiusSqr ) {
					ModTile rawMyTile = ModContent.GetModTile( Main.tile[x, y].type );
					if( rawMyTile == null || !(rawMyTile is OrbTileBase) ) { continue; }

					var mytile = (OrbTileBase)rawMyTile;
					Color color = mytile.PrimaryColor;

					drawColor.R = (byte)((float)drawColor.R * ((float)color.R / 255f));
					drawColor.G = (byte)((float)drawColor.G * ((float)color.G / 255f));
					drawColor.B = (byte)((float)drawColor.B * ((float)color.B / 255f));
				}
			}
		}
	}*/
}
