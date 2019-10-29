using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class ShotAnimation : Drawable
    {
        private readonly List<Sprite> ShotAnimationList;
        private int animationIndex = 0;
        public ShotAnimation()
        {
            ShotAnimationList = new List<Sprite>();
            ShotAnimationList.Add(new Sprite(Content.ShotAnimationTextures[0]));
            ShotAnimationList.Add(new Sprite(Content.ShotAnimationTextures[1]));
            ShotAnimationList.Add(new Sprite(Content.ShotAnimationTextures[2]));
            foreach (var i in ShotAnimationList)
            {
                i.Origin = new Vector2f(i.Texture.Size.X / 2, 180 * Global.SCALE);
                i.Scale = new Vector2f(Global.SCALE * 2f, Global.SCALE * 2f);
            }
        }
        public void Update(Vector2f Pos,float Rotation)
        {
            if (animationIndex < ShotAnimationList.Count)
            {
            ShotAnimationList[animationIndex].Position = Pos;
            ShotAnimationList[animationIndex].Rotation = Rotation;
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            ShotAnimationList[animationIndex].Draw(target,states);
            if (++animationIndex > ShotAnimationList.Count - 1) animationIndex = 0;
        }
    }
}
