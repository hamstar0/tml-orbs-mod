/*using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace Orbs {
	public partial class OrbsMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int npcChatIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: NPC / Sign Dialog" ) );
			if(npcChatIdx == -1 ) {
				return;
			}

			//int topIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Over" ) );
			//if(topIdx == -1 ) {
			//	return;
			//}

			//

			var orbCycle = new LegacyGameInterfaceLayer(
				"Orbs: Draw Cycler",
				() => {
					IList<int> invOrbIdxs = OrbsPlayer.GetInventoryOrbIndexes( Main.LocalPlayer );
					Vector2 pos = Main.MouseScreen;

					this.DrawInventoryOrbList( pos, invOrbIdxs, Main.LocalPlayer.selectedItem );

					return true;
				},
				InterfaceScaleType.UI
			);

			//

			layers.Add(orbCycle );    //barsIdx + 1
		}


		////////////////

		private void DrawInventoryOrbList( Vector2 screenPos, IList<int> inventoryOrbIndexes, int currentItemIdx ) {
			if( inventoryOrbIndexes.Count <= 1 ) {
				return;
			}

			int heddItemdIdx = inventoryOrbIndexes.IndexOf( currentItemIdx );
			int currItemOffset = inventoryOrbIndexes
				.TakeWhile( i => i != currentItemIdx )
				.Count();

			for( int i=Math.Max(currItemOffset - 1, 0); i<inventoryOrbIndexes.Count; i++ ) {
				int invIdx = inventoryOrbIndexes[i];
				Item item = Main.LocalPlayer.inventory[invIdx];

				if( i < currItemOffset ) {
					this.DrawInventoryOrbListItem( screenPos, item, -1 );
				} else if( i == currItemOffset ) {
					this.DrawInventoryOrbListItem( screenPos, item, 0 );
				} else {
					this.DrawInventoryOrbListItem( screenPos, item, i - currItemOffset );
				}
			}
		}


		private void DrawInventoryOrbListItem( Vector2 baseScreenPos, Item orbItem, int rowPos ) {
			float illum = Math.Abs( rowPos ) / 3f;
			illum = 1f - illum;
			if( illum <= 0f ) {
				return;
			}

			Texture2D tex = orbItem.type < Main.itemTexture.Length
				? Main.itemTexture[orbItem.type]
				: ModContent.GetTexture( orbItem.modItem.Texture );

			Vector2 pos = baseScreenPos;
			pos.X += (float)rowPos * 32f;
			pos.Y += 32f;

			Main.spriteBatch.Draw(
				texture: tex,
				position: pos,
				sourceRectangle: null,
				color: Color.White * illum,
				rotation: 0f,
				origin: tex.Bounds.Center.ToVector2(),
				scale: rowPos == 0 ? 1f : 0.75f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
		}
	}
}*/
