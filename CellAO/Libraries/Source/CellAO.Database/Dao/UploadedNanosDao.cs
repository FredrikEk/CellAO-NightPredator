﻿#region License

// Copyright (c) 2005-2014, CellAO Team
// 
// 
// All rights reserved.
// 
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

#endregion

namespace CellAO.Database.Dao
{
    #region Usings ...

    using System.Collections.Generic;
    using System.Linq;

    using CellAO.Database.Entities;
    using CellAO.Interfaces;

    #endregion

    /// <summary>
    /// </summary>
    public class UploadedNanosDao : Dao<DBUploadedNano, UploadedNanosDao>
    {
        /// <summary>
        /// </summary>
        /// <param name="charId">
        /// </param>
        /// <returns>
        /// </returns>
        public IEnumerable<int> ReadNanos(int charId)
        {
            return this.GetAll(new { CharacterId = charId }).Select(x => x.NanoId).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="charId">
        /// </param>
        /// <param name="nanos">
        /// </param>
        public void WriteNano(int charId, IUploadedNanos nanos)
        {
            if (!this.ReadNanos(charId).Contains(nanos.NanoId))
            {
                DBUploadedNano temp = new DBUploadedNano();
                temp.CharacterId = charId;
                temp.NanoId = nanos.NanoId;
                this.Add(temp);
            }
        }

        public void WriteNanos(int charId, List<IUploadedNanos> nanos)
        {
            List<int> temp = this.ReadNanos(charId).ToList();
            foreach (IUploadedNanos nano in nanos)
            {
                if (!temp.Contains(nano.NanoId))
                {
                    this.Add(new DBUploadedNano() { CharacterId = charId, NanoId = nano.NanoId });
                }
            }
        }
    }
}