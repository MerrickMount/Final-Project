using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D background;
        Texture2D bot;
        Rectangle tribbleRect;

        Vector2 tribblespeed1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.ApplyChanges();
            tribbleRect = new Rectangle(500, 10, 100, 100);
            tribblespeed1 = new Vector2(-2, -2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");
            bot = Content.Load<Texture2D>("bot");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            tribbleRect.X += (int)tribblespeed1.X;
            if (tribbleRect.Right > _graphics.PreferredBackBufferWidth || tribbleRect.X < 30)
                tribblespeed1.X *= -1;

            tribbleRect.Y += (int)tribblespeed1.Y;
            if (tribbleRect.Bottom > _graphics.PreferredBackBufferHeight || tribbleRect.Y < 0)
                tribblespeed1.Y *= -1;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0 , 0, 1200, 900), Color.White );
            _spriteBatch.Draw(bot, tribbleRect, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
