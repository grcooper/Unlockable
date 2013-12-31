#region Using Statements
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace Unlockable
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class GameOverMenuScreen : MenuScreen
    {
        #region Initialization
        List<string> playerData = new List<string>();
        dataManagement data = new dataManagement();

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameOverMenuScreen()
            : base("Game Over")
        {
            // Create our menu entries.
            MenuEntry tryAgainGameMenuEntry = new MenuEntry("Try Again");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

            // Hook up menu event handlers.
            tryAgainGameMenuEntry.Selected += TryAgainGameMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(tryAgainGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>

        void TryAgainGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            playerData = data.ReadPlayerData();

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

        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }


        #endregion
    }
}