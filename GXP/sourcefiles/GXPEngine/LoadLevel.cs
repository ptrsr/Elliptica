using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            loadLevel2 = new Buttons("PortalMenuAnimation.png", 0, 5, 6,"2", true);
            AddChild(loadLevel2);
            loadLevel2.OnClick += HandleOnClick;
            loadLevel2.x = 421;
            loadLevel2.y = 250;

            loadLevel3 = new Buttons("PortalMenuAnimation.png", 0, 5, 6, "3", true);
            AddChild(loadLevel3);
            loadLevel3.OnClick += HandleOnClick;
            loadLevel3.x = 586;
            loadLevel3.y = 250;

            loadLevel4 = new Buttons("PortalMenuAnimation.png", 0, 5, 6, "4", true);
            AddChild(loadLevel4);
            loadLevel4.OnClick += HandleOnClick;
            loadLevel4.x = 751;
            loadLevel4.y = 250;

            loadLevel5 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "5", true);
            AddChild(loadLevel5);
            loadLevel5.OnClick += HandleOnClick;
            loadLevel5.x = 256;
            loadLevel5.y = 415;

            loadLevel6 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "6", true);
            AddChild(loadLevel6);
            loadLevel6.OnClick += HandleOnClick;
            loadLevel6.x = 421;
            loadLevel6.y = 415;

            loadLevel7 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "7", true);
            AddChild(loadLevel7);
            loadLevel7.OnClick += HandleOnClick;
            loadLevel7.x = 586;
            loadLevel7.y = 415;

            loadLevel8 = new Buttons("PortalMenuAnimationGreen.png", 0, 5, 6, "8", true);
            AddChild(loadLevel8);
            loadLevel8.OnClick += HandleOnClick;
            loadLevel8.x = 751;
            loadLevel8.y = 415;

            loadLevel9 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "9", true);
            AddChild(loadLevel9);
            loadLevel9.OnClick += HandleOnClick;
            loadLevel9.x = 256;
            loadLevel9.y = 580;

            loadLevel10 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "10", true);
            AddChild(loadLevel10);
            loadLevel10.OnClick += HandleOnClick;
            loadLevel10.x = 421;
            loadLevel10.y = 580;

            loadLevel11 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "11", true);
            AddChild(loadLevel11);
            loadLevel11.OnClick += HandleOnClick;
            loadLevel11.x = 586;
            loadLevel11.y = 580;

            loadLevel12 = new Buttons("PortalMenuAnimationPurple.png", 0, 5, 6, "12", true);
            AddChild(loadLevel12);
            loadLevel12.OnClick += HandleOnClick;
            loadLevel12.x = 751;
            loadLevel12.y = 580;

            back = new Buttons("Back.png", 0, 2, 1);
            AddChild(back);
            back.OnClick += HandleOnClick;
            back.x = 100;
            back.y = 680;
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
    }
}
