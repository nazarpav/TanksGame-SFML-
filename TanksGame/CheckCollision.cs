using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    static class CheckCollision
    {
            private static bool PointIntol(Vector2f p, Vector2f a, Vector2f b, float r)
            {
                float ra = MathF.Atan2(p.Y - a.Y, p.X - a.X) * 180f / MathF.PI + 180;
                float rb = MathF.Atan2(p.Y - b.Y, p.X - b.X) * 180f / MathF.PI;

                float rar = (ra - r < 0) ? ra - r + 360 : ra - r;
                float rbr = (rb - r < 0) ? rb - r + 360 : rb - r;
                if (rar > 180 && rar < 270 && rbr > 180 && rbr < 270) return true;

                return false;
            }
        public static bool CheckCollisionRect(RectangleShape rectangleShape1, RectangleShape rectangleShape2)
        {

            Vector2f aa = rectangleShape1.Transform.TransformPoint(rectangleShape1.GetPoint(0));
            Vector2f ab = rectangleShape1.Transform.TransformPoint(rectangleShape1.GetPoint(1));
            Vector2f ac = rectangleShape1.Transform.TransformPoint(rectangleShape1.GetPoint(2));
            Vector2f ad = rectangleShape1.Transform.TransformPoint(rectangleShape1.GetPoint(3));

            Vector2f ba = rectangleShape2.Transform.TransformPoint(rectangleShape2.GetPoint(0));
            Vector2f bb = rectangleShape2.Transform.TransformPoint(rectangleShape2.GetPoint(1));
            Vector2f bc = rectangleShape2.Transform.TransformPoint(rectangleShape2.GetPoint(2));
            Vector2f bd = rectangleShape2.Transform.TransformPoint(rectangleShape2.GetPoint(3));

            return
            PointIntol(aa, ba, bc, rectangleShape2.Rotation) ||
            PointIntol(ab, ba, bc, rectangleShape2.Rotation) ||
            PointIntol(ac, ba, bc, rectangleShape2.Rotation) ||
            PointIntol(ad, ba, bc, rectangleShape2.Rotation) ||
            PointIntol(ba, aa, ac, rectangleShape1.Rotation) ||
            PointIntol(bb, aa, ac, rectangleShape1.Rotation) ||
            PointIntol(bc, aa, ac, rectangleShape1.Rotation) ||
            PointIntol(bd, aa, ac, rectangleShape1.Rotation);
        }
    }
}
