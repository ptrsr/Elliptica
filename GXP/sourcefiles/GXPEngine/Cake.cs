using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace GXPEngine
{
    [XmlRoot("cakes")]
    [XmlInclude(typeof(EndScreen))]
    [XmlInclude(typeof(Level))]
    [XmlInclude(typeof(MyGame))]
    public class Cake : GameObject
    {
        [XmlAttribute("level")]
        public StateType stateNow;
        [XmlAttribute("cakePieces")]
        public int cakePiece;

        XmlSerializer xmlSerializer;
        private string cake = "cakes";
        AnimSprite cakeSprite;

        public Cake(StateType state) : base()
        {
            stateNow = state;
            cakeSprite = new AnimSprite("cake.png", 4, 1);
            cakeSprite.SetFrame(0);
            AddChild(cakeSprite);
        }


        public Cake()
        {

        }

        public void OnePiece()
        {
            cakeSprite.SetFrame(1);
            cakePiece = 1;
            Save();
        }
        public void TwoPiece()
        {
            cakeSprite.SetFrame(2);
            cakePiece = 2;
            Save();
        }
        public void ThreePiece()
        {
            cakeSprite.SetFrame(3);
            cakePiece = 3;
            Save();
        }

        private void Save()
        {


            xmlSerializer = new XmlSerializer(typeof(Cake));
            XmlWriter xmlWriter = XmlWriter.Create(cake + " " + stateNow.ToString() + ".xml");


            xmlSerializer.Serialize(xmlWriter, this);
            xmlWriter.Close();

        }
    }
}
