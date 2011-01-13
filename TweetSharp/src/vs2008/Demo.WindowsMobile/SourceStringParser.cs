using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Demo.WindowsMobile
{
    internal class SourceStringParser
    {
        public SourceStringParser(string sourceString)
        {
            Parse(HttpUtility.HtmlDecode(sourceString));
        }

        private void Parse(string sourceString)
        {
            string uri;
            string text;
            Parse(sourceString, out uri, out text);
            Text = text;
            URL = uri;
        }

        private static void Parse(string source, out string uri, out string text)
        {
            try
            {
                var lowerSource = source.ToLower();
                var doc = new XmlDocument();
                doc.LoadXml(lowerSource);
                var nodes = doc.SelectNodes("/a");
                if (nodes != null && nodes.Count > 0 && nodes[0].Attributes["href"] != null)
                {
                    uri = nodes[0].Attributes["href"].Value;
                    //restore original case of the text string
                    var m = Regex.Match(source, nodes[0].InnerText, RegexOptions.IgnoreCase);
                    text = m.Success ? m.Value : nodes[0].InnerText;
                }
                else
                {
                    uri = string.Empty;
                    text = source;
                }
            }
            catch (XmlException)
            {
                uri = string.Empty;
                text = source;
            }
        }
        
        public string URL { get; private set; }
        public string Text { get; private set; }
        public bool HasURL
        {
            get
            {
                return !string.IsNullOrEmpty(URL);
            }
        }
    }
}
