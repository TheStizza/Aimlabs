    using KWEngine3;
using KWEngine3.GameObjects;
using Aimlabs.App.Classes;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Aimlabs.App
{
    public class GameWorld01 : World
    {
        private const float ESC_COOLDOWN = 1f;
        private float _esc_timestamp = 0f;
        private HUDObjectImage crosshair = new HUDObjectImage();
        private HUDObjectImage CrosshairHit = new HUDObjectImage();
        private HUDObjectText ingametime;
        private HUDObjectText botscorex;
        private HUDObjectText ballscorex;

        public override void Act()
        {
            if(Keyboard.IsKeyDown(Keys.Escape) && WorldTime - _esc_timestamp > ESC_COOLDOWN)
            {
                if(GlobalSettings.IsPaused == false)
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
            ingametime.SetText("Time:"+WorldTime);
            ballscorex.SetText("Ballscore:" + Stats.ballscore);
            botscorex.SetText("Botscore:" + Stats.botscore);
            if (Stats.start == true && Stats.spawned == false)
            {
                Targetball s1 = new Targetball();
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
                AddGameObject(s2);
                Stats.spawned = true;

                Targetball.spawnnewTargetball();
                   
            }
        }

        public override void Prepare()
        {
            KWEngine.LoadModel("Bot", "./App/Models/bot.gltf");
            KWEngine.LoadModel("Gun", "./App/Models/BrowningHP.gltf");

            crosshair. SetPosition(Window.Width / 2, Window.Height / 2);
            crosshair.SetTexture("./app/Textures/Crosshair.png");
            CrosshairHit.SetPosition(Window.Width / 2, Window.Height / 2);
            CrosshairHit.SetTexture("./app/Textures/Corsshair hit.png");
            CrosshairHit.SetScale(15, 15);
            AddHUDObject(crosshair);
            SetCameraFOV(100);

            ingametime = new HUDObjectText("Time:" + WorldTime);
            ingametime.SetPosition(100f, 34f);
            ingametime.Name = "time";
            ingametime.SetScale(18);
            ingametime.SetFont(FontFace.NovaMono);
            AddHUDObject(ingametime);

            botscorex = new HUDObjectText("Botscore:"+Stats.botscore);
            botscorex.SetPosition(100f, 100f);
            botscorex.Name = "Botscore";
            botscorex.SetScale(18);
            botscorex.SetFont(FontFace.NovaMono);
            AddHUDObject(botscorex);

            ballscorex = new HUDObjectText("Ballscore:" + Stats.ballscore);
            ballscorex.SetPosition(100f, 166f);
            ballscorex.Name = "Botscore";
            ballscorex.SetScale(18);
            ballscorex.SetFont(FontFace.NovaMono);
            AddHUDObject(ballscorex);


            SetColorAmbient(0.8f, 0.8f, 0.8f);
            SetBackgroundSkybox("./App/Textures/equirectangular_example.dds", 0f, SkyboxType.Equirectangular);
            SetBackgroundBrightnessMultiplier(1.2f);

            Floor f1 = new Floor();
            f1.Name = "Floor";
            f1.SetModel("KWCube");
            f1.SetScale(20f, 0.1f, 20f);
            f1.SetPosition(0.0f, -0.05f, 0.0f);
            f1.SetTexture("./App/Textures/iron_panel_albedo.dds", TextureType.Albedo);
            f1.SetTexture("./App/Textures/iron_panel_normal.dds", TextureType.Normal);
            f1.SetTexture("./App/Textures/iron_panel_metal.dds", TextureType.Metallic);
            f1.SetTexture("./App/Textures/iron_panel_roughness.dds", TextureType.Roughness);
            f1.SetTextureRepeat(10, 10);
            f1.IsCollisionObject = true;
            f1.IsShadowCaster = true;
            AddGameObject(f1);

            Player p1 = new Player();
            p1.Name = "Player #1";
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

            Startbutton huen = new Startbutton();
            huen.Name = "Sphere1";
            huen.SetScale(1, 2, 1);
            huen.SetPosition(1, 2, 0);
            huen.IsCollisionObject = true;
            huen.SetTexture("./App/Textures/start.jpg");
            huen.Name = "Starts";
            AddGameObject(huen);

            AddGameObject(new Walls("w1",0,0,10,"./App/Textures/wandtextur.jpg",20,8,0.5f,0,0,0));

            AddGameObject(new Walls("w2",0,0,-10, "./App/Textures/wandtextur.jpg", 20,8,0.5f,0,0,0));

            AddGameObject(new Walls("w3",10,0,0, "./App/Textures/wandtextur.jpg", 20,8,0.5f,0,90,0));

            AddGameObject(new Walls("w4",-10,0,0, "./App/Textures/wandtextur.jpg", 20,8,0.5f,0,90,0));

            AddGameObject(new Walls("w5",7.5f,0,0, "./App/Textures/Black.png", 20,0.5f,0.01f,0,90,0));

            AddGameObject(new Walls("w6", 0.05f, 4, 0.15f, "./App/Textures/wandtextur.jpg", 21, 20, 0.5f, 90, 0, 0));





            Weapon fpw = new Weapon();
            fpw.SetModel("Gun");
            fpw.SetOffset(0.1f, -0.2f, 0.3f);
            fpw.SetScale(0.5f); 
            SetViewSpaceGameObject(fpw);
            
            
            LightObject sun = new LightObject(LightType.Sun, ShadowQuality.High);
            sun.Name = "Sunlight";
            sun.SetPosition(25f, 50f, 25f);
            sun.SetNearFar(40f, 125f);
            sun.SetFOV(65);
            sun.SetColor(1f, 1f, 1f, 3f);
            AddLightObject(sun);
            SetColorAmbient(0.5f, 0.5f, 0.5f);
            SetBackgroundBrightnessMultiplier(1.75f);

            MouseCursorGrab();

            KWEngine.MouseSensitivity = 0.1f;

            // KAR: Test
            Target t1 = new Target();
            t1.SetModel("Bot");
            t1.SetRotation(0, 90, 0);
            t1.SetHitboxScale(0.4f, 1f, 1f);
            t1.SetScale(0.75f);
            t1.IsShadowCaster = true;
            t1.Name = "Enemy Bot";
            AddGameObject(t1);
        }
    }
}
