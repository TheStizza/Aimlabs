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
        private HUDObjectImage ballleft;
        private HUDObjectImage ballright;
        private HUDObjectImage botleft;
        private HUDObjectImage botright;
        private HUDObjectImage movingleft;
        private HUDObjectImage movingright;
        private HUDObjectText movingcount;
        private HUDObjectText ballcount;
        private HUDObjectText botcount;
        private HUDObjectImage back;
        private HUDObjectText settingstext;
        private HUDObjectText volume;
        private HUDObjectText loudness;
        private HUDObjectText ballcounttext;
        private HUDObjectText botcounttext;
        private HUDObjectText movingcounttext;
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
            //Sound
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
            //Bots
            if (botleft.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.botcount > 1)
                {
                    Stats.botcount -= 1;
                    botcount.SetText(" " + Stats.botcount);
                }
            }
            if (botright.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.botcount < 10)
                {
                    Stats.botcount += 1;
                    botcount.SetText(" " + Stats.botcount);
                }
            }
            //Balls
            if (ballleft.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.ballcount > 1)
                {
                    Stats.ballcount -= 1;
                    ballcount.SetText(" " + Stats.ballcount);
                }
            }
            if (ballright.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.ballcount < 10)
                {
                    Stats.ballcount += 1;
                    ballcount.SetText(" " + Stats.ballcount);
                }
            }
            //movingballs
            if (movingleft.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.movingcount > 1)
                {
                    Stats.movingcount -= 1;
                    movingcount.SetText(" " + Stats.movingcount);
                }
            }
            if (movingright.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                if (Stats.movingcount < 10)
                {
                    Stats.movingcount += 1;
                    movingcount.SetText(" " + Stats.movingcount);
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
            start.SetPosition(Window.Width/1.94f, Window.Height / 2);
            start.Name = "start";
            AddHUDObject(start);

            settings = new HUDObjectImage("./App/Textures/settings.png");
            settings.SetPosition(Window.Width/1.05f, Window.Height/10);
            settings.Name = "settings";
            settings.SetScale(100f, 100f);
            AddHUDObject(settings);

            aimlabslogo = new HUDObjectImage("./App/Textures/aimlabslogo.png");
            aimlabslogo.SetPosition(Window.Width/1.94f, Window.Height/4.5f);
            aimlabslogo.Name = "aimlabslogo";
            aimlabslogo.SetScale(600f, 100f);
            AddHUDObject(aimlabslogo);

            left = new HUDObjectImage("./App/Textures/arrowleft.png");
            left.SetPosition(Window.Width/1.85f, Window.Height/4);
            left.SetScale(80, 80);
            left.Name = "arrowleft";

            right = new HUDObjectImage("./App/Textures/arrowright.png");
            right.SetPosition(Window.Width/ 1.60f, Window.Height/4);
            right.SetScale(80, 80);
            right.Name = "arrowright";

            ballleft = new HUDObjectImage("./App/Textures/arrowleft.png");
            ballleft.SetPosition(Window.Width / 1.85f, Window.Height / 3);
            ballleft.SetScale(80, 80);
            ballleft.Name = "ballleft";

            ballright = new HUDObjectImage("./App/Textures/arrowright.png");
            ballright.SetPosition(Window.Width / 1.60f, Window.Height / 3);
            ballright.SetScale(80, 80);
            ballright.Name = "ballright";

            botleft = new HUDObjectImage("./App/Textures/arrowleft.png");
            botleft.SetPosition(Window.Width / 1.85f, Window.Height / 2.388f);
            botleft.SetScale(80, 80);
            botleft.Name = "botleft";

            botright = new HUDObjectImage("./App/Textures/arrowright.png");
            botright.SetPosition(Window.Width / 1.60f, Window.Height / 2.388f);
            botright.SetScale(80, 80);
            botright.Name = "botright";

            movingleft = new HUDObjectImage("./App/Textures/arrowleft.png");
            movingleft.SetPosition(Window.Width / 1.85f, Window.Height / 2f);
            movingleft.SetScale(80, 80);
            movingleft.Name = "movingleft";

            movingright = new HUDObjectImage("./App/Textures/arrowright.png");
            movingright.SetPosition(Window.Width / 1.60f, Window.Height / 2f);
            movingright.SetScale(80, 80);
            movingright.Name = "movingright";

            settingstext = new HUDObjectText("Settings");
            settingstext.SetPosition(Window.Width/2, 50);
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

            ballcounttext = new HUDObjectText("Ballcount:");
            ballcounttext.SetPosition(Window.Width / 2.5f, Window.Height / 3);
            ballcounttext.SetTextAlignment(TextAlignMode.Center);
            ballcounttext.SetFont(FontFace.XanhMono);
            ballcounttext.SetColor(1f, 1f, 1f);
            ballcounttext.SetScale(50);
            ballcounttext.Name = "ballcounttext";

            botcounttext = new HUDObjectText("Botcount:");
            botcounttext.SetPosition(Window.Width / 2.5f, Window.Height / 2.388f);
            botcounttext.SetTextAlignment(TextAlignMode.Center);
            botcounttext.SetFont(FontFace.XanhMono);
            botcounttext    .SetColor(1f, 1f, 1f);
            botcounttext.SetScale(50);
            botcounttext.Name = "botcounttext";

            movingcounttext = new HUDObjectText("Movingcount:");
            movingcounttext.SetPosition(Window.Width / 2.5f, Window.Height / 2);
            movingcounttext.SetTextAlignment(TextAlignMode.Center);
            movingcounttext.SetFont(FontFace.XanhMono);
            movingcounttext.SetColor(1f, 1f, 1f);
            movingcounttext.SetScale(50);
            movingcounttext.Name = "movingcounttext";

            loudness = new HUDObjectText(" " + Stats.uservolume);
            loudness.SetPosition(Window.Width/1.7f, Window.Height / 4);
            loudness.SetTextAlignment(TextAlignMode.Center);
            loudness.SetFont(FontFace.XanhMono);
            loudness.SetColor(1f, 1f, 1f);
            loudness.SetScale(50);
            loudness.Name = "volumenumber";

            ballcount = new HUDObjectText(" " + Stats.ballcount);
            ballcount.SetPosition(Window.Width / 1.7f, Window.Height / 3);
            ballcount.SetTextAlignment(TextAlignMode.Center);
            ballcount.SetFont(FontFace.XanhMono);
            ballcount.SetColor(1f, 1f, 1f);
            ballcount.SetScale(50);
            ballcount.Name = "volumenumber";

            botcount = new HUDObjectText(" " + Stats.botcount);
            botcount.SetPosition(Window.Width / 1.7f, Window.Height / 2.388f);
            botcount.SetTextAlignment(TextAlignMode.Center);
            botcount.SetFont(FontFace.XanhMono);
            botcount.SetColor(1f, 1f, 1f);
            botcount.SetScale(50);
            botcount.Name = "volumenumber";

            movingcount = new HUDObjectText(" " + Stats.movingcount);
            movingcount.SetPosition(Window.Width / 1.7f, Window.Height / 2);
            movingcount.SetTextAlignment(TextAlignMode.Center);
            movingcount.SetFont(FontFace.XanhMono);
            movingcount.SetColor(1f, 1f, 1f);
            movingcount.SetScale(50);
            movingcount.Name = "volumenumber";

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
            AddHUDObject(ballcount);
            AddHUDObject(ballleft);
            AddHUDObject(ballright);
            AddHUDObject(botcount);
            AddHUDObject(botleft);
            AddHUDObject(botright);
            AddHUDObject(movingcount);
            AddHUDObject(movingleft);
            AddHUDObject(movingright);
            AddHUDObject(ballcounttext);
            AddHUDObject(botcounttext);
            AddHUDObject(movingcounttext);
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
            RemoveHUDObject(ballcount);
            RemoveHUDObject(ballleft);
            RemoveHUDObject(ballright);
            RemoveHUDObject(botcount);
            RemoveHUDObject(botleft);
            RemoveHUDObject(botright);
            RemoveHUDObject(movingcount);
            RemoveHUDObject(movingleft);
            RemoveHUDObject(movingright);
            RemoveHUDObject(ballcounttext);
            RemoveHUDObject(botcounttext);
            RemoveHUDObject(movingcounttext);
            AddHUDObject(start);
            AddHUDObject(aimlabslogo);
            AddHUDObject(settings);
            SetBackground2D("./App/Textures/backround.png");
            isSettingsVisible = false;
        }
    }
}
