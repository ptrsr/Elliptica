using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Menu : GameObject
    {
        MyGame _game;
        ButtonComponent startGame;
        ButtonComponent loadLevel;
        ButtonComponent exitGame;

        public Menu(MyGame myGame)
        {
            _game = myGame;
            game.Add(this);

            Level level = new Level("level1.tmx");
            AddChild(level);

            startGame = new ButtonComponent(200, 40, System.Drawing.Color.DarkBlue, "Start Game");
            AddChild(startGame);
            startGame.OnClick += HandleOnClick;
            startGame.x = game.width / 2.5f;
            startGame.y = 300;

            loadLevel = new ButtonComponent(200, 40, System.Drawing.Color.DarkBlue, "Load Level");
            AddChild(loadLevel);
            loadLevel.OnClick += HandleOnClick;
            loadLevel.x = game.width / 2.5f;
            loadLevel.y = 400;

            exitGame = new ButtonComponent(200, 40, System.Drawing.Color.DarkBlue, "Exit Game");
            AddChild(exitGame);
            exitGame.OnClick += HandleOnClick;
            exitGame.x = game.width / 2.5f;
            exitGame.y = 500;
        }

        void HandleOnClick (ButtonComponent button)
        {
            if (button == startGame)
            {
                _game.SetState("level1");
            }
            if(button == loadLevel)
            {
                _game.SetState("loadLevel");
            }
            if ( button == exitGame)
            {
                Environment.Exit(0);
            }
        }
    }
}
