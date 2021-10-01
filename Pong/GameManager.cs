﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong {
    //game states
    public enum GameState
    {
        Menu,
        Playing,
        Pause,
        End
    }

    public class GameManager {

        // Game setting variables
        public int startLives;
        public int percSpaceToPlayer;

        public Vector2 ballStartPos;
        public float ballDefaultSpeed = 250f;

        private int screenWidth, screenHeight;

        // Create the players and ball
        public Player playerOne;
        public Player playerTwo;
        public Ball ball;
        
        //miscellanious
        public ScoreObject scoreOne;
        public ScoreObject scoreTwo;
        private string winner;

        private int lastHit;

        static Random rng = new Random();
        float counter;
        int timer = rng.Next(0,10);

        //gamestate
        public GameState gameState { get; set; }

        public GameManager(int startLivesParam, int percSpace = 20) {
            
            //Set game settings
            startLives = startLivesParam;
            percSpaceToPlayer = percSpace;
        }

        public string GetWinner()
        {
            return winner;
        }

        public Vector2 calcPlayerStartPos(int playerTeam) {

            float playerY = (screenHeight / 2) - 48;
            float playerX = 0;

            if (playerTeam == 1) {
                playerX = screenWidth * (percSpaceToPlayer / 100f);
            } else if (playerTeam == 2) {
                playerX = screenWidth - (screenWidth * (percSpaceToPlayer / 100f));
            }
            
            return new Vector2(playerX, playerY);
        }

        private Vector2 generateDirection()
        {
            Vector2 dir = new Vector2(0,0);
            switch (rng.Next(1, 5))
            {
                case 1:
                    dir = new Vector2(1, 1); //right down
                    break;
                case 2:
                    dir = new Vector2(-1, 1); //left down
                    break;
                case 3:
                    dir = new Vector2(1, -1); //right up
                    break;
                case 4:
                    dir = new Vector2(-1, -1); //left up
                    break;
            }
            return dir;
        }

        //generates a random position on screen
        private Vector2 GeneratePosition() {

            return new Vector2(rng.Next(0, screenWidth), rng.Next(0, screenHeight));
        }


        // Moves the correct player
        public void MovePlayer(int team, float deltaTime) {
            switch(team) {
                case 1:
                    playerOne.ChangeVerticalPos(playerOne.speed * deltaTime);   // Check case 2 comm.
                    break;

                case 2:
                    playerTwo.ChangeVerticalPos(playerTwo.speed * deltaTime);   // Multiply with
                                                            // deltatime to avoid framerate
                                                            // dependant speeds
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// This function initializes the game and properly assigns class instances to all
        /// declared classes.
        /// </summary>
        /// <param name="width">Get the width of the window to use in calculation</param>
        /// <param name="height">Get the width of the window to use in calculation</param>
        /// <param name="defaultBallPos">Tell whether to use default ball position or not</param>
        public void InitialiseGame(int width, int height, bool defaultBallPos, int percScreenSpace) {
            screenWidth = width; screenHeight = height;

            playerOne = new Player(1, calcPlayerStartPos(1), startLives,
                                    new Vector2(width, height));
            playerTwo = new Player(2, calcPlayerStartPos(2), startLives,
                                    new Vector2(width, height));

            if (defaultBallPos) {
                ballStartPos = new Vector2((width / 2) - Constants.DEFAULTBALLWIDTH,
                    (height / 2) - Constants.DEFAULTBALLHEIGHT);
            }  // Calculate middle of screen to spawn ball,
                                                            // keeping in mind ball dimensions

            ball = new Ball(ballStartPos, ballDefaultSpeed);

            scoreOne = new ScoreObject(percScreenSpace, screenWidth);
            scoreTwo = new ScoreObject(percScreenSpace, screenWidth);

            gameState = new GameState();
            gameState = GameState.Menu;
        }

        public void PowerupsTimer(float time) {
            counter += time;

            if (counter >= timer) {
                SpawnPowerups();
                counter = 0;
                timer = rng.Next(0, 10);
    
            }
        }

        private void SpawnPowerups() {
            int num = rng.Next(1, 1);

            switch (num) {

                case 1:
                    Powerup peer = new Powerup(GeneratePosition());
                    break;

            }

        //public void draw(GameTime gametime, SpriteBatch spriteBatch);
        }



        public void CheckCollisions()
        {
            Vector2 ballPos = ball.GetPos();
            Rectangle ballBox = ball.GetHitBox();
            Rectangle playerOneBox = playerOne.GetHitBox();
            Rectangle playerTwoBox = playerTwo.GetHitBox();
            
            if (CheckCollision(ballBox, playerOneBox) && lastHit != 1) {
                ball.BounceOffPlayer(1);
                lastHit = 1;
            }

            if (CheckCollision(ballBox, playerTwoBox) && lastHit != 2) {
                ball.BounceOffPlayer(1);
                lastHit = 2;
            }

            // Check if ball is hitting score object on left side
            if (ballPos.X < scoreOne.GetSSX())
            {
                Console.WriteLine("Right player scores!");
                Score(2);
            }

            // Check if ball is hitting score object on right side
            if ((ballPos.X + Constants.DEFAULTBALLWIDTH) > (screenWidth - scoreOne.GetSSX()))
            {
                Console.WriteLine("Left player scores!");
                Score(1);
            }
        }

        private bool CheckCollision (Rectangle obj1, Rectangle obj2) {

            return obj1.Left < obj2.Right &&
                    obj1.Right > obj2.Left &&
                    obj1.Top < obj2.Bottom &&
                    obj1.Bottom > obj2.Top;

        }

        private void Score(int team)
        {
            switch (team)
            {
                case 1:
                    playerTwo.ChangeLives(-1);
                    break;

                case 2:
                    playerOne.ChangeLives(-1);
                    break;

                default:
                    break;
            }

            ball.Respawn(ballStartPos, ballDefaultSpeed, generateDirection());
            lastHit = 0;

            if (playerOne.GetLives() <= 0)
            {
                gameState = GameState.End;
                winner = "Player two";
            }
            else if (playerTwo.GetLives() <= 0)
            {
                gameState = GameState.End;
                winner = "Player one";
            }
        }

        public void RestartGame() { 
            InitialiseGame(screenWidth, screenHeight, false, Constants.DEFAULTSCREENSPACEPERC);

        }
    }
}