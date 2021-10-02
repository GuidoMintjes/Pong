using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Pong {

    public class Pong : Game {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D ball; 
        Texture2D bluePlayer, redPlayer;
        Texture2D pongArt; //splashscreen

        SpriteFont font, fontBig;

        public int screenWidth, screenHeight;

        // Game settings variables
        public GameManager manager;

        public Pong() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

    protected override void Initialize() {

            // Set screen size variables for later use
            screenHeight = 720; //GraphicsDevice.DisplayMode.Height;
            screenWidth = 1280;  //GraphicsDevice.DisplayMode.Width;


            //Set window screen sizes
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = Convert.ToInt32(screenWidth);
            _graphics.PreferredBackBufferHeight = Convert.ToInt32(screenHeight);
            _graphics.ApplyChanges();

            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            //initialise game manager
            manager = new GameManager(3, 5);

            // initialise game
            manager.InitialiseGame(screenWidth, screenHeight, true, Constants.DEFAULTSCREENSPACEPERC, Content);

            base.Initialize();
        }



        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //ball+player+splashscreen
            ball = Content.Load<Texture2D>("Sprites/bal");
            bluePlayer = Content.Load<Texture2D>("Sprites/blauweSpeler");
            redPlayer = Content.Load<Texture2D>("Sprites/rodeSpeler");
            pongArt = Content.Load<Texture2D>("Sprites/PONG");
        

            //fonts
            font = Content.Load<SpriteFont>("fonts/file");
            fontBig = Content.Load<SpriteFont>("fonts/file2");
        }


        protected override void Update(GameTime gameTime) {

            // Save current keyboard 'state' and check if player wants to exit
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape)) Exit();


            // Save the current delta time
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;


            if (manager.gameState == GameState.Menu) {
                if (state.IsKeyDown(Keys.Space)) {
                    manager.gameState = GameState.Playing;
                }
            }


            if (manager.gameState == GameState.Playing) {
                // Key checks for player 1
                if (state.IsKeyDown(Keys.W)) {
                    manager.MovePlayer(1, -manager.playerOne.speed * deltaTime);
                }
                if (state.IsKeyDown(Keys.S)) {
                    manager.MovePlayer(1, manager.playerOne.speed * deltaTime);
                }


                // Key checks for player 2
                if (state.IsKeyDown(Keys.Up)) {
                    manager.MovePlayer(2, -manager.playerTwo.speed * deltaTime);
                }
                if (state.IsKeyDown(Keys.Down)) {
                    manager.MovePlayer(2, manager.playerTwo.speed * deltaTime);
                }

                // Key to pause game
                if (state.IsKeyDown(Keys.P ))
                {
                    manager.gameState = GameState.Pause;
                    
                }

                //move the ball if playing
                manager.MoveBalls(deltaTime, new Vector2(screenWidth, screenHeight));

                manager.CheckCollisions();

                manager.PowerupsTimer(deltaTime, Content);
            }
             
            if (manager.gameState == GameState.Pause) {
                if (state.IsKeyDown(Keys.Space))
                {
                    manager.gameState = GameState.Playing;
                }
            }

            if (manager.gameState == GameState.End) { 
                
                if (state.IsKeyDown(Keys.Space))
                {
                    RestartGame();
                }
            }

            base.Update(gameTime);
        }


        private void RestartGame() {
            manager.InitialiseGame(screenWidth, screenHeight, false, Constants.DEFAULTSCREENSPACEPERC, Content );

        }

        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.Beige);
            
            //if game state is 'menu', draw the menu
            if (manager.gameState == GameState.Menu) {
                _spriteBatch.Begin();
                _spriteBatch.Draw(pongArt, new Vector2((screenWidth / 2 - 411), 
                    (screenHeight / 2 - 159)), Color.White);
                _spriteBatch.DrawString(font, "Press space to start \nPress P to pause \nPlayer one: W + D \nPlayer two: Up + Down", 
                    new Vector2((screenWidth / 2 - 80), (screenHeight / 2 + 179 )), Color.Black); 
                _spriteBatch.End();
            }

            // if the game state is 'playing' draw the game
            if (manager.gameState == GameState.Playing) {
                _spriteBatch.Begin();
                _spriteBatch.Draw(bluePlayer, manager.playerOne.GetHitBox(), Color.White);
                _spriteBatch.Draw(redPlayer, manager.playerTwo.GetHitBox(), Color.White);
                //_spriteBatch.Draw(ball, manager.ball.GetHitBox(), Color.White);
                manager.DrawPowerups(_spriteBatch);
                manager.DrawBalls(_spriteBatch);
                _spriteBatch.DrawString(fontBig, Convert.ToString(manager.playerOne.GetLives()) , new Vector2(10,10), Color.Black);
                _spriteBatch.DrawString(fontBig, Convert.ToString(manager.playerTwo.GetLives()), new Vector2(screenWidth - 25, 10), Color.Black);
                _spriteBatch.End();
            }

            if (manager.gameState == GameState.End) {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(fontBig, "END OF THE GAME \nWinner: " + manager.GetWinner() + "\n\nPress space to restart \nPress escape to exit",
                    new Vector2(screenWidth / 2 - 120, screenHeight / 2 - 150), Color.Black) ;
                _spriteBatch.End();
            }

            if (manager.gameState == GameState.Pause)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(fontBig, "GAME PAUSED \nPress space to resume \nPress escape to exit",
                    new Vector2(screenWidth / 2 - 120, screenHeight / 2 - 150), Color.Black);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}