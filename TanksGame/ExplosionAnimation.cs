using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class ExplosionAnimation : Drawable
    {
        private readonly List<Sprite> ExplosionAnimationList;
        private Dictionary<Vector2f, int> ExplosionList;
        private List<Vector2f> AllObj;
        private Clock clock;
        public ExplosionAnimation()
        {
            clock = new Clock();
            ExplosionAnimationList = new List<Sprite>();
            ExplosionList = new Dictionary<Vector2f, int>();
            AllObj = new List<Vector2f>();
            for (int i = 0; i < 9; i++)ExplosionAnimationList.Add(new Sprite(Content.ExplosionAnimationTextures[i]));
            foreach (var i in ExplosionAnimationList)
            {
                i.Origin = new Vector2f(i.Texture.Size.X / 2, i.Texture.Size.Y / 2);
                i.Scale = new Vector2f(Global.SCALE*1.2f, Global.SCALE*1.2f);
            }
        }
        public void AddNewExplosion(Vector2f Pos)
        {
            if (ExplosionList.ContainsKey(Pos)) return;
            Content.ExplosionSound.Play();
            ExplosionList.Add(Pos,0);
            AllObj.Add(Pos);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
                foreach (var i in ExplosionList)
                {
                    ExplosionAnimationList[i.Value].Position = i.Key;
                    ExplosionAnimationList[ExplosionList[i.Key]].Draw(target, states);
                    ExplosionList.GetEnumerator().MoveNext();
                    //Console.WriteLine(ExplosionList[i.Key]);
                }
            if (clock.ElapsedTime.AsMilliseconds() > 15)
            {
                clock.Restart();
                for (int i = 0; i < AllObj.Count; i++)
                {
                    if (++ExplosionList[AllObj[i]] > 8)
                    {
                        ExplosionList.Remove(AllObj[i]);
                        AllObj.Remove(AllObj[i]);
                    }
                }
            }
        }
    }
}
