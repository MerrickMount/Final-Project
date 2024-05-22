﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Final_Project
{
    public class Game1 : Game
    {
        float botAngleMouse, botAngle;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D background;
        Texture2D bot;
        Rectangle botrectPlayer, botrectEnemy;
        Vector2 botspeed1, botlocationmouse;
        MouseState mouseState;
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
            botrectPlayer = new Rectangle(500, 400, 100, 100);
            botspeed1 = new Vector2(-2, -2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("backgroundnew");
            bot = Content.Load<Texture2D>("bot_blue");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            Window.Title = mouseState.X + ", " + mouseState.Y;
            botrectPlayer.X += (int)botspeed1.X;
            if (botrectPlayer.Right > 870 || botrectPlayer.X < 30)
                botspeed1.X *= -1;
            botrectPlayer.Y += (int)botspeed1.Y;
            if (botrectPlayer.Bottom > 870 || botrectPlayer.Y < 30)
                botspeed1.Y *= -1;
            // TODO: Add your update logic here
            botAngleMouse = GetAngle(botlocationmouse, new Vector2(mouseState.X, mouseState.Y));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0 , 0, 1200, 900), Color.White );
            _spriteBatch.Draw(bot, botrectPlayer, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
            
        }
        public float GetAngle(Vector2 originPoint, Vector2 secondPoint)
        {
            float rise = secondPoint.Y - originPoint.Y;
            float run = secondPoint.X - originPoint.X;
            if (originPoint.X <= secondPoint.X && originPoint.Y <= secondPoint.Y || originPoint.X <= secondPoint.X && originPoint.Y >= secondPoint.Y)
                return (float)Math.Atan(rise / run);
            else
                return (float)(Math.PI + Math.Atan(rise / run));
        }
    }
}
