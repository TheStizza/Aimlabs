using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using KWEngine3.GameObjects;
using KWEngine3;
using System;
using Aimlabs.App.Classes;
using System.Transactions;
using System.Xml.Linq;

namespace Aimlabs.App
{
    public class Startscreen : World
    {
        private HUDObjectImage _h;
        private HUDObjectImage _g;
        private HUDObjectImage _e;
        private HUDObjectImage _f;
        private HUDObjectImage _k;
        private HUDObjectText _l;
        private HUDObjectImage back;
        private HUDObjectText settings;
        private bool isSettingsVisible = false;
        private HUDObjectText ingametime;

        public override void Act()
        {
            if (_h != null && _h.IsMouseCursorOnMe() == true)
            {
                _h.SetColorEmissive(0.4f, 0.4f, 0.4f);
                _h.SetColorEmissiveIntensity(0.3f);
            }
            else
            {
                _h.SetColorEmissiveIntensity(0);
            }
            if (_g.IsMouseCursorOnMe() == true)
            {
                _g.SetColorEmissive(0.5f, 0.5f, 0.5f);
                _g.SetColorEmissiveIntensity(0.3f);
            }
            else
            {
                _g.SetColorEmissiveIntensity(0);
            }
            if (back.IsMouseCursorOnMe() == true)
            {
                back.SetColorEmissive(1, 1, 1);
                back.SetColorEmissiveIntensity(0.5f);
            }
            else
            {
                back.SetColorEmissive(0, 1, 0);
                back.SetColorEmissiveIntensity(0.3f);
            }
            if (isSettingsVisible == false && _h.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                Window.SetWorld(new loading());
            }
            if (_g.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                RemoveHUDObject(_h);
                RemoveHUDObject(_e);
                RemoveHUDObject(_g);
                AddHUDObject(settings);
                AddHUDObject(back);
                SetBackground2D("./App/Textures/Black.png");
                isSettingsVisible = true;
            }
            if (back.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                RemoveHUDObject(settings);
                RemoveHUDObject(back);
                AddHUDObject(_h);
                AddHUDObject(_e);
                AddHUDObject(_g);
                SetBackground2D("./App/Textures/backround.png");
                isSettingsVisible = false;
            }
        }
        public override void Prepare()
        {
            _h = new HUDObjectImage("./App/Textures/startbutton.png");
            _h.SetPosition(740f, 390f);
            _h.Name = "start";
            AddHUDObject(_h);

            _g = new HUDObjectImage("./App/Textures/settings.png");
            _g.SetPosition(1373f, 65f);
            _g.Name = "settings";
            _g.SetScale(100f, 100f);
            AddHUDObject(_g);

            _e = new HUDObjectImage("./App/Textures/aimlabslogo.png");
            _e.SetPosition(740f, 100f);
            _e.Name = "aimlabslogo";
            _e.SetScale(500f, 100f);
            AddHUDObject(_e);

            _f = new HUDObjectImage("./App/Textures/backarrow.png");
            _f.SetPosition(120, 70);
            _f.SetColorEmissive(0, 1, 0);
            _f.SetColorEmissiveIntensity(0.3f);
            _f.SetScale(160);
            _f.Name = "up";

            _k = new HUDObjectImage("./App/Textures/backarrow.png");
            _k.SetPosition(120, 70);
            _k.SetColorEmissive(0, 1, 0);
            _k.SetColorEmissiveIntensity(0.3f);
            _k.SetScale(160);
            _k.Name = "down";

            ingametime = new HUDObjectText("Time:" + WorldTime);
            ingametime.SetPosition(670f, 22f);
            ingametime.Name = "time";
            ingametime.SetScale(18);
            ingametime.SetFont(FontFace.NovaMono);
            AddHUDObject(ingametime);

            settings = new HUDObjectText("Settings");
            settings.SetPosition(Window.Width / 2, 50);
            settings.SetTextAlignment(TextAlignMode.Center);
            settings.SetFont(FontFace.XanhMono);
            settings.SetColor(1f, 1f, 1f);
            settings.SetScale(50);
            settings.Name = "settingsfont";

            SetBackground2D("./App/Textures/backround.png");
        }
    }
}
