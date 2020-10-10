using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    partial class Game
    {
        private readonly RenderWindow win;
        private readonly Cam cam;
        private readonly Random rnd;
        private Clock SpawnEnvilClock;
        private InformativePanel InformativePanel;
        public Game(RenderWindow win)
        {
            this.win = win;
            SpawnEnvilClock = new Clock();
            rnd = new Random();
            win.Closed += Win_Closed;
            win.KeyPressed += KeyPressed;
            win.KeyReleased += KeyReleased;
            win.MouseButtonPressed += MouseButtonPressed;
            win.MouseButtonReleased += MouseButtonReleased;
            win.MouseMoved += MouseMoved;
            //win.SetMouseCursorVisible(false);
            win.SetMouseCursor(new Cursor(Cursor.CursorType.Cross));
            //win.TextEntered += TextEntered;
            InformativePanel = new InformativePanel();
            cam = new Cam();
        }
        
        private void UpdateMouseCoord()
        {
            Global.MousePosition = (Vector2f)Mouse.GetPosition();
            Global.MousePosition = win.MapPixelToCoords(Mouse.GetPosition());
        }
        private void SpawnRandomEnvil()
        {
            //for (int i = 0; i < 1; i++)
            //{
            //Global.EnemyOnMap.Add(new Enemy(new Vector2f(rnd.Next(-10000, 10000), rnd.Next(-10000, 10000))));
            //}
            Global.EnemyOnMap.Add(new Enemy(new Vector2f(rnd.Next(-1000, 1000), rnd.Next(-1000, 1000))));
        }
        private void SpawnMedicalChest()
        {
            for (int i = 0; i < 1; i++)
            {
                Global.PredmetsOnMap.Add(new MedicalChest(new Vector2f(rnd.Next(-10000, 10000), rnd.Next(-10000, 10000))));
            }
        }
        private void SpawnAmmoKit()
        {
            for (int i = 0; i < 1; i++)
            {
                Global.PredmetsOnMap.Add(new Ammo(new Vector2f(rnd.Next(-10000, 10000), rnd.Next(-10000, 10000))));
            }
        }
        public void Start()
        {
            while (win.IsOpen)
            {
                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    win.Close();
                }
                win.Clear(new Color(80, 80, 60));
                cam.Update(win);
                UpdateMouseCoord();
                win.DispatchEvents();
                foreach (var i in Global.EnemyOnMap)win.Draw(i);
                Global.player.Update();
                win.Draw(Global.player);
                Global.shellUpdater.CheckAllObjectCollision();
                Global.shellUpdater.Update();
                win.Draw(Global.shellUpdater);
                win.Draw(Global.ExplosionAnimation_);
                Global.ObjectOnMapUpdater.Update();
                win.Draw(Global.ObjectOnMapUpdater);
                InformativePanel.UpdatePanel();
                win.Draw(InformativePanel);
                win.Display();
                if (Global.GameOver) return;
            }

        }
    }
}
