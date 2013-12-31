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
    class LevelOne : GameScreen
    {
        #region Fields
        ContentManager content;
        dataManagement data = new dataManagement();

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
        Texture2D oneFloor1;
        Texture2D oneFloor2;
        Texture2D oneWall;
        Texture2D platLong1;
        Texture2D platLong2;
        Texture2D platShort1;
        Texture2D platShort2;
        Texture2D platShort3;
        Texture2D platShort4;
        Texture2D platShort5;
        Texture2D platShort6;
        Texture2D wallMed;
        Texture2D wallSmall;

        Texture2D lava;

        Vector2 lavaPosition = new Vector2(0f, 0f);
        Vector2 side1Position = new Vector2(0f, 0f);
        Vector2 side2Position = new Vector2(790f, 0f);
        Vector2 top1Position = new Vector2(0f, 0f);
        Vector2 top2Position = new Vector2(0f, 470f);
        Vector2 oneFloor1Pos = new Vector2(10, 420f);
        Vector2 oneFloor2Pos = new Vector2(540, 410);
        Vector2 oneWallPos = new Vector2(260, 210);
        Vector2 wallMedPos = new Vector2(695, 270);
        Vector2 wallSmallPos = new Vector2(585, 10);
        Vector2 platShort1Pos = new Vector2(180, 230);
        Vector2 platShort2Pos = new Vector2 (10, 290);
        Vector2 platShort3Pos = new Vector2(180, 350);
        Vector2 platShort4Pos = new Vector2(270, 390);
        Vector2 platShort5Pos = new Vector2(760, 180);
        Vector2 platShort6Pos = new Vector2(580, 250);
        Vector2 platLong1Pos = new Vector2(390, 310);
        Vector2 platLong2Pos = new Vector2(585, 110);
            

        //Orbs + positions
        Texture2D orb1, orb2, orb3, orb4, orb5, orb6, orb7, orb8;

        Vector2 orb1Position = new Vector2(200, 190);
        Vector2 orb2Position = new Vector2(330, 370);
        Vector2 orb3Position = new Vector2(300, 370);
        Vector2 orb4Position = new Vector2(200, 330);
        Vector2 orb5Position = new Vector2(20, 270);
        Vector2 orb6Position = new Vector2(760, 160);
        Vector2 orb7Position = new Vector2(590, 380);
        Vector2 orb8Position = new Vector2(610, 380);

        Color orb1Color = new Color();
        Color orb2Color = new Color();
        Color orb3Color = new Color();
        Color orb4Color = new Color();
        Color orb5Color = new Color();
        Color orb6Color = new Color();
        Color orb7Color = new Color();
        Color orb8Color = new Color();





        //exits + key
        Texture2D key;
        Texture2D exit;
        Texture2D enter;

        Vector2 keyPosition = new Vector2(650, 90);
        Vector2 exitPosition = new Vector2(760, 370);
        Vector2 enterPostion = new Vector2(30, 380);

        Color keyColor = new Color();
        Color exitColor = new Color();
        Color enterColor = new Color();

        //Always nice to have a random!
        Random random = new Random();

        List<string> playerData = new List<string>();

        float pauseAlpha;

        InputAction pauseAction;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelOne()
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
                oneFloor1 = content.Load <Texture2D>("oneFloor");
                oneFloor2 = content.Load<Texture2D>("oneFloor");
                oneWall = content.Load<Texture2D>("oneWall");
                wallMed = content.Load<Texture2D>("wallMed");
                wallSmall = content.Load<Texture2D>("wallSmall");
                platShort1 = content.Load<Texture2D>("platMed");
                platShort2 = content.Load<Texture2D>("platMed");
                platShort3 = content.Load<Texture2D>("platMed");
                platShort4 = content.Load<Texture2D>("platLong");
                platShort5 = content.Load<Texture2D>("platShort");
                platShort6 = content.Load<Texture2D>("platMed");
                platLong1 = content.Load<Texture2D>("platLong");
                platLong2 = content.Load<Texture2D>("platLong");



                lava = content.Load<Texture2D>("lava");


                //Load Orbs
                orb1 = content.Load<Texture2D>("orb");
                orb2 = content.Load<Texture2D>("orb");
                orb3 = content.Load<Texture2D>("orb");
                orb4 = content.Load<Texture2D>("orb");
                orb5 = content.Load<Texture2D>("orb");
                orb6 = content.Load<Texture2D>("orb");
                orb7 = content.Load<Texture2D>("orb");
                orb8 = content.Load<Texture2D>("orb");

                orb1Color = Color.White;
                orb2Color = Color.White;
                orb3Color = Color.White;
                orb4Color = Color.White;
                orb5Color = Color.White;
                orb6Color = Color.White;
                orb7Color = Color.White;
                orb8Color = Color.White;

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

                //Set score from external file
                playerData = data.ReadPlayerData();

                score.heroScore = Convert.ToInt32(playerData[0]);

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
                new Rectangle((int)oneFloor1Pos.X, (int)oneFloor1Pos.Y,
                oneFloor1.Width, oneFloor1.Height);

            Rectangle floor2Rec =
                new Rectangle((int)oneFloor2Pos.X, (int)oneFloor2Pos.Y,
                oneFloor1.Width, oneFloor1.Height);

            Rectangle oneWallRec =
                new Rectangle((int)oneWallPos.X, (int)oneWallPos.Y,
                oneWall.Width, oneWall.Height);

            Rectangle platSmall1Rec =
                new Rectangle((int)platShort1Pos.X, (int)platShort1Pos.Y,
                platShort1.Width, platShort1.Height);

            Rectangle platSmall2Rec =
                new Rectangle((int)platShort2Pos.X, (int)platShort2Pos.Y,
                platShort2.Width, platShort2.Height);

            Rectangle platSmall3Rec =
                new Rectangle((int)platShort3Pos.X, (int)platShort3Pos.Y,
                platShort3.Width, platShort3.Height);

            Rectangle platSmall4Rec =
                new Rectangle((int)platShort4Pos.X, (int)platShort4Pos.Y,
                platShort4.Width, platShort4.Height);

            Rectangle platSmall5Rec =
                new Rectangle((int)platShort5Pos.X, (int)platShort5Pos.Y,
                platShort5.Width, platShort5.Height);

            Rectangle platSmall6Rec =
                new Rectangle((int)platShort6Pos.X, (int)platShort6Pos.Y,
                platShort6.Width, platShort6.Height);

            Rectangle platLong1Rec =
                new Rectangle((int)platLong1Pos.X, (int)platLong1Pos.Y,
                platLong1.Width, platLong1.Height);

            Rectangle platLong2Rec =
                new Rectangle((int)platLong2Pos.X, (int)platLong2Pos.Y,
                platLong1.Width, platLong1.Height);

            Rectangle wallMedRec =
                new Rectangle((int)wallMedPos.X, (int)wallMedPos.Y,
                wallMed.Width, wallMed.Height);

            Rectangle wallSmallRec =
                new Rectangle((int)wallSmallPos.X, (int)wallSmallPos.Y,
                wallSmall.Width, wallSmall.Height);


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

            Rectangle orb4Rectangle =
                    new Rectangle((int)orb4Position.X, (int)orb4Position.Y,
                    orb1.Width, orb1.Height);

            Rectangle orb5Rectangle =
                    new Rectangle((int)orb5Position.X, (int)orb5Position.Y,
                    orb2.Width, orb2.Height);

            Rectangle orb6Rectangle =
                    new Rectangle((int)orb6Position.X, (int)orb6Position.Y,
                    orb3.Width, orb3.Height);

            Rectangle orb7Rectangle =
                    new Rectangle((int)orb7Position.X, (int)orb7Position.Y,
                    orb2.Width, orb2.Height);

            Rectangle orb8Rectangle =
                    new Rectangle((int)orb8Position.X, (int)orb8Position.Y,
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

                if (heroRec.Intersects(top2Rec) ||
                    heroRec.Intersects(floor1Rec) ||
                    heroRec.Intersects(floor2Rec))
                {
                    heroSprite.moveDown = false;
                }

                if (heroRec.Intersects(top1Rec))
                {
                    heroSprite.moveUp = false;
                }

                if (heroRec.Intersects(orb1Rectangle) ||
                    heroRec.Intersects(orb2Rectangle) ||
                    heroRec.Intersects(orb3Rectangle) ||
                    heroRec.Intersects(orb4Rectangle) ||
                    heroRec.Intersects(orb5Rectangle) ||
                    heroRec.Intersects(orb6Rectangle) ||
                    heroRec.Intersects(orb7Rectangle) ||
                    heroRec.Intersects(orb8Rectangle))
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

                    if (heroRec.Intersects(orb4Rectangle) && heroSprite.touchingOrb4 == true)
                    {
                        score.heroScore += 1;
                        orb4Color = Color.Transparent;
                        heroSprite.touchingOrb4 = false;
                    }

                    if (heroRec.Intersects(orb5Rectangle) && heroSprite.touchingOrb5 == true)
                    {
                        score.heroScore += 1;
                        orb5Color = Color.Transparent;
                        heroSprite.touchingOrb5 = false;
                    } 
                    if (heroRec.Intersects(orb6Rectangle) && heroSprite.touchingOrb6 == true)
                    {
                        score.heroScore += 1;
                        orb6Color = Color.Transparent;
                        heroSprite.touchingOrb6 = false;
                    } 
                    if (heroRec.Intersects(orb7Rectangle) && heroSprite.touchingOrb7 == true)
                    {
                        score.heroScore += 1;
                        orb7Color = Color.Transparent;
                        heroSprite.touchingOrb7 = false;
                    }
                    if (heroRec.Intersects(orb8Rectangle) && heroSprite.touchingOrb8 == true)
                    {
                        score.heroScore += 1;
                        orb8Color = Color.Transparent;
                        heroSprite.touchingOrb8 = false;
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

                if (heroRec.Intersects(platLong1Rec))
                {
                    if (heroSprite.spritePosition.X < platLong1Pos.X && heroSprite.spritePosition.Y > platLong1Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platLong1Pos.X && heroSprite.spritePosition.Y > platLong1Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platLong1Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(platLong2Rec))
                {
                    if (heroSprite.spritePosition.X < platLong1Pos.X && heroSprite.spritePosition.Y > platLong2Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platLong2Pos.X && heroSprite.spritePosition.Y > platLong2Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platLong2Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(platSmall1Rec))
                {
                    if (heroSprite.spritePosition.X < platShort1Pos.X && heroSprite.spritePosition.Y > platShort1Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platShort1Pos.X && heroSprite.spritePosition.Y > platShort1Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platLong1Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(platSmall2Rec))
                {
                    if (heroSprite.spritePosition.X < platShort2Pos.X && heroSprite.spritePosition.Y > platShort2Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platShort2Pos.X && heroSprite.spritePosition.Y > platShort2Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platShort2Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }
                if (heroRec.Intersects(platSmall3Rec))
                {
                    if (heroSprite.spritePosition.X < platShort3Pos.X && heroSprite.spritePosition.Y > platShort3Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platShort3Pos.X && heroSprite.spritePosition.Y > platShort3Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platShort3Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(platSmall4Rec))
                {
                    if (heroSprite.spritePosition.X < platShort4Pos.X && heroSprite.spritePosition.Y > platShort4Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platShort4Pos.X && heroSprite.spritePosition.Y > platShort4Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platShort4Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(platSmall5Rec))
                {
                    if (heroSprite.spritePosition.X < platShort5Pos.X && heroSprite.spritePosition.Y > platShort5Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platShort5Pos.X && heroSprite.spritePosition.Y > platShort5Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platShort5Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(platSmall6Rec))
                {
                    if (heroSprite.spritePosition.X < platShort6Pos.X && heroSprite.spritePosition.Y > platShort6Pos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > platShort6Pos.X && heroSprite.spritePosition.Y > platShort6Pos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < platShort6Pos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(oneWallRec))
                {
                    if (heroSprite.spritePosition.X < oneWallPos.X && heroSprite.spritePosition.Y > oneWallPos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > oneWallPos.X && heroSprite.spritePosition.Y > oneWallPos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < oneWallPos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(wallMedRec))
                {
                    if (heroSprite.spritePosition.X < wallMedPos.X && heroSprite.spritePosition.Y > wallMedPos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > wallMedPos.X && heroSprite.spritePosition.Y > wallMedPos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < wallMedPos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
                }

                if (heroRec.Intersects(wallSmallRec))
                {
                    if (heroSprite.spritePosition.X < wallSmallPos.X && heroSprite.spritePosition.Y > wallSmallPos.Y)
                    {
                        heroSprite.moveRight = false;
                    }
                    if (heroSprite.spritePosition.X > wallSmallPos.X && heroSprite.spritePosition.Y > wallSmallPos.Y)
                    {
                        heroSprite.moveLeft = false;
                    }
                    if (heroSprite.spritePosition.Y < wallSmallPos.Y)
                    {
                        heroSprite.moveDown = false;
                    }
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

                if (heroSprite.a4 == 0 || heroSprite.a4 == -1)
                {
                    heroSprite.a4 = 1;
                    data.WritePlayerData(score.heroScore, 1, heroSprite.a1, heroSprite.a2, heroSprite.a3, heroSprite.a4, heroSprite.a5);
                }

                ScreenManager.AddScreen(new LevelCompletedMenuScreen(), ControllingPlayer);
            }
            else
            {
                if ((heroSprite.touchingOrb1 == false) &&
                    (heroSprite.touchingOrb2 == false) &&
                    (heroSprite.touchingOrb3 == false) &&
                    (heroSprite.touchingOrb4 == false) &&
                    (heroSprite.touchingOrb5 == false) &&
                    (heroSprite.touchingOrb6 == false) &&
                    (heroSprite.touchingOrb7 == false) &&
                    (heroSprite.touchingOrb8 == false) &&
                    (heroSprite.aorb == false))
                {
                    playerData = data.ReadPlayerData();

                    heroSprite.a1 = Convert.ToInt32(playerData[2]);
                    heroSprite.a2 = Convert.ToInt32(playerData[3]);
                    heroSprite.a3 = Convert.ToInt32(playerData[4]);
                    heroSprite.a4 = Convert.ToInt32(playerData[5]);
                    heroSprite.a5 = Convert.ToInt32(playerData[6]);

                    if (heroSprite.a5 == 0 || heroSprite.a5 == -1)
                    {
                        heroSprite.a5 = 1;
                        data.WritePlayerData(score.heroScore, 1, heroSprite.a1, heroSprite.a2, heroSprite.a3, heroSprite.a4, heroSprite.a5);
                    }
                    heroSprite.aorb = true;
                }
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
            spriteBatch.Draw(orb4, orb4Position, orb4Color);
            spriteBatch.Draw(orb5, orb5Position, orb5Color);
            spriteBatch.Draw(orb6, orb6Position, orb6Color);
            spriteBatch.Draw(orb7, orb7Position, orb7Color);
            spriteBatch.Draw(orb8, orb8Position, orb8Color);

            spriteBatch.Draw(side1, side1Position, Color.White);
            spriteBatch.Draw(side2, side2Position, Color.White);
            spriteBatch.Draw(top1, top1Position, Color.White);
            spriteBatch.Draw(top2, top2Position, Color.White);
            spriteBatch.Draw(oneFloor1, oneFloor1Pos, Color.White);
            spriteBatch.Draw(oneFloor2, oneFloor2Pos, Color.White);
            spriteBatch.Draw(oneWall, oneWallPos, Color.White);
            spriteBatch.Draw(wallMed, wallMedPos, Color.White);
            spriteBatch.Draw(wallSmall, wallSmallPos, Color.White);
            spriteBatch.Draw(platShort1, platShort1Pos, Color.White);
            spriteBatch.Draw(platShort2, platShort2Pos, Color.White);
            spriteBatch.Draw(platShort3, platShort3Pos, Color.White);
            spriteBatch.Draw(platShort4, platShort4Pos, Color.White);
            spriteBatch.Draw(platShort5, platShort5Pos, Color.White);
            spriteBatch.Draw(platShort6, platShort6Pos, Color.White);
            spriteBatch.Draw(platLong1, platLong1Pos, Color.White);
            spriteBatch.Draw(platLong2, platLong2Pos, Color.White);


            heroSprite.Draw(ScreenManager.SpriteBatch);

            spriteBatch.Draw(lava, lavaPosition, Color.White);

            score.Draw(ScreenManager.SpriteBatch);

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
