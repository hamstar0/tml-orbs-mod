using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.Errors;


namespace Orbs {
	public partial class OrbsConfig : ModConfig {
		[DefaultValue( true )]
		public bool IsGeoResonantOrbSoldByDryad { get; set; } = true;

		////

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PurpleOrbDropsViaShadowOrb { get; set; } = 1;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		public float AnyOrbPercentChancePerChest { get; set; } = 0.5f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BlueOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CyanOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float GreenOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PinkOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PurpleOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RedOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BrownOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WhiteOrbWeightPerOrbChest { get; set; } = 1f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float YellowOrbWeightPerOrbChest { get; set; } = 1f;


		[DefaultValue(true)]
		public bool OnlyGenOrbsInUndergroundChests { get; set; } = true;
	}
}
