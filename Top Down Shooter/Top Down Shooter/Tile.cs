using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public TileTypes TileType;

        //For convenience purposes; X and Y index in the tile engine
        public int IndexX;
        public int IndexY;

        public Tile()
        {
            TileType = TileTypes.None;
            IndexX = IndexY = 0;
        }

        public Tile(int indexx, int indexy) : this()
        {
            IndexX = indexx;
            IndexY = indexy;
        }

        public Tile(TileTypes tiletype, int indexx, int indexy) : this(indexx, indexy)
        {
            TileType = tiletype;
        }

        public Vector2 TileLocation
        {
            get { return new Vector2(IndexX * TileSize, IndexY * TileSize); }
        }

        public Vector2 TileCenter
        {
            get { return TileLocation + new Vector2(TileSize / 2); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tilelocation = TileLocation;

            spriteBatch.Draw(LoadAssets.ScalableBox, tilelocation, null, Color.Black, 0f, Vector2.Zero, new Vector2(TileSize, 1), SpriteEffects.None, .998f);
            spriteBatch.Draw(LoadAssets.ScalableBox, new Vector2(tilelocation.X, tilelocation.Y + 1), null, Color.Black, 0f, Vector2.Zero, new Vector2(1, TileSize - 1), SpriteEffects.None, .998f);

            String text = "N";
            Color color = Color.White;

            switch(TileType)
            {
                case TileTypes.Block: text = "B";
                    color = Color.Red;
                    break;
                case TileTypes.NoNPC: text = "NN";
                    color = Color.Gray;
                    break;
                case TileTypes.Warp: text = "W";
                    color = Color.Purple;
                    break;
            }

            Vector2 stringsize = LoadAssets.MenuFont.MeasureString(text) * .5f;
            spriteBatch.DrawString(LoadAssets.MenuFont, text, TileCenter, color, 0f, new Vector2(stringsize.X / 2, stringsize.Y / 2), .5f, SpriteEffects.None, .998f);
        }
    }
}
