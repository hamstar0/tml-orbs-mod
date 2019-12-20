using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Orbs.Tiles.Base;


namespace Orbs {
	partial class OrbsNPC : GlobalNPC {
		public void SetPseudoBiomeEffects( NPC npc ) {
			if( npc.friendly ) {
				return;
			}

			var myworld = ModContent.GetInstance<OrbsWorld>();
			if( myworld == null ) { return; }

			int npcTileX = (int)( npc.position.X / 16f );
			int npcTileY = (int)( npc.position.Y / 16f );
			int biomeRadiusSqr = OrbsConfig.Instance.OrbPseudoBiomeTileRadius;
			biomeRadiusSqr *= biomeRadiusSqr;

			foreach( (int tileX, ISet<int> tileYs) in myworld.GetOrbs() ) {
				foreach( int tileY in tileYs ) {
					int diffX = npcTileX - tileX;
					int diffY = npcTileY - tileY;
					int distSqr = (diffX*diffX) + (diffY*diffY);

					if( distSqr < biomeRadiusSqr ) {
						this.ApplyOrbEffects( tileX, tileY, npc );
					}
				}
			}
		}



		////////////////

		public void ApplyTint( NPC npc, ref Color drawColor ) {
			Color color = this.Tint.Value;

			drawColor.R = (byte)( (float)drawColor.R * ( (float)color.R / 255f ) );
			drawColor.G = (byte)( (float)drawColor.G * ( (float)color.G / 255f ) );
			drawColor.B = (byte)( (float)drawColor.B * ( (float)color.B / 255f ) );
		}

		////////////////

		public void ApplyOrbEffects( int tileX, int tileY, NPC npc ) {
			Tile tile = Main.tile[ tileX, tileY ];
			if( tile?.active() != true ) { return; }

			ModTile mytile = ModContent.GetModTile( tile.type );
			if( mytile != null && mytile is OrbTileBase ) {
				((OrbTileBase)mytile).ApplyPseudoBiomeToNPC( npc );
			}
		}
	}
}
