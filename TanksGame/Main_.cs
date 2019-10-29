#undef DEBUG
using System;


namespace TanksGame
{
    class Main_
    {
        static void Main()
        {
            GameMainMenu menu = new GameMainMenu();
            menu.Start();
#if DEBUG
            
#endif
        }
    }
}
