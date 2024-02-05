using KWEngine3;

namespace Aimlabs.App
{
    public class GameWindow : GLWindow
    {
        public GameWindow() : base(1280, 720)
        {
            Title = "Aimlabs v0.0.1";

            GameWorld01 gws = new GameWorld01();
            SetWorld(gws);
        }
    }
}
