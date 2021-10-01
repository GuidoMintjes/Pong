using System;
using Microsoft.Xna.Framework;

namespace Pong {
    public class Ball {

        // Variables related to position, speed, appearance, collider
        private Vector2 position;
        private Vector2 direction = new Vector2(-1, -1);
        private float speed;
        private float speedup = 10F;
        private Rectangle hitBox; 

        public Vector2 GetPos() {
            return position;
        }
        public Rectangle GetHitBox() {
            return hitBox;
        }

        public void SetPos(Vector2 newPos) {
            position = newPos;
        }

        public void SetSpeed(float newSpeed) {
            speed = newSpeed;
        }

        public void SetDirection(Vector2 newDir)
        {
            direction = newDir;
        }

        public Ball(Vector2 startPos, float defaultSpeed) {

            position = startPos;
            speed = defaultSpeed;
            hitBox = new Rectangle((int)position.X, (int)position.Y, Constants.DEFAULTBALLWIDTH, Constants.DEFAULTBALLHEIGHT);
        }


        public void MoveBallNormal(float deltaTime, Vector2 screenSize) {

            Vector2 combinedDir = direction * speed * deltaTime;
            Vector2 newPos = GetPos() + combinedDir;

            if (newPos.Y >= (screenSize.Y - (Constants.DEFAULTBALLHEIGHT)) || newPos.Y <= 0) {
                direction.Y *= -1;
            }

            hitBox.X = (int)newPos.X;
            hitBox.Y = (int)newPos.Y;

            speed += speedup * deltaTime;

            SetPos(newPos);
        }

        public void BounceOffPlayer(int directionn) {
            if  (directionn == 1) {
                direction.X *= -1;
            } else if (directionn == 2) {
                direction.Y *= -1;
            }
        }


        public void Respawn(Vector2 pos, float newSpeed, Vector2 newDirection) {
            position = pos;
            speed = newSpeed;
            direction = newDirection;
         }
    }
}