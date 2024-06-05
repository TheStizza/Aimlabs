
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aimlabs.App.Classes

{
    public static class Stats
    {
        public static float ballscore = 0 ;
        public static float botscore = 0;
        public static float MovingBallscore = 0;
        public static bool ballstart = false;
        public static bool botstart = false;
        public static bool MovingBallstart = false;
        public static bool ballsspawned = false;
        public static bool botspawned = false;
        public static bool MovingBallspawned = false;
        public static bool hit = false;
        public static float leftmouseclicks = 0;
        public static double accuracy = 100;
        public static int uservolume = 1;
        public static float volume = (float)Stats.uservolume / 10;
    }
}