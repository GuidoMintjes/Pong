using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong

{
    public enum Fruit {
        Peer,
        Banaan,
        Appel,
        Druif,
        Kers
    }

    public class Powerup
    {

        int width, height;
        Vector2 position;
        Rectangle hitBox;
        Texture2D sprite;
        public Fruit Type { get; set; }

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

        public void DoThing(GameManager manager) {
            switch (Type) {

                case Fruit.Peer:
                    manager.ball.SetSize(2, 2);
                    break;

                case Fruit.Banaan:
                    manager.ball.BounceOffPlayer(1);
                    break;

                case Fruit.Kers:
                    manager.ExtraBall(position);
                    Console.WriteLine("ball gepspawnd op: " + position);
                    break;
            }

        }

    }
}
