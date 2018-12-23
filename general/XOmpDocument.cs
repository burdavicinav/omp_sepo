using System;
using System.Xml.Linq;

namespace general
{
    public class XOmpDocument : XDocument
    {
        public string Namespace
        {
            get
            {
                string xml_namespace = String.Empty;

                XAttribute xmlns_attr = Root.Attribute("xmlns");
                if (xmlns_attr != null)
                {
                    xml_namespace = xmlns_attr.Value;
                }

                return xml_namespace;
            }
        }
    }
}