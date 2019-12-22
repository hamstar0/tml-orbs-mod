using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Helpers.World;
using System;


namespace Orbs {
	class OrbsPlayerData : CustomPlayerData {
		public int WorldCode { get; private set; } = -1;



		////////////////

		protected override void OnEnter( object data ) {
			this.WorldCode = WorldHelpers.GetUniqueIdForCurrentWorld( true ).GetHashCode();
		}

		protected override object OnExit() {
			this.WorldCode = -1;
			return base.OnExit();
		}
	}
}
