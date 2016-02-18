using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace GXPEngine
{
    class LoadLevel : GameObject
    {
        MyGame _game;
        Buttons loadLevel1;
        Buttons loadLevel2;
        Buttons loadLevel3;
        Buttons loadLevel4;
        Buttons loadLevel5;
        Buttons loadLevel6;
        Buttons loadLevel7;
        Buttons loadLevel8;
        Buttons loadLevel9;
        Buttons loadLevel10;
        Buttons loadLevel11;
        Buttons loadLevel12;
        Buttons back;
        const int Width = 0;
        const int Height = 1;
        int[,] _data = new int[Height, Width];

        Cake cake1, cake2, cake3, cake4, cake5, cake6, cake7, cake8, cake9, cake10, cake11, cake12;

        public LoadLevel(MyGame myGame)
        {
            _game = myGame;
            game.Add(this);

            Level level = new Level("Select.tmx",_game);
            AddChild(level);

            AnimSprite header = new AnimSprite("buttons_menu.png", 2, 3);
            header.SetOrigin(header.width / 2, header.height / 2);
            header.SetFrame(3);
            header.SetXY(game.width / 2, 100);
            AddChild(header);

            loadLevel1 = new Buttons("PortalMenuAnimation.png", 0, 5, 6,"I", true);
            AddChild(loadLevel1);
            loadLevel1.OnClick += HandleOnClick;
            loadLevel1.x = 256;
            loadLevel1.y = 250;
            cake1 = DeserializingCakes("First_Level.txt");
            AddChild(cake1);

            loadLevel2 = new Buttons("PortalMenuAnimation.png", 0, 5, 6,"2", true);
            AddChild(loadLevel2);
            loadLevel2.OnClick += HandleOnClick;
            loadLevel2.x = 421;
            loadLevel2.y = 250;
            cake2 = DeserializingCakes("Second_Level.txt");
            AddChild(cake2);

            loadLevel3 = new Buttons("PortalMenuAnimation.png", 0, 5, 6, "3", true);
            AddChild(loadLevel3);
            loadLevel3.OnClick += HandleOnClick;
            loadLevel3.x = 586;
            loadLevel3.y = 250;
            cake3 = DeserializingCakes("Third_Level.txt");
            AddChild(cake3);

            loadLevel4 = new Buttons("PortalMenuAnimation.png", 0, 5, 6, "4", true);
            AddChild(loadLevel4);
            loadLevel4.OnClick += HandleOnClick;
            loadLevel4.x = 751;
            loadLevel4.y = 250;
            cake4 = DeserializingCakes("Forth_Level.txt");
            AddChild(cake4);

            loadLevel5 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "5", true);
            AddChild(loadLevel5);
            loadLevel5.OnClick += HandleOnClick;
            loadLevel5.x = 256;
            loadLevel5.y = 415;
            cake5 = DeserializingCakes("Fifth_Level.txt");
            AddChild(cake5);

            loadLevel6 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "6", true);
            AddChild(loadLevel6);
            loadLevel6.OnClick += HandleOnClick;
            loadLevel6.x = 421;
            loadLevel6.y = 415;
            cake6 = DeserializingCakes("Sixth_Level.txt");
            AddChild(cake6);

            loadLevel7 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "7", true);
            AddChild(loadLevel7);
            loadLevel7.OnClick += HandleOnClick;
            loadLevel7.x = 586;
            loadLevel7.y = 415;
            cake7 = DeserializingCakes("Seventh_Level.txt");
            AddChild(cake7);

            loadLevel8 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "8", true);
            AddChild(loadLevel8);
            loadLevel8.OnClick += HandleOnClick;
            loadLevel8.x = 751;
            loadLevel8.y = 415;
            cake8 = DeserializingCakes("Eight_Level.txt");
            AddChild(cake8);

            loadLevel9 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "9", true);
            AddChild(loadLevel9);
            loadLevel9.OnClick += HandleOnClick;
            loadLevel9.x = 256;
            loadLevel9.y = 580;
            cake9 = DeserializingCakes("Ninth_Level.txt");
            AddChild(cake9);

            loadLevel10 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "10", true);
            AddChild(loadLevel10);
            loadLevel10.OnClick += HandleOnClick;
            loadLevel10.x = 421;
            loadLevel10.y = 580;
            cake10 = DeserializingCakes("Tenth_Level.txt");
            AddChild(cake10);

            loadLevel11 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "11", true);
            AddChild(loadLevel11);
            loadLevel11.OnClick += HandleOnClick;
            loadLevel11.x = 586;
            loadLevel11.y = 580;
            cake11 = DeserializingCakes("Eleventh_Level.txt");
            AddChild(cake11);

            loadLevel12 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "12", true);
            AddChild(loadLevel12);
            loadLevel12.OnClick += HandleOnClick;
            loadLevel12.x = 751;
            loadLevel12.y = 580;
            cake12 = DeserializingCakes("Twelveth_Level.txt");
            AddChild(cake12);

            back = new Buttons("Back.png", 0, 2, 1);
            AddChild(back);
            back.OnClick += HandleOnClick;
            back.x = 100;
            back.y = 680;
        }

        void Update()
        {
            SetCakeXY(loadLevel1.GetMouse(), loadLevel1, cake1);
            SetCakeXY(loadLevel2.GetMouse(), loadLevel2, cake2);
            SetCakeXY(loadLevel3.GetMouse(), loadLevel3, cake3);
            SetCakeXY(loadLevel4.GetMouse(), loadLevel4, cake4);
            SetCakeXY(loadLevel5.GetMouse(), loadLevel5, cake5);
            SetCakeXY(loadLevel6.GetMouse(), loadLevel6, cake6);
            SetCakeXY(loadLevel7.GetMouse(), loadLevel7, cake7);
            SetCakeXY(loadLevel8.GetMouse(), loadLevel8, cake8);
            SetCakeXY(loadLevel9.GetMouse(), loadLevel9, cake9);
            SetCakeXY(loadLevel10.GetMouse(), loadLevel10, cake10);
            SetCakeXY(loadLevel11.GetMouse(), loadLevel11, cake11);
            SetCakeXY(loadLevel12.GetMouse(), loadLevel12, cake12);
            
        }

        private void SetCakeXY(bool button, Buttons levelbutton,Cake cake)
        {
            if (levelbutton.GetMouse())
            {
                cake.x = levelbutton.x - 15;
                cake.y = levelbutton.y + 20;
            }
            else
            {
                cake.x = levelbutton.x - 15;
                cake.y = levelbutton.y - 15;
            }
        }
        
        void HandleOnClick(Buttons button)
        {
            if (button == loadLevel1)
            {
                _game.stateType = StateType.First_Level;
            }
            if (button == loadLevel2)
            {
                _game.stateType = StateType.Second_Level;
            }
            if (button == loadLevel3)
            {
                _game.stateType = StateType.Third_Level;
            }
            if (button == loadLevel4)
            {
                _game.stateType = StateType.Forth_Level;
            }
            if (button == loadLevel5)
            {
                _game.stateType = StateType.Fifth_Level;
            }
            if (button == loadLevel6)
            {
                _game.stateType = StateType.Sixth_Level;
            }
            if (button == loadLevel7)
            {
                _game.stateType = StateType.Seventh_Level;
            }
            if (button == loadLevel8)
            {
                _game.stateType = StateType.Eight_Level;
            }
            if (button == loadLevel9)
            {
                _game.stateType = StateType.Ninth_Level;
            }
            if (button == loadLevel10)
            {
                _game.stateType = StateType.Tenth_Level;
            }
            if (button == loadLevel11)
            {
                _game.stateType = StateType.Eleventh_Level;
            }
            if (button == loadLevel12)
            {
                _game.stateType = StateType.Twelveth_Level;
            }
            if (button == back)
            {
                _game.stateType = StateType.Menu;
            }
        }

        private Cake DeserializingCakes(string textfile)
        {

            try
            {
                StreamReader streamReader = new StreamReader(textfile);
                string fileData = streamReader.ReadToEnd();
                streamReader.Close();

                string[] line = fileData.Split('\n');
                Cake cake = new Cake();
                cake.cakePiece = Int32.Parse(line[0]);
                cake.CakeSetFrame(cake.cakePiece);
                return cake;

            }
            catch
            {
                Cake cake = new Cake();
                cake.CakeSetFrame(0);
                return cake;
            }
        
                
        }
    }
}
