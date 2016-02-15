using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class GameOver : GameObject
    {
        MyGame _game;
        ButtonComponent cont;
        ButtonComponent exitGame;

        public GameOver(MyGame myGame)
        {
            _game = myGame;
            game.Add(this);

            Level level = new Level("level1.tmx");
            AddChild(level);

            cont = new ButtonComponent(200, 40, System.Drawing.Color.DarkBlue, "Continue");
            AddChild(cont);
            cont.OnClick += HandleOnClick;
            cont.x = game.width / 2.5f;
            cont.y = 300;

            exitGame = new ButtonComponent(200, 40, System.Drawing.Color.DarkBlue, "Exit");
            AddChild(exitGame);
            exitGame.OnClick += HandleOnClick;
            exitGame.x = game.width / 2.5f;
            exitGame.y = 400;
        }

        void HandleOnClick(ButtonComponent button)
        {
            if (button == cont)
            {
                _game.SetState(_game.GetState());
            }
            if (button == exitGame)
            {
                Environment.Exit(0);
            }
        }
    }
}
