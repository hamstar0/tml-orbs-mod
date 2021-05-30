using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Services.Network.SimplePacket;
using Orbs.Items.Base;


namespace Orbs.Protocols {
	[Serializable]
	class OrbActivateProtocol : SimplePacketPayload {
		public static void Broadcast( OrbColorCode colorCode, int chunkGridX, int chunkGridY ) {
			if( Main.netMode != 1 ) { throw new ModLibsException( "Not client" ); }

			var packet = new OrbActivateProtocol( (int)colorCode, chunkGridX, chunkGridY );

			SimplePacket.SendToServer( packet );
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

		public override void ReceiveOnClient() {
			OrbItemBase.ActivateOrbUponTileChunk( this.ChunkGridX, this.ChunkGridY );
		}

		public override void ReceiveOnServer( int fromWho ) {
			OrbItemBase.ActivateOrbUponTileChunk( this.ChunkGridX, this.ChunkGridY );
		}
	}
}
