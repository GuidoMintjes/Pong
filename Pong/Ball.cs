using System;
using Microsoft.Xna.Framework;

namespace Pong {
    public class Ball {

        // Variables related to position, speed, appearance, collider
        private Vector2 position;
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
    }
}