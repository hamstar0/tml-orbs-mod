using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace Orbs {
	public partial class OrbsConfig : ModConfig {
		public List<string> BreakableTilesWhitelist { get; set; } = new List<string> {
			//TileID.GetUniqueKey( TileID.HellstoneBrick ),
			//TileID.GetUniqueKey( TileID.ObsidianBrick ),
			//TileID.GetUniqueKey( TileID.DemoniteBrick ),
			//TileID.GetUniqueKey( TileID.CrimtaneBrick ),
			//
			TileID.GetUniqueKey( TileID.Platforms ),
			//
			TileID.GetUniqueKey( TileID.PiggyBank ),
			TileID.GetUniqueKey( TileID.Safes ),
			TileID.GetUniqueKey( TileID.DefendersForge ),
			//
			TileID.GetUniqueKey( TileID.Extractinator ),
			TileID.GetUniqueKey( TileID.SharpeningStation ),
			TileID.GetUniqueKey( TileID.AlchemyTable ),
			TileID.GetUniqueKey( TileID.LunarCraftingStation ),
			TileID.GetUniqueKey( TileID.BewitchingTable ),
			//
			TileID.GetUniqueKey( TileID.Campfire ),
			TileID.GetUniqueKey( TileID.HangingLanterns ),
			TileID.GetUniqueKey( TileID.Banners ),
			TileID.GetUniqueKey( TileID.WarTableBanner ),
			//
			TileID.GetUniqueKey( TileID.Torches ),
			TileID.GetUniqueKey( TileID.WaterCandle ),
			TileID.GetUniqueKey( TileID.PeaceCandle ),
			TileID.GetUniqueKey( TileID.Candles ),
			TileID.GetUniqueKey( TileID.PlatinumCandle ),
			//
			TileID.GetUniqueKey( TileID.Rope ),
			TileID.GetUniqueKey( TileID.SilkRope ),
			TileID.GetUniqueKey( TileID.VineRope ),
			TileID.GetUniqueKey( TileID.WebRope ),
			TileID.GetUniqueKey( TileID.Chain ),
			TileID.GetUniqueKey( TileID.MinecartTrack ),
			//
			TileID.GetUniqueKey( TileID.Bottles ),
			TileID.GetUniqueKey( TileID.Books ),
			//
			TileID.GetUniqueKey( TileID.Cannon ),
			TileID.GetUniqueKey( TileID.SnowballLauncher ),
			TileID.GetUniqueKey( TileID.LandMine ),
			TileID.GetUniqueKey( TileID.ElderCrystalStand ),
			//
			TileID.GetUniqueKey( TileID.Timers ),
			TileID.GetUniqueKey( TileID.Lever ),
			TileID.GetUniqueKey( TileID.InletPump ),
			TileID.GetUniqueKey( TileID.OutletPump ),
			TileID.GetUniqueKey( TileID.Statues ),
			TileID.GetUniqueKey( TileID.Explosives ),
			TileID.GetUniqueKey( TileID.PressurePlates ),
			TileID.GetUniqueKey( TileID.WeightedPressurePlate ),
			//
			TileID.GetUniqueKey( TileID.WoodBlock ),
			TileID.GetUniqueKey( TileID.BorealWood ),
			TileID.GetUniqueKey( TileID.RichMahogany ),
			TileID.GetUniqueKey( TileID.LivingWood ),
			TileID.GetUniqueKey( TileID.LeafBlock ),
			TileID.GetUniqueKey( TileID.WoodenBeam ),
			TileID.GetUniqueKey( TileID.ClosedDoor ),
			TileID.GetUniqueKey( TileID.OpenDoor ),
			TileID.GetUniqueKey( TileID.MushroomBlock ),
			TileID.GetUniqueKey( TileID.CactusBlock ),
			TileID.GetUniqueKey( TileID.LivingMahogany ),
			TileID.GetUniqueKey( TileID.LivingMahoganyLeaves ),
			//
			TileID.GetUniqueKey( TileID.Trees ),
			TileID.GetUniqueKey( TileID.MushroomTrees ),
			TileID.GetUniqueKey( TileID.PalmTree ),
			TileID.GetUniqueKey( TileID.Saplings ),
			TileID.GetUniqueKey( TileID.Sunflower ),
			TileID.GetUniqueKey( TileID.Plants ),
			TileID.GetUniqueKey( TileID.Plants2 ),
			TileID.GetUniqueKey( TileID.Vines ),
			TileID.GetUniqueKey( TileID.MushroomPlants ),
			TileID.GetUniqueKey( TileID.CorruptPlants ),
			TileID.GetUniqueKey( TileID.CorruptThorns ),
			TileID.GetUniqueKey( TileID.FleshWeeds ),
			TileID.GetUniqueKey( TileID.CrimtaneThorns ),
			TileID.GetUniqueKey( TileID.CrimsonVines ),
			TileID.GetUniqueKey( TileID.HallowedPlants ),
			TileID.GetUniqueKey( TileID.HallowedPlants2 ),
			TileID.GetUniqueKey( TileID.HallowedVines ),
			TileID.GetUniqueKey( TileID.JunglePlants ),
			TileID.GetUniqueKey( TileID.JunglePlants2 ),
			TileID.GetUniqueKey( TileID.JungleVines ),
			TileID.GetUniqueKey( TileID.JungleThorns ),
			TileID.GetUniqueKey( TileID.PlantDetritus ),
			TileID.GetUniqueKey( TileID.Coral ),
			TileID.GetUniqueKey( TileID.BeachPiles ),
			TileID.GetUniqueKey( TileID.Cactus ),
			TileID.GetUniqueKey( TileID.DyePlants ),
			TileID.GetUniqueKey( TileID.ImmatureHerbs ),
			TileID.GetUniqueKey( TileID.BloomingHerbs ),
			TileID.GetUniqueKey( TileID.MatureHerbs ),
			TileID.GetUniqueKey( TileID.BlueMoss ),
			TileID.GetUniqueKey( TileID.BrownMoss ),
			TileID.GetUniqueKey( TileID.GreenMoss ),
			TileID.GetUniqueKey( TileID.LavaMoss ),
			TileID.GetUniqueKey( TileID.LongMoss ),
			TileID.GetUniqueKey( TileID.PurpleMoss ),
			TileID.GetUniqueKey( TileID.RedMoss ),
			TileID.GetUniqueKey( TileID.Pumpkins ),
			//
			TileID.GetUniqueKey( TileID.Cobweb ),
			TileID.GetUniqueKey( TileID.BreakableIce ),
			TileID.GetUniqueKey( TileID.MagicalIceBlock ),
			//
			TileID.GetUniqueKey( TileID.HoneyBlock ),
			TileID.GetUniqueKey( TileID.CrispyHoneyBlock ),
			TileID.GetUniqueKey( TileID.Hive ),
			//
			TileID.GetUniqueKey( TileID.Heart ),
			TileID.GetUniqueKey( TileID.Pots ),
			TileID.GetUniqueKey( TileID.ShadowOrbs ),
			TileID.GetUniqueKey( TileID.DemonAltar ),
			TileID.GetUniqueKey( TileID.LifeFruit ),
			TileID.GetUniqueKey( TileID.PlanteraBulb ),
			//
			TileID.GetUniqueKey( TileID.Copper ),
			TileID.GetUniqueKey( TileID.Tin ),
			TileID.GetUniqueKey( TileID.Iron ),
			TileID.GetUniqueKey( TileID.Lead ),
			TileID.GetUniqueKey( TileID.Silver ),
			TileID.GetUniqueKey( TileID.Tungsten ),
			TileID.GetUniqueKey( TileID.Gold ),
			TileID.GetUniqueKey( TileID.Platinum ),
			TileID.GetUniqueKey( TileID.Meteorite ),
			TileID.GetUniqueKey( TileID.Demonite ),
			TileID.GetUniqueKey( TileID.Crimtane ),
			TileID.GetUniqueKey( TileID.Obsidian ),
			TileID.GetUniqueKey( TileID.Hellstone ),
			TileID.GetUniqueKey( TileID.Cobalt ),
			TileID.GetUniqueKey( TileID.Palladium ),
			TileID.GetUniqueKey( TileID.Mythril ),
			TileID.GetUniqueKey( TileID.Orichalcum ),
			TileID.GetUniqueKey( TileID.Adamantite ),
			TileID.GetUniqueKey( TileID.Titanium ),
			TileID.GetUniqueKey( TileID.Chlorophyte ),
			TileID.GetUniqueKey( TileID.LunarOre ),
			//
			TileID.GetUniqueKey( TileID.Amethyst ),
			TileID.GetUniqueKey( TileID.Sapphire ),
			TileID.GetUniqueKey( TileID.Topaz ),
			TileID.GetUniqueKey( TileID.Emerald ),
			TileID.GetUniqueKey( TileID.Ruby ),
			TileID.GetUniqueKey( TileID.Diamond ),
			TileID.GetUniqueKey( TileID.ExposedGems ),
			//
			TileID.GetUniqueKey( TileID.SnowBlock ),
			TileID.GetUniqueKey( TileID.Cloud ),
			TileID.GetUniqueKey( TileID.RainCloud ),
			TileID.GetUniqueKey( TileID.SnowCloud ),
			TileID.GetUniqueKey( TileID.HoneyDrip ),
			TileID.GetUniqueKey( TileID.LavaDrip ),
			TileID.GetUniqueKey( TileID.SandDrip ),
			TileID.GetUniqueKey( TileID.WaterDrip ),
			//
			TileID.GetUniqueKey( TileID.Ash ),
			TileID.GetUniqueKey( TileID.Sand ),
			TileID.GetUniqueKey( TileID.Pearlsand ),
			TileID.GetUniqueKey( TileID.Crimsand ),
			TileID.GetUniqueKey( TileID.Ebonsand ),
			TileID.GetUniqueKey( TileID.DesertFossil ),
			TileID.GetUniqueKey( TileID.FossilOre ),
			TileID.GetUniqueKey( TileID.Silt ),
			TileID.GetUniqueKey( TileID.Slush ),
			//
			TileID.GetUniqueKey( TileID.CopperCoinPile ),
			TileID.GetUniqueKey( TileID.SilverCoinPile ),
			TileID.GetUniqueKey( TileID.GoldCoinPile ),
			TileID.GetUniqueKey( TileID.PlatinumCoinPile ),
			TileID.GetUniqueKey( TileID.Stalactite ),
			TileID.GetUniqueKey( TileID.SmallPiles ),
			TileID.GetUniqueKey( TileID.LargePiles ),
			TileID.GetUniqueKey( TileID.LargePiles2 ),
			//
			TileID.GetUniqueKey( TileID.Boulder ),
			TileID.GetUniqueKey( TileID.BeeHive ),
			TileID.GetUniqueKey( TileID.Tombstones ),
			//
			TileID.GetUniqueKey( TileID.Painting2X3 ),
			TileID.GetUniqueKey( TileID.Painting3X2 ),
			TileID.GetUniqueKey( TileID.Painting3X3 ),
			TileID.GetUniqueKey( TileID.Painting4X3 ),
			TileID.GetUniqueKey( TileID.Painting6X4 ),
			TileID.GetUniqueKey( TileID.ChristmasTree ),
			TileID.GetUniqueKey( TileID.HolidayLights ),
			TileID.GetUniqueKey( TileID.SillyStreamerBlue ),
			TileID.GetUniqueKey( TileID.SillyStreamerGreen ),
			TileID.GetUniqueKey( TileID.SillyStreamerPink ),
		};

		public Dictionary<string, List<int>> BreakableTilesWhitelistFrameExceptions { get; set; }
			= new Dictionary<string, List<int>>();

		/////

		[DefaultValue( true )]
		public bool HardmodeBreakableDirt { get; set; } = true;
	}
}
