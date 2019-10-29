using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class MiniMap:Drawable
    {
        private readonly Sprite map;
        private readonly Sprite PlayerPoint;
        private readonly Sprite EnemyPoint;
        private readonly Sprite SecretPoint;
        private Vector2f MapCenterOffset;
        public MiniMap()
        {
            map = new Sprite(Content.MiniMapBackground);
            PlayerPoint = new Sprite(Content.PlayerPoint);
            PlayerPoint.Origin = new Vector2f(PlayerPoint.Texture.Size.X/2, PlayerPoint.Texture.Size.Y / 2);
            EnemyPoint = new Sprite(Content.EnemyPoint);
            SecretPoint = new Sprite(Content.SecretPoint);
            MapCenterOffset = new Vector2f(Global.MAPSIZEX/2,Global.MAPSIZEY/2);
            MapCenterOffset /= Global.MAPSCALE;
            PlayerPoint.Scale = new Vector2f(0.7f,0.7f);
            EnemyPoint.Scale = new Vector2f(2f,2f);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            map.Position = new Vector2f(Global.player.ObjectShape.Position.X + Global.SCREENWIDTH - 380,
           Global.player.ObjectShape.Position.Y - Global.SCREENHEIGHT);
            map.Draw(target,states);
            PlayerPoint.Position= map.Position + MapCenterOffset + Global.player.ObjectShape.Position / Global.MAPSCALE;
            PlayerPoint.Rotation = Global.player.ObjectShape.Rotation;
            PlayerPoint.Draw(target,states);
            foreach (var i in Global.EnemyOnMap)
            {
                EnemyPoint.Position = map.Position + MapCenterOffset + i.ObjectShape.Position / Global.MAPSCALE;
                EnemyPoint.Draw(target,states);
            }
            foreach (var i in Global.PredmetsOnMap)
            {
                SecretPoint.Position = map.Position + MapCenterOffset + i.ObjectShape.Position / Global.MAPSCALE;
                SecretPoint.Draw(target, states);
            }
        }
    }
}
