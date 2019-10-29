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
        private void TextEntered(object sender, TextEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void MouseMoved(object sender, MouseMoveEventArgs e)
        {
            
        }

        private void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    Global.player.Action(Global.player.Shelltype);
                    break;
                case Mouse.Button.Right:
                    break;
                case Mouse.Button.Middle:
                    break;
                case Mouse.Button.XButton1:
                    break;
                case Mouse.Button.XButton2:
                    break;
                case Mouse.Button.ButtonCount:
                    break;
                default:
                    break;
            }
        }

        private void KeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                case Keyboard.Key.S:
                    Global.player.Speed = 0;
                    break;
                case Keyboard.Key.A:
                case Keyboard.Key.D:
                    Global.player.HullRotate = 0;
                    break;
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    Global.player.Speed = Global.SPEEDTANKFORWARD;
                    break;
                case Keyboard.Key.S:
                    Global.player.Speed = -Global.SPEEDTANKBACKWARD;
                    break;
                case Keyboard.Key.A:
                    Global.player.HullRotate = -Global.SPEEDTANKROTATE;
                    break;
                case Keyboard.Key.D:
                    Global.player.HullRotate = Global.SPEEDTANKROTATE;
                    break;
                case Keyboard.Key.Num1:
                    Global.player.Shelltype = TypeSelectedShell.LightShell;
                    InformativePanel.SelectedShellSprite = new Sprite(Content.LightShellTexture);
                    InformativePanel.SelectedShellSprite.Scale = new Vector2f(5, 5);
                    break;
                case Keyboard.Key.Num2:
                    Global.player.Shelltype = TypeSelectedShell.MediumShell;
                    InformativePanel.SelectedShellSprite = new Sprite(Content.MediumShellTexture);
                    InformativePanel.SelectedShellSprite.Scale = new Vector2f(5, 5);
                    break;
                case Keyboard.Key.Num3:
                    Global.player.Shelltype = TypeSelectedShell.HeavyShell;
                    InformativePanel.SelectedShellSprite = new Sprite(Content.HeavyShellTexture);
                    InformativePanel.SelectedShellSprite.Scale = new Vector2f(5, 5);
                    break;
                case Keyboard.Key.Num4:
                    Global.player.Shelltype = TypeSelectedShell.GranadeShell;
                    InformativePanel.SelectedShellSprite = new Sprite(Content.GranadeShellTexture);
                    InformativePanel.SelectedShellSprite.Scale = new Vector2f(5, 5);
                    break;
                case Keyboard.Key.Num5:
                    Global.player.Shelltype = TypeSelectedShell.SniperShell;
                    InformativePanel.SelectedShellSprite = new Sprite(Content.SniperShellTexture);
                    InformativePanel.SelectedShellSprite.Scale = new Vector2f(5, 5);
                    break;
                case Keyboard.Key.F1:
                    SpawnRandomEnvil();
                    break;
                case Keyboard.Key.F2:
                    SpawnMedicalChest();
                    break;
                case Keyboard.Key.F3:
                    SpawnAmmoKit();
                    break;
                case Keyboard.Key.F11:
                    Global.player.ObjectSprite.Position = new Vector2f(0,0);
                    break;
                case Keyboard.Key.F12:
                    Global.DEBUG = !Global.DEBUG;
                    break;
                case Keyboard.Key.Subtract:
                    cam.Zoom__();

                    break;
                case Keyboard.Key.Add:
                    cam.Zoom_();
                    break;
                default:
                    break;
            }
        }
        private void Win_Closed(object sender, EventArgs e)
        {
            win.Close();
        }
    }
}
