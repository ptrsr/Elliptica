using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class BigTimer : GameObject
    {
        private AnimSprite firstDigit;
        private AnimSprite doubleDots;
        private AnimSprite secondDigit;
        private AnimSprite thirdDigit;
        private AnimSprite clockIcon;
        public int first;
        public int second;
        public int third;
        private bool isTimerStop;
        
        private float delay = 60; //frames per sec
        private float timer = 0;
        

        public BigTimer() : base()
        {
            firstDigit = new AnimSprite("digits.png", 12, 1);
            firstDigit.SetFrame(1);
            AddChild(firstDigit);
            firstDigit.SetXY(x, y);
            doubleDots = new AnimSprite("digits.png", 12, 1);
            doubleDots.SetFrame(0);
            doubleDots.SetXY(x + 32, 0);
            AddChild(doubleDots);
            secondDigit = new AnimSprite("digits.png", 12, 1);
            secondDigit.SetFrame(1);
            secondDigit.SetXY(x + 64, 0);
            AddChild(secondDigit);
            thirdDigit = new AnimSprite("digits.png", 12, 1);
            thirdDigit.SetFrame(1);
            thirdDigit.SetXY(x + 96, 0);
            AddChild(thirdDigit);
            clockIcon = new AnimSprite("digits.png", 12, 1);
            clockIcon.SetFrame(12);
            clockIcon.SetXY(x + 128, 0);
            AddChild(clockIcon);
        }
        
        
        void Update()
        {
            Digits();
            Timer();
        }
        private void Digits()
        {
            firstDigit.SetFrame(1 + first);
            secondDigit.SetFrame(1 + second);
            thirdDigit.SetFrame(1 + third);
        }

        private void Timer()
        {
            if (!isTimerStop)
            {
                if (timer > delay)
                {

                    third++;
                    timer = 0;
                    if (third == 10 && second <= 9)
                    {
                        second++;
                        third = 0;
                    }
                    if (second == 6)
                    {
                        if (first != 9)
                        {
                            first++;
                        }
                        second = 0;

                    }
                }
                timer++;

            }
        }
        public void StopTimer()
        {
            isTimerStop = true;
        }
        public bool IsTimerStopeed()
        {
            if (isTimerStop == true) return true;
            else return false;
        }


    }
}
