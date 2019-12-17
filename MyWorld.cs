using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Orbs.Tiles.Base;


namespace Orbs {
	partial class OrbsWorld : ModWorld {
		internal IDictionary<int, ISet<int>> Orbs = new Dictionary<int, ISet<int>>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.Orbs.Clear();

			if( !tag.ContainsKey("count") ) {
				return;
			}

			int count = tag.GetInt( "count" );
			for( int i=0; i<count; i++ ) {
				int x = tag.GetInt( "orb_"+i+"_x" );
				int y = tag.GetInt( "orb_"+i+"_y" );
				int code = tag.GetInt( "orb_"+i+ "_code" );
				int tileType = OrbTile.GetTileTypeFromCode( code );

				if( Main.netMode != 1 ) {
					if( Main.tile[x, y]?.active() == true && Main.tile[x, y].type == tileType ) {
						this.Orbs.Set2D( x, y );
					}
				}
			}
		}

		public override TagCompound Save() {
			int orbCount = this.Orbs.Count2D();
			var tag = new TagCompound { { "count", orbCount } };

			int i = 0;
			foreach( (int tileX, ISet<int> tileYs) in this.Orbs ) {
				foreach( int tileY in tileYs ) {
					tag[ "orb_"+i+"_x" ] = tileX;
					tag[ "orb_"+i+"_y" ] = tileY;
					tag[ "orb_"+i+"_code" ] = OrbTile.GetCodeFromTileType( Main.tile[tileX, tileY].type );
					i++;
				}
			}

			return tag;
		}


		////////////////

		public void AddOrb( int tileX, int tileY ) {
			this.Orbs.Set2D( tileX, tileY );
		}
	}
}
