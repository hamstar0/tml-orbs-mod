using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.World;
using Orbs.Items.Base;


namespace Orbs {
	partial class OrbsWorld : ModWorld {
		public static int GetTileChunkCode( int tileX, int tileY ) {
			return (tileX >> 4) + ((tileY * Main.maxTilesX) >> 4);
		}

		public static int GetWorldCode() {
			return WorldHelpers.GetUniqueIdForCurrentWorld( true ).GetHashCode();
		}



		////////////////

		private IDictionary<int, OrbColorCode> TileChunkColors = null;



		////////////////
		
		private void InitTileChunkColors( UnifiedRandom rand ) {
			ushort maxX = (ushort)( Main.maxTilesX >> 4 );
			ushort maxY = (ushort)( Main.maxTilesY >> 4 );

			this.TileChunkColors = new ConcurrentDictionary<int, OrbColorCode>();

			for( ushort i = 0; i < maxX; i += 16 ) {
				for( ushort j = 0; j < maxY; j += 16 ) {
					OrbColorCode color = OrbItemBase.GetNextRandomColorCode( rand );
					int code = OrbsWorld.GetTileChunkCode( i, j );

					this.TileChunkColors[ code ] = color;
				}
			}
		}


		////////////////

		public OrbColorCode GetTileColorCode( int tileX, int tileY ) {
			if( this.TileChunkColors == null ) {
				int worldCode = OrbsWorld.GetWorldCode();
				var rand = new UnifiedRandom( worldCode );

				this.InitTileChunkColors( rand );
			}

			int code = OrbsWorld.GetTileChunkCode( tileX, tileY );
			return this.TileChunkColors.GetOrDefault( code );
		}
	}
}
