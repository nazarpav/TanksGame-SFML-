using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
/*
barrier=0,
medicalChest,
ammo,
trap,
Shell,
player,
enemy
*/
namespace TanksGame
{
    abstract class Tank : BaseObject, IAction
    {
        protected Sprite _gunSprite;
        protected TrackAnimation Track;
        protected ShotAnimation _ShotAnimation;
        protected int counterAnimationShot = 0;
        protected float HullRotated;
        public float HullRotate;
        public float Speed;
        protected Vector2f _dir;
        protected bool IsShot = false;
        protected int health = 50;
        public int Health { get => health; set => health = value; }


        public Tank(Sprite sprite, RectangleShape shape, BaseObjectType _type) : base(sprite, shape, _type)
        {
            _gunSprite = new Sprite(Content.GunTexture);
            _ShotAnimation = new ShotAnimation();
            _gunSprite.Texture.Smooth = true;
            _dir = new Vector2f();
            _gunSprite.Origin = new Vector2f(_gunSprite.Texture.Size.X / 2, 153);
            base.ObjectSprite.Origin = new Vector2f(base.ObjectSprite.Texture.Size.X / 2, 146);
            base._ObjectShape.Origin = base._ObjectSprite.Origin;
            _gunSprite.Scale = new Vector2f(Global.SCALE, Global.SCALE);
            Track = new TrackAnimation(base.ObjectSprite.Position, (int)base.ObjectSprite.Origin.Y, (int)base.ObjectSprite.Texture.Size.X);
        }
        public abstract void Action(TypeSelectedShell selectedShell);
        public override void Draw(RenderTarget target, RenderStates states)
        {
            this.Track.Draw(target, states);
            this.ObjectSprite.Draw(target, states);
            this._gunSprite.Draw(target, states);
            if (IsShot) this._ShotAnimation.Draw(target, states);
            if (Global.DEBUG)
                this.ObjectShape.Draw(target, states);
        }
    }
    abstract class OtherObject : BaseObject, IAction2
    {
        public OtherObject(Sprite sprite, RectangleShape shape, BaseObjectType _type) : base(sprite, shape, _type)
        {
        }
        public abstract void Action(Tank baseObject);
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base._ObjectSprite.Draw(target, states);
            if (Global.DEBUG)
                base._ObjectShape.Draw(target, states);
        }
    }
    abstract class Shell : BaseObject, IAction2
    {
        private readonly Vector2f Dir;
        private readonly int SPEED;
        private readonly int DAMEGE;
        public Shell(Sprite SpriteShell, int DAMEGE, int SPEED, Vector2f Pos, float Rotate) : base(SpriteShell, new RectangleShape((Vector2f)Content.MediumShellTexture.Size), BaseObjectType.shell)
        {
            this.SPEED = SPEED;
            this.DAMEGE = DAMEGE;
            base._ObjectSprite.Rotation = Rotate;
            base._ObjectShape.Rotation = base._ObjectSprite.Rotation;
            base._ObjectSprite.Scale = new Vector2f(Global.SCALE + 0.2f, Global.SCALE + 0.2f);
            base._ObjectShape.Scale = base._ObjectSprite.Scale;
            Dir.X = (float)Math.Cos((Rotate - 90) * (float)Math.PI / 180f);
            Dir.Y = (float)Math.Sin((Rotate - 90) * (float)Math.PI / 180f);
            base._ObjectSprite.Origin = new Vector2f(base._ObjectSprite.Texture.Size.X / 2, base._ObjectSprite.Texture.Size.Y / 2);
            base._ObjectShape.Origin = base._ObjectSprite.Origin;
            base._ObjectShape.FillColor = new Color(20, 250, 20, 80);
            base._ObjectShape.OutlineColor = Color.Black;
            base._ObjectShape.OutlineThickness = 4;
            //100
            base._ObjectSprite.Position = Pos + (197 * Global.SCALE) * Dir;
            base._ObjectShape.Position = base._ObjectSprite.Position;
        }
        public void Action(Tank tank)
        {
            tank.Health -= DAMEGE;
            if (tank.Health < 0)
            {
                if (tank.Type == BaseObjectType.enemy)
                    Global.EnemyOnMap.Remove(tank);
            }
        }
        public void Update()
        {
            base._ObjectSprite.Position += SPEED * Dir;
            base._ObjectShape.Position = base._ObjectSprite.Position;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            this.ObjectSprite.Draw(target, states);
            if (Global.DEBUG)
                base._ObjectShape.Draw(target, states);
        }
    }
    interface IAction
    {
        void Action(TypeSelectedShell selectedShell);
    }
    interface IAction2
    {
        void Action(Tank baseObj);
    }

    internal class Barrier : OtherObject
    {
        public Barrier(Sprite sprite, RectangleShape shape) : base(sprite, shape, BaseObjectType.barrier)
        {
        }

        public override void Action(Tank baseObject)
        {
            throw new NotImplementedException();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }
    }

    internal class MedicalChest : OtherObject
    {
        public MedicalChest(Vector2f Position) : base(new Sprite(Content.MedicalChest), new RectangleShape((Vector2f)Content.MedicalChest.Size), BaseObjectType.medicalChest)
        {
            base._ObjectSprite.Position = Position;
            base._ObjectShape.Position = base._ObjectSprite.Position;
            base._ObjectShape.FillColor = new Color(20, 70, 120, 150);
            base._ObjectShape.OutlineColor = Color.Black;
            base._ObjectShape.OutlineThickness = 5;
            ObjectSprite.Scale = new Vector2f(Global.SCALE / 1.5f, Global.SCALE / 1.5f);
            _ObjectShape.Scale = _ObjectSprite.Scale;
        }

        public override void Action(Tank baseObject)
        {
            baseObject.Health += Global.random.Next(30, 60);
        }
    }
    internal class Ammo : OtherObject
    {
        public Ammo(Vector2f Position) : base(new Sprite(Content.AmmoKit), new RectangleShape((Vector2f)Content.AmmoKit.Size), BaseObjectType.Ammo)
        {
            base._ObjectSprite.Position = Position;
            base._ObjectShape.Position = base._ObjectSprite.Position;
            base._ObjectShape.FillColor = new Color(220, 70, 20, 150);
            base._ObjectShape.OutlineColor = Color.Black;
            base._ObjectShape.OutlineThickness = 5;
            ObjectSprite.Scale = new Vector2f(Global.SCALE / 1.5f, Global.SCALE / 1.5f);
            _ObjectShape.Scale = _ObjectSprite.Scale;
        }

        public override void Action(Tank tank)
        {
            if (tank.Type == BaseObjectType.player)
                ((Player)tank).AddShell(Global.random.Next(0, 30),
                                                        Global.random.Next(0, 25),
                                                        Global.random.Next(0, 15),
                                                        Global.random.Next(0, 10),
                                                        Global.random.Next(0, 10)
                                                        );
        }
    }
    internal class Trap : OtherObject
    {
        public Trap(Sprite sprite, RectangleShape shape) : base(sprite, shape, BaseObjectType.trap)
        {
        }
        public override void Action(Tank tank)
        {
            throw new NotImplementedException();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }
    }
    class LightShell : Shell
    {
        public LightShell(Vector2f Pos, float Rotate) : base(new Sprite(Content.LightShellTexture), Global.LIGHTSHELLDAMAGE, Global.LIGHTHELLSPEED, Pos, Rotate)
        {
        }
    }
    class MediumShell : Shell
    {
        public MediumShell(Vector2f Pos, float Rotate) : base(new Sprite(Content.MediumShellTexture), Global.MEDIUMSHELLDAMAGE, Global.MEDIUMSHELLSPEED, Pos, Rotate)
        {
        }
    }
    class HeavyShell : Shell
    {
        public HeavyShell(Vector2f Pos, float Rotate) : base(new Sprite(Content.HeavyShellTexture), Global.HEAVYSHELLDAMAGE, Global.HEAVYSHELLSPEED, Pos, Rotate)
        {
        }
    }
    class GranadeShell : Shell
    {
        public GranadeShell(Vector2f Pos, float Rotate) : base(new Sprite(Content.GranadeShellTexture), Global.GRANADESHELLDAMAGE, Global.GRANADESHELLSPEED, Pos, Rotate)
        {
        }
    }
    class SniperShell : Shell
    {
        public SniperShell(Vector2f Pos, float Rotate) : base(new Sprite(Content.SniperShellTexture), Global.SNIPERSHELLDAMAGE, Global.SNIPERSHELLSPEED, Pos, Rotate)
        {
        }
    }
    class Player : Tank
    {
        public int LightCounterShell { get; private set; }
        public int MediumCounterShell { get; private set; }
        public int HeavyCounterShell { get; private set; }
        public int GranadeCounterShell { get; private set; }
        public int SniperCounterShell { get; private set; }
        internal TypeSelectedShell Shelltype { get; set; }

        public Player() : base(new Sprite(Content.HullTexture), new RectangleShape((Vector2f)Content.HullTexture.Size), BaseObjectType.player)
        {
            base.Health = Global.MAXHEALTHPLAYER;
            LightCounterShell = 1000000;
            MediumCounterShell = 1000000;
            HeavyCounterShell = 1000000;
            GranadeCounterShell = 10000000;
            SniperCounterShell = 1000000;

            base._ObjectSprite.Position = new Vector2f(0, 200);
            base._ObjectShape.Position = base._ObjectSprite.Position;
            base._ObjectShape.FillColor = new Color(20, 20, 255, 80);
            base._ObjectShape.OutlineColor = Color.Black;
            base._ObjectShape.OutlineThickness = 5;
        }
        public void AddShell(int LightShell, int MediumShell, int HeavyShell, int GranadeShell, int SniperShell)
        {
            if (LightShell >= 0 && MediumShell >= 0 && HeavyShell >= 0 && GranadeShell >= 0 && SniperShell >= 0)
            {
                this.LightCounterShell += LightShell;
                this.MediumCounterShell += MediumShell;
                this.HeavyCounterShell += HeavyShell;
                this.GranadeCounterShell += GranadeShell;
                this.SniperCounterShell += SniperShell;
            }
        }
        public override void Action(TypeSelectedShell selectedShell)
        {
            switch (selectedShell)
            {
                case TypeSelectedShell.LightShell:
                    if (LightCounterShell > 0)
                    {
                        Content.ShotSound.Play();
                        LightCounterShell--;
                        IsShot = true;
                        Global.shellUpdater.AddShellToList(new LightShell(_gunSprite.Position, _gunSprite.Rotation));
                    }
                    break;
                case TypeSelectedShell.MediumShell:
                    if (MediumCounterShell > 0)
                    {
                        Content.ShotSound.Play();
                        MediumCounterShell--;
                        IsShot = true;
                        Global.shellUpdater.AddShellToList(new MediumShell(_gunSprite.Position, _gunSprite.Rotation));
                    }
                    break;
                case TypeSelectedShell.HeavyShell:
                    if (HeavyCounterShell > 0)
                    {
                        Content.ShotSound.Play();
                        HeavyCounterShell--;
                        IsShot = true;
                        Global.shellUpdater.AddShellToList(new HeavyShell(_gunSprite.Position, _gunSprite.Rotation));
                    }
                    break;
                case TypeSelectedShell.GranadeShell:
                    if (GranadeCounterShell > 0)
                    {
                        Content.ShotSound.Play();
                        GranadeCounterShell--;
                        IsShot = true;
                        Global.shellUpdater.AddShellToList(new GranadeShell(_gunSprite.Position, _gunSprite.Rotation));
                    }
                    break;
                case TypeSelectedShell.SniperShell:
                    if (SniperCounterShell > 0)
                    {
                        Content.ShotSound.Play();
                        SniperCounterShell--;
                        IsShot = true;
                        Global.shellUpdater.AddShellToList(new SniperShell(_gunSprite.Position, _gunSprite.Rotation));
                    }
                    break;
                default:
                    break;
            }
        }
        public void Update()
        {
            if (Health <= 0) { Global.GameOver = true; return; }
            if (Health > Global.MAXHEALTHPLAYER)
                Health = Global.MAXHEALTHPLAYER;
            _gunSprite.Rotation = ((MathF.Atan2(Global.MousePosition.Y - base.ObjectSprite.Position.Y, Global.MousePosition.X - base.ObjectSprite.Position.X)) * 180f / MathF.PI) + 90;
            HullRotated += HullRotate;
            if (HullRotated > 360) HullRotated = 0;
            if (HullRotated < 0) HullRotated = 360;
            base._ObjectShape.Rotation = base.ObjectSprite.Rotation;
            base.ObjectSprite.Rotation = HullRotated;
            _dir.X = (float)Math.Cos((HullRotated - 90) * (float)Math.PI / 180f);
            _dir.Y = (float)Math.Sin((HullRotated - 90) * (float)Math.PI / 180f);
            if ((base.ObjectSprite.Position + Speed * _dir).X < Global.MAPSIZEX / 2 && (base.ObjectSprite.Position + Speed * _dir).X > -Global.MAPSIZEX / 2
               && (base.ObjectSprite.Position + Speed * _dir).Y < Global.MAPSIZEY / 2 && (base.ObjectSprite.Position + Speed * _dir).Y > -Global.MAPSIZEY / 2
               )
            {
                base.ObjectSprite.Position += Speed * _dir;
                base._ObjectShape.Position = base.ObjectSprite.Position;
                _gunSprite.Position = base.ObjectSprite.Position;
            }
            Global.player.ObjectSprite.Position = base.ObjectSprite.Position;
            Track.Update(base.ObjectSprite.Position, base.ObjectSprite.Rotation, (base.ObjectSprite.Position != base.ObjectSprite.Position + Speed * _dir || base.ObjectSprite.Rotation != HullRotated + HullRotate));
            if (++counterAnimationShot == 4)
            {
                IsShot = false;
                counterAnimationShot = 0;
            }
            else
                _ShotAnimation.Update(_gunSprite.Position, _gunSprite.Rotation);
        }
    }
    internal class Enemy : Tank
    {
        private RectangleShape HealthRect;
        private Clock clockShot;
        private Random rnd;
        public Enemy(Vector2f Position) : base(new Sprite(Content.HullTexture), new RectangleShape((Vector2f)Content.HullTexture.Size), BaseObjectType.enemy)
        {
            base.Health = 60;
            HealthRect = new RectangleShape(new Vector2f(10, Health));
            HealthRect.FillColor = Color.Green;
            HealthRect.OutlineColor = Color.Black;
            HealthRect.OutlineThickness = 3;
            HealthRect.Origin = new Vector2f(5, Health / 2);
            clockShot = new Clock();
            base.ObjectSprite.Position = new Vector2f(Global.random.Next(0, 200), Global.random.Next(0, 200));
            base.ObjectSprite.Color = new Color(255, 0, 0, 255);
            _gunSprite.Color = base.ObjectSprite.Color;

            base._ObjectShape.FillColor = new Color(255, 0, 0, 80);
            base._ObjectShape.OutlineColor = Color.Black;
            base._ObjectShape.OutlineThickness = 5;
            Track = new TrackAnimation(base.ObjectSprite.Position, (int)base.ObjectSprite.Origin.Y, (int)base.ObjectSprite.Texture.Size.X);
            rnd = new Random();
        }
        public override void Action(TypeSelectedShell selectedShell)
        {
            Content.ShotSound.Play();
            IsShot = true;
            Global.shellUpdater.AddShellToList(new MediumShell(_gunSprite.Position, _gunSprite.Rotation));
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            Update();
            this.Track.Draw(target, states);
            this.ObjectSprite.Draw(target, states);
            this._gunSprite.Draw(target, states);
            this.HealthRect.Draw(target, states);
            if (IsShot) _ShotAnimation.Draw(target, states);
            if (Global.DEBUG)
                base._ObjectShape.Draw(target, states);
        }
        public void Update()
        {
            if (Health > 80)
                Health = 80;
            if (MathF.Abs((Global.player.ObjectSprite.Position - ObjectSprite.Position).X) < 1000 && MathF.Abs((Global.player.ObjectSprite.Position - ObjectSprite.Position).Y) < 1000)
            {
                if (clockShot.ElapsedTime.AsMilliseconds() > 3000)
                {
                    Action(TypeSelectedShell.MediumShell);
                    clockShot.Restart();
                }
            }

            if (rnd.Next(0, 100) == 2)
                Speed = Global.SPEEDTANKFORWARD;
            if (rnd.Next(0, 200) == 4)
                Speed = -Global.SPEEDTANKBACKWARD;
            //Action();.
            if (rnd.Next(0, 2) == 1)
                if (rnd.Next(0, 2) == 1)
                    HullRotate = Global.SPEEDTANKROTATE * 2;
                else
                    HullRotate = -Global.SPEEDTANKROTATE * 2;


            HullRotated += HullRotate;
            if (HullRotated > 360) HullRotated = 0;
            if (HullRotated < 0) HullRotated = 360;
            _gunSprite.Rotation = ((MathF.Atan2(Global.player.ObjectSprite.Position.Y - base.ObjectSprite.Position.Y, Global.player.ObjectSprite.Position.X - base.ObjectSprite.Position.X)) * 180f / MathF.PI) + 90;
            base.ObjectSprite.Rotation = HullRotated;
            base._ObjectShape.Rotation = base.ObjectSprite.Rotation;
            _dir.X = (float)Math.Cos((HullRotated - 90) * (float)Math.PI / 180f);
            _dir.Y = (float)Math.Sin((HullRotated - 90) * (float)Math.PI / 180f);
            if ((base.ObjectSprite.Position + Speed * _dir).X < Global.MAPSIZEX/2 && (base.ObjectSprite.Position + Speed * _dir).X > -Global.MAPSIZEX/2
                && (base.ObjectSprite.Position + Speed * _dir).Y < Global.MAPSIZEY /2&& (base.ObjectSprite.Position + Speed * _dir).Y > -Global.MAPSIZEY/2
                )
            {
                base.ObjectSprite.Position += Speed * _dir;
                base._ObjectShape.Position = base.ObjectSprite.Position;
                _gunSprite.Position = base.ObjectSprite.Position;
            }
            HealthRect.Size = new Vector2f(10, Health);
            HealthRect.Position = _ObjectSprite.Position;
            HealthRect.Rotation = _ObjectSprite.Rotation + 90;
            Track.Update(base.ObjectSprite.Position, base.ObjectSprite.Rotation, (base.ObjectSprite.Position != base.ObjectSprite.Position + Speed * _dir || base.ObjectSprite.Rotation != HullRotated + HullRotate));
            if (counterAnimationShot++ == 3)
            {
                IsShot = false;
                counterAnimationShot = 0;
            }
            else
                _ShotAnimation.Update(_gunSprite.Position, _gunSprite.Rotation);
        }

    }
}
