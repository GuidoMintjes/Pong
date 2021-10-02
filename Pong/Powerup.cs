using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Powerup
    {
        int width, height;
        Vector2 position;
        Rectangle hitBox;
        Texture2D sprite;

        public Rectangle GetBox() {
            return hitBox;
        }

        public Texture2D GetSprite() {
            return sprite;
        }


        public Powerup (Vector2 pos, Texture2D texture){
            position = pos;
            width = Constants.DEFAULTPOWERWIDTH;
            height = Constants.DEFAULTPOWERHEIGHT;
            hitBox = new Rectangle((int)position.X, (int)position.Y, width, height);
            sprite = texture;
        }

        public void InitialisePowerups() {

        }


    }
}
