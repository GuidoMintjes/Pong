using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class Powerup
    {
        int width, height;
        Vector2 position;
        Rectangle hitBox;

        public Vector2 GetPos() {
            return position;
        }


        public Powerup (Vector2 pos){
            position = pos;
            width = Constants.DEFAULTPOWERWIDTH;
            height = Constants.DEFAULTPOWERHEIGHT;
            hitBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void InitialisePowerups() {

        }


    }
}
