using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Orbs {
	class MyItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			if( item.type == ItemID.Binoculars ) {
				var tip = new TooltipLine( this.mod, "OrbsBinoculars", "Reveals patches of orb-resonant terrain (by color)" );
				tooltips.Add( tip );
			}
		}
	}
}
