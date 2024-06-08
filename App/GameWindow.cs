using KWEngine3;

namespace Aimlabs.App
{
    public class GameWindow : GLWindow
    {
        public GameWindow() : base(
            Math.Min(1920, KWEngine.ScreenInformation.PrimaryScreenWidth),                                          // Breite des primären Bildschirms
            Math.Min(1080, KWEngine.ScreenInformation.PrimaryScreenHeight),                                         // Höhe des primären Bildschirms
            true,                                                                                                   // VSync an?
            PostProcessingQuality.High,                                                                             // Renderqualität
            KWEngine.ScreenInformation.PrimaryScreenWidth > 1920 ? WindowMode.Default : WindowMode.BorderlessWindow // Art des Fensters (Standard oder rahmenlos)
            )
        {
            Title = "Aimlabs v0.0.1";

            Startscreen sws = new Startscreen();
            SetWorld(sws);
        }
    }
}
