using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Unlockable
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class HighScores : MenuScreen
    {
        #region Fields

        Score score = new Score(null);
        dataManagement data = new dataManagement();

        
        MenuEntry One;
        MenuEntry Two;
        MenuEntry Three;
        MenuEntry Four;
        MenuEntry Five;

        static string one = "";
        static string two = "";
        static string three = "";
        static string four = "";
        static string five = "";

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public HighScores()
            : base("HighScores")
        {
            List<int> highScores = data.ReadData();
                try
                {
                    one = Convert.ToString(highScores[0]);
                    two = Convert.ToString(highScores[1]);
                    three = Convert.ToString(highScores[2]);
                    four = Convert.ToString(highScores[3]);
                    five = Convert.ToString(highScores[4]);
                }
                catch
                {
                    one = "";
                    two = "";
                    three = "";
                    four = "";
                    five = "";
                }

            One = new MenuEntry(string.Empty);
            Two = new MenuEntry(string.Empty);
            Three = new MenuEntry(string.Empty);
            Four = new MenuEntry(string.Empty);
            Five = new MenuEntry(string.Empty);

            SetMenuEntryText();

            //SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(One);
            MenuEntries.Add(Two);
            MenuEntries.Add(Three);
            MenuEntries.Add(Four);
            MenuEntries.Add(Five);
            MenuEntries.Add(back);
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            One.Text = "1st: " + one;
            Two.Text = "2nd: " + two;
            Three.Text = "3rd: " + three;
            Four.Text = "4th: " + four;
            Five.Text = "5th: " + five;
        }

        


        #endregion
    }
}