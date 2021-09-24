using System;
using Microsoft.Xna.Framework;

namespace Pong {
    public class GameManager {

        // Game setting variables
        public int startLives;
        public int percSpaceToPlayer;

        private int screenWidth, screenHeight;

        // Create the players
        public Player playerOne = new Player();
        public Player playerTwo = new Player();

        public GameManager() {
            //Set game settings
            Console.Write("Please choose your desired game settings:\n");


            Console.Write("Percentage player space: ");
            percSpaceToPlayer = Console.Read();

            Console.Write("Amount of Lives: ");
            startLives = Console.Read();

        }


        public Vector2 calcPlayerStartPos(int playerTeam) {

            int playerY = screenHeight / 2;
            int playerX;

            switch (playerTeam) {

                case 1:
                    playerX = screenWidth * (percSpaceToPlayer / 100);
                    break;

                case 2:
                    playerX = screenWidth - (screenWidth * (percSpaceToPlayer / 100));
                    break;

                default:
                    playerX = 0;
                    break;
            }


            return new Vector2(playerX, playerY);
        }

        public void initialiseGame(int width, int height) {
            screenWidth = width; screenHeight = height;

            playerOne.initPlayer(0, calcPlayerStartPos(1), 0);
            playerTwo.initPlayer(0, calcPlayerStartPos(2), 0);
        }
    }
}