using Final_Project.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Final_Project
{
    public class Game1 : Game
    {
        float botAngle1,botAngle2;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D buttonStartTexture, buttonOptionsTexture, buttonExitTexture, buttonLevel1Texture, buttonLevel2Texture, buttonLevel3Texture, buttonReturnTexture;
        Texture2D background, IntroTexture, GearTexture, GameSelectTexture, OptionsMenuTexture, explosion;
        Texture2D botPlayer, botEnemy, healthbarplayer, healthbarEnemy, armourUp, damageUp;
        Rectangle botrectPlayer, botrectEnemy, healthbarplayerRect, healthbarenemyRect;
        Vector2 botspeed1, botspeed2;
        MouseState mouseState, prevmouseState;
        double healthmathplayer, healthPlayer, healthEnemy, healthmultiplier, healthmathEnemy, botplayerDamage, botenemyDamage, healthPlayerMAX;
        Random movementPlayer = new Random();
        Random movementEnemy = new Random();
        Random timer = new Random();
        int timerCalc, timerCalc1, TimerCalc2, playerMoney, Timer, Timer2, Difficulty;
        Button buttonStart, buttonOption, buttonExit, buttonLevel1, buttonLevel2, buttonLevel3, buttonStartGear, buttonExitGear, buttonExitOptions, TESTBUTTON, purchaseArmour, PurchaseDamage, buttonReturn;
        List<Bullet> bullets;
        List<Bullet> bullets1;
        Rectangle bulletRect;
        Vector2 bulletSpeed;
        Texture2D bulletTexture;
        float bulletLocation;
        private SpriteFont font, Cashfont;
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

            //Window.Title = "Bot Arena 4";                 //REMOVE COMMENT MARKS FOR THIS ONE BEFORE HANDING IN
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
            healthPlayerMAX = 100;
            botAngle1 = 0;
            botAngle2 = 0;
            healthmultiplier = 5.8;
            botplayerDamage = 4.5;
            botenemyDamage = 4;
            screen = Screen.Intro;
            buttonStart = new Button(buttonStartTexture, new Rectangle(525, 120, 150, 75));
            buttonOption = new Button(buttonOptionsTexture, new Rectangle(525, 385, 150, 75));
            buttonExit = new Button(buttonExitTexture, new Rectangle(525, 540, 150, 75));
            buttonLevel1 = new Button(buttonLevel1Texture, new Rectangle(525, 150, 150, 75));
            buttonLevel2 = new Button(buttonLevel2Texture, new Rectangle(525, 450, 150, 75));
            buttonLevel3 = new Button(buttonLevel3Texture, new Rectangle(525, 750, 150, 75));
            buttonStartGear = new Button(buttonExitTexture, new Rectangle(1070, 791, 120, 82));
            buttonExitGear = new Button(buttonExitTexture, new Rectangle(828,791,122,82));
            buttonExitOptions = new Button(buttonExitTexture, new Rectangle(1000, 800, 150, 75));
            TESTBUTTON = new Button(buttonExitTexture, new Rectangle(1000, 800, 150, 75));
            purchaseArmour = new Button(armourUp, new Rectangle(16, 250, 350, 216));
            PurchaseDamage = new Button(damageUp, new Rectangle(425, 250, 350, 216));
            buttonReturn = new Button(buttonReturnTexture, new Rectangle(500,700,150,75));

            bulletRect = new Rectangle(100,100,10,10);
            bullets = new List<Bullet>();
            bullets1 = new List<Bullet>();
            GameWinorLose.paused = false;
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
            IntroTexture = Content.Load<Texture2D>("StartMenu1");
            buttonExitTexture = Content.Load<Texture2D>("ButtonExit");
            buttonOptionsTexture = Content.Load<Texture2D>("ButtonOptions");
            GameSelectTexture = Content.Load<Texture2D>("GameSelect");
            buttonLevel1Texture = Content.Load<Texture2D>("ButtonLevel1");
            buttonLevel2Texture = Content.Load<Texture2D>("ButtonLevel2");
            buttonLevel3Texture = Content.Load<Texture2D>("ButtonLevel3");
            GearTexture = Content.Load<Texture2D>("Options1");
            bulletTexture = Content.Load<Texture2D>("bullet");
            OptionsMenuTexture = Content.Load<Texture2D>("OPTIONSdone");
            font = Content.Load<SpriteFont>("font");
            explosion = Content.Load<Texture2D>("explosion");
            damageUp = Content.Load<Texture2D>("ButtonDamage");
            armourUp = Content.Load<Texture2D>("ButtonArmour");
            Cashfont = Content.Load<SpriteFont>("Cash");
            buttonReturnTexture = Content.Load<Texture2D>("ReurnButton");
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
            if (screen == Screen.Options)
            {
                buttonExitOptions.Update(mouseState, prevmouseState);
                if (buttonExitOptions.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Intro;
                }

            }
            if (screen == Screen.Gear)
            {
                buttonStartGear.Update(mouseState, prevmouseState);
                buttonExitGear.Update(mouseState, prevmouseState);
                purchaseArmour.Update(mouseState, prevmouseState);
                PurchaseDamage.Update(mouseState, prevmouseState);
                if (buttonStartGear.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.GameSelect;
                }
                if (buttonExitGear.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Intro;
                }
                if (purchaseArmour.IsClicked(mouseState, prevmouseState) && playerMoney >= 250)
                {
                    healthPlayerMAX = healthPlayerMAX + 10;
                    playerMoney = playerMoney - 250;
                }
                if (PurchaseDamage.IsClicked(mouseState, prevmouseState) && playerMoney >= 250)
                {
                    botplayerDamage = botplayerDamage +2;
                    playerMoney = playerMoney - 250;
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
                    botenemyDamage = 4;
                    healthEnemy = 100;
                    GameWinorLose.paused = false;
                    Difficulty = 1;
                    healthPlayer = healthPlayerMAX;
                }
                if (buttonLevel2.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Gameplay;
                    botenemyDamage = 6;
                    healthEnemy = 110;
                    GameWinorLose.paused = false;
                    Difficulty = 2;
                    healthPlayer = healthPlayerMAX;
                }
                if (buttonLevel3.IsClicked(mouseState, prevmouseState))
                {
                    screen = Screen.Gameplay;
                    botenemyDamage = 8;
                    healthEnemy = 120;
                    GameWinorLose.paused = false;
                    Difficulty = 3;
                    healthPlayer = healthPlayerMAX;
                }
            }
            if (screen == Screen.Gameplay)
            {
                healthbarplayerRect = new Rectangle(972, 224, 42, 580);
                healthbarenemyRect = new Rectangle(1092, 224, 42, 580);
                if (GameWinorLose.paused == false)
                {
                    TESTBUTTON.Update(mouseState, prevmouseState);
                    if (TESTBUTTON.IsClicked(mouseState, prevmouseState))
                    {
                        GameWinorLose.paused = true;
                    }
                    if (healthPlayer < 1 )
                    {
                        GameWinorLose.paused = true;
                    }
                    if (healthEnemy < 1)
                    {
                        GameWinorLose.paused = true;
                    }
                    if (timerCalc1 == 0)
                    {
                        timerCalc = timer.Next(1, 6);
                        timerCalc1 = timerCalc * 60;
                        botspeed1.X = movementPlayer.Next(-2, 2);
                        botspeed1.Y = movementPlayer.Next(-2, 2);
                        if (botspeed1.X == 0)
                        {
                            botspeed1.X = movementPlayer.Next(-2, 2);
                        }
                        if (botspeed1.Y == 0)
                        {
                            botspeed1.Y = movementPlayer.Next(-2, 2);
                        }

                        if (botspeed1.X == -1)
                        {
                            botspeed1.X = -2;
                        }
                        if (botspeed1.X == 1)
                        {
                            botspeed1.X = 2;
                        }
                        if (botspeed1.Y == 1)
                        {
                            botspeed1.Y = 2;
                        }
                        if (botspeed1.Y == -1)
                        {
                            botspeed1.Y = -2;
                        }
                        


                    }
                    if (TimerCalc2 == 0)
                    {
                        timerCalc = timer.Next(1, 8);
                        TimerCalc2 = timerCalc * 60;
                        botspeed2.X = movementEnemy.Next(-2, 2);
                        botspeed2.Y = movementEnemy.Next(-2, 2);
                        if (botspeed2.X == 0)
                        {
                            botspeed2.X = movementPlayer.Next(-2, 2);
                        }
                        if (botspeed2.Y == 0)
                        {
                            botspeed2.Y = movementPlayer.Next(-2, 2);
                        }

                        if (botspeed2.X == -1)
                        {
                            botspeed2.X = -2;
                        }
                        if (botspeed2.X == 1)
                        {
                            botspeed2.X = 2;
                        }
                        if (botspeed2.Y == 1)
                        {
                            botspeed2.Y = 2;
                        }
                        if (botspeed2.Y == -1)
                        {
                            botspeed2.Y = -2;
                        }
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
                    if (Timer == 0)
                    {
                        Timer = 40;
                        if (Vector2.Distance(botrectEnemy.Location.ToVector2(), botrectPlayer.Location.ToVector2()) <= 450)
                        {
                            bullets.Add(new Bullet(bulletTexture, botrectPlayer.Center.ToVector2(), botrectEnemy.Center.ToVector2(), 10));

                        }

                    }
                    if (Timer2 == 0)
                    {
                        Timer2 = 40;
                        if (Vector2.Distance(botrectPlayer.Location.ToVector2(), botrectEnemy.Location.ToVector2()) <= 450)
                        {
                            bullets1.Add(new Bullet(bulletTexture, botrectEnemy.Center.ToVector2(), botrectPlayer.Center.ToVector2(), 10));

                        }

                    }
                    botAngle1 = (float)Math.Atan2(botspeed1.Y, botspeed1.X);
                    botAngle2 = (float)Math.Atan2(botspeed2.Y, botspeed2.X);
                    timerCalc1--;
                    TimerCalc2--;
                    Timer--;
                    Timer2--;
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].Update();
                        if (bullets[i].Rect.Intersects(botrectEnemy))
                        {
                            healthEnemy = healthEnemy - botplayerDamage;
                            bullets.RemoveAt(i);
                            i--;
                        }
                        else if (bullets[i].Rect.X < 30 && i >=0 || bullets[i].Rect.X > 870 && i >= 0)
                        {
                            bullets.RemoveAt(i);
                            i--;
                        }
                        else if (bullets[i].Rect.Y < 30 && i >= 0 || bullets[i].Rect.Y > 870 && i >= 0)
                        {
                            bullets.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < bullets1.Count; i++)
                    {
                        bullets1[i].Update();
                        if (bullets1[i].Rect.Intersects(botrectPlayer))
                        {
                            healthPlayer = healthPlayer - botenemyDamage;
                            bullets1.RemoveAt(i);
                            i--;
                        }
                        else if (bullets1[i].Rect.X < 30 && i >= 0 || bullets1[i].Rect.X > 870 && i >= 0)
                        {
                            bullets1.RemoveAt(i);
                            i--;
                        }
                        else if (bullets1[i].Rect.Y < 30 && i >= 0 || bullets1[i].Rect.Y > 870 && i >= 0)
                        {
                            bullets1.RemoveAt(i);
                            i--;
                        }
                    }
                     
                }
                if (GameWinorLose.paused == true && healthPlayer < 0)
                {
                    buttonReturn.Update(mouseState, prevmouseState);
                    if (buttonReturn.IsClicked(mouseState, prevmouseState))
                    {
                        screen = Screen.Gear;

                    }
                }
                if (GameWinorLose.paused == true && healthEnemy < 0)
                {
                    buttonReturn.Update(mouseState, prevmouseState);
                    if (buttonReturn.IsClicked(mouseState, prevmouseState))
                    {
                        playerMoney = playerMoney + (Difficulty * 100);
                        screen = Screen.Gear;

                    }
                }
            }
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
                foreach (Bullet bullet in bullets)
                    bullet.Draw(_spriteBatch);
                foreach (Bullet bullet in bullets1)
                    bullet.Draw(_spriteBatch);

                 _spriteBatch.Draw(buttonExitTexture, new Rectangle(1000, 800, 150, 75), Color.White);
            }
            if (GameWinorLose.paused == true && screen == Screen.Gameplay && healthPlayer < 1)
            {
                _spriteBatch.Draw(explosion, botrectPlayer, Color.White);
                _spriteBatch.Draw(buttonReturnTexture, new Rectangle(500, 700, 150, 75), Color.White);
                _spriteBatch.DrawString(font, "You Lose", new Vector2(400, 400), Color.Black);

            }
            if (GameWinorLose.paused == true && screen == Screen.Gameplay && healthEnemy < 1)
            {
                _spriteBatch.Draw(explosion, botrectEnemy, Color.White);
                _spriteBatch.Draw(buttonReturnTexture, new Rectangle(500, 700, 150, 75),Color.White);
                _spriteBatch.DrawString(font, "You Win", new Vector2(400, 400), Color.Black);

            }
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(IntroTexture, new Rectangle(0, 0, 1200, 900), Color.White);
                _spriteBatch.Draw(buttonStartTexture, new Rectangle(525, 120, 150, 75), Color.White);
                _spriteBatch.Draw(buttonOptionsTexture, new Rectangle(525, 385, 150, 75),Color.White);
                _spriteBatch.Draw(buttonExitTexture, new Rectangle(525,540,150,75), Color.White);
            }
            if (screen == Screen.Options)
            {
                _spriteBatch.Draw(OptionsMenuTexture, new Rectangle(0, 0, 1200, 900), Color.White);
                _spriteBatch.Draw(buttonExitTexture, new Rectangle(1000,800,150,75),Color.White);

            }
            if (screen == Screen.Gear)
            {
                _spriteBatch.Draw(GearTexture, new Rectangle(0, 0, 1200, 900), Color.White);
                _spriteBatch.Draw(armourUp, new Rectangle(16, 250, 350, 216), Color.White);
                _spriteBatch.Draw(damageUp, new Rectangle(425, 250, 350, 216), Color.White);
                _spriteBatch.DrawString(Cashfont,playerMoney.ToString(), new Vector2(1000, 690), Color.White);
                if (purchaseArmour.IsClicked(mouseState, prevmouseState) && playerMoney < 250)
                {
                        _spriteBatch.DrawString(font, "Not Enough Cash", new Vector2(400, 400), Color.Black);
                }
                if (PurchaseDamage.IsClicked(mouseState, prevmouseState) && playerMoney < 250)
                {
                        _spriteBatch.DrawString(font, "Not Enough Cash", new Vector2(400, 400), Color.Black);
                    
                }
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
