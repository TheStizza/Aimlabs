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
                Window.SetWorld(new GameWorld01());
            }
        }

        public override void Prepare()
        {
            _h = new HUDObjectImage("./App/Textures/startbutton2.png");
            _h.SetPosition(740f, 360f);
            _h.Name = "start";
            _h.SetScale(500f,280f);
            AddHUDObject(_h);

            SetBackground2D("./App/Textures/backround.png");
        }
    }
}
