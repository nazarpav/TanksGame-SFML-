using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksGame
{
    static class Content
    {
        public static readonly Texture GunTexture;
        public static readonly Texture HullTexture;
        
        public static readonly Texture LightShellTexture;
        public static readonly Texture MediumShellTexture;
        public static readonly Texture HeavyShellTexture;
        public static readonly Texture GranadeShellTexture;
        public static readonly Texture SniperShellTexture;

        public static readonly Texture AmmoKit;
        public static readonly Texture MedicalChest;
        public static readonly Texture MiniMapBackground;
        public static readonly Texture EnemyPoint;
        public static readonly Texture PlayerPoint;
        public static readonly Texture SecretPoint;
        public static readonly List<Texture> TrackTextures;
        public static readonly List<Texture> ShotAnimationTextures;
        public static readonly List<Texture> ExplosionAnimationTextures;
        public static readonly Sound ShotSound;
        public static readonly Sound ExplosionSound;
        public static readonly Text Health; 
        public static readonly Text DebugString; 
        static Content()
        {
            GunTexture = new Texture(Global.CONTENT+Global.DIRIMAGES+ "Gun1.png");
            HullTexture = new Texture(Global.CONTENT+Global.DIRIMAGES+ "Hull1.png");

            LightShellTexture = new Texture(Global.CONTENT+Global.DIRIMAGES+ "LightShell.png");
            MediumShellTexture = new Texture(Global.CONTENT+Global.DIRIMAGES+ "MediumShell.png");
            HeavyShellTexture = new Texture(Global.CONTENT+Global.DIRIMAGES+ "HeavyShell.png");
            GranadeShellTexture = new Texture(Global.CONTENT+Global.DIRIMAGES+ "GranadeShell.png");
            SniperShellTexture = new Texture(Global.CONTENT+Global.DIRIMAGES+ "SniperShell.png");

            AmmoKit = new Texture(Global.CONTENT+Global.DIRIMAGES+ "Ammo.png");
            MedicalChest = new Texture(Global.CONTENT+Global.DIRIMAGES+ "MedicalChest.png");
            
            MiniMapBackground = new Texture(Global.CONTENT + Global.DIRIMAGES + "MiniMapBackground.png");
            EnemyPoint = new Texture(Global.CONTENT + Global.DIRIMAGES + "EnemyPoint.png");
            PlayerPoint = new Texture(Global.CONTENT + Global.DIRIMAGES + "PlayerArrow.png");
            SecretPoint = new Texture(Global.CONTENT + Global.DIRIMAGES + "SecretPoint.png");
            
            TrackTextures = new List<Texture>();
            TrackTextures.Add(new Texture(Global.CONTENT + Global.DIRIMAGES + "Track1.png"));
            TrackTextures.Add(new Texture(Global.CONTENT + Global.DIRIMAGES + "Track2.png"));
            ShotAnimationTextures = new List<Texture>();
            ShotAnimationTextures.Add(new Texture(Global.CONTENT + Global.DIRIMAGES + "Shot1.png"));
            ShotAnimationTextures.Add(new Texture(Global.CONTENT + Global.DIRIMAGES + "Shot2.png"));
            ShotAnimationTextures.Add(new Texture(Global.CONTENT + Global.DIRIMAGES + "Shot3.png"));
            ExplosionAnimationTextures = new List<Texture>();
            for (int i = 0; i < 9; i++)
                ExplosionAnimationTextures.Add(new Texture(Global.CONTENT + Global.DIRIMAGES + "Explosion"+i+".png"));
            Health = new Text("Health", new Font(Global.CONTENT + Global.DIRFONTS+ "Font1.otf"));
            DebugString = new Text("Quantity tank => ", new Font(Global.CONTENT + Global.DIRFONTS+ "Font1.otf"));
            Health.OutlineColor = Color.Black;
            DebugString.OutlineColor = Color.Black;
            Health.OutlineThickness = 2;
            DebugString.OutlineThickness = 2;
            ShotSound = new Sound(new SoundBuffer(Global.CONTENT + Global.DIRSOUNDS+"Shot3.wav"));
            //ShotSound.Volume = 40;
            //ShotSound.Pitch = 0.8f;
            ExplosionSound = new Sound(new SoundBuffer(Global.CONTENT + Global.DIRSOUNDS+ "Explosion.wav"));
            //ExplosionSound.Pitch = 0.8f;
        }
    }
}
