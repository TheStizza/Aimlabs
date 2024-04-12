﻿using OpenTK.Windowing.GraphicsLibraryFramework;
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

            _g = new HUDObjectImage("./App/Textures/neusettings.png");
            _g.SetPosition(1400f, 50f);
            _g.Name = "settings";
            _g.SetScale(100f, 100f);
            AddHUDObject(_g);

            _f = new HUDObjectImage("./App/Textures/highscore.png");
            _f.SetPosition(100f, 50f);
            _f.Name = "settings";
            _f.SetScale(170f, 100f);
            AddHUDObject(_f);

            SetBackground2D("./App/Textures/backround.png");
        }
    }
}
