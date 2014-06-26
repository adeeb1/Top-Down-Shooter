using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //A tile for the Tile Engine
    public sealed class Tile
    {
        //Enum for the types of tiles
        public enum TileTypes : byte
        {
            None, Block, NoNPC, Warp
        };

        //The size of each tile
        public const int TileSize = 16;

        //Tile type
        public int TileType;

        //For convenience purposes; X and Y index in the tile engine
        public int IndexX;
        public int IndexY;

        public Tile()
        {
            TileType = (int)TileTypes.None;
            IndexX = IndexY = 0;
        }

        public Vector2 TileLocation
        {
            get { return new Vector2(IndexX * TileSize, IndexY * TileSize); }
        }
    }
}
