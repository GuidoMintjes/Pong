using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong {
    public class Pong : Game {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ball;
        Texture2D bluePlayer, redPlayer;

        MouseState mouse;
        Vector2 cursorSpeed, cursorPos, cursorLastPos;

        public int screenWidth, screenHeight;

        // Game settings variables
        public GameManager manager = new GameManager();


        public Pong() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }



        protected override void Initialize() {

            // Set screen size variables for later use
            screenHeight = GraphicsDevice.DisplayMode.Height;
            screenWidth = GraphicsDevice.DisplayMode.Width;

            //initialise game
            manager.initialiseGame(screenWidth, screenHeight);

            //Set window screen sizes
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = Convert.ToInt32(screenWidth * 0.9);
            _graphics.PreferredBackBufferHeight = Convert.ToInt32(screenHeight * 0.9);
            _graphics.ApplyChanges();

            base.Initialize();
        }



        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            ball = Content.Load<Texture2D>("Sprites/bal");
            bluePlayer = Content.Load<Texture2D>("Sprites/blauweSpeler");
            redPlayer = Content.Load<Texture2D>("Sprites/rodeSpeler");

        }



        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Gets the state of the mouse, allowing us to get its location
            mouse = Mouse.GetState();

            cursorPos = new Vector2(mouse.X, mouse.Y);
            if (cursorLastPos == new Vector2(0, 0)) { base.Update(gameTime); } 
            else {

                cursorSpeed = new Vector2(Math.Abs(cursorPos.X - cursorLastPos.X),
                    Math.Abs(cursorPos.Y - cursorLastPos.Y));

                /*Console.WriteLine("Speed of cursor is: \tX-axis:" +
                    cursorSpeed.X + ", \tY-axis:" + cursorSpeed.Y);*/
            }

            cursorLastPos = cursorPos;
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.Transparent);
            

            _spriteBatch.Begin();
            _spriteBatch.Draw(ball, new Vector2(mouse.X, mouse.Y), Color.White);
            //_spriteBatch.Draw(bluePlayer, playerOne.GetPos(), Color.White);
            //_spriteBatch.Draw(redPlayer, playerTwo.GetPos(), Color.White);
            _spriteBatch.End();
 
            base.Draw(gameTime);
        }
    }
}