using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;
using System.Collections.Generic;
using Terraria.ID;

namespace Orbs {
	public partial class OrbsConfig : ModConfig {
		/*
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

		public string DisabledOrbMessage { get; set; } = "";

		////

		[DefaultValue( true )]
		public bool OrbAffectsOnlyVanillaEarthTiles { get; set; } = true;

		public HashSet<string> OrbAffectedTilesBlacklist { get; set; } = new HashSet<string> {
			TileID.GetUniqueKey( TileID.ObsidianBrick ),
			TileID.GetUniqueKey( TileID.HellstoneBrick ),
			TileID.GetUniqueKey( TileID.EbonstoneBrick ),
			TileID.GetUniqueKey( TileID.CrimtaneBrick ),
			//
			TileID.GetUniqueKey( TileID.Ebonstone ),
			TileID.GetUniqueKey( TileID.Crimstone ),
			TileID.GetUniqueKey( TileID.Pearlstone )
		};

		////

		/*[DefaultValue( false )]
		public bool EnableOrbPseudoBiomeForTiles { get; set; } = false;

		[Range( 0, 1000 )]
		[DefaultValue( 80 )]
		public int OrbPseudoBiomeTileRadius { get; set; } = 80;*/
	}
}
