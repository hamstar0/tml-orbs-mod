using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using Orbs.Items.Base;
using System;
using Terraria;


namespace Orbs.Protocols {
	class OrbActivateProtocol : PacketProtocolBroadcast {
		public static void Broadcast( OrbColorCode colorCode, int tileX, int tileY ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException( "Not client" ); }

			var protocol = new OrbActivateProtocol( (int)colorCode, tileX, tileY );

			protocol.SendToServer( true );
		}



		////////////////

		public int ColorCode;
		public int TileX;
		public int TileY;



		////////////////

		private OrbActivateProtocol() { }

		private OrbActivateProtocol( int colorCode, int tileX, int tileY ) {
			this.ColorCode = colorCode;
			this.TileX = tileX;
			this.TileY = tileY;
		}


		////////////////

		protected override void ReceiveOnClient() {
			OrbItemBase.ActivateOrbUponTileChunk( this.TileX, this.TileY );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			OrbItemBase.ActivateOrbUponTileChunk( this.TileX, this.TileY );
		}
	}
}
