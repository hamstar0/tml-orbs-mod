using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace Orbs {
	public partial class OrbsMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.gameMenu ) {
				return;
			}

			int layerIdx = layers.FindIndex( layer => layer.Name.Equals("Vanilla: Mouse Over") );
			if( layerIdx == -1 ) {
				return;
			}

			//

			var binocsLayer = new LegacyGameInterfaceLayer(
				"Orbs: Mouse Icon",
				() => {
					if( Main.gameMenu ) {
						return true;
					}

					this.DrawMouseOrb_If( Main.spriteBatch );
					this.DrawCurrentOrbableChunk_If( Main.spriteBatch );

					return true;
				},
				InterfaceScaleType.Game
			);

			//

			layers.Insert( layerIdx + 1, binocsLayer );
		}
	}
}