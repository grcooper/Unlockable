using System;

namespace Unlockable
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Unlockable game = new Unlockable())
            {
                game.Run();
            }
        }
    }
#endif
}

