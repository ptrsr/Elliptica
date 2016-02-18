using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Turret : AnimSprite
    {
        private float frame;
        private float firstframe;
        private float lastframe;
        LineSegment line;
        private int timer;

        public Turret(float xPos, float yPos) : base("turret.png", 6, 2)
        {
            SetOrigin(width / 2, height / 2);
            firstframe = 6;
            lastframe = 11;
            line = new LineSegment(new Vec2(-32, 0), new Vec2(-CheckLength(xPos, yPos), 0), 0xffe22f2f, 2);
            AddChild(line);
        }

        void UpdateAnimation()
        {
            frame = frame - 0.1f;
            if (frame >= lastframe + 1)
                frame = firstframe;
            if (frame < firstframe)
                frame = lastframe;
            SetFrame((int)frame);
        }
        void Update()
        {
            UpdateAnimation();
            CheckCollision();
        }

        float CheckLength(float xPos, float yPos)
        {
            float length = 832;
            for (int i = 0; i < TMXLevel.Return().GetBackGroundList().Count; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    GameObject tiledObject = TMXLevel.Return().GetBackGroundList()[i];
                    if (tiledObject.HitTestPoint(xPos - j * 32, yPos) && length > j * 32)
                    {
                        length = j * 32;
                    }
                }
            }
            return length;
        }

        public LineSegment GetLine()
        {
            return line;
        }

        void CheckCollision()
        {
            Player player = TMXLevel.Return().GetPlayer();
            if (player != null)
            if (player.x < line.start.x + x && player.x > line.end.x + x && player.y > y && player.y < y + 64)
            {
                Console.WriteLine(true);
                timer++;
            }
            else
            {
                timer = 0;
                line.lineWidth = 2;
            }
            if (timer > 15)
            {
                line.lineWidth = 5;
                player.Die();
            }
        }
    }
}
