using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    class TrackAnimation : Drawable
    {
        private readonly List<Sprite> Traks;
        private bool animation=false;
        private int DrawIndex1 = 0, DrawIndex2 = 2;
        public TrackAnimation(Vector2f Pos,int lengthToBeginning,int width)
        {
            Traks = new List<Sprite>();
            Traks.Add(new Sprite(Content.TrackTextures[0]));
            Traks.Add(new Sprite(Content.TrackTextures[1]));
            Traks.Add(new Sprite(Content.TrackTextures[0]));
            Traks.Add(new Sprite(Content.TrackTextures[1]));
            foreach (var i in Traks)
            {
                i.Position = Pos;
                i.Scale = new Vector2f(Global.SCALE, Global.SCALE);
            }
            Traks[0].Origin = new Vector2f(-width / 2 + 24, lengthToBeginning);
            Traks[1].Origin = new Vector2f(-width/2 + 24, lengthToBeginning);
            Traks[2].Origin = new Vector2f(width/2 + 18, lengthToBeginning);
            Traks[3].Origin = new Vector2f(width/2 + 18, lengthToBeginning);
        }
        public void Update(Vector2f Pos, float Rotation, bool isAnimation)
        {
            if (isAnimation)
                if (animation)
                {
                    DrawIndex1 = 0;
                    DrawIndex2 = 2;
                    animation = !animation;
                }
                else
                {
                    DrawIndex1 = 1;
                    DrawIndex2 = 3;
                    animation = !animation;
                }
            foreach (var i in Traks)
            {
                i.Position = Pos;
                i.Rotation = Rotation;
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            Traks[DrawIndex1].Draw(target, states);
            Traks[DrawIndex2].Draw(target, states);
        }
    }
}
