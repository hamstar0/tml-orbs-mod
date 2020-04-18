using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace Orbs {
	partial class OrbsPlayer : ModPlayer {
		internal IList<(int TileX, int TileY)> NearbyOrbs = new List<(int, int)>();

		internal IDictionary<string, int> NoSoGKillCount = new Dictionary<string, int>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.NoSoGKillCount.Clear();

			if( !tag.ContainsKey( "sog_npc_kills_netid_count" ) ) {
				return;
			}

			int killCount = tag.GetInt( "sog_npc_kills_netid_count" );

			for( int i=0; i<killCount; i++ ) {
				string npcKey = tag.GetString( "sog_npc_kills_key_" + i );
				int kills = tag.GetInt( "sog_npc_kills_" + i );

				this.NoSoGKillCount[npcKey] = kills;
			}
		}


		public override TagCompound Save() {
			var tag = new TagCompound { { "sog_npc_kills_netid_count", this.NoSoGKillCount.Count } };

			int i = 0;
			foreach( (string npcKey, int kills) in this.NoSoGKillCount ) {
				tag["sog_npc_kills_key_" + i] = npcKey;
				tag["sog_npc_kills_" + i] = kills;
				i++;
			}

			return base.Save();
		}
	}
}
