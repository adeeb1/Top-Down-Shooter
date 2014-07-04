using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tiles[i] = new Tile[numy];

                for (int j = 0; j < Tiles[i].Length; j++)
                {
                    Tiles[i][j] = new Tile(i, j);
                }
            }
            Tiles[7][8] = new Tile(Tile.TileTypes.Block, 7, 8);
        }

        //Creates tiles based on a level size
        public TileEngine(Vector2 LevelSize) : this((int)LevelSize.X / Tile.TileSize, (int)LevelSize.Y / Tile.TileSize)
        {

        }

        public Tile this[int x, int y]
        {
            get 
            {
                if (x > 0 && y > 0 && x < Tiles.Length && y < Tiles[x].Length) return Tiles[x][y];
                else return new Tile();
            }
        }

        //The minimum tile in the X direction
        public int MinTilesX
        {
            get { return 0; }
        }

        //The maximum tile in the X direction
        public int MaxTilesX
        {
            get { return Tiles.Length - 1; }
        }

        //The minimum tile in the Y direction
        public int MinTilesY
        {
            get { return 0; }
        }

        //The maximum tile in the Y direction
        public int MaxTilesY
        {
            get { return Tiles[0].Length - 1; }
        }

        //Gets the tile an object is on
        public Tile GetTile(Vector2 objectpos)
        {
            return this[(int)objectpos.X / Tile.TileSize, (int)objectpos.Y / Tile.TileSize];
        }

        //Checks for a tile in the direction the object is moving
        public Tile CheckNextTile(Rectangle feetloc, Vector2 velocity)
        {
            Vector2 truefeetloc = new Vector2(feetloc.X + (int)velocity.X, feetloc.Y + (int)velocity.Y);

            if (velocity.X > 0) truefeetloc.X += feetloc.Width;
            if (velocity.Y > 0) truefeetloc.Y += feetloc.Height;

            return GetTile(truefeetloc);
        }

        //Checks for all the tiles an object is touching
        public List<Tile> CheckCollidingTiles(Rectangle feetloc, Vector2 velocity)
        {
            feetloc.X += (int)velocity.X;
            feetloc.Y += (int)velocity.Y;

            //The list of tiles we touched
            List<Tile> tilestouching = new List<Tile>();

            int increasex = Tile.TileSize;

            //Loop through the width of the feet rectangle, increasing by the tile size each time until hitting the right of the rectangle
            for (int i = feetloc.X; ; i += increasex)
            {
                int increasey = Tile.TileSize;

                //Loop through the height of the feet rectangle, increasing by the tile size each time until hitting the bottom of the rectangle
                for (int j = feetloc.Y; ; j += increasey)
                {
                    //Check the tile at this location and see if it's in our tile list; if not, add it
                    Tile tile = GetTile(new Vector2(i, j));
                    if (tilestouching.Contains(tile) == false)
                        tilestouching.Add(tile);

                    //This is the bottom of the rectangle; break
                    if (j == feetloc.Bottom) break;
                    else if ((j + Tile.TileSize) > feetloc.Bottom) increasey = (feetloc.Bottom - j);
                }

                //This is the right of the rectangle; break
                if (i == feetloc.Right) break;
                else if ((i + Tile.TileSize) > feetloc.Right) increasex = (feetloc.Right - i);
            }

            return tilestouching;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                for (int j = 0; j < Tiles[i].Length; j++)
                {
                    Tiles[i][j].Draw(spriteBatch);
                }
            }
        }
    }
}
