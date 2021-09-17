using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ball;

        MouseState mouse;
        Vector2 cursorSpeed, cursorPos, cursorLastPos;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("Sprites/bal");
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

                Console.WriteLine("Speed of cursor is: \tX-axis:" +
                    cursorSpeed.X + ", \tY-axis:" + cursorSpeed.Y);
            }

            cursorLastPos = cursorPos;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            int redColour = gameTime.TotalGameTime.Milliseconds / 4;
            GraphicsDevice.Clear(new Color(redColour, 0, 0));

            _spriteBatch.Begin();
            _spriteBatch.Draw(ball, new Vector2(mouse.X, mouse.Y), Color.White);
            _spriteBatch.End();


            
            base.Draw(gameTime);
        }
    }
}
