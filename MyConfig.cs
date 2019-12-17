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
		public int OrbsPerHugeWorld { get; set; } = 128;

		////

		[Range(0f, 1f)]
		[DefaultValue(100f)]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RedOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float GreenOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float BlueOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float YellowOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float PurpleOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float PinkOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float TealOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float BlackOrbWeight { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 100f )]
		public float WhiteOrbWeight { get; set; } = 1f;
	}
}
