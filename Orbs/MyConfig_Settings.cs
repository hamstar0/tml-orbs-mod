using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;


namespace Orbs {
	public partial class OrbsConfig : ModConfig {
		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeTheColorsDuke { get; set; } = false;

		/*public bool DebugModeCheatCreate { get; set; } = false;


		[Range( 0, 1024 )]
		[DefaultValue( 16 )]
		public int OrbsPerTinyWorld { get; set; } = 16;

		[Range( 0, 1024 )]
		[DefaultValue( 24 )]
		public int OrbsPerSmallWorld { get; set; } = 24;

		[Range( 0, 1024 )]
		[DefaultValue( 48 )]
		public int OrbsPerMediumWorld { get; set; } = 48;

		[Range( 0, 1024 )]
		[DefaultValue( 96 )]
		public int OrbsPerLargeWorld { get; set; } = 96;

		[Range( 0, 1024 )]
		[DefaultValue( 128 )]
		public int OrbsPerHugeWorld { get; set; } = 128;*/

		////

		[DefaultValue( true )]
		public bool EnableOrbUseUponTiles { get; set; } = true;

		[DefaultValue( false )]
		public bool EnableOrbPseudoBiomeForTiles { get; set; } = false;

		////

		public string OrbDisabledMessage { get; set; } = "";

		////

		[Range( 0, 1000 )]
		[DefaultValue( 80 )]
		public int OrbPseudoBiomeTileRadius { get; set; } = 80;


		[DefaultValue( true )]
		public bool CanDestroyActuatedTiles { get; set; } = true;


		[Range( 0f, 1f )]
		[DefaultValue( 0.075f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BinocularsCaveDiscoveryIntensity { get; set; } = 0.075f;
	}
}
