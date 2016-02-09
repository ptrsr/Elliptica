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
        public Level(string filename) : base()
        {
            InterpretLayer(filename);
            InterpretObjectGroup(filename);
            
        }


    }
}
