using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class PowerupManager
    {
        int width, height;
        public Vector2 pos;

        public PowerupManager (){
                
            }

        public void InitialisePowerup() {
            width = Constants.DEFAULTPOWERWIDTH;
            height = Constants.DEFAULTPOWERHEIGHT;
            


        }


    }
}
