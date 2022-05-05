using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Orbs.Items.Base;


namespace Orbs {
	public partial class OrbsMod : Mod {
		private void DrawMouseOrb_If( SpriteBatch sb ) {
			if( Main.LocalPlayer.dead ) {
				return;
			}

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
			Vector2 pos = Main.MouseScreen + new Vector2( -20f, -8f );
			float pulse = (float)Main.mouseTextColor / 255f;
			Color color = Color.White * pulse * pulse;

			//

			sb.Draw( orbTex, pos, color );

			//

			string label = Enum.GetName( typeof(OrbColorCode), code );
			Vector2 textPos = pos + new Vector2( 24f, -12f );
			Color textFgColor = OrbItemBase.ColorValues[code] * pulse;
			Color textBgColor = Color.Black * pulse;

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: label,
				x: textPos.X,
				y: textPos.Y,
				textColor: textFgColor,
				borderColor: textBgColor,
				origin: default
			);
		}
	}
}