using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class TMXLevel : GameObject
    {
        //constructor not used because level uses inheritence and only needs the methods
        private AnimSprite animSprite;
        const int TileSize = 64;

        protected List<Item> items = new List<Item>();
        protected List<AnimSprite> background = new List<AnimSprite>();
        protected List<int> tiles = new List<int>();
        public TMXLevel()
        {
        }
        //this is for interpreting only an object groups there is a different method for layer
        protected void InterpretObjectGroup(string filename)
        {
            //create a TMXParser
            TMXParser tmxParser = new TMXParser();
            //parse the file 'level.tmx'
            Map map = tmxParser.Parse(filename);
            //read all object groups
            ObjectGroup[] objectGroups = map.objectGroup;
            if(objectGroups != null)
            {
                for (int i = 0; i < objectGroups.Length; i++)
                {
                    interpretObjectGroup(objectGroups[i]);
                }
            }
        }

        //interpreting an object group
        private void interpretObjectGroup(ObjectGroup objectGroup)
        {
            SingleObject[] objects = objectGroup.singleobject;
            for (int i = 0; i < objects.Length; i++)
            {
                interpretObject(objects[i]);
            }
        }

        //read a single Tiled Object
        private void interpretObject(SingleObject tiledObject)
        {
            //create a scenery item
            Item scenery = new Item(tiledObject);
            items.Add(scenery);
            AddChild(scenery);
        }

        //finds a player spawnpoint
        ////checks for all children and if there is type_spawn it returns it
        //protected Item FindPlayerSpawn()
        //{
        //    foreach (GameObject child in GetChildren())
        //    {
        //        if (child is Item)
        //        {
        //            Item scenery = child as Item;
        //            if (scenery.GetItemType() == Item.TYPE_SPAWN) return scenery;
        //        }
        //    }
        //    return null;
        //}

        //for interpreting the layers and not the object groups
        protected void InterpretLayer(string filename)
        {
            name = filename;
            Map map;
            TMXParser TMXParser = new TMXParser();
            map = TMXParser.Parse(filename);
            Layer[] layer = map.layer;
            
            for (int i = 0; i < layer.Length; i++)
            {
                interpretLayer(layer[i]);
            }
        }

        //interpreting a single layer
        private void interpretLayer(Layer layer)
        {
            string csvData = layer.data.innerXml;
            if(csvData != null)
            {
                string[] lines = csvData.Split('\n');
                for (int j = 0; j < lines.Length; j++)
                {
                    string[] cols = lines[j].Split(',');
                    for (int i = 0; i < cols.Length; i++)
                    {
                        int tile;
                        if (Int32.TryParse(cols[i], out tile))
                        {
                            if (tile != 0)
                            {

                                interpretCell(i * TileSize, (j - 1) * TileSize, tile - 1);
                                tiles.Add(tile);
                            }
                        }
                    }
                }
                csvData = csvData.Replace(Environment.NewLine, "\n");
            }
           
            
        }
        //interpreting a single tile
        private void interpretCell(int x, int y, int frame)
        {
            AddSprite(frame);
            animSprite.SetXY(x, y);

        }
        //adding an animation sprite with the right frame from the level
        private void AddSprite(int frame)
        {
            animSprite = new AnimSprite("tilesheet1.png", 9, 5);
            animSprite.SetFrame(frame);
            AddChild(animSprite);
            background.Add(animSprite);

        }

    }
}
