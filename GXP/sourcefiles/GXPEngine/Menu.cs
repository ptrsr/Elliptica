using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Menu : GameObject
    {
        MyGame _game;
        public Buttons startGame;
        public Buttons loadLevel;
        public Buttons exitGame;

        public Menu(MyGame myGame)
        {
            _game = myGame;
            game.Add(this);

            Sprite background = new Sprite("background.png");
            AddChild(background);

            Level level = new Level("Start.tmx", _game);
            AddChild(level);

            startGame = new Buttons("buttons_menu.png", 0,2,3);
            AddChild(startGame);
            startGame.OnClick += HandleOnClick;
            startGame.x = game.width / 2;
            startGame.y = 300;

            loadLevel = new Buttons("buttons_menu.png", 2,2, 3);
            AddChild(loadLevel);
            loadLevel.OnClick += HandleOnClick;
            loadLevel.x = game.width / 2;
            loadLevel.y = 429;

            exitGame = new Buttons("buttons_menu.png", 4, 2, 3);
            AddChild(exitGame);
            exitGame.OnClick += HandleOnClick;
            exitGame.x = game.width / 2;
            exitGame.y = 558;
        }

        void HandleOnClick(Buttons button)
        {
            if (button == startGame)
            {
                _game.stateType = StateType.First_Level;
            }
            if (button == loadLevel)
            {
                _game.stateType = StateType.Level_Select;
            }
            if (button == exitGame)
            {
                Environment.Exit(0);
            }
        }
    }
}
