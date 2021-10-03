using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Pong {

    public enum Fruit {
        Peer,
        Banaan,
        Appel,
        Druif,
        Kers
    }


    public class Powerup {

        int width, height;
        Vector2 position;
        Rectangle hitBox;
        Texture2D sprite;


        public Fruit Type { get; set; }


        // Getters of power-ups
        public Rectangle GetBox() {
            return hitBox;
        }
        public Texture2D GetSprite() {
            return sprite;
        }


        public Powerup(Vector2 pos, Texture2D texture) {

            position = pos;
            width = Constants.DEFAULTPOWERWIDTH;
            height = Constants.DEFAULTPOWERHEIGHT;
            hitBox = new Rectangle((int)position.X, (int)position.Y, width, height);
            sprite = texture;
        }


        public void DoThing(GameManager manager, Ball ball) {

            switch (Type) {

                case Fruit.Peer:
                    ball.SetSize(2, 2);

                    break;


                case Fruit.Banaan:
                    ball.BounceOffPlayer(1);
                    ball.SetLastHit(0);

                    break;


                case Fruit.Kers:
                    manager.ExtraBall(position);
                    Console.WriteLine("ball gepspawnd op: " + position);

                    break;
            }
        }
    }
}