using System;
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

        double angle;
        double inObjDistance;

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

        public ScoreObject scoreOne;
        public ScoreObject scoreTwo;
        private string winner;

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

        public Vector2 generateDirection()
        {
            Vector2 dir = new Vector2(0,0);
            Random rng = new Random();
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

            if (defaultBallPos)
                ballStartPos = new Vector2(300, 300);
                    
                    //new Vector2((width / 2) - Constants.DEFAULTBALLWIDTH, 
                    //(height / 2) - Constants.DEFAULTBALLHEIGHT);   // Calculate middle of screen to spawn ball,
                                                            // keeping in mind ball dimensions

            ball = new Ball(ballStartPos, ballDefaultSpeed);

            scoreOne = new ScoreObject(percScreenSpace, screenWidth);
            scoreTwo = new ScoreObject(percScreenSpace, screenWidth);

            gameState = new GameState();
            gameState = GameState.Menu;
        }

        public void CheckCollision()
        {

            Vector2 ballPos = ball.GetPos();
            Vector2 ballOrigin = ball.GetOrigin();
            Vector2 playerOnePos = playerOne.GetPos();
            Vector2 playerOrigin = playerOne.GetOrigin();
            Vector2 playerTwoPos = playerTwo.GetPos();

            Vector2 ballCenter = ballPos + ballOrigin;
            Vector2 playerOneCenter = playerOnePos + playerOrigin;
            Vector2 playerTwoCenter = playerTwoPos + playerOrigin;


            double dis1 = (calcDistance(ballCenter, playerOneCenter, playerOrigin));
            bool bounce = (dis1 <= 0);

            //check if ball is hitting player1
            if (bounce) {
                ball.BounceOffPlayer(1);
                Console.WriteLine("Distance p1 is " + dis1 + "\n" + bounce);
                Console.WriteLine("inobj is " + inObjDistance);
                Console.WriteLine("angle is " + angle);
                Console.WriteLine("math cos " + Math.Cos(angle));
            }

            //check if ball is hitting player2
            if (calcDistance(ballCenter, playerTwoCenter, playerOrigin) <= 0) {
                ball.BounceOffPlayer(1);
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

        public double calcDistance(Vector2 posBal, Vector2 posObj, Vector2 originObj) {


            float disX;
            float disY;
            //total X&Y distance
            if (posBal.X > posObj.X ) {
                disX = posBal.X - posObj.X;
                Console.WriteLine(disX + " disX");
            }   
            else { 
                disX = posObj.X - posBal.X;
                Console.WriteLine(disX + " disX");
            }
            disY = Math.Abs(posBal.Y - posObj.Y);

            //Angle of object that is being compared with ball
            this.angle = Math.Atan2(disY, disX);
   

            //total distance from center to center of ball & object
            double disTotal = Math.Sqrt(disX*disX + disY*disY);
            

            //part of the distance that is inside the object
            if (angle > 1.5 ) {
                this.inObjDistance = 48;
            }
            else if (Math.Cos(angle) <= 0.06) {
                this.inObjDistance = 8;
            }
            else {
                this.inObjDistance = (originObj.X / Math.Cos(angle));
            }

            //distance between the objects (ball is circle so radius can be used)
            double dis = disTotal - (float)inObjDistance;// - ball.GetOrigin().X;

            Console.WriteLine(inObjDistance + " inobj");
            Console.WriteLine(disTotal + " total");
            Console.WriteLine(dis + "goede dis \n");

            return dis;  

        }




        public void Score(int team)
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

        public void RestartGame() { 
            InitialiseGame(screenWidth, screenHeight, false, Constants.DEFAULTSCREENSPACEPERC);

        }
    }
}