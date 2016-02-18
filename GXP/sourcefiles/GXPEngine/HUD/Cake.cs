using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GXPEngine
{
    public class Cake : GameObject
    {
        public StateType stateNow;
        public int cakePiece = 0;
        

        AnimSprite cakeSprite;

        public Cake(StateType state) : base()
        {
            stateNow = state;
            cakeSprite = new AnimSprite("cake.png", 4, 1);
            cakeSprite.SetFrame(cakePiece);
            AddChild(cakeSprite);
        }


        public Cake()
        {
            cakeSprite = new AnimSprite("cake.png", 4, 1);
            cakeSprite.SetFrame(cakePiece);
            AddChild(cakeSprite);
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
            string cakepiece = GetCakePiece().ToString();
            string statyType = stateNow.ToString();

            StreamWriter writer = new StreamWriter(".../Debug/" + stateNow + ".txt");
            writer.WriteLine(cakepiece);
            writer.WriteLine(statyType);
            writer.Close();

        }

        public int GetCakePiece()
        {
            return cakePiece;
        }
        public void CakeSetFrame(int cakepieces)
        {
            cakeSprite.SetFrame(cakepieces);
        }
    }
}
