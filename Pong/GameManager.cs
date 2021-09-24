using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong {
    public class GameManager {

        // Game setting variables
        public int startLives;
        public int percSpaceToPlayer;

        private int screenWidth, screenHeight;

        // Create the players
        public Player playerOne;
        public Player playerTwo;

        public GameManager(int startLivesParam, int percSpace = 20) {
            //Set game settings
            startLives = startLivesParam;
            percSpaceToPlayer = percSpace;
        }


        public Vector2 calcPlayerStartPos(int playerTeam) {

            float playerY = (screenHeight / 2) - 48;
            float playerX = 0;

            Console.WriteLine(screenWidth);

            if (playerTeam == 1) {
                playerX = screenWidth * (percSpaceToPlayer / 100f);
            } else if (playerTeam == 2) {
                playerX = screenWidth - (screenWidth * (percSpaceToPlayer / 100f));
            }
            
            Console.WriteLine("\n" + playerX + " " + playerY);
            return new Vector2(playerX, playerY);
        }

        public void initialiseGame(int width, int height) {
            screenWidth = width; screenHeight = height;

            playerOne = new Player(1, calcPlayerStartPos(1), startLives,
                                    new Vector2(width, height));
            playerTwo = new Player(2, calcPlayerStartPos(2), startLives,
                                    new Vector2(width, height));
        }
    }
}