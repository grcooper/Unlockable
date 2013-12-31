#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
#endregion

namespace Unlockable
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class Tut : GameScreen
    {
        #region Fields
        ContentManager content;
        dataManagement data = new dataManagement();
        List<string> playerData = new List<string>();

        //Load the scoreboard
        Score score;
        //Load a new hero sprite
        Hero heroSprite;
        Texture2D heroTexture;
        //Background Textures
        Texture2D side1;
        Texture2D side2;
        Texture2D top1;
        Texture2D top2;
        Texture2D floor1;
        Texture2D floor2;
        Texture2D wall;
        Texture2D lava;
        Texture2D tutwrite;

        Vector2 tutWritePosition = new Vector2(0f, 0f);
        Vector2 lavaPosition = new Vector2(0f, 0f);
        Vector2 side1Position = new Vector2(0f, 0f);
        Vector2 side2Position = new Vector2(790f, 0f);
        Vector2 top1Position = new Vector2(0f, 0f);
        Vector2 top2Position = new Vector2(0f, 470f);
        Vector2 floor1Position = new Vector2(0f, 420f);
        Vector2 floor2Position = new Vector2(540f, 420f);
        Vector2 wallPosition = new Vector2(200f, 350f);


        //Orbs + positions
        Texture2D orb1, orb2, orb3;

        Vector2 orb1Position = new Vector2(400, 390);
        Vector2 orb2Position = new Vector2(370, 390);
        Vector2 orb3Position = new Vector2(340, 390);

        Color orb1Color = new Color();
        Color orb2Color = new Color();
        Color orb3Color = new Color();

        //exits + key
        Texture2D key;
        Texture2D exit;
        Texture2D enter;

        Vector2 keyPosition = new Vector2(650, 390);
        Vector2 exitPosition = new Vector2(760, 380);
        Vector2 enterPostion = new Vector2(30, 380);

        Color keyColor = new Color();
        Color exitColor = new Color();
        Color enterColor = new Color();

        //Always nice to have a random!
        Random random = new Random();

        float pauseAlpha;

        InputAction pauseAction;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public Tut()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                // Load score font
                score = new Score(content.Load<SpriteFont>("menufont"));
                score.heroScore = 0;
                //Load the hero for rectangle size
                heroTexture = content.Load<Texture2D>("hero");

                //Create the background
                side1 = content.Load<Texture2D>("tutside");
                side2 = content.Load<Texture2D>("tutside");
                top1 = content.Load<Texture2D>("tuttop");
                top2 = content.Load<Texture2D>("tuttop");
                floor1 = content.Load<Texture2D>("tutfloor1");
                floor2 = content.Load<Texture2D>("tutfloor2");
                wall = content.Load<Texture2D>("tutwall");
                lava = content.Load<Texture2D>("lava");
                tutwrite = content.Load<Texture2D>("tutwriting");


                //Load Orbs
                orb1 = content.Load<Texture2D>("orb");
                orb2 = content.Load<Texture2D>("orb");
                orb3 = content.Load<Texture2D>("orb");

                orb1Color = Color.White;
                orb2Color = Color.White;
                orb3Color = Color.White;

                //Load keys and exit
                key = content.Load<Texture2D>("key");
                exit = content.Load<Texture2D>("Exit");
                enter = content.Load<Texture2D>("Enter");
                enterColor = Color.White;
                exitColor = Color.Transparent;
                keyColor = Color.White;

                //hero loading content
                heroSprite = new Hero();
                heroSprite.LoadContent(this.content);
                
                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
                Thread.Sleep(100);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }

#if WINDOWS_PHONE
            if (Microsoft.Phone.Shell.PhoneApplicationService.Current.State.ContainsKey("PlayerPosition"))
            {
                playerPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"];
                enemyPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"];
            }
