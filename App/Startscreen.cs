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
        private HUDObjectImage left;
        private HUDObjectImage right;
        private HUDObjectImage back;
        private HUDObjectText settings;
        private HUDObjectText volume;
        private bool isSettingsVisible = false;

        public override void Act()
        {
            if (_h.IsMouseCursorOnMe() == true)
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
            if (left.IsMouseCursorOnMe() == true)
            {
                left.SetColorEmissive(1, 1, 1);
                left.SetColorEmissiveIntensity(0.5f);
            }
            else
            {
                left.SetColorEmissive(0, 0, 0);
                left.SetColorEmissiveIntensity(0.6f);
            }
            if (right.IsMouseCursorOnMe() == true)
            {
                right.SetColorEmissive(1, 1, 1);
                right.SetColorEmissiveIntensity(0.5f);
            }
            else
            {
                right.SetColorEmissive(0, 0, 0);
                right.SetColorEmissiveIntensity(0.6f);
            }
            if (isSettingsVisible == false && _h.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                Window.SetWorld(new loading());
            }
            if (left.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                Stats.volume -= 0.03f;
            }
            if (right.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                Stats.volume += 0.03f;
            }
            if (_g.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                RemoveHUDObject(_h);
                RemoveHUDObject(_e);
                RemoveHUDObject(_g);
                AddHUDObject(settings);
                AddHUDObject(volume);
                AddHUDObject(back);
                AddHUDObject(left);
                AddHUDObject(right);
                SetBackground2D("./App/Textures/Black.png");
                isSettingsVisible = true;
            }
            if (back.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                RemoveHUDObject(settings);
                RemoveHUDObject(back);
                RemoveHUDObject(volume);
                RemoveHUDObject(left);
                RemoveHUDObject(right);
                AddHUDObject(_h);
                AddHUDObject(_e);
                AddHUDObject(_g);
                SetBackground2D("./App/Textures/backround.png");
                isSettingsVisible = false;
            }
        }
        public override void Prepare()
        {
            back = new HUDObjectImage("./App/Textures/backarrow.png");
            back.SetPosition(120, 70);
            back.SetColorEmissive(0, 1, 0);
            back.SetColorEmissiveIntensity(0.3f);
            back.SetScale(160);
            back.Name = "back";

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

            left = new HUDObjectImage("./App/Textures/arrowleft.png");
            left.SetPosition(Window.Width/ 1.86f, Window.Height/ 4);
            left.SetScale(80, 80);
            //left.SetColorEmissive(0, 1, 0);
            //left.SetColorEmissiveIntensity(0.3f);
            left.Name = "arrowleft";

            right = new HUDObjectImage("./App/Textures/arrowright.png");
            right.SetPosition(Window.Width / 1.7f, Window.Height / 4);
            right.SetScale(80, 80);
            //right.SetColorEmissive(0, 1, 0);
            //right.SetColorEmissiveIntensity(0.3f);
            right.Name = "arrowright";

            settings = new HUDObjectText("Settings");
            settings.SetPosition(Window.Width / 2, 50);
            settings.SetTextAlignment(TextAlignMode.Center);
            settings.SetFont(FontFace.XanhMono);
            settings.SetColor(1f, 1f, 1f);
            settings.SetScale(50);
            settings.Name = "settingsfont";

            volume = new HUDObjectText("volume");
            volume.SetPosition(Window.Width / 2.5f, Window.Height / 4);
            volume.SetTextAlignment(TextAlignMode.Center);
            volume.SetFont(FontFace.XanhMono);
            volume.SetColor(1f, 1f, 1f);
            volume.SetScale(50);
            volume.Name = "volumefont";

            SetBackground2D("./App/Textures/backround.png");
        }
    }
}
