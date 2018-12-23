using System;
using System.Xml.Linq;

namespace general
{
    public class XDocumentExt
    {
        public XDocument Document { get; set; }

        public XNamespace Namespace { get; private set; }

        public XDocumentExt()
        {
        }

        public XDocumentExt(string file)
            : this()
        {
            Document = XDocument.Load(file);
            SetNameSpace();
        }

        public XDocumentExt(XDocument doc)
        {
            Document = doc;
        }

        private void SetNameSpace()
        {
            if (Document != null)
            {
                Namespace = String.Empty;

                XAttribute xmlns_attr = Document.Root.Attribute("xmlns");
                if (xmlns_attr != null)
                {
                    Namespace = xmlns_attr.Value;
                }
            }
        }

        public XElement Root
        {
            get
            {
                return Document.Root;
            }
        }

        public XName GetXName(string tag)
        {
            return XName.Get(tag, Namespace.NamespaceName);
        }
    }
}