using System;
using System.Collections.Generic;
using Terraria.ModLoader;


namespace Orbs {
	partial class OrbsWorld : ModWorld {
		//private IDictionary<int, ISet<int>> Orbs = new Dictionary<int, ISet<int>>();



		////////////////

		/*public override void Load( TagCompound tag ) {
			this.Orbs.Clear();

			if( !tag.ContainsKey("count") ) {
				return;
			}

			int count = tag.GetInt( "count" );
			for( int i=0; i<count; i++ ) {
				int x = tag.GetInt( "orb_"+i+"_x" );
				int y = tag.GetInt( "orb_"+i+"_y" );
				int code = tag.GetInt( "orb_"+i+ "_code" );
				int tileType = OrbTileBase.GetTileTypeFromCode( code );

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
					tag[ "orb_"+i+"_code" ] = OrbTileBase.GetCodeFromTileType( Main.tile[tileX, tileY].type );
					i++;
				}
			}

			return tag;
		}*/


		////////////////

		/*public void AddOrb( int tileX, int tileY ) {
			this.Orbs.Set2D( tileX, tileY );
		}*/


		////////////////

		/*public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int shards;
			WorldSize wldSize = WorldLibraries.GetSize();

			switch( wldSize ) {
			default:
			case WorldSize.SubSmall:
				shards = OrbsConfig.Instance.OrbsPerTinyWorld;
				break;
			case WorldSize.Small:
				shards = OrbsConfig.Instance.OrbsPerSmallWorld;
				break;
			case WorldSize.Medium:
				shards = OrbsConfig.Instance.OrbsPerMediumWorld;
				break;
			case WorldSize.Large:
				shards = OrbsConfig.Instance.OrbsPerLargeWorld;
				break;
			case WorldSize.SuperLarge:
				shards = OrbsConfig.Instance.OrbsPerHugeWorld;
				break;
			}

			tasks.Add( new OrbsWorldGen( shards ) );
		}*/


		////////////////

		public IDictionary<int, ISet<int>> GetOrbs() {
			/*foreach( (int tileX, ISet<int> tileYs) in this.Orbs ) {
				bool needsEdit = false;

				foreach( int tileY in tileYs ) {
					if( Main.tile[tileX, tileY]?.active() != true ) {
						needsEdit = true;
						break;
					}
				}

				if( needsEdit ) {
					foreach( int tileY in tileYs.ToArray() ) {
						if( Main.tile[tileX, tileY]?.active() != true ) {
							tileYs.Remove( tileY );
						}
					}
				}
			}

			return this.Orbs;*/
			return new Dictionary<int, ISet<int>>();
		}
	}
}
