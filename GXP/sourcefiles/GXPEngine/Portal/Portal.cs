using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Portal : AnimSprite
    {
        public string _side;

        public Vec2 _topPos;
        public Vec2 _botPos;
        public Vec2 _Pos;

        private float _frame = 0;
        private string _color;
        private bool _spawned = false;

        private PortalAnim _anim;

        public Portal(string side, string color, float xPos, float yPos) : base("Portal Opening " + color + ".png",8,1)
        {
            SetOrigin(0, height / 2);
            x += xPos;
            y += yPos;

            if (side == "up")
            {
                rotation = -90;
                _Pos = new Vec2(x, y);
                _topPos = new Vec2(x + height / 2, y);
                _botPos = new Vec2(x - height / 2, y);
            }
            else if (side == "down")
            {
                rotation = 90;
                _topPos = new Vec2(x - height / 2, y);
                _botPos = new Vec2(x + height / 2, y);
            }
            else if (side == "left")
            {
                rotation = 180;
                _topPos = new Vec2(x, y + height / 2);
                _botPos = new Vec2(x, y - height / 2);
            }
            else if (side == "right")
            {
                _topPos = new Vec2(x, y + height / 2);
                _botPos = new Vec2(x, y - height / 2);
            }

            _color = color;
            _side = side;
        }

        void Update()
        {
            UpdateAnimation();

        }

        private void UpdateAnimation()
        {
            if (_frame < 8)
            {
                _frame += 0.5f;
            }

            if (_frame >= 8 && _spawned == false)
            {
                _anim = new PortalAnim(_color);
                AddChild(_anim);
                _anim.SetXY(width / 2 + 16, -height / 2);
                _spawned = true;

            }

            SetFrame((int)_frame);
        }

        public void Remove()
        {
            _anim.Destroy();
            Destroy();
        }
    }

    public class PortalAnim : AnimSprite
    {
        float _timer = 0;
        public PortalAnim(string color) : base("Portal Idle " + color + ".png",4,2)
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
