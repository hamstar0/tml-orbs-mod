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
	}
}