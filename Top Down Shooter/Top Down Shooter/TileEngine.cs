using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //The tile engine for the game; it contains tiles with properties
    public sealed class TileEngine
    {
        //The tiles
        public Tile[][] Tiles;

        public TileEngine(int numx, int numy)
        {
            Tiles = new Tile[numx][];
            for (int i = 0; i < Tiles.Length; i++) Tiles[i] = new Tile[numy];
        }

        //Creates tiles based on a level size
        public TileEngine(Vector2 LevelSize) : this((int)LevelSize.X / Tile.TileSize, (int)LevelSize.Y / Tile.TileSize)
        {

        }

        public Tile this[int x, int y]
        {
            get { return Tiles[x][y]; }
        }

        //Gets the tile an object is on
        public Tile GetTile(Vector2 objectpos)
        {
            return this[(int)objectpos.X / Tile.TileSize, (int)objectpos.Y / Tile.TileSize];
        }

        //Checks for a tile in the direction the object is moving
        //NOTE: This may be changed later to be more accurate
        public Tile CheckNextTile(Rectangle feetloc, Vector2 velocity)
        {
            Vector2 truefeetloc = new Vector2(feetloc.X, feetloc.Y);

            if (velocity.X > 0) truefeetloc.X += feetloc.Width;
            if (velocity.Y > 0) truefeetloc.Y += feetloc.Height;

            return GetTile(truefeetloc);
        }
    }
}
