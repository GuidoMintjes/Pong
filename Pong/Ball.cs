using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Pong {

    public class Ball {

        // Variables related to position, speed, appearance, collider
        private Vector2 position;
        private Vector2 direction = new Vector2(-1, -1);
        private float speed;
        private float speedup = 2F;
        private Rectangle hitBox;
        private int lastWallHit;
        private int lastHit;
        private Texture2D sprite;


        // Ball getters
        public Vector2 GetPos() {
            return position;
        }
        public Rectangle GetHitBox() {
            return hitBox;
        }
        public int GetLastHit() {
            return lastHit;
        }
        public Texture2D GetSprite() {
            return sprite;
        }


        // Ball setters
        public void SetSize(int width, int height) {
            hitBox.Width *= width;
            hitBox.Height *= height;
        }
        public void SetPos(Vector2 newPos) {
            position = newPos;
        }
        public void SetSpeed(float newSpeed) {
            speed = newSpeed;
        }
        public void SetDirection(Vector2 newDir) {
            direction = newDir;
        }
        public void SetLastHit(int hit) {
            lastHit = hit;
        }

        public Ball(Vector2 startPos, float defaultSpeed, Texture2D sprite) {

            position = startPos;
            speed = defaultSpeed;
            hitBox = new Rectangle((int)position.X, (int)position.Y, Constants.DEFAULTBALLWIDTH, Constants.DEFAULTBALLHEIGHT);
            this.sprite = sprite;
        }


        public void MoveBallNormal(float deltaTime, Vector2 screenSize) {

            Vector2 combinedDir = direction * speed * deltaTime;
            Vector2 newPos = GetPos() + combinedDir;

            if (newPos.Y >= (screenSize.Y - (hitBox.Height)) && lastWallHit != 2 )  {
                direction.Y *= -1;
                lastWallHit = 2;
            }
            
            if (newPos.Y <= 0 && lastWallHit != 1) {
                direction.Y *= -1;
                lastWallHit = 1;
            }

            hitBox.X = (int)newPos.X;
            hitBox.Y = (int)newPos.Y;

            speed += speedup * deltaTime;

            SetPos(newPos);
        }


        public void BounceOffPlayer(int directionCheck, Player player = null) {

            float bounceDiff = 0 ;

            if (player != null) {
                Rectangle playerBox = player.GetHitBox();
                float ballCenter = (position.Y  + hitBox.Height / 2);
                float playerCenter = (playerBox.Y + playerBox.Height / 2);

                bounceDiff = Math.Abs(ballCenter - playerCenter) % 1;

                //Vector2 diff = new Vector2(Math.Abs(position.X - playerPos.X), Math.Abs(position.Y - playerPos.Y));
                //bounceAngle = Math.At

            }

            if (directionCheck == 1) {

                direction.X *= -1f;
                direction.Y *=   1 + bounceDiff;

            } else if (directionCheck == 2) {

                direction.Y *= -1f;
            }
        }

        //old bounce with randomizer
        /*
        public void BounceOffPlayer(int directionCheck) {

            if(directionCheck == 1) {

                if (new Random().Next(1, 5) == 3) {

                    if(direction.X < -direction.X) {

                        direction.X = 1;
                    } else {

                        direction.X = -1;
                    }
                } else {

                    int randomnessX = new Random().Next(70, 200);
                    float randomnessXF = randomnessX / 100f;

                    direction.X *= -1f * randomnessXF;
                }

            } else if (directionCheck == 2) {

                direction.Y *= -1f;
            }
        }
        */

        public void Despawn(List<Ball> list ) {
            int ballIndex = list.IndexOf(this);
            list.RemoveAt(ballIndex);

        }


        public void Respawn(Vector2 pos, float newSpeed, Vector2 newDirection) {
            position = pos;
            speed = newSpeed;
            direction = newDirection;
            lastWallHit = 0;
            hitBox.Width = Constants.DEFAULTBALLWIDTH;
            hitBox.Height = Constants.DEFAULTBALLHEIGHT;
            lastHit = 0;
        }
    }
}