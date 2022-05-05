using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace Orbs.Items.Base {
	public abstract partial class OrbItemBase : ModItem {
		public override bool PreDrawInInventory(
					SpriteBatch sb,
					Vector2 position,
					Rectangle frame,
					Color drawColor,
					Color itemColor,
					Vector2 origin,
					float scale ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<OrbsPlayer>();
			bool isResonating = myplayer.CurrentNearbyChunkTypes.Contains( this.ColorCode );

			//

			this.DrawOrbGlow( sb, position, scale, isResonating );

			//

			return base.PreDrawInInventory( sb, position, frame, drawColor, itemColor, origin, scale );
		}


		////

		public void DrawOrbGlow(
					SpriteBatch sb,
					Vector2 position,
					float scale,
					bool isResonating ) {
			//Main.inventoryBackTexture
			Texture2D orbTex = ModContent.GetTexture( this.Texture );
			Texture2D glowTex = OrbItemBase.GlowTex;

			Vector2 orbTexOrigin = new Vector2(orbTex.Width, orbTex.Height) * 0.5f;
			Vector2 glowTexOrigin = new Vector2(glowTex.Width, glowTex.Height) * 0.5f;
			Vector2 posOffset = orbTexOrigin * scale;

			Color color = OrbItemBase.ColorValues[this.ColorCode];
			color *= (Main.rand.NextFloat() + 1f) / 2f;

			if( !isResonating ) {
				color *= 0.25f;
			}

			//

			sb.Draw(
				texture: glowTex,
				position: position + posOffset,
				sourceRectangle: null,
				color: color,
				rotation: 0f,
				origin: glowTexOrigin,
				scale: scale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
		}
	}
}