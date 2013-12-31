#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace Unlockable
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry deleteSaves;
        MenuEntry deleteScores;


        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            deleteScores = new MenuEntry(string.Empty);
            deleteSaves = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            deleteSaves.Selected += DeleteSavesSelected;
            deleteScores.Selected += DeleteScoresSelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(deleteSaves);
            MenuEntries.Add(deleteScores);

            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {

            deleteSaves.Text = "Delete Saves";
            deleteScores.Text = "Delete Scores";


        }

        


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Delete Saves menu entry is selected.
        /// </summary>
        void DeleteScoresSelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to delete all saved scores?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            dataManagement data = new dataManagement();

            data.DeleteData();
        }

        void DeleteSavesSelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to delete all saved player data?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmDeleteMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void ConfirmDeleteMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            dataManagement data = new dataManagement();

            data.DeletePlayerData();
        }

        #endregion
    }
}
