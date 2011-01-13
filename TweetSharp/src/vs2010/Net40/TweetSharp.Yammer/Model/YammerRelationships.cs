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
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Yammer.Model
{
    /// <summary>
    /// Allowable types of org-chart relationships
    /// </summary>
    public enum OrgChartRelationshipType
    {
        /// <summary>
        /// User B reports to user A.
        /// </summary>
        Subordinate,
        /// <summary>
        /// User A reports to user B.
        /// </summary>
        Superior,
        /// <summary>
        /// Users A and B are peers.
        /// </summary>
        Colleague
    }

    /// <summary>
    /// Representation of org-chart relationships
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerRelationships : YammerObjectBase, IYammerModel
    {
        private IEnumerable<YammerUserReference> _colleagues;
        private IEnumerable<YammerUserReference> _subordinates;
        private IEnumerable<YammerUserReference> _superiors;

        /// <summary>
        /// Gets or sets an enumeration of user references that represent superiors of another user in the company org-chart. 
        /// </summary>
        [JsonProperty("Superiors")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerUserReference> Superiors
        {
            get { return _superiors; }
            set
            {
                if (_superiors == value)
                {
                    return;
                }
                _superiors = value;
                OnPropertyChanged("Superior");
            }
        }

        /// <summary>
        /// Gets or sets an enumeration of user references that represent subordinates of another user in the company org-chart. 
        /// </summary>
        [JsonProperty("Subordinates")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerUserReference> Subordinates
        {
            get { return _subordinates; }
            set
            {
                if (_subordinates == value)
                {
                    return;
                }
                _subordinates = value;
                OnPropertyChanged("Subordinates");
            }
        }

        /// <summary>
        /// Gets or sets an enumeration of user references that represent collegues of the another user. 
        /// </summary>
        [JsonProperty("Colleagues")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerUserReference> Colleagues
        {
            get { return _colleagues; }
            set
            {
                if (_colleagues == value)
                {
                    return;
                }
                _colleagues = value;
                OnPropertyChanged("Colleagues");
            }
        }

#if !Smartphone
        /// <summary>
        /// The source content used to deserialize the model entity instance.
        /// Can be XML or JSON, depending on the endpoint used.
        /// </summary>
        [DataMember]
#endif
        public virtual string RawSource { get; set; }
    }
}