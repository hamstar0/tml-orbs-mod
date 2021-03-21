using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.World.Generation;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Hooks.LoadHooks;
using Orbs.Items.Base;


namespace Orbs {
	class OrbsCustomWorld : ILoadable {
		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() {
			LoadHooks.AddPostWorldUnloadEachHook( () => {
				var myworld = ModContent.GetInstance<OrbsWorld>();
				myworld.ResetChunks();
			} );
		}
	}




	partial class OrbsWorld : ModWorld {
		public static int GetOrbChunkCodeOfTile( int tileX, int tileY ) {
			int chunkTileSize = OrbItemBase.ChunkTileSize;
			int chunkColumn = tileX / chunkTileSize;
			int chunkRow = tileY / chunkTileSize;
			int maxChunkColumns = Main.maxTilesX / chunkTileSize;

			return chunkColumn + (chunkRow * maxChunkColumns);
		}

		public static int GetWorldCode() {
			return WorldHelpers.GetUniqueIdForCurrentWorld( true ).GetHashCode();
		}



		////////////////

		private IDictionary<int, OrbColorCode> OrbChunkColorCodes = null;



		////////////////

		public override void Initialize() {
			this.InitOrbChunkColors();
		}

		////

		internal void ResetChunks() {
			this.OrbChunkColorCodes.Clear();
		}


		////////////////

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			tasks.Add( new OrbsWorldGen() );
		}


		////////////////

		private void InitOrbChunkColors() {
			int worldCode = OrbsWorld.GetWorldCode();
			var rand = new UnifiedRandom( worldCode );

			this.OrbChunkColorCodes = new ConcurrentDictionary<int, OrbColorCode>();
			
			for( int i = 0; i < Main.maxTilesX; i += 16 ) {
				for( int j = 0; j < Main.maxTilesY; j += 16 ) {
					OrbColorCode color = OrbItemBase.GetNextRandomColorCode( rand );
					int code = OrbsWorld.GetOrbChunkCodeOfTile( i, j );

					this.OrbChunkColorCodes[ code ] = color;
				}
			}
		}


		////////////////

		public OrbColorCode GetColorCodeOfOrbChunkOfTile( int tileX, int tileY ) {
			int chunkCode = OrbsWorld.GetOrbChunkCodeOfTile( tileX, tileY );
			
			if( this.OrbChunkColorCodes.TryGetValue(chunkCode, out OrbColorCode colorCode) ) {
				return colorCode;
			}
			return 0;
		}
	}
}
