using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class GameOver : GameObject
    {
        MyGame _game;
        Buttons cont;
        Buttons exitGame;

        public GameOver(MyGame myGame)
        {
            _game = myGame;
            game.Add(this);

            Sprite background = new Sprite("darken.png");
            AddChild(background);

            cont = new Buttons("buttons_gameover.png", 0, 2, 2);
            AddChild(cont);
            cont.OnClick += HandleOnClick;
            cont.x = game.width / 2.5f;
            cont.y = 300;

            exitGame = new Buttons("buttons_gameover.png",2,2,2);
            AddChild(exitGame);
            exitGame.OnClick += HandleOnClick;
            exitGame.x = game.width / 2.5f;
            exitGame.y = 400;
        }

        void HandleOnClick(Buttons button)
        {
            if (button == cont)
            {
                //_game.SetState(_game.GetState());
            }
            if (button == exitGame)
            {
                Environment.Exit(0);
            }
        }
    }
}
