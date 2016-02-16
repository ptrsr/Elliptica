using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Portal : AnimSprite
    {
        string _side;
        bool _spawned = false;
        float _timer = 0;
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
            {
                _timer += 0.5f;
            }

            if (_timer >= 8 && _spawned == false)
            {
                PortalAnim anim = new PortalAnim();
                AddChild(anim);
                anim.SetXY(width / 2 + 16, -height / 2);
                _spawned = true;

            }

            SetFrame((int)_timer);
            

        }
    }

    public class PortalAnim : AnimSprite
    {
        float _timer = 0;
        public PortalAnim() : base("Portal Idle.png",4,2)
        {

        }

        void Update()
        {
            _timer += 0.4f;

            if (_timer > 8)
                _timer = 4;

            SetFrame((int)_timer);
        }
    }
}
