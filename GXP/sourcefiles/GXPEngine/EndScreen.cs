using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class EndScreen : GameObject
    {
        BigTimer bigTimer;
        Cake cake;
        private bool saved = false;
        
        public EndScreen(BigTimer timer, StateType state) : base()
        {
            Sprite sprite = new Sprite("pop_up.png");
            sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
            sprite.x = x + 500;
            sprite.y = y + 350;
            AddChild(sprite);
            if(timer != null)
            {
                bigTimer = timer;
                bigTimer.x = game.width - 500;
                bigTimer.y = game.height - 400;
                AddChild(bigTimer);
            }

            cake = new Cake(state);
            cake.x = game.width - 650;
            cake.y = game.height - 420;
            AddChild(cake);
        }
        public EndScreen()
        {

        }

        void Update()
        {
            if(bigTimer != null)
            if (bigTimer.IsTimerStopeed())
            {
                CalculateCakes();
            }
        }

        private void CalculateCakes()
        {
            if(bigTimer.first > 0)
            {
                if (!saved)
                {
                    cake.OnePiece();
                    saved = true;
                }
            }
            if(bigTimer.second >= 1 && bigTimer.third < 1) 
            {
                if (!saved)
                {
                    cake.TwoPiece();
                    saved = true;
                }
            }

            if (bigTimer.second < 1 && bigTimer.first < 1)
            {
                if (!saved)
                {
                    cake.ThreePiece();
                    saved = true;
                }
            }
        }

    }
}
