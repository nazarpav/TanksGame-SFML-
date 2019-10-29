using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class Cam
    {
        private readonly View cam;
        private float Zoom;
        public Cam()
        {
            cam = new View(Global.player.ObjectSprite.Position,new Vector2f(Global.SCREENWIDTH, Global.SCREENHEIGHT));
            cam.Zoom(Global.DEFAULTHZOOMMAINCAM);
        }
        public void Zoom_()
        {
            Zoom = 0.99f;
            cam.Zoom(Zoom);
        }
        public void Zoom__()
        {
            Zoom = 1.01f;
            cam.Zoom(Zoom);
        }
        public View Cam_ => cam;

        public void Update(RenderWindow win)
        {
            cam.Center = Global.player.ObjectSprite.Position;
            win.SetView(cam);
        }
    }
}
