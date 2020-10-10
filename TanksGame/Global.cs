using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    enum BaseObjectType
    {
        barrier=0,
        medicalChest,
        Ammo,
        trap,
        shell,
        player,
        enemy
    }
    enum TypeSelectedShell
    {
        LightShell=0,
        MediumShell,
        HeavyShell,
        GranadeShell,
        SniperShell
    }
    static class Global
    {
        static Global()
        {
            MousePosition = new Vector2f(0,0);
            shellUpdater = new ShellUpdater();
            player = new Player();
            EnemyOnMap = new List<Tank>();
            PredmetsOnMap = new List<OtherObject>();
            ExplosionAnimation_ = new ExplosionAnimation();
            ObjectOnMapUpdater = new PredmetsOnMapUpdater();
            random = new Random();
        }
        public static bool DEBUG = false;
        public static bool GameOver = false;
        public static readonly ShellUpdater shellUpdater;
        public static readonly PredmetsOnMapUpdater ObjectOnMapUpdater;
        public static readonly ExplosionAnimation ExplosionAnimation_; 
        public static List<Tank> EnemyOnMap;
        public static List<OtherObject> PredmetsOnMap;
        public static readonly Player player;
        public static readonly Random random;

        public const int MAPSIZEX = 20000;
        public const int MAPSIZEY = 20000;
        public const int MAPSCALE = 50;

        public const float SCALE = 1f;
        public const float SPEEDTANKFORWARD = 20;
        public const float SPEEDTANKBACKWARD = 3;
        public const float SPEEDTANKROTATE = 1;
        public const int MAXHEALTHPLAYER = 150000000; 

        public const int LIGHTHELLSPEED = 48;
        public const int LIGHTSHELLDAMAGE = 10;

        public const int MEDIUMSHELLSPEED = 32;
        public const int MEDIUMSHELLDAMAGE = 20;

        public const int HEAVYSHELLSPEED = 32;
        public const int HEAVYSHELLDAMAGE = 50;

        public const int GRANADESHELLSPEED = 120;
        public const int GRANADESHELLDAMAGE = 100;

        public const int SNIPERSHELLSPEED = 180;
        public const int SNIPERSHELLDAMAGE = 70;

        public const uint SCREENWIDTH=1920;
        public const uint SCREENHEIGHT=1080;
        public const float DEFAULTHZOOMMAINCAM = 2.05f;
        public const string CONTENT = @"Content\";
        public const string DIRFONTS = @"Fonts\";
        public const string DIRIMAGES = @"Images\";
        public const string DIRSOUNDS = @"Sounds\";

        public static Vector2f MousePosition;
        //public static Vector2f PlayerPosition;
    }
}
