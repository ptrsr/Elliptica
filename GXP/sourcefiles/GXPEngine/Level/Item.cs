using System;
using System.Collections.Generic;
using GXPEngine;

namespace GXPEngine
{

    public class Item : Sprite
    {
        //all different Item types
        const int ITEMCOUNT = 8;
        //we can use this instead of the cases to determine which item is it
        //for example type_turret = 1 and you can get that in the level
        public const int TYPE_SPAWN = 1;
        public const int TYPE_WALL = 2;
        public const int TYPE_KEY = 3;
        public const int TYPE_NPC = 4;
        public const int TYPE_DOOR = 5;
        public const int TYPE_GEM = 6;
        public const int TYPE_STAR = 7;
        public const int TYPE_COIN = 8;

        //field that stores this items Item type
        private int type;
        //we use an AnimSprite as a graphic (currentFrame==type)
        private AnimSprite gfx;
        //properties can be stored as a Dictionary
        public Dictionary<string, string> properties = new Dictionary<string, string>();

        //object is made from it's hitbox (since it is 2.5D)
        public Item(SingleObject tiledObject) : base("hitbox.png")
        {
            //give the items x and y the value that is in tiled
            x = tiledObject.x;
            y = tiledObject.y;
            //adds graphics to the specific tile from tiled
            addGfx(tiledObject.gid);

            //Storing properties in the dictionary
            if (tiledObject.properties != null)
            {
                foreach (Property p in tiledObject.properties.Property)
                {
                    properties.Add(p.Name, p.Value);
                }
            }
        }



        //adds a visual to this object
        void addGfx(int itemID)
        {
            //making the anim sprite(this is where the spritesheet is put)
            gfx = new AnimSprite("tiles.png", ITEMCOUNT, 1);
            AddChild(gfx);
            SetOrigin(0, height);
            gfx.SetOrigin(0, gfx.height);
            SetItemType(itemID);
            alpha = 0;
        }

        //change item type, also updates gfx.currentFrame
        public void SetItemType(int type)
        {
            if (type > gfx.frameCount) type = 1;
            if (type < 1) type = 1;
            this.type = type;
            gfx.currentFrame = type - 1;
        }

        //returns item type
        public int GetItemType()
        {
            return this.type;
        }

        //returns true if tile is walkable by player
        //we can use this as the collisions if layer 1 true if not false
        public bool IsWalkable()
        {
            if (this.type == TYPE_KEY) return true;
            if (this.type == TYPE_GEM) return true;
            if (this.type > 1) return false;
            return true;
        }
    }
}