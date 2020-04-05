using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace UpdatePackagesWithSuffix
{
    internal class ProjectFile
    {
        private readonly string _fileName;
        private XDocument _document;

        public ProjectFile(string fileName)
        {
            _document = XDocument.Load(fileName, LoadOptions.PreserveWhitespace);
            _fileName = fileName;
        }

        public IEnumerable<XElement> PackageReferences => _document.Descendants("PackageReference");

        public void Save()
        {
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };            
            using (var xmlWriter = XmlWriter.Create(_fileName, settings))
                _document.Save(xmlWriter);
        }
    }
}
