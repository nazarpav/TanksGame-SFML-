using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    abstract class BaseObject:Drawable
    {
        protected readonly Sprite _ObjectSprite ;
        protected readonly RectangleShape _ObjectShape;
        protected readonly BaseObjectType _type;
        public Sprite ObjectSprite => _ObjectSprite;
        public RectangleShape ObjectShape => _ObjectShape;
        internal BaseObjectType Type => _type;


        public BaseObject(Sprite sprite, RectangleShape shape, BaseObjectType _type)
        {
            this._ObjectSprite = sprite;
            this._ObjectShape = shape;
            this._type = _type;
            ObjectSprite.Scale = new Vector2f(Global.SCALE, Global.SCALE);
            _ObjectShape.Scale = _ObjectSprite.Scale;
            _ObjectSprite.Texture.Smooth = true;
        }

        abstract public void Draw(RenderTarget target, RenderStates states);
    }
}
