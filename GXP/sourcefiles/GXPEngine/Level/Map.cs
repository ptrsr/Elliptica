using System;
using System.IO;
using System.Xml.Serialization;

namespace GXPEngine
{
    [XmlRootAttribute("map")]
    public class Map
    {

        [XmlAttribute("width")]
        public int Width = 0;

        [XmlAttribute("height")]
        public int Height = 0;

        [XmlElement("layer")]
        public Layer[] layer;

        [XmlElement("objectgroup")]
        public ObjectGroup[] objectGroup;



        public Map() { }
    }

    [XmlRootAttribute("objectgroup")]
    public class ObjectGroup
    {
        [XmlAttribute("name")]
        public string name;

        [XmlElement("object")]
        public SingleObject[] singleobject;
    }

    [XmlRootAttribute("object")]
    public class SingleObject
    {
        [XmlAttribute("gid")]
        public int gid;

        [XmlAttribute("x")]
        public int x;

        [XmlAttribute("y")]
        public int y;

        [XmlElement("properties")]
        public Properties properties;
    }
    [XmlRootAttribute("properties")]
    public class Properties
    {
        [XmlElement("property")]
        public Property[] Property;
    }
    
    [XmlRootAttribute("property")]
    public class Property
    {
        [XmlAttribute("name")]
        public string Name;
        [XmlAttribute("value")]
        public string Value;
    }

    [XmlRootAttribute("layer")]
    public class Layer
    {
        [XmlAttribute("name")]//name of layer
        public string name;

        [XmlElement("data")]//goes into data
        public Data data;


        public Layer() { }
    }
    [XmlRootAttribute("data")]//shows nested csv data
    public class Data
    {
        [XmlAttribute("encoding")]
        public string Encoding;

        [XmlText]
        public string innerXml;

        public Data() { }
    }
}

