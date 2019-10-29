using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class GameMainMenu
    {
        private readonly RenderWindow win;
        private readonly Game game;
        public GameMainMenu()
        {
            win = new RenderWindow(new VideoMode(Global.SCREENWIDTH, Global.SCREENHEIGHT), "Tank GAME"
               , Styles.Fullscreen);
            win.SetFramerateLimit(120);
           // win.SetKeyRepeatEnabled(false);

            
            game = new Game(win);
        }

        

        public void Start()
        {
            game.Start();
        }



    }
}
