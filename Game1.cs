using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Final_Project
{
    public class Game1 : Game
    {
        float botAngle1,botAngle2, gunAngle1, gunAngle2;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D buttonStartTexture, buttonOptionsTexture, buttonExitTexture, buttonLevel1Texture, buttonLevel2Texture, buttonLevel3Texture;
        Texture2D background, IntroTexture, GearTexture, GameSelectTexture, OptionsMenuTexture;
        Texture2D botPlayer, botEnemy, healthbarplayer, healthbarEnemy, gun;
        Rectangle botrectPlayer, botrectEnemy, healthbarplayerRect, healthbarenemyRect, gunRect, gunRect2;
        Vector2 botspeed1, botspeed2, botLocation1, botlocation2;
        MouseState mouseState, prevmouseState;
        double healthmathplayer, healthPlayer, healthEnemy, healthmultiplier, healthmathEnemy, botplayerDamage, botenemyDamage;
        Random movementPlayer = new Random();
        Random movementEnemy = new Random();
        Random timer = new Random();
        int timerCalc, timerCalc1, TimerCalc2, playerMoney;
        Button buttonStart, buttonOption, buttonExit, buttonLevel1, buttonLevel2, buttonLevel3, buttonStartGear, buttonExitGear;
        enum Screen
        {
            Intro,
            Gear,
            GameSelect,
            Gameplay,
            Options,
            win,
            lose
        }
        Screen screen;

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
            botrectPlayer = new Rectangle(100, 400, 85, 85);
            botrectEnemy = new Rectangle(770 ,400 ,85 ,85);
            botspeed2 = new Vector2(-2, 0);
            botspeed1 = new Vector2(2, 0);
            healthbarplayerRect = new Rectangle(972, 224, 42, 580);
            healthbarenemyRect = new Rectangle(1092, 224, 42, 580);
            playerMoney = 0;
            healthEnemy = 100;
            healthPlayer = 100;
            botAngle1 = 0;
            botAngle2 = 0;
            gunAngle1 = 0;
            healthmultiplier = 5.8;
            botplayerDamage = 1;
            botenemyDamage = 1;
            screen = Screen.Intro;
            buttonStart = new Button(buttonStartTexture, new Rectangle(525, 233, 150, 75));
            buttonOption = new Button(buttonOptionsTexture, new Rectangle(525, 385, 150, 75));
            buttonExit = new Button(buttonExitTexture, new Rectangle(525, 540, 150, 75));
            buttonLevel1 = new Button(buttonLevel1Texture, new Rectangle(525, 150, 150, 75));
            buttonLevel2 = new Button(buttonLevel2Texture, new Rectangle(525, 450, 150, 75));
            buttonLevel3 = new Button(buttonLevel3Texture, new Rectangle(525, 750, 150, 75));
            buttonStartGear = new Button(buttonExitTexture, new Rectangle(1070, 791, 120, 82));
            buttonExitGear = new Button(buttonExitTexture, new Rectangle(828,791,122,82));
            gunRect = new Rectangle(0,0,101,50);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("backgroundnew1");
            botPlayer = Content.Load<Texture2D>("bot_blue");
            botEnemy = Content.Load<Texture2D>("bot_red");
            healthbarplayer = Content.Load<Texture2D>("healthbarplayer1");
            healthbarEnemy = Content.Load<Texture2D>("healthbarenemy1");
            buttonStartTexture = Content.Load<Texture2D>("ButtonStart");
            IntroTexture = Content.Load<Texture2D>("StartMenu");
            buttonExitTexture = Content.Load<Texture2D>("ButtonExit");
            buttonOptionsTexture = Content.Load<Texture2D>("ButtonOptions");
            GameSelectTexture = Content.Load<Texture2D>("GameSelect");
            gun = Content.Load<Texture2D>("Gun1");
            buttonLevel1Texture = Content.Load<Texture2D>("ButtonLevel1");
            buttonLevel2Texture = Content.Load<Texture2D>("ButtonLevel2");
            buttonLevel3Texture = Content.Load<Texture2D>("ButtonLevel3");
            GearTexture = Content.Load<Texture2D>("Options1");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            prevmouseState = mouseState;
            mouseState = Mouse.GetState();
            Window.Title = mouseState.X + ", " + mouseState.Y; //REMOVE THIS BEFORE FINAL HAND IN
            if (screen == Screen.Intro)
            {
                buttonStart.Update(mouseState, prevmouseState);
                buttonOption.Update(mouseState, prevmouseState);
                buttonExit.Update(mouseState, prevmouseState);

                if (buttonStart.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Gear;
                }
                if (buttonOption.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Options;
                }
                if (buttonExit.IsClicked(mouseState, prevmouseState)) 
                {
                    Exit();
                }

            }
            if (screen == Screen.Gear)
            {
                buttonStartGear.Update(mouseState, prevmouseState);
                buttonExitGear.Update(mouseState, prevmouseState);
                if (playerMoney < 0)
                {
                    playerMoney = 0;
                }
                if (buttonStartGear.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.GameSelect;
                }
                if (buttonExitGear.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Intro;
                }
            }
            if (screen == Screen.GameSelect)
            {
                buttonLevel1.Update(mouseState, prevmouseState);
                buttonLevel2.Update(mouseState, prevmouseState);
                buttonLevel3.Update(mouseState, prevmouseState);

                if (buttonLevel1.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Gameplay;
                    botenemyDamage = 1;
                    healthEnemy = 100;

                }
                if (buttonLevel2.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Gameplay;
                    botenemyDamage = 2;
                    healthEnemy = 110;

                }
                if (buttonLevel3.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Gameplay;
                    botenemyDamage = 3;
                    healthEnemy = 120;
                }
            }
            if (screen == Screen.Gameplay)
            {
                if (timerCalc1 == 0)
                {
                    timerCalc = timer.Next(1, 6);
                    timerCalc1 = timerCalc * 60;
                    botspeed1.X = movementPlayer.Next(-2, 2);
                    botspeed1.Y = movementPlayer.Next(-2, 2);


                }
                if (TimerCalc2 == 0)
                {
                    timerCalc = timer.Next(1, 8);
                    TimerCalc2 = timerCalc * 60;
                    botspeed2.X = movementEnemy.Next(-2, 2);
                    botspeed2.Y = movementEnemy.Next(-2, 2);
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (healthEnemy > 1 || healthPlayer > 1)
                {
                    botrectPlayer.X += (int)botspeed1.X;
                    if (botrectPlayer.Right > 870 || botrectPlayer.X < 30)
                        botspeed1.X *= -1;
                    botrectPlayer.Y += (int)botspeed1.Y;
                    if (botrectPlayer.Bottom > 870 || botrectPlayer.Y < 30)
                        botspeed1.Y *= -1;
                }
                if (healthPlayer > 1 || healthEnemy > 1)
                {
                    botrectEnemy.X += (int)botspeed2.X;
                    if (botrectEnemy.Right > 870 || botrectEnemy.X < 30)
                        botspeed2.X *= -1;
                    botrectEnemy.Y += (int)botspeed2.Y;
                    if (botrectEnemy.Bottom > 870 || botrectEnemy.Y < 30)
                        botspeed2.Y *= -1;
                }
                if (botrectEnemy.Intersects(botrectPlayer))
                {

                    botspeed2.X *= -1;
                    botspeed2.Y *= -1;
                    botspeed1.Y *= -1;
                    botspeed1.X *= -1;
                    healthPlayer = healthPlayer - botenemyDamage;
                    healthEnemy = healthEnemy - botplayerDamage;
                }
                if (healthPlayer < 100)
                {
                    healthmathplayer = (healthPlayer * healthmultiplier);
                    healthbarplayerRect = new Rectangle(972, 224, 42, Convert.ToInt32(healthmathplayer));
                }
                if (healthEnemy < 100)
                {
                    healthmathEnemy = (healthEnemy * healthmultiplier);
                    healthbarenemyRect = new Rectangle(1092, 224, 42, Convert.ToInt32(healthmathEnemy));
                }
                if (healthEnemy < 1)
                {
                    screen = Screen.win;
                    playerMoney = playerMoney + 100;
                }
                if (healthPlayer < 1)
                {
                    screen = Screen.lose;
                    playerMoney = playerMoney - 25;
                }
                if (Vector2.Distance(botrectEnemy.Location.ToVector2(), botrectPlayer.Location.ToVector2()) > 50) ;
                {
                    //Shoot gun at the other
                }
                botAngle1 = (float)Math.Atan2(botspeed1.Y, botspeed1.X);
                botAngle2 = (float)Math.Atan2(botspeed2.Y, botspeed2.X);
                gunAngle1 = (float)Math.Atan2(botspeed1.Y, botspeed1.X);
                gunAngle2 = (float)Math.Atan2(botspeed2.Y, botspeed2.X);
                timerCalc1--;
                TimerCalc2--;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Gameplay)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 1200, 900), Color.White);
                _spriteBatch.Draw(botPlayer, new Rectangle(botrectPlayer.Center, botrectPlayer.Size), null, Color.White, botAngle1, new Vector2(botPlayer.Width / 2, botPlayer.Height / 2), SpriteEffects.None, 1f);
                _spriteBatch.Draw(botEnemy, new Rectangle(botrectEnemy.Center, botrectEnemy.Size), null, Color.White, botAngle2, new Vector2(botEnemy.Width / 2, botEnemy.Height / 2), SpriteEffects.None, 1f);
                _spriteBatch.Draw(healthbarplayer, healthbarplayerRect, Color.White);
                _spriteBatch.Draw(healthbarEnemy, healthbarenemyRect, Color.White);
                _spriteBatch.Draw(gun, new Rectangle((botrectPlayer.Center.X), botrectPlayer.Center.Y , gunRect.Width, gunRect.Height), null, Color.White, gunAngle1, new Vector2(gunRect.Width/2, gunRect.Height/2), SpriteEffects.None, 1f);
                _spriteBatch.Draw(gun, new Rectangle((botrectEnemy.Center.X), botrectEnemy.Center.Y, gunRect.Width, gunRect.Height), null, Color.White, gunAngle2, new Vector2(gunRect.Width / 2, gunRect.Height / 2), SpriteEffects.None, 1f);

            }
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(IntroTexture, new Rectangle(0, 0, 1200, 900), Color.White);
                _spriteBatch.Draw(buttonStartTexture, new Rectangle(525, 233, 150, 75), Color.White);
                _spriteBatch.Draw(buttonOptionsTexture, new Rectangle(525, 385, 150, 75),Color.White);
                _spriteBatch.Draw(buttonExitTexture, new Rectangle(525,540,150,75), Color.White);
            }
            if (screen == Screen.Options)
            {
                _spriteBatch.Draw(OptionsMenuTexture, new Rectangle(0, 0, 1200, 900), Color.White);

            }
            if (screen == Screen.Gear)
            {
                _spriteBatch.Draw(GearTexture, new Rectangle(0, 0, 1200, 900), Color.White);
            }
            if (screen == Screen.GameSelect)
            {
                _spriteBatch.Draw(GameSelectTexture, new Rectangle(0, 0, 1200, 900), Color.White);
                _spriteBatch.Draw(buttonLevel1Texture, new Rectangle(525, 150, 150, 75),Color.White);
                _spriteBatch.Draw(buttonLevel2Texture, new Rectangle(525, 450, 150, 75), Color.White);
                _spriteBatch.Draw(buttonLevel3Texture, new Rectangle(525, 750, 150, 75), Color.White);

                buttonLevel2 = new Button(buttonLevel2Texture, new Rectangle(525, 450, 150, 75));
                buttonLevel3 = new Button(buttonLevel3Texture, new Rectangle(525, 750, 150, 75));
            }
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
