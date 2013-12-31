using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameStateManagement;

namespace Unlockable
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Unlockable : Microsoft.Xna.Framework.Game
    {
       GraphicsDeviceManager graphics;
       ScreenManager screenManager;
       ScreenFactory screenFactory;

        /// <summary>
        /// The main game constructor.
        /// </summary>
        public Unlockable()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            // Create the screen factory and add it to the Services
            screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            // Create the screen manager component.
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            // On Windows and Xbox we just add the initial screens
            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        //protected override void LoadContent()
        //{
        //    // Create a new SpriteBatch, which can be used to draw textures.
        //    spriteBatch = new SpriteBatch(GraphicsDevice);

        //    // TODO: use this.Content to load your game content here
        //}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        //protected override void UnloadContent()
        //{
        //     TODO: Unload any non ContentManager content here
        //}

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //protected override void Update(GameTime gameTime)
        //{
        //    // Allows the game to exit
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        //        this.Exit();

        //    // TODO: Add your update logic here

        //    base.Update(gameTime);
        //}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }
    }
}
