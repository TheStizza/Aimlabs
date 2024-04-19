using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using KWEngine3.GameObjects;
using KWEngine3;
using System;
using Aimlabs.App;

namespace GruppeC.App
{
    public class loading : World
    {
        public override void Act()
        {
            Window.SetWorld(new GameWorld01());
        }

        public override void Prepare()
        {
            SetBackground2D("./App/Textures/loading.png");
        }
    }
}
