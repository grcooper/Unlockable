using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unlockable
{
        /// <summary>
        /// The options screen is brought up over the top of the main menu
        /// screen, and gives the user a chance to configure the game
        /// in various hopefully useful ways.
        /// </summary>
        class AchievementScreen : MenuScreen
        {
            #region Fields

            MenuEntry GenericMenuEntry;
            MenuEntry LevelOneMenuEntry;
            MenuEntry LevelTwoMenuEntry;
            MenuEntry LevelThreeMenuEntry;

            bool noNew = true;

            List<string> playerData = new List<string>();
            dataManagement data = new dataManagement();

            //static string[] Generic = { "Start the game", "Complete the Tutorial", "Fall in the Lava" };
            static List<string> Generic = new List<string>();
            static int currentGeneric = 0;
            static List<string> OneAchievements = new List<string>();
            static int currentOne = 0;

            #endregion

            #region Initialization


            /// <summary>
            /// Constructor.
            /// </summary>
            public AchievementScreen()
                : base("Achievements")
            {
                // Create our menu entries.
                GenericMenuEntry = new MenuEntry(string.Empty);
                LevelOneMenuEntry = new MenuEntry(string.Empty);
                LevelTwoMenuEntry = new MenuEntry(string.Empty);
                LevelThreeMenuEntry = new MenuEntry(string.Empty);

                currentGeneric = 0;
                currentOne = 0;

                

                SetMenuEntryText();

                MenuEntry back = new MenuEntry("Back");

                // Hook up menu event handlers.
                GenericMenuEntry.Selected += GenericMenuEntrySelected;
                LevelOneMenuEntry.Selected += OneMenuEntrySelected;
                LevelTwoMenuEntry.Selected += FrobnicateMenuEntrySelected;
                LevelThreeMenuEntry.Selected += ElfMenuEntrySelected;
                back.Selected += OnCancel;

                // Add entries to the menu.
                MenuEntries.Add(GenericMenuEntry);
                MenuEntries.Add(LevelOneMenuEntry);
                //MenuEntries.Add(LevelTwoMenuEntry);
                //MenuEntries.Add(LevelThreeMenuEntry);
                MenuEntries.Add(back);

                
            }


            /// <summary>
            /// Fills in the latest values for the options screen menu text.
            /// </summary>
            void SetMenuEntryText()
            {
                playerData = data.ReadPlayerData();

                if (playerData[2] == "1")
                {
                    Generic.Add("Play the Game");
                    noNew = false;
                }
                if (playerData[3] == "1")
                {
                    Generic.Add("Complete The Tutorial");
                    noNew = false;
                }
                if (playerData[4] == "1")
                {
                    Generic.Add("Fall in the Lava");
                    noNew = false;
                }
                if (playerData[5] == "1")
                {
                    OneAchievements.Add("Complete the Level");
                    noNew = false;
                }
                if (playerData[6] == "1")
                {
                    OneAchievements.Add("Collect all of the Orbs");
                    noNew = false;
                }

                if (noNew == true)
                {
                    Generic.Clear();
                    OneAchievements.Clear();
                }

                if (Generic.Count == 0)
                {
                    Generic.Add("");
                }
                if (OneAchievements.Count == 0)
                {
                    OneAchievements.Add("");
                }
                
                GenericMenuEntry.Text = "Generic: " + Generic[currentGeneric];
                LevelOneMenuEntry.Text = "Level 1: " + OneAchievements[currentOne];
                LevelTwoMenuEntry.Text = "Level 2: ";
                LevelThreeMenuEntry.Text = "Level 3: ";
            }


            #endregion

            #region Handle Input


            /// <summary>
            /// Event handler for when the Ungulate menu entry is selected.
            /// </summary>
            void GenericMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                currentGeneric = (currentGeneric + 1) % Generic.Count;

                SetMenuEntryText();
            }


            /// <summary>
            /// Event handler for when the Language menu entry is selected.
            /// </summary>
            void OneMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                currentOne = (currentOne + 1) % OneAchievements.Count;

                SetMenuEntryText();
            }


            /// <summary>
            /// Event handler for when the Frobnicate menu entry is selected.
            /// </summary>
            void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {

            }


            /// <summary>
            /// Event handler for when the Elf menu entry is selected.
            /// </summary>
            void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
            }


            #endregion
        }
    }
