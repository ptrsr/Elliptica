//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace GXPEngine
//{
//    class Turret : AnimSprite
//    {
//        private float frame;
//        private float firstframe;
//        private float lastframe;
//        LineSegment line;

//        public Turret() : base("turret.png",6,2)
//        {
//            SetOrigin(width / 2, height / 2);
//            firstframe = 6;
//            lastframe = 11;
//            line = new LineSegment(new Vec2(-32,0), new Vec2(-100, 0), 0xffe22f2f, 2);
//            AddChild(line);
//        }

//        void UpdateAnimation()
//        {
//            frame = frame - 0.1f;
//            if (frame >= lastframe + 1)
//                frame = firstframe;
//            if (frame < firstframe)
//                frame = lastframe;
//            SetFrame((int)frame);
//        }
//        void Update()
//        {
//            UpdateAnimation();
//        }

//        void CheckLength()
//        {
//            bool done = false;
//            while (done == false)
//            {
//                for (int i = 0; i < TMXLevel.)
//            }
//        }
//    }
//}
