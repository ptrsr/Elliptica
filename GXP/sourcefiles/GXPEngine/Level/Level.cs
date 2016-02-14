using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace GXPEngine
{
    class Level : TMXLevel
    {
        private List<Ball> ballz = new List<Ball>();
        public Level(string filename) : base()
        {
            InterpretLayer(filename);
            InterpretObjectGroup(filename);
        }

        void Update()
        {
            CheckBallCollision();
            
        }
        
        private void CheckBallCollision()
        {
            if(player != null)
            {
                if (ball != null)
                {

                    if (!player.arm.CheckHasBall())
                    {
                        if ((ball.position.x - player.position.x) < 3)
                        {
                            ball.Destroy();
                            ball = null;
                            player.PickUpBall();
                        }
                    }
                }
            }
        }
    }
}
