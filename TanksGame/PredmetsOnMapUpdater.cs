using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class PredmetsOnMapUpdater:Drawable
    {
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var i in Global.PredmetsOnMap)
            {
               i.Draw(target,states);
            }
        }

        public void Update()
        {
            for (int i = 0; i < Global.PredmetsOnMap.Count; i++)
            {
                if (CheckCollision.CheckCollisionRect(Global.PredmetsOnMap[i].ObjectShape, Global.player.ObjectShape))
                {
                    Global.PredmetsOnMap[i].Action(Global.player);
                    Global.PredmetsOnMap.Remove(Global.PredmetsOnMap[i]);
                    continue;
                }
                foreach (var obj in Global.EnemyOnMap)
                {
                    if (CheckCollision.CheckCollisionRect(Global.PredmetsOnMap[i].ObjectShape, obj.ObjectShape))
                    {
                        Global.PredmetsOnMap[i].Action(obj);
                        Global.PredmetsOnMap.Remove(Global.PredmetsOnMap[i]);
                        break;
                    }
                }
            }
        }

    }
}
