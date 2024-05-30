using KWEngine3;

namespace Aimlabs.App
{
    public class GameWindow : GLWindow
    {
        public GameWindow() : base(1440, 720)
        {
            Title = "Aimlabs v0.0.1";

            Startscreen sws = new Startscreen();
            SetWorld(sws);
        }
    }
}
