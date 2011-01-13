using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TweetSharp.Core
{
    
#if !SILVERLIGHT
    ///<summary> 
    /// Available data format for REST queries
    ///</summary>
    [Serializable]
#endif
    public enum WebFormat
    {
#if !SILVERLIGHT && !Smartphone
        /// <summary>
        /// The eXtensible Markup Language
        /// </summary>
        [EnumMember] Xml,
        /// <summary>
        /// JavaScript Object Notation
        /// </summary>
        [EnumMember] Json,
        /// <summary>
        /// Really Simple Syndication
        /// </summary>
        [EnumMember] Rss,
        /// <summary>
        /// Atom syndication format
        /// </summary>
        [EnumMember] Atom,
        /// <summary>
        /// No specific format specified
        /// </summary>
        [EnumMember] None
#else
        Xml,
        Json,
        Rss,
        Atom,
        None
#endif
    }
}
