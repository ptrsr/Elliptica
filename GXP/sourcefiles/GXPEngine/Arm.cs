using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Arm : Sprite
    {
        private Player _player;
        private Vec2 _armVector;
        public Projectiles projectile;

        public Arm(Player player) : base("hand.png")
        {
            SetOrigin(0, height / 2);
            _player = player;
        }

        void Update()
        {
            _armVector = new Vec2(Input.mouseX - _player.x, Input.mouseY - _player.y).Normalize().Multiply(this.height * 1.5f);
            RotateArm();
        }

        void RotateArm ()
        {
            float angleInRadians;
            if (_player.scaleX == 1)
            {
                angleInRadians = Mathf.Atan2(_armVector.y, _armVector.x);
                rotation = Vec2.Rad2Deg(angleInRadians);
            }
            if (_player.scaleX == -1)
            {
                angleInRadians = -Mathf.Atan2(_armVector.y, _armVector.x);
                rotation = Vec2.Rad2Deg(angleInRadians) + 180;
            }
        }

        public void Shooting()
        {
            if(projectile == null)
            {
                projectile = new Projectiles();
                game.AddChild(projectile);
                projectile.x = _player.x + _armVector.x;
                projectile.y = _player.y + _armVector.y;
                projectile.rotation = Vec2.Rad2Deg(Mathf.Atan2(_armVector.y, _armVector.x));
            }
        }


    }
}
