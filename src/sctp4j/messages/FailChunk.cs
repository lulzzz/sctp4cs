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
// Modified by Andrés Leone Gámez


using SCTP4CS.Utils;

/**
*
* @author tim
*/
namespace pe.pi.sctp4j.sctp.messages {
	internal class FailChunk : Chunk {
		public FailChunk(CType type, byte flags, int length, ref ByteBuffer pkt) : base(type, flags, length, ref pkt) { }

		protected override void putFixedParams(ref ByteBuffer ret) { }
	}
}
