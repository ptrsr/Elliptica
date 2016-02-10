using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace GXPEngine
{
    public class TMXParser
    {
        public TMXParser()
        {
        }

        public Map Parse(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            TextReader reader = new StreamReader(filename);
            Map map = serializer.Deserialize(reader) as Map;
            reader.Close();
            return map;
        }
    }
}
