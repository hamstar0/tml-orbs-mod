using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.DotNET.Reflection;
using Orbs.Items;


namespace Orbs {
	public partial class OrbsConfig : ModConfig {
		private IDictionary<string, object> Overrides = new ConcurrentDictionary<string, object>();



		////////////////

		public T Get<T>( string propName ) {
			if( !this.Overrides.TryGetValue(propName, out object val) ) {
				if( !ReflectionHelpers.Get(this, propName, out T myval) ) {
					throw new ModHelpersException( "Invalid property "+propName+" of type "+typeof(T).Name );
				}
				return myval;
			}
		
			if( val.GetType() != typeof(T) ) {
				throw new ModHelpersException( "Invalid type ("+typeof(T).Name+") of property "+propName+"." );
			}
			return (T)val;
		}

		////

		public void SetOverride<T>( string propName, T value ) {
			if( !ReflectionHelpers.Get(this, propName, out T _) ) {
				throw new ModHelpersException( "Invalid property " + propName + " of type " + typeof( T ).Name );
			}
			this.Overrides[propName] = value;
		}


		////////////////

		public IEnumerable<(float Weight, int OrbItemType)> GetOrbChestWeights( out float totalWeight ) {
			totalWeight = this.BlueOrbPercentChanceForOrbChest;
			totalWeight += this.CyanOrbPercentChanceForOrbChest;
			totalWeight += this.GreenOrbPercentChanceForOrbChest;
			totalWeight += this.PinkOrbPercentChanceForOrbChest;
			totalWeight += this.PurpleOrbPercentChanceForOrbChest;
			totalWeight += this.RedOrbPercentChanceForOrbChest;
			totalWeight += this.TealOrbPercentChanceForOrbChest;
			totalWeight += this.WhiteOrbPercentChanceForOrbChest;
			totalWeight += this.YellowOrbPercentChanceForOrbChest;

			IEnumerable<(float, int)> getOrbs() {
				yield return (this.BlueOrbPercentChanceForOrbChest, ModContent.ItemType<BlueOrbItem>());
				yield return (this.CyanOrbPercentChanceForOrbChest, ModContent.ItemType<CyanOrbItem>());
				yield return (this.GreenOrbPercentChanceForOrbChest, ModContent.ItemType<GreenOrbItem>());
				yield return (this.PinkOrbPercentChanceForOrbChest, ModContent.ItemType<PinkOrbItem>());
				yield return (this.PurpleOrbPercentChanceForOrbChest, ModContent.ItemType<PurpleOrbItem>());
				yield return (this.RedOrbPercentChanceForOrbChest, ModContent.ItemType<RedOrbItem>());
				yield return (this.TealOrbPercentChanceForOrbChest, ModContent.ItemType<TealOrbItem>());
				yield return (this.WhiteOrbPercentChanceForOrbChest, ModContent.ItemType<WhiteOrbItem>());
				yield return (this.YellowOrbPercentChanceForOrbChest, ModContent.ItemType<YellowOrbItem>());
			}
			return getOrbs();
		}
	}
}
