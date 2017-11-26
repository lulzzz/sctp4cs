/*
 * Copyright 2017 pi.pe gmbh .
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using pe.pi.sctp4j.sctp.behave;
using System;
using System.Net.Sockets;
using System.Threading;

/**
 *
 * @author tim
 */
namespace pe.pi.sctp4j.sctp.small {
	public class UDPForwardingStream : BlockingSCTPStream {
		Socket _udpSock;
		private Thread _rcv;

		public UDPForwardingStream(Association a, int id, int toPort) : base(a, id) {
			_udpSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			_udpSock.Connect("localhost", toPort);
			_rcv = new Thread(run);
			_rcv.Name = "UDPForwarding_rcv";
			_rcv.Start();
			SCTPStreamBehaviour behave = mkBehave();
			base.setBehave(behave);
		}
		
		public void run() {
			try {
				_udpSock.ReceiveTimeout = 1000;
				byte[] buff = new byte[4096];
				while (_rcv != null) {
					try {
						int l = _udpSock.Receive(buff);
						if (l > buff.Length) {
							Log.warn("truncated packet from " + _udpSock.RemoteEndPoint.ToString());
							l = buff.Length;
						}
						byte[] pkt = new byte[l];
						Array.Copy(buff, 0, pkt, 0, l);
						send(pkt);
					} catch (SocketException stx) {
						; // ignore - lets us check for close....
					}
				}
			} catch (Exception x) {

			}
			// clean up here.....
		}


		private SCTPStreamBehaviour mkBehave() {
			return new UnorderedStreamBehaviour();
		}
	}
}
