using KWEngine3;
using KWEngine3.GameObjects;
using Aimlabs.App.Classes;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using KWEngine3.Helper;
using System;
using System.Collections.Generic;
using System.Timers;
using System.ComponentModel.Design.Serialization;

namespace Aimlabs.App
{
    public class StartingWorld : World
    {
        private const float ESC_COOLDOWN = 1f;
        private float _esc_timestamp = 0f;
        private readonly HUDObjectImage crosshair = new();
        private readonly HUDObjectImage CrosshairHit = new();
        private readonly HUDObjectImage HUDOverlay = new();
        private HUDObjectText ingametime;
        private HUDObjectText botscorex;
        private HUDObjectText ballscorex;
        private HUDObjectText accuracy;
        private double time = 0;
        private double realtime = 0;

        public override void Act()
        {

            if (Keyboard.IsKeyDown(Keys.Escape) && WorldTime - _esc_timestamp > ESC_COOLDOWN)
            {
                if (GlobalSettings.IsPaused == false)
                {
                    MouseCursorReset();
                    GlobalSettings.IsPaused = true;
                }
                else
                {
                    MouseCursorGrab();
                    GlobalSettings.IsPaused = false;
                }
                _esc_timestamp = WorldTime;
            }
            if (Stats.hit == true)
            {
                crosshair.SetColor(0.3f, 0.1f, 0.1f);
                crosshair.SetScale(30, 20);
                AddHUDObject(CrosshairHit);
                Stats.hit = false;
            }
            else
            {
                crosshair.SetColor(0, 1, 0);
                crosshair.SetScale(30, 20);
                RemoveHUDObject(CrosshairHit);
            }
            if (Mouse.IsButtonDown(MouseButton.Left))
            {
                crosshair.SetColor(0.3f,0.1f,0.1f);
                crosshair.SetScale(30,20);
                AddHUDObject(CrosshairHit);
            }
            else
            {
                crosshair.SetColor(0,1,0);
                crosshair.SetScale(30,20);
               
                RemoveHUDObject(CrosshairHit);
            }
            if (realtime == 10)//10 für Test
            {
                Window.SetWorld(new Scoreboard());
            }
            //Console.WriteLine(Stats.leftmouseclicks);
            ingametime.SetText("Time:" + realtime);
            if(Stats.ballscore >= Stats.MovingBallscore)
            {
                ballscorex.SetText("Ballscore:" + Stats.ballscore);
            }
            else
            {
                ballscorex.SetText("Ballscore:" + Stats.MovingBallscore);
            }

            botscorex.SetText("Botscore:" + Stats.botscore);
            float allscore = Stats.botscore + Stats.ballscore + Stats.MovingBallscore;
            if (Stats.leftmouseclicks > 0)
            {
                Stats.accuracy = Math.Round(allscore / Stats.leftmouseclicks * 100.0f, 2);
                accuracy.SetText("Accuracy:" + Stats.accuracy + "%");
            }
            if (Stats.ballsspawned == true || Stats.botspawned == true || Stats.MovingBallspawned == true)
            {
                time++;
            }
            if (time == 240)
            {
                realtime++;
                time = 0;
            }
            if (Stats.botstart == true && Stats.botspawned == false)
            {
                KWEngine.CurrentWorld.RemoveGameObjectsOfType<Startbutton>(false);
                AddHUDObject(botscorex);
                int i2 = 0;
                while(Stats.botcount > i2)
                {
                    Target.spawnnewTarget();
                    i2++;
                }
                Stats.botspawned = true;
            }
            if(Stats.MovingBallstart == true && Stats.MovingBallspawned == false)
            {
                AddHUDObject(ballscorex);
                KWEngine.CurrentWorld.RemoveGameObjectsOfType<Startbutton>(false);
                int i1 = 0;
                while(Stats.movingcount > i1)
                {
                    MovingTargetBall.SpawnnewMovingTargetball();
                    i1++;
                }            
                Stats.MovingBallspawned = true;
            }
            if (Stats.ballstart == true && Stats.ballsspawned == false)
            {
                AddHUDObject(ballscorex);
                KWEngine.CurrentWorld.RemoveGameObjectsOfType<Startbutton>(false);
                int i3 = 0;
                while (Stats.ballcount > i3)
                {
                    Targetball.spawnnewTargetball();
                    i3++;
                }
                Stats.ballsspawned = true;
            }
        }

