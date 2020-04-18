using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Reflection;


namespace Orbs.UI {
	class UIMapButton : UIPanel {
		private Color BaseBgColor;



		////////////////

		public UIMapButton() : base() {
			this.BaseBgColor = this.BackgroundColor;
		}


		////

		public CalculatedStyle GetMyDimensions() {
			return new CalculatedStyle(
				16f,//this.Left.Pixels,
				16f,//this.Top.Pixels,
				168f,//this.Width.Pixels,
				32f//this.Height.Pixels
			);
		}


		////

		public override void Draw( SpriteBatch sb ) {
			var mydim = this.GetMyDimensions();

			ReflectionHelpers.Set( typeof( UIElement ), this, "_dimensions", mydim );
			this.DrawSelf( sb );

			var textPos = new Vector2( mydim.X + 12, mydim.Y + 4 );
			sb.DrawString( Main.fontMouseText, "Toggle Orb Colors", textPos, Color.White );

			bool isHovering = false;

			if( Main.mouseX >= mydim.X && Main.mouseX < ( mydim.X + mydim.Width ) ) {
				if( Main.mouseY >= mydim.Y && Main.mouseY < ( mydim.Y + mydim.Height ) ) {
					isHovering = true;

					if( Main.mouseLeft && Main.mouseLeftRelease ) {
						this.Click( null );
					}
				}
			}

			if( isHovering ) {
				this.BackgroundColor = this.BaseBgColor * 1.2f;
			} else {
				this.BackgroundColor = this.BaseBgColor;
			}
		}
	}
}