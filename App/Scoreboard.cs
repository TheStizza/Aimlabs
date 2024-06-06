using Aimlabs.App.Classes;
using GruppeC.App;
using KWEngine3;
using KWEngine3.GameObjects;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Text.Json;

namespace Aimlabs.App
{
    public class Scoreboard : World
    {
        private HUDObjectText botscorex;
        private HUDObjectText ballscorex;
        private HUDObjectText clicks;
        private HUDObjectText accuracy;
        private HUDObjectImage playagain;
        private NameInput nameInput;
        private List<Score> scoreBoardList;

        public override void Act()
        {
            if (playagain.IsMouseCursorOnMe() == true && Mouse.IsButtonPressed(MouseButton.Left))
            {
                Stats.ballscore = 0;
                Stats.botscore = 0;
                Stats.ballstart = false;
                Stats.botstart = false;
                Stats.ballsspawned = false;
                Stats.hit = false;
                Stats.leftmouseclicks = 0;
                Stats.accuracy = 100;
                Stats.MovingBallscore = 0;
                Stats.MovingBallstart = false;
                Stats.botspawned = false;
                Stats.MovingBallspawned = false;
                Stats.volume = 0.1f;
                Window.SetWorld(new StartingWorld());
                return;
            }

            if (nameInput.HasName() == false)
            {
                nameInput.Update(Keyboard);
            }
            else
            {
                if (nameInput.IsDone() == false)
                {
                    string nameNew = nameInput.GetNameEntered();
                    Score score = new()
                    {
                        timer = Stats.botscore + Stats.ballscore + Stats.MovingBallscore,
                        accuracy = Stats.accuracy,
                        name = nameNew
                    };
                    scoreBoardList.Add(score);
                    scoreBoardList.Sort((scoreA, scoreB) =>
                    {
                        return scoreA.timer < scoreB.timer ? 1 : -1;
                    });

                    File.WriteAllText("./scoreboard.json", JsonSerializer.Serialize(scoreBoardList));
                    nameInput.RemoveHUDObjects();

                    RebuildScoreboard();
                }
            }
        }

        public override void Prepare()
        {
            playagain = new HUDObjectImage();
            playagain.SetPosition(720, 360);
            playagain.Name = "name";
            playagain.SetScale(300);
            AddHUDObject(playagain);

            botscorex = new HUDObjectText("Botscore:" + Stats.botscore);
            botscorex.SetPosition(100f, 100f);
            botscorex.Name = "Botscore";
            botscorex.SetScale(18);
            botscorex.SetFont(FontFace.NovaMono);
            AddHUDObject(botscorex);

            ballscorex = new HUDObjectText("Ballscore:" + Stats.ballscore);
            ballscorex.SetPosition(100f, 166f);
            ballscorex.Name = "Botscore";
            ballscorex.SetScale(18);
            ballscorex.SetFont(FontFace.NovaMono);
            AddHUDObject(ballscorex);

            clicks = new HUDObjectText("Clicks:" + Stats.leftmouseclicks);
            clicks.SetPosition(1000f, 166f);
            clicks.Name = "clicks";
            clicks.SetScale(18);
            clicks.SetFont(FontFace.NovaMono);
            AddHUDObject(clicks);

            accuracy = new HUDObjectText("Accuracy:" + Stats.accuracy + "%");
            accuracy.SetPosition(1000f, 100f);
            accuracy.Name = "accuracy";
            accuracy.SetScale(18);
            accuracy.SetFont(FontFace.NovaMono);
            AddHUDObject(accuracy);
            SetBackground2D("");

            try
            {
                scoreBoardList = JsonSerializer.Deserialize<List<Score>>(File.ReadAllText("./scoreboard.json"));
            }
            catch
            {
                scoreBoardList = new List<Score>();
            }

            HUDObjectText scoreboardHeadline = new HUDObjectText(scoreBoardList.Count > 0 ? "Scoreboard" : "wow, such empty... :-/");
            scoreboardHeadline.SetTextAlignment(TextAlignMode.Center);
            scoreboardHeadline.SetColor(1.0f, 1.0f, 0.75f);
            scoreboardHeadline.SetPosition(KWEngine.Window.Width / 2, KWEngine.Window.Height * 0.35f);
            AddHUDObject(scoreboardHeadline);

            RebuildScoreboard();

            nameInput = new NameInput();
            nameInput.AddToWorld();
        }

        private void RebuildScoreboard()
        {
            List<HUDObject> objects = KWEngine.CurrentWorld.GetHUDObjectsByName("SCORELIST");
            foreach (HUDObject h in objects)
            {
                RemoveHUDObject(h);
            }

            float listoffset = KWEngine.Window.Height * 0.4f;
            float listoffsetStep = Math.Max(16f, KWEngine.Window.Height * 0.05f);
            int maxNameLengthForDisplay = 12;
            int step = 0;
            foreach (Score scoreInList in scoreBoardList)
            {
                HUDObjectText listObject;
                if (scoreInList.name.Length > maxNameLengthForDisplay)
                {
                    listObject = new HUDObjectText(scoreInList.name.Substring(0, maxNameLengthForDisplay) + "...");
                }
                else
                {
                    listObject = new HUDObjectText(scoreInList.name);
                }

                listObject.SetPosition(KWEngine.Window.Width * 0.2f, listoffset + listoffsetStep * step);
                listObject.Name = "SCORELIST";
                listObject.SetScale(16);
                listObject.SetColor(0.75f, 0.75f, 0.75f);
                AddHUDObject(listObject);

                HUDObjectText listObject2 = new HUDObjectText(Convert.ToString(scoreInList.accuracy).PadLeft(3, '0') + "%");
                listObject2.SetPosition(KWEngine.Window.Width * 0.5f, listoffset + listoffsetStep * step);
                listObject2.Name = "SCORELIST";
                listObject2.SetScale(16);
                listObject2.SetColor(0.75f, 0.75f, 0.75f);
                AddHUDObject(listObject2);

                HUDObjectText listObject3 = new HUDObjectText(Convert.ToString(scoreInList.timer));
                listObject3.SetTextAlignment(TextAlignMode.Right);
                listObject3.SetPosition(KWEngine.Window.Width * 0.8f, listoffset + listoffsetStep * step);
                listObject3.Name = "SCORELIST";
                listObject3.SetScale(16);
                listObject3.SetColor(0.75f, 0.75f, 0.75f);
                AddHUDObject(listObject3);


                step++;
                if (step > 4)
                    break;
            }
        }
    }
}
