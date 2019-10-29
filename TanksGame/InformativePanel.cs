using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class InformativePanel : Drawable
    {
        public Sprite SelectedShellSprite { get; set; }
        private MiniMap miniMap;

        public InformativePanel()
        {
            SelectedShellSprite = new Sprite(Content.LightShellTexture);
            SelectedShellSprite.Scale = new Vector2f(5, 5);
            miniMap = new MiniMap();
            Content.DebugString.CharacterSize = 40;
            Content.Health.CharacterSize = Content.DebugString.CharacterSize;
        }
        public void UpdatePanel()
        {
            SelectedShellSprite.Position = new Vector2f(Global.player.ObjectShape.Position.X - Global.SCREENWIDTH, Global.player.ObjectShape.Position.Y + Global.SCREENHEIGHT - 350);
            Content.Health.Position = new Vector2f(Global.player.ObjectShape.Position.X - Global.SCREENWIDTH, Global.player.ObjectShape.Position.Y - Global.SCREENHEIGHT);
            Content.Health.DisplayedString = "Health " + Global.player.Health ;
            Content.DebugString.Position = new Vector2f(Global.player.ObjectShape.Position.X - Global.SCREENWIDTH, Global.player.ObjectShape.Position.Y - Global.SCREENHEIGHT + 40);
            Content.DebugString.DisplayedString = "Quantity tank =>  " + Global.EnemyOnMap.Count +
              "\nQuantity Other Object =>  " + Global.PredmetsOnMap.Count +
              "\nQuantity shell on map => " + Global.shellUpdater.Counter +
              "\nYour Coordination => " + Global.player.ObjectSprite.Position +
              "\n |  Counter Shell Light > " + Global.player.LightCounterShell+
              "\n |  Counter Shell Medium > " + Global.player.MediumCounterShell +
              "\n |  Counter Shell Heavy > " + Global.player.HeavyCounterShell+
              "\n |  Counter Shell Granade > " + Global.player.GranadeCounterShell+
              "\n |  Counter Shell Sniper > " + Global.player.SniperCounterShell+
              "\nDebug = " + Global.DEBUG +
              "\nF1 Spawn enemy" +
              "\nF2 Spawn MedChest" +
              "\nF3 Spawn Ammo kit" +
              "\nF12 Off/On Debug"

              ;
            //miniMap.Update(win);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            Content.DebugString.Draw(target,states);
            Content.Health.Draw(target,states);
            SelectedShellSprite.Draw(target,states);
            miniMap.Draw(target, states);

        }
    }
}