        public override void Prepare()
        {
            Stats.accuracy = 100.0f;
            KWEngine.LoadModel("Bot", "./App/Models/bot.gltf");
            /*foreach(string bonename in KWEngine.GetModelBoneNames("Bot"))
            {
                Console.WriteLine(bonename);
            }*/
            KWEngine.LoadModel("Gun", "./App/Models/BrowningHP.gltf");

            HUDOverlay.SetPosition(720f, 50f);
            HUDOverlay.SetTexture("./App/Textures/hudaimlabs.png");
            AddHUDObject(HUDOverlay);

            crosshair.SetPosition(Window.Width / 2, Window.Height / 2);
            crosshair.SetTexture("./app/Textures/Crosshair.png");
            CrosshairHit.SetPosition(Window.Width / 2, Window.Height / 2);
            CrosshairHit.SetTexture("./app/Textures/Corsshair hit.png");
            CrosshairHit.SetScale(15, 15);
            AddHUDObject(crosshair);
            SetCameraFOV(100);

            ingametime = new HUDObjectText("Time:" + WorldTime);
            ingametime.SetPosition(670f, 22f);
            ingametime.Name = "time";
            ingametime.SetScale(18);
            ingametime.SetFont(FontFace.NovaMono);
            AddHUDObject(ingametime);

            /*clicks = new HUDObjectText("Clicks:" + Stats.leftmouseclicks);
            clicks.SetPosition(100f, 166f);
            clicks.Name = "clicks";
            clicks.SetScale(18);
            clicks.SetFont(FontFace.NovaMono);
            AddHUDObject(clicks);*/

            accuracy = new HUDObjectText("Accuracy:" + Stats.accuracy);
            accuracy.SetPosition(950f, 22f);
            accuracy.Name = "accuracy";
            accuracy.SetScale(18);
            accuracy.SetFont(FontFace.NovaMono);
            AddHUDObject(accuracy);

            botscorex = new HUDObjectText("Botscore:" + Stats.botscore);
            botscorex.SetPosition(285f, 22f);
            botscorex.Name = "Botscore";
            botscorex.SetScale(18);
            botscorex.SetFont(FontFace.NovaMono);

            ballscorex = new HUDObjectText("Ballscore:" + Stats.ballscore);
            ballscorex.SetPosition(285f, 22f);
            ballscorex.Name = "Botscore";
            ballscorex.SetScale(18);
            ballscorex.SetFont(FontFace.NovaMono);

            SetColorAmbient(1, 1, 1);
            SetBackgroundSkybox("./App/Textures/SkyWater.dds", 0f, SkyboxType.CubeMap);
            SetBackgroundBrightnessMultiplier(1.2f);

            Floor f1 = new()
            {
                Name = "Floor"
            };
            f1.SetModel("KWCube");
            f1.SetScale(20f, 0.1f, 20f);
            f1.SetPosition(0.0f, -0.05f, 0.0f);
            f1.SetTexture("./App/Textures/tiles.png", TextureType.Albedo);
            f1.SetTextureRepeat(10, 10);
            f1.IsCollisionObject = true;
            f1.IsShadowCaster = true;
            AddGameObject(f1);

            Player p1 = new()
            {
                Name = "Player #1"
            };
            p1.SetScale(1);
            p1.SetPosition(8, 0, 0);
            p1.SetRotation(0, 90, 0);
            p1.IsCollisionObject = true;
            p1.IsShadowCaster = false;
            p1.SetOpacity(0);
            AddGameObject(p1);
            SetCameraToFirstPersonGameObject(p1, Player.CAM_OFFSET);

            // Anm. v. KAR: Kugelmodell unnötig da Standardmodell KWSphere für Kugeln bereits eingebaut ist.
            //              Außerdem fehlte die OBJ-Datei, weil sie nicht mit Rechtsklick -> "Ignorierte Datei zur Quellverwaltung..."
            //              hinzugefügt wurde (muss bei OBJ-Dateien passieren).
            //KWEngine.LoadModel("Sphere", "./App/Models/sphere.OBJ");

            /*Targetball s1 = new Targetball();
            s1.Name = "Sphere1";
            s1.SetModel("KWSphere");
            s1.SetScale(0.3f, 0.3f, 0.3f);
            s1.SetPosition(0, 2, 0);
            s1.IsCollisionObject = true;
            AddGameObject(s1);

            Targetball s2 = new Targetball();
            s2.Name = "Sphere1";
            s2.SetModel("KWSphere");
            s2.SetScale(0.3f, 0.3f, 0.3f);
            s2.SetPosition(1, 2, 0);
            s2.IsCollisionObject = true;
            AddGameObject(s2);*/

            Startbutton huen = new()
            {
                Name = "Ball"
            };
            huen.SetModel("KWQuad2D");
            huen.SetScale(1.6f, 1, 1);
            huen.SetPosition(1, 1, 0);
            huen.SetRotation(0, 90, 0);
            huen.IsCollisionObject = true;
            huen.SetTexture("./App/Textures/targets.png");
            huen.HasTransparencyTexture = true;
            AddGameObject(huen);

            Startbutton bots = new()
            {
                Name = "Bot"
            };
            bots.SetModel("KWQuad2D");
            bots.SetScale(1.4f, 0.8f, 0.8f);
            bots.SetPosition(1, 1.2f, 2);
            bots.SetRotation(0, 90, 0);
            bots.IsCollisionObject = true;
            bots.SetTexture("./App/Textures/botstest.png");
            bots.HasTransparencyTexture = true;
            AddGameObject(bots);

            Startbutton MovingBall = new()
            {
                Name = "MovingBall"
            };
            MovingBall.SetModel("KWQuad2D");
            MovingBall.SetScale(1.4f, 1, 1);
            MovingBall.SetPosition(1, 1, -2);
            MovingBall.SetRotation(0, 90, 0);
            MovingBall.IsCollisionObject = true;
            MovingBall.SetTexture("./App/Textures/Movingtargets.png");
            MovingBall.HasTransparencyTexture = true;
            AddGameObject(MovingBall);

            AddGameObject(new Walls("w1", 0, 0, 10, "./App/Textures/wandtextur.jpg", 20, 8, 0.5f, 0, 0, 0, 3, 3, 1));

            AddGameObject(new Walls("w2", 0, 0, -10, "./App/Textures/wandtextur.jpg", 20, 8, 0.5f, 0, 0, 0, 3, 3, 1));

            AddGameObject(new Walls("w3", 10, 0, 0, "./App/Textures/wandtextur.jpg", 20, 8, 0.5f, 0, 90, 0, 3, 3, 1));

            AddGameObject(new Walls("w4", -10, 0, 0, "./App/Textures/wandtextur.jpg", 20, 8, 0.5f, 0, 90, 0, 3, 3, 1));

            AddGameObject(new Walls("w5", 7.5f, 0, 0, "./App/Textures/Black.png", 20, 0.5f, 0.01f, 0, 90, 0, 3, 3, 1));

            AddGameObject(new Walls("w6", -2.60f, 4, 0, "./App/Textures/wandtextur.jpg", 15, 20.5f, 0.5f, 90, 0, 0, 3, 3, 1));
            
            AddGameObject(new Walls("w7", 8, 4, 0.2f, "./App/Textures/wandtextur.jpg", 6, 20.5f, 0.5f, 90, 0, 0, 3, 3, 0));

            Weapon fpw = new();
            fpw.SetModel("Gun");
            fpw.SetOffset(0.1f, -0.2f, 0.3f);
            fpw.SetScale(0.5f);
            SetViewSpaceGameObject(fpw);


            LightObject sun = new(LightType.Sun, ShadowQuality.High)
            {
                Name = "Sunlight"
            };
            sun.SetPosition(25f, 50f, 25f);
            sun.SetNearFar(40f, 125f);
            sun.SetFOV(65);
            sun.SetColor(1f, 1f, 1f, 3f);
            AddLightObject(sun);
            SetColorAmbient(0.5f, 0.5f, 0.5f);
            SetBackgroundBrightnessMultiplier(1.75f);

            MouseCursorGrab();

            KWEngine.MouseSensitivity = 0.1f;

        }
    }
}
