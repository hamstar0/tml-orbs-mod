using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.Errors;


namespace Orbs {
	public partial class OrbsConfig : ModConfig {
		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int BlueOrbRecipeUniqueIngredientCount { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int CyanOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 3 )]
		public int GreenOrbRecipeUniqueIngredientCount { get; set; } = 3;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int PinkOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int PurpleOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 8 )]
		public int RedOrbRecipeUniqueIngredientCount { get; set; } = 8;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int BrownOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 5 )]
		public int YellowOrbRecipeUniqueIngredientCount { get; set; } = 5;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int WhiteOrbRecipeUniqueIngredientCount { get; set; } = 1;

		////

		[Range( 0, 99 )]
		[DefaultValue( 2 )]
		public int BlueOrbRecipeStack { get; set; } = 2;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int CyanOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int GreenOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PinkOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int PurpleOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int RedOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int BrownOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int YellowOrbRecipeStack { get; set; } = 1;

		[Range( 0, 99 )]
		[DefaultValue( 1 )]
		public int WhiteOrbRecipeStack { get; set; } = 1;
	}
}
