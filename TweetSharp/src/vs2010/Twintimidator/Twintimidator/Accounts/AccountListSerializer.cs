#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Twintimidator.Accounts
{
    public class AccountListSerializer : IAccountListSerializer
    {
        public AccountListSerializer()
        {
            FileName = "accounts.list.cache";
        }

        #region IAccountListSerializer Members

        public string FilePath
        {
            get
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                return Path.Combine(appData, FileName);
            }
        }

        public string FileName { get; set; }

        public IEnumerable<IUserAccount> DeserializeFromDisk()
        {
            var bf = new BinaryFormatter();
            try
            {
                var file = File.Open(FilePath, FileMode.Open);
                var list = (List<IUserAccount>) bf.Deserialize(file);
                file.Dispose();
                return list;
            }
            catch (IOException)
            {
                return null;
            }
        }

        public bool SerializeToDisk(IEnumerable<IUserAccount> accounts)
        {
            var bf = new BinaryFormatter();
            var list = accounts.ToList();
            try
            {
                var file = File.Open(FilePath, FileMode.Create);
                bf.Serialize(file, list);
                file.Dispose();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion
    }
}