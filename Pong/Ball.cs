﻿using System;
using Microsoft.Xna.Framework;

namespace Pong {
    public class Ball {

        // Variables related to position, speed, appearance, collider
        private Vector2 position, origin;
        private Vector2 direction = new Vector2(-1, -1);
        private float speed;
        private float speedup = 10F;


        public Vector2 GetPos() {
            return position;
        }
        public Vector2 GetOrigin() {
            return origin;
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
            origin = new Vector2(Constants.DEFAULTBALLWIDTH, Constants.DEFAULTPLAYERHEIGHT) / 2;
        }


        public void MoveBallNormal(float deltaTime, Vector2 screenSize) {

            Vector2 combinedDir = direction * speed * deltaTime;
            Vector2 newPos = GetPos() + combinedDir;


            if (newPos.X >= (screenSize.X - (Constants.DEFAULTBALLWIDTH)) || newPos.X <= 0) {
                direction.X *= -1;
            }

            if (newPos.Y >= (screenSize.Y - (Constants.DEFAULTBALLHEIGHT)) || newPos.Y <= 0) {
                direction.Y *= -1;
            }


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