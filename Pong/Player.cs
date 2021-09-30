using System;
using Microsoft.Xna.Framework;

namespace Pong {
    public class Player {

        // Variables related to miscellaneous
        private float screenX, screenY;

        // Variables related to padel main usage
        private int padelTeam;
        private int lives;
        private Vector2 position;
        private Rectangle hitBox;

        // Variables related to padel extended usage
        public float speed = 20;    // Also sets default speed
        private bool inverted;
        private int size;


        // Getters and setters to communicate between drawer, game manager and this
        // player class
        private void SetPos(Vector2 pos) {
            this.position = pos; 
        }

        private void SetPadelTeam(int team) {
            padelTeam = team;
        }

        private void SetLives(int livesSetter) {
            lives = livesSetter;
        }


        public int GetLives() {
            return lives;
        }

        public Vector2 GetPos() {
            return position;
        }

        public Rectangle GetHitBox() {
            return hitBox;
        }

        public Player(int team, Vector2 pos, int startLives, Vector2 screenSize) {

            SetPos(pos);
            SetPadelTeam(team);
            SetLives(startLives);
            hitBox = new Rectangle((int)pos.X, (int) pos.Y, Constants.DEFAULTPLAYERWIDTH, Constants.DEFAULTPLAYERHEIGHT);

            screenX = screenSize.X;
            screenY = screenSize.Y;
        }

        // Change the amount of player lives outside this class
        public void ChangeLives(int amt) {
            SetLives(GetLives() + amt);
        }


        // Change the vertical position of this player
        public void ChangeVerticalPos(float amt) {

            float posY = GetPos().Y + amt;
            posY = Math.Clamp(posY, 0, (screenY - Constants.DEFAULTPLAYERHEIGHT));
            hitBox.Y = (int)posY;
            SetPos(new Vector2(GetPos().X, posY));
        }
    }
}