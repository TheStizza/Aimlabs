using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using KWEngine3.GameObjects;
using KWEngine3;
using System;

namespace Aimlabs.App
{
    public class Settings : World
    {
        public override void Act()
        {
            
        }

        public override void Prepare()
        {
            HUDObjectText settings = new HUDObjectText("Settings");
            settings.SetPosition(Window.Width/2, 50);
            settings.SetTextAlignment(TextAlignMode.Center);
            settings.SetFont(FontFace.XanhMono);
            settings.SetColor(1f, 1f, 1f);
            settings.SetScale(50);
            settings.Name = ("settingsfont");
            AddHUDObject(settings);
        }
    }
}
