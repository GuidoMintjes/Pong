using System;
using Microsoft.Xna.Framework;

namespace Pong {
    public class Ball {

        // Variables related to position, speed, appearance, collider
        private Vector2 position;
        private Vector2 direction = new Vector2(1, -1);
        private float speed;


        public Vector2 GetPos() {
            return position;
        }


        public void SetPos(Vector2 newPos) {
            position = newPos;
        }

        public void SetSpeed(float newSpeed) {
            speed = newSpeed;
        }


        public Ball(Vector2 startPos, float defaultSpeed) {

            position = startPos;
            speed = defaultSpeed;
        }


        public void MoveBallNormal(float deltaTime, Vector2 screenSize) {

            Console.WriteLine(position);

            Vector2 combinedDir = direction * speed * deltaTime;
            Vector2 newPos = GetPos() + combinedDir;

            if(newPos.X >= screenSize.X || newPos.X <= 0) {
                direction.X *= -1;

                combinedDir = direction * speed * deltaTime;
                newPos = GetPos() + combinedDir;
            }

            if(newPos.Y >= screenSize.Y || newPos.Y <= 0) {
                direction.Y *= -1;

                combinedDir = direction * speed * deltaTime;
                newPos = GetPos() + combinedDir;
            }

            

            SetPos(newPos);
        }
    }
}