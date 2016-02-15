using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Portal : AnimSprite
    {
        string _side;
        float _timer;
        public Portal(string side) : base("Portal Opening.png",8,1)
        {
            SetOrigin(0, height / 2);
            _side = side;
        }

        void Update()
        {
            UpdateAnimation();

        }

        private void UpdateAnimation()
        {
            if (_timer < 8)
                _timer += 0.4f;

            SetFrame((int)_timer);
            

        }
    }
}
