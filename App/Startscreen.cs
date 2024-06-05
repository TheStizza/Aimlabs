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
        private HUDObjectImage start;
        private HUDObjectImage settings;
        private HUDObjectImage aimlabslogo;
        private HUDObjectImage left;
        private HUDObjectImage right;
        private HUDObjectImage back;
        private HUDObjectText settingstext;
        private HUDObjectText volume;
        private HUDObjectText loudness;
        private bool isSettingsVisible = false;

        public override void Act()
        {
            if (start.IsMouseCursorOnMe() == true)
            {
                start.SetColorEmissive(0.4f, 0.4f, 0.4f);
                start.SetColorEmissiveIntensity(0.3f);
            }
            else
            {
                start.SetColorEmissiveIntensity(0);
            }
            if (settings.IsMouseCursorOnMe() == true)
            {
                settings.SetColorEmissive(0.5f, 0.5f, 0.5f);
                settings.SetColorEmissiveIntensity(0.3f);
            }
            else
            {
                settings.SetColorEmissiveIntensity(0);
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
            if (isSettingsVisible == false && start.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                Window.SetWorld(new loading());
            }
            if (left.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.uservolume > 0)
                {
                    Stats.uservolume -= 1;
                    Stats.volume = (float)Stats.uservolume / 10;
                    loudness.SetText(" "+ Stats.uservolume);
                }
            }
            if (right.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.uservolume < 10)
                {
                    Stats.uservolume += 1;
                    Stats.volume = (float)Stats.uservolume / 10;
                    loudness.SetText(" " + Stats.uservolume);
                }
            }
            if (settings.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                ShowSettings();
            }
            if (back.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                HideSettings();
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

            start = new HUDObjectImage("./App/Textures/startbutton.png");
            start.SetPosition(740f, 390f);
            start.Name = "start";
            AddHUDObject(start);

            settings = new HUDObjectImage("./App/Textures/settings.png");
            settings.SetPosition(1373f, 65f);
            settings.Name = "settings";
            settings.SetScale(100f, 100f);
            AddHUDObject(settings);

            aimlabslogo = new HUDObjectImage("./App/Textures/aimlabslogo.png");
            aimlabslogo.SetPosition(740f, 100f);
            aimlabslogo.Name = "aimlabslogo";
            aimlabslogo.SetScale(500f, 100f);
            AddHUDObject(aimlabslogo);

            left = new HUDObjectImage("./App/Textures/arrowleft.png");
            left.SetPosition(Window.Width/ 1.85f, Window.Height/ 4);
            left.SetScale(80, 80);
            left.Name = "arrowleft";

            right = new HUDObjectImage("./App/Textures/arrowright.png");
            right.SetPosition(Window.Width / 1.60f, Window.Height / 4);
            right.SetScale(80, 80);
            right.Name = "arrowright";

            settingstext = new HUDObjectText("Settings");
            settingstext.SetPosition(Window.Width / 2, 50);
            settingstext.SetTextAlignment(TextAlignMode.Center);
            settingstext.SetFont(FontFace.XanhMono);
            settingstext.SetColor(1f, 1f, 1f);
            settingstext.SetScale(50);
            settingstext.Name = "settingstext";

            volume = new HUDObjectText("volume");
            volume.SetPosition(Window.Width / 2.5f, Window.Height / 4);
            volume.SetTextAlignment(TextAlignMode.Center);
            volume.SetFont(FontFace.XanhMono);
            volume.SetColor(1f, 1f, 1f);
            volume.SetScale(50);
            volume.Name = "volumetext";

            loudness = new HUDObjectText(" " + Stats.uservolume);
            loudness.SetPosition(Window.Width/1.7f, Window.Height / 4);
            loudness.SetTextAlignment(TextAlignMode.Center);
            loudness.SetFont(FontFace.XanhMono);
            loudness.SetColor(1f, 1f, 1f);
            loudness.SetScale(50);
            loudness.Name = "volumenumber";

            SetBackground2D("./App/Textures/backround.png");
        }
        private void ShowSettings()
        {
            RemoveHUDObject(start);
            RemoveHUDObject(aimlabslogo);
            RemoveHUDObject(settings);
            AddHUDObject(settingstext);
            AddHUDObject(volume);
            AddHUDObject(back);
            AddHUDObject(left);
            AddHUDObject(right);
            AddHUDObject(loudness);
            SetBackground2D("./App/Textures/Black.png");
            isSettingsVisible = true;
        }
        private void HideSettings()
        {
            RemoveHUDObject(settingstext);
            RemoveHUDObject(back);
            RemoveHUDObject(volume);
            RemoveHUDObject(left);
            RemoveHUDObject(right);
            RemoveHUDObject(loudness);
            AddHUDObject(start);
            AddHUDObject(aimlabslogo);
            AddHUDObject(settings);
            SetBackground2D("./App/Textures/backround.png");
            isSettingsVisible = false;
        }
    }
}
