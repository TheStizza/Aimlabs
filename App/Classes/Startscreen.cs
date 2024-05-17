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
        private HUDObjectImage _g;
        private HUDObjectImage _f;
        private HUDObjectImage _e;
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

            _g = new HUDObjectImage("./App/Textures/settings.png");
            _g.SetPosition(1373f, 65f);
            _g.Name = "settings";
            _g.SetScale(100f, 100f);
            AddHUDObject(_g);

            /*_f = new HUDObjectImage("./App/Textures/highscore.png");
            _f.SetPosition(100f, 50f);
            _f.Name = "highscore";
            _f.SetScale(170f, 100f);
            AddHUDObject(_f);*/

            _e = new HUDObjectImage("./App/Textures/aimlabslogo.png");
            _e.SetPosition(740f, 100f);
            _e.Name = "aimlabslogo";
            _e.SetScale(500f, 100f);
            AddHUDObject(_e);

            SetBackground2D("./App/Textures/backround.png");
        }
    }
}
