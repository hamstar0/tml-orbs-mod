using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;
using HamstarHelpers.Classes.UI.ModConfig;


namespace Orbs {
	class MyFloatInputElement : FloatInputElement { }




	class OrbsConfig : StackableModConfig {
		public static OrbsConfig Instance => ModConfigStack.GetMergedConfigs<OrbsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

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

		[Range(0, 1000)]
		[DefaultValue(80)]
		public int OrbPseudoBiomeTileRadius { get; set; } = 80;


		////

		[Range( 0, 99 )]
		[DefaultValue( 2 )]
		public int PinkOrbDropsViaTrickster { get; set; } = 2;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int RedOrbDropsViaBossWithoutSoG { get; set; } = 2;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int GreenOrbDropsViaBossWithSoG { get; set; } = 2;
	}
}
