using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

//game states
public enum GameState {
    Menu,
    Playing,
    Pause
}


namespace Pong {
    public class Pong : Game {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ball;
        Texture2D bluePlayer, redPlayer;
        Texture2D pongArt;

        MouseState mouse;
        Vector2 cursorSpeed, cursorPos, cursorLastPos;

        public int screenWidth, screenHeight;

        // Game settings variables
        public GameManager manager;
        private GameState gameState;


        public Pong() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
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

            //initialise game state
            gameState = new GameState();
            gameState = GameState.Menu;


            //initialise game manager
            manager = new GameManager(3, 5);

            // initialise game
            manager.initialiseGame(screenWidth, screenHeight);

            base.Initialize();
        }



        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            ball = Content.Load<Texture2D>("Sprites/bal");
            bluePlayer = Content.Load<Texture2D>("Sprites/blauweSpeler");
            redPlayer = Content.Load<Texture2D>("Sprites/rodeSpeler");
            pongArt = Content.Load<Texture2D>("Sprites/PONG");

        }



        protected override void Update(GameTime gameTime) {

            // Save current keyboard 'state' and check if player wants to exit
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape)) Exit();


            if (gameState == GameState.Menu) {
                if (state.IsKeyDown(Keys.Space)) {
                    gameState = GameState.Playing;
                }
            }


            if (gameState == GameState.Playing) {
                // Key checks for player 1
                if (state.IsKeyDown(Keys.W)) {
                    manager.playerOne.ChangeVerticalPos(-manager.playerOne.speed);
                }
                if (state.IsKeyDown(Keys.S)) {
                    manager.playerOne.ChangeVerticalPos(manager.playerOne.speed);
                }


                // Key checks for player 2
                if (state.IsKeyDown(Keys.Up)) {
                    manager.playerTwo.ChangeVerticalPos(-manager.playerTwo.speed);
                }
                if (state.IsKeyDown(Keys.Down)) {
                    manager.playerTwo.ChangeVerticalPos(manager.playerTwo.speed);
                }
            }


            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.Beige);
            
            //if game state is 'menu', draw the menu
            if (gameState == GameState.Menu) {
                _spriteBatch.Begin();
                _spriteBatch.Draw(pongArt, new Vector2((screenWidth / 2 - 411), 
                    (screenHeight / 2 - 159)), Color.White);
               // _spriteBatch.DrawString(0,"press space to start", new Vector2 (s)
                _spriteBatch.End();
            }

            // if the game state is 'playing' draw the game
            if (gameState == GameState.Playing) {
                _spriteBatch.Begin();
                _spriteBatch.Draw(bluePlayer, manager.playerOne.GetPos(), Color.White);
                _spriteBatch.Draw(redPlayer, manager.playerTwo.GetPos(), Color.White);
                _spriteBatch.End();
            }
            
 
            base.Draw(gameTime);
        }
    }
}