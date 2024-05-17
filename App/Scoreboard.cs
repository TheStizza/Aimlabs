using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using KWEngine3.GameObjects;
using KWEngine3;
using System;
using Aimlabs.App.Classes;
using OpenTK.Mathematics;
using KWEngine3.Helper;
using System.Collections.Generic;
using System.Text.Json;
using GruppeC.App;

namespace Aimlabs.App
{
    public class Scoreboard : World
    {
        private HUDObjectText botscorex;
        private HUDObjectText ballscorex;
        private HUDObjectText clicks;
        private HUDObjectText accuracy;
        private HUDObjectImage playagain;
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
                Window.SetWorld(new StartingWorld());
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

            accuracy = new HUDObjectText("Accuracy:" + Stats.accuracy+"%");
            accuracy.SetPosition(1000f, 100f);
            accuracy.Name = "accuracy";
            accuracy.SetScale(18);
            accuracy.SetFont(FontFace.NovaMono);
            AddHUDObject(accuracy);
            SetBackground2D("");
            Console.WriteLine($"Your Time:");
            Console.Write("Name: ");

            List<Score> scoreBoardList;
            try
            {
                scoreBoardList = JsonSerializer.Deserialize<List<Score>>(File.ReadAllText("./scoreboard.json"));
            }
            catch
            {
                scoreBoardList = new List<Score>();
            }
            foreach (Score scoreInList in scoreBoardList)
            {
                Console.WriteLine($"Name: {scoreInList.name} - Zeit: {scoreInList.timer}");
            }

            Score score = new Score();
            score.timer = Stats.botscore + Stats.ballscore;
            if(Stats.ballscore > Stats.botscore)
            {
                Console.WriteLine($"Your Ballscore: {score.timer}");
            }
            else
            {
                Console.WriteLine($"Your Botscore: {score.timer}");
            }
            Console.Write("Name: ");
            score.name = Console.ReadLine();

            scoreBoardList.Add(score);
            scoreBoardList.Sort((scoreA, scoreB) =>
            {
                return scoreA.timer > scoreB.timer ? 1 : -1;
            });
            File.WriteAllText("./scoreboard.json", JsonSerializer.Serialize<List<Score>>(scoreBoardList));
        }
    }
}
