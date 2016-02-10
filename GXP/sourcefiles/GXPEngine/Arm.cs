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

        public Arm(Player player) : base("checkers.png")
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
            float angleInRadians = Mathf.Atan2(_armVector.y, _armVector.x);
            rotation = Vec2.Rad2Deg(angleInRadians);
        }

        public void Shooting()
        {
            Projectiles Projectile = new Projectiles();
            game.AddChild(Projectile);
            Projectile.x = _player.x + _armVector.x;
            Projectile.y = _player.y + _armVector.y;
            Projectile.rotation = this.rotation;
        }


    }
}
