using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;


namespace Orbs {
	public partial class OrbsConfig : ModConfig {
		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeTheColorsDuke { get; set; } = false;


		////

		[DefaultValue( true )]
		public bool CanDestroyActuatedTiles { get; set; } = true;


		[Range( 0f, 1f )]
		[DefaultValue( 0.075f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BinocularsCaveDiscoveryIntensity { get; set; } = 0.075f;
	}
}