#endif
        }


        public override void Deactivate()
        {
#if WINDOWS_PHONE
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"] = playerPosition;
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"] = enemyPosition;
#endif

            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();

#if WINDOWS_PHONE
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("PlayerPosition");
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("EnemyPosition");
#endif
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            //Reset bools
            heroSprite.moveDown = true;
            heroSprite.moveLeft = true;
            heroSprite.moveRight = true;
            heroSprite.moveUp = true;
            //Create rectangles for collision
            Rectangle keyRec =
                new Rectangle((int)keyPosition.X, (int)keyPosition.Y,
                key.Width, key.Height);

            Rectangle exitRec =
                new Rectangle((int)exitPosition.X, (int)exitPosition.Y,
                exit.Width, exit.Height);

            Rectangle heroRec =
                    new Rectangle((int)heroSprite.spritePosition.X, (int)heroSprite.spritePosition.Y,
                    heroTexture.Width, heroTexture.Height);
                    
            Rectangle side1Rec =
                    new Rectangle((int)side1Position.X, (int)side1Position.Y,
                    side1.Width, side1.Height);

            Rectangle side2Rec =
                new Rectangle((int)side2Position.X, (int)side2Position.Y,
                side2.Width, side2.Height);

            Rectangle top1Rec =
                new Rectangle((int)top1Position.X, (int)top1Position.Y,
                top1.Width, top1.Height);

            Rectangle top2Rec =
                new Rectangle((int)top2Position.X, (int)top2Position.Y,
                top2.Width, top2.Height);

            Rectangle floor1Rec =
                new Rectangle((int)floor1Position.X, (int)floor1Position.Y,
                floor1.Width, floor1.Height);

            Rectangle floor2Rec =
                new Rectangle((int)floor2Position.X, (int)floor2Position.Y,
                floor2.Width, floor2.Height);

            Rectangle wallRec =
                new Rectangle((int)wallPosition.X, (int)wallPosition.Y,
                wall.Width, wall.Height);

            Rectangle lavaRec =
                new Rectangle(0, 450, 800, 30);

            Rectangle orb1Rectangle = 
                    new Rectangle((int)orb1Position.X, (int)orb1Position.Y,
                    orb1.Width, orb1.Height);

            Rectangle orb2Rectangle =
                    new Rectangle((int)orb2Position.X, (int)orb2Position.Y,
                    orb2.Width, orb2.Height);

            Rectangle orb3Rectangle =
                    new Rectangle((int)orb3Position.X, (int)orb3Position.Y,
                    orb3.Width, orb3.Height);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                //Collision Detection

                if (heroRec.Intersects(side1Rec))
                {
                    heroSprite.moveLeft = false;
                }

                if (heroRec.Intersects(side2Rec))
                {
                    heroSprite.moveRight = false;
                }

                if (heroRec.Intersects(floor1Rec) ||
                    heroRec.Intersects(floor2Rec) ||
                    heroRec.Intersects(top2Rec))
                {
                    heroSprite.moveDown = false;
                }

                if (heroRec.Intersects(top1Rec))
                {
                    heroSprite.moveUp = false;
                }

                if (heroRec.Intersects(wallRec))
                {
                    if (heroSprite.spritePosition.X < wallPosition.X && heroSprite.spritePosition.Y > wallPosition.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > wallPosition.X && heroSprite.spritePosition.Y > wallPosition.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < wallPosition.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(orb1Rectangle) ||
                    heroRec.Intersects(orb2Rectangle) ||
                    heroRec.Intersects(orb3Rectangle))
                {
                    if (heroRec.Intersects(orb1Rectangle) && heroSprite.touchingOrb1 == true)
                    {
                        score.heroScore += 1;  
                        orb1Color = Color.Transparent;
                        heroSprite.touchingOrb1 = false;
                    }
                    if (heroRec.Intersects(orb2Rectangle) && heroSprite.touchingOrb2 == true)
                    {
                        score.heroScore += 1;
                        orb2Color = Color.Transparent;
                        heroSprite.touchingOrb2 = false;
                    }
                    if (heroRec.Intersects(orb3Rectangle) && heroSprite.touchingOrb3 == true)
                    {
                        score.heroScore += 1;
                        orb3Color = Color.Transparent;
                        heroSprite.touchingOrb3 = false;
                    }
                }
                if (heroRec.Intersects(lavaRec))
                {
                    playerData = data.ReadPlayerData();

                    heroSprite.a1 = Convert.ToInt32(playerData[2]);
                    heroSprite.a2 = Convert.ToInt32(playerData[3]);
                    heroSprite.a3 = Convert.ToInt32(playerData[4]);
                    heroSprite.a4 = Convert.ToInt32(playerData[5]);
                    heroSprite.a5 = Convert.ToInt32(playerData[6]);
                    
                    if (heroSprite.a3 == 0 || heroSprite.a3 == -1)
                    {
                        heroSprite.a3 = 1;
                        data.WritePlayerData(score.heroScore, 0, heroSprite.a1, heroSprite.a2, heroSprite.a3, heroSprite.a4, heroSprite.a5);  
                    }

                    heroSprite.HERO_DEAD = true;
                }

                if (heroRec.Intersects(keyRec))
                {
                    heroSprite.openExit = true;
                    keyColor = Color.Transparent;
                    exitColor = Color.White;
                }

                if (heroRec.Intersects(exitRec) && heroSprite.openExit == true)
                {
                    

                    heroSprite.win = true;
                }            
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
#if WINDOWS_PHONE
                ScreenManager.AddScreen(new PhonePauseScreen(), ControllingPlayer);
#else
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
#endif
            }
            if (heroSprite.HERO_DEAD == true)
            {
                ScreenManager.AddScreen(new GameOverMenuScreen(), ControllingPlayer);
            }

            else if (heroSprite.win == true)
            {
                data.ReadData();
               
                data.WriteData(score.heroScore);

                playerData = data.ReadPlayerData();

                heroSprite.a1 = Convert.ToInt32(playerData[2]);
                heroSprite.a2 = Convert.ToInt32(playerData[3]);
                heroSprite.a3 = Convert.ToInt32(playerData[4]);
                heroSprite.a4 = Convert.ToInt32(playerData[5]);
                heroSprite.a5 = Convert.ToInt32(playerData[6]);

                if (heroSprite.a2 == 0 || heroSprite.a2 == -1)
                {
                    heroSprite.a2 = 1;
                    data.WritePlayerData(score.heroScore, 1, heroSprite.a1, heroSprite.a2, heroSprite.a3, heroSprite.a4, heroSprite.a5);
                }
                else
                {
                    data.WritePlayerData(score.heroScore, 1, heroSprite.a1, heroSprite.a2, heroSprite.a3, heroSprite.a4, heroSprite.a5);
                }

                ScreenManager.AddScreen(new LevelCompletedMenuScreen(), ControllingPlayer);
            }
            else
            {
                heroSprite.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.White, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(key, keyPosition, keyColor);
            spriteBatch.Draw(exit, exitPosition, exitColor);
            spriteBatch.Draw(enter, enterPostion, enterColor);
            spriteBatch.Draw(orb1, orb1Position, orb1Color);

            spriteBatch.Draw(orb2, orb2Position, orb2Color);

            spriteBatch.Draw(orb3, orb3Position, orb3Color);

            spriteBatch.Draw(side1, side1Position, Color.White);
            spriteBatch.Draw(side2, side2Position, Color.White);
            spriteBatch.Draw(top1, top1Position, Color.White);
            spriteBatch.Draw(top2, top2Position, Color.White);
            spriteBatch.Draw(floor1, floor1Position, Color.White);
            spriteBatch.Draw(floor2, floor2Position, Color.White);
            spriteBatch.Draw(wall, wallPosition, Color.White);

            heroSprite.Draw(ScreenManager.SpriteBatch);

            spriteBatch.Draw(lava, lavaPosition, Color.White);

            score.Draw(ScreenManager.SpriteBatch);

            spriteBatch.Draw(tutwrite, tutWritePosition, Color.White);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
        #endregion
    }
}
