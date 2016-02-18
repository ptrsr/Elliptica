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
            background.alpha = 1;
            AddChild(background);

            cont = new Buttons("buttons_gameover.png", 0, 2, 2);
            AddChild(cont);
            cont.OnClick += HandleOnClick;
            cont.x = game.width / 2f;
            cont.y = 300;

            exitGame = new Buttons("main.png", 0, 2, 1);
            AddChild(exitGame);
            exitGame.OnClick += HandleOnClick;
            exitGame.x = game.width / 2f;
            exitGame.y = 465;
        }

        void HandleOnClick(Buttons button)
        {
            if (button == cont)
            {
                _game.stateType = _game.stateType;
            }
            if (button == exitGame)
            {
                _game.stateType = StateType.Menu;
            }
        }
    }
}
