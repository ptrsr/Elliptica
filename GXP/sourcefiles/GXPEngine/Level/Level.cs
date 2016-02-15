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
            if(trigger.GetFrame() == 1)
            {
                door.ActivateDoor();
                if (door.DistanceTo(player.position.x, player.position.y) < 200)
                {
                    door.UpdateAnimation(door.DistanceTo(player.position.x, player.position.y));
                }
            }
            
            
            
        }

    }
}
