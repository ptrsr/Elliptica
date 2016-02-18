using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Cursor : AnimSprite
    {
        static int _frame = 0;
        public Cursor() : base("cursor.png", 4,1)
        {
            SetFrame(0);
            x = Input.mouseX;
            y = Input.mouseY;
        }

        void Update()
        {
            x = Input.mouseX;
            y = Input.mouseY;
            SetFrame(_frame);
        }

        public static void SetCursorFrame(int frame)
        {
            _frame = frame;
        }
    }
}
