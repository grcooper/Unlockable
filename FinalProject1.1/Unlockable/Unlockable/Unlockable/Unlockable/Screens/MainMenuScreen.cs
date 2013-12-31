#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace Unlockable
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization
        dataManagement data = new dataManagement();
        List<string> playerData = new List<string>();
        Hero heroSprite = new Hero();

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Unlockable")
        {
            // Create our menu entries.
            MenuEntry continueGameMenuEntry = new MenuEntry("Continue Game");
            MenuEntry newMenuEntry = new MenuEntry("New Game");
            MenuEntry highScoreMenuEntry = new MenuEntry("High Scores");
            MenuEntry achievementsMenuEntry = new MenuEntry("Achievements");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            continueGameMenuEntry.Selected += ContinueGameMenuEntrySelected;
            newMenuEntry.Selected += NewMenuEntrySelected;
            highScoreMenuEntry.Selected += HighScoreMenuEntrySelected;
            achievementsMenuEntry.Selected += AchievementMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(continueGameMenuEntry);
            MenuEntries.Add(newMenuEntry);
            MenuEntries.Add(highScoreMenuEntry);
            MenuEntries.Add(achievementsMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void ContinueGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            playerData = data.ReadPlayerData();

            if (playerData[1] == "-1" || playerData == null)
            {
                const string message = "There is no saved data.\nStart at the Tutorial Level?";

                MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

                confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

                ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
            }
            else
            {
                if (playerData[1] == "1")
                {
                    LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                                   new LevelOne());
                }
                else
                {
                    LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                                   new Tut());
                }
            }
            
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void NewMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to delete all saved data?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            data.WritePlayerData(-1, -1, -1, -1, -1, -1, -1);

            playerData = data.ReadPlayerData();

            heroSprite.a1 = Convert.ToInt32(playerData[2]);
            heroSprite.a2 = Convert.ToInt32(playerData[3]);
            heroSprite.a3 = Convert.ToInt32(playerData[4]);
            heroSprite.a4 = Convert.ToInt32(playerData[5]);
            heroSprite.a5 = Convert.ToInt32(playerData[6]);

            if (heroSprite.a1 == 0 || heroSprite.a1 == -1)
            {
                heroSprite.a1 = 1;
                data.WritePlayerData(0, 0, heroSprite.a1, heroSprite.a2, heroSprite.a3, heroSprite.a4, heroSprite.a5);
            }

            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new Tut());
        }

        void HighScoreMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HighScores(), e.PlayerIndex);
        }

        void AchievementMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new AchievementScreen(), e.PlayerIndex);
        }

        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
