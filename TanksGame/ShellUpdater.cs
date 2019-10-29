using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class ShellUpdater:Drawable
    {
        private List<Shell> shellList;
        public int Counter { get => shellList.Count; }
        public ShellUpdater()
        {
            shellList = new List<Shell>();
        }
        public void AddShellToList(Shell shell)
        {
            shellList.Add(shell);
            //Console.WriteLine(shellList.Count);
        }
        public void CheckAllObjectCollision()
        {
            for (int i = 0; i < shellList.Count; i++)
            {
                if (shellList[i].ObjectShape.Position.X > Global.MAPSIZEX/2 || shellList[i].ObjectShape.Position.X < -Global.MAPSIZEX/2 ||
                    shellList[i].ObjectShape.Position.Y > Global.MAPSIZEY/2 || shellList[i].ObjectShape.Position.Y < -Global.MAPSIZEY/2)
                {
                    Global.ExplosionAnimation_.AddNewExplosion(shellList[i].ObjectSprite.Position);
                    shellList.Remove(shellList[i]);
                    continue;
                }
                if (CheckCollision.CheckCollisionRect(shellList[i].ObjectShape, Global.player.ObjectShape))
                {
                    shellList[i].Action(Global.player);
                    Global.ExplosionAnimation_.AddNewExplosion(shellList[i].ObjectSprite.Position);
                    shellList.Remove(shellList[i]);
                    continue;
                }

                foreach (var obj in Global.EnemyOnMap)
                {
                    if (CheckCollision.CheckCollisionRect(shellList[i].ObjectShape, obj.ObjectShape))
                    {
                        shellList[i].Action(obj);
                        Global.ExplosionAnimation_.AddNewExplosion(shellList[i].ObjectSprite.Position);
                        shellList.Remove(shellList[i]);
                        break;
                    }
                }
            }

        }
        public void Update()
        {
            foreach (var i in shellList)
                i.Update();
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var i in shellList) i.Draw(target,states);
        }
    }
}
