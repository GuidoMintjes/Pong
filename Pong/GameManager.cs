using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
//using System.Collections;
using System.Collections.Generic;


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
        List<Ball> ballList = new List<Ball>();

        //score
        public ScoreObject scoreOne;
        public ScoreObject scoreTwo;
        private string winner;

        //powerups
        List<Powerup> powerupsList = new List<Powerup>();
        bool firstPower = true;
        
        //misc
        static Random rng = new Random();
        float counter;
        int timer = rng.Next(1,1);

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

            return new Vector2(rng.Next(0, screenWidth - 64 ), rng.Next(0, screenHeight - 64));
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
        public void InitialiseGame(int width, int height, bool defaultBallPos, int percScreenSpace, ContentManager Content) {
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

            ball = new Ball(ballStartPos, ballDefaultSpeed, Content.Load<Texture2D>("Sprites/bal"));
            ballList.Add(ball);

            scoreOne = new ScoreObject(percScreenSpace, screenWidth);
            scoreTwo = new ScoreObject(percScreenSpace, screenWidth);

            gameState = new GameState();
            gameState = GameState.Menu;

        }

        public void MoveBalls(float time, Vector2 screensize) {
            foreach (Ball b in ballList) {
                b.MoveBallNormal(time, screensize);

            }
        }

        public void ExtraBall(Vector2 pos) {
            Ball extraBall = new Ball(pos, ballDefaultSpeed, ball.GetSprite());
            extraBall.SetDirection(generateDirection());
            ballList.Add(extraBall);

        }

        public void PowerupsTimer(float time, ContentManager content) {
            counter += time;

            if (firstPower) {
                Powerup blank = new Powerup(new Vector2(-100, -100), content.Load<Texture2D>("Sprites/pixel"));
                powerupsList.Add(blank);
                timer = rng.Next(3, 10);
                firstPower = false;
            }

            if (counter >= timer) {
                SpawnPowerups(content);
                counter = 0;
                timer = rng.Next(3, 10);
    
            }
        }

        private void SpawnPowerups(ContentManager Content) {
            int num = rng.Next(2, 4);

            switch (num) {

                case 1:
                    Powerup peer = new Powerup(GeneratePosition(), Content.Load<Texture2D>("Sprites/Peer"));
                    peer.Type = Fruit.Peer;
                    powerupsList.Add (peer);
                    break;

                case 2:
                    Powerup banaan = new Powerup(GeneratePosition(), Content.Load<Texture2D>("Sprites/Banaan"));
                    banaan.Type = Fruit.Banaan;
                    powerupsList.Add(banaan);
                    break;

                case 3:
                    Powerup kers = new Powerup(GeneratePosition(), Content.Load<Texture2D>("Sprites/Kerspng"));
                    kers.Type = Fruit.Kers;
                    powerupsList.Add(kers);
                    break;
            }

        }

        public void DrawPowerups(SpriteBatch spriteBatch) {
            foreach (Powerup p in powerupsList) {
                spriteBatch.Draw(p.GetSprite(), p.GetBox(), Color.White);

            }
        }

        public void DrawBalls(SpriteBatch spriteBatch) {
            foreach (Ball b in ballList) {
                spriteBatch.Draw(b.GetSprite(), b.GetHitBox(), Color.White);

            }
        }

        public void CheckCollisions() {

            Rectangle playerOneBox = playerOne.GetHitBox();
            Rectangle playerTwoBox = playerTwo.GetHitBox();
            int hitIndex = 0;

            foreach (Ball b in ballList) {

                Rectangle ballBox = ball.GetHitBox();

                if (CheckCollision(ballBox, playerOneBox) && b.GetLastHit() != 1) {
                    ball.BounceOffPlayer(1);
                    b.SetLastHit(1);
                }

                if (CheckCollision(ballBox, playerTwoBox) && b.GetLastHit() != 2) {
                    ball.BounceOffPlayer(1);
                    b.SetLastHit(2);
                }

                //check collisions with powerups
                foreach (Powerup p in powerupsList) {
                    if (CheckCollision(ballBox, p.GetBox())) {
                        //p.DoThing(this);
                        Console.WriteLine("hij is geraakt");
                        hitIndex = powerupsList.IndexOf(p);
                    }
                }
                //if (hitIndex != 0) {
                  //  powerupsList.RemoveAt(hitIndex);
                    //hitIndex = 0;
                //}


                // Check if ball is hitting score object on left side
                if (ballBox.X < scoreOne.GetSSX()) {
                    Console.WriteLine("Right player scores!");
                    Score(2);
                }

                // Check if ball is hitting score object on right side
                if ((ballBox.X + Constants.DEFAULTBALLWIDTH) > (screenWidth - scoreOne.GetSSX())) {
                    Console.WriteLine("Left player scores!");
                    Score(1);
                }
            }

            if (hitIndex != 0) {
                powerupsList[hitIndex].DoThing(this);
                Console.WriteLine("doet ding");
                powerupsList.RemoveAt(hitIndex);
                hitIndex = 0;
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

    }
}