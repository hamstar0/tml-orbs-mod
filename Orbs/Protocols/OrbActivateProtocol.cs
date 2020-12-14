using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using Orbs.Items.Base;


namespace Orbs.Protocols {
	class OrbActivateProtocol : PacketProtocolBroadcast {
		public static void Broadcast( OrbColorCode colorCode, int chunkGridX, int chunkGridY ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException( "Not client" ); }

			var protocol = new OrbActivateProtocol( (int)colorCode, chunkGridX, chunkGridY );

			protocol.SendToServer( true );
		}



		////////////////

		public int ColorCode;
		public int ChunkGridX;
		public int ChunkGridY;



		////////////////

		private OrbActivateProtocol() { }

		private OrbActivateProtocol( int colorCode, int chunkGridX, int chunkGridY ) {
			this.ColorCode = colorCode;
			this.ChunkGridX = chunkGridX;
			this.ChunkGridY = chunkGridY;
		}


		////////////////

		protected override void ReceiveOnClient() {
			OrbItemBase.ActivateOrbUponTileChunk( this.ChunkGridX, this.ChunkGridY );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			OrbItemBase.ActivateOrbUponTileChunk( this.ChunkGridX, this.ChunkGridY );
		}
	}
}
