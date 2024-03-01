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
        }

        public override void Prepare()
        {
            crosshair. SetPosition(Window.Width / 2, Window.Height / 2);
            crosshair.SetTexture("./app/Textures/Crosshair.png");
            CrosshairHit.SetPosition(Window.Width / 2, Window.Height / 2);
            CrosshairHit.SetTexture("./app/Textures/Corsshair hit.png");
            CrosshairHit.SetScale(15, 15);
            AddHUDObject(crosshair);
            SetCameraFOV(100);

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
            p1.IsCollisionObject = true;
            p1.IsShadowCaster = false;
            p1.SetOpacity(0); 
            AddGameObject(p1);

            KWEngine.LoadModel("Sphere", "./App/Models/sphere.OBJ");

            Targetball s1 = new Targetball();
            s1.Name = "Sphere1";
            s1.SetModel("Sphere");
            s1.SetScale(0.3f, 0.3f, 0.3f);
            s1.SetPosition(0, 2, 0);
            s1.IsCollisionObject = true;
            AddGameObject(s1);

            Walls w1 = new();
            w1.SetPosition(0, 0, 10);
            w1.SetScale(20, 2, 0.5f);
            w1.SetTexture("./App/Textures/iron_panel_metal.dds");
            w1.IsCollisionObject = true;
            AddGameObject(w1);

            Walls w2 = new();
            w2.SetPosition(0, 0, -10);
            w2.SetScale(20, 2, 0.5f);
            w2.SetTexture("./App/Textures/iron_panel_metal.dds");
            w2.IsCollisionObject = true;
            AddGameObject(w2);

            Walls w3 = new();
            w3.SetPosition(10, 0,0);
            w3.SetScale(20, 2, 0.5f);
            w3.SetTexture("./App/Textures/iron_panel_metal.dds");
            w3.IsCollisionObject = true;
            w3.SetRotation(0, 90, 0);
            AddGameObject(w3);

            Walls w4 = new();
            w4.SetPosition(-10, 0,0);
            w4.SetScale(20, 2, 0.5f);
            w4.SetTexture("./App/Textures/iron_panel_metal.dds");
            w4.IsCollisionObject = true;
            w4.SetRotation(0, 90, 0);
            AddGameObject(w4);

            Walls w5 = new();
   
            w5.SetPosition(7.5f, 0, 0);
            w5.SetScale(20, 0.25f, 0.5f);
            w5.SetTexture("./App/Textures/iron_panel_metal.dds");
            w5.IsCollisionObject = true;
            w5.SetRotation(0, 90, 0);
            AddGameObject(w5);


            KWEngine.LoadModel("Gun", "./App/Models/BrowningHP.gltf");

            Weapon fpw = new Weapon();
            fpw.SetModel("Gun");
            fpw.SetOffset(0.1f, -0.2f, 0.3f);
            fpw.SetScale(0.5f); 
            SetViewSpaceGameObject(fpw);
            SetCameraToFirstPersonGameObject(p1, Player.CAM_OFFSET);
            LightObject sun = new LightObject(LightType.Sun, ShadowQuality.High);
            sun.Name = "Sunlight";
            sun.SetPosition(-50f, 50f, 25f);
            sun.SetNearFar(25f, 125f);
            sun.SetFOV(80);
            sun.SetColor(1f, 1f, 1f, 3f);
            AddLightObject(sun);

            MouseCursorGrab();

            KWEngine.MouseSensitivity = 0.1f;
        }
    }
}
