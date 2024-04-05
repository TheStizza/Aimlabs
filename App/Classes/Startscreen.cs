using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using KWEngine3.GameObjects;
using KWEngine3;
using System;
using Aimlabs.App;

namespace GruppeC.App
{
    public class Startscreen : World
    {
        private HUDObjectImage _h;
        public override void Act()
        {
            if (_h.IsMouseCursorOnMe() == true)
            {
                _h.SetColorEmissiveIntensity(1.5f);
            }
            else
            {
                _h.SetColorEmissiveIntensity(1.5f);
            }
            if (_h.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                Window.SetWorld(new loading());
            }
        }

        public override void Prepare()
        {
            _h = new HUDObjectImage("./App/Textures/startbutton2.png");
            _h.SetPosition(740f, 360f);
            _h.Name = "start";
            _h.SetScale(500f,280f);
            AddHUDObject(_h);

            _h = new HUDObjectImage("./App/Textures/settings2.png");
            _h.SetPosition(1400f, 50f);
            _h.Name = "settings";
            _h.SetScale(100f, 100f);
            AddHUDObject(_h);

            SetBackground2D("./App/Textures/backround.png");
        }
    }
}
