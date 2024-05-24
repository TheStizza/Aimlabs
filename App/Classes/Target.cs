using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWEngine3.GameObjects;
using KWEngine3.Helper;

namespace Aimlabs.App.Classes
{
    public class Target : GameObject
    {
        int Targethealth = 10;
        public override void Act()
        {
            if(HasAnimations)
            {
                SetAnimationID(13);
                SetAnimationPercentageAdvance(0.001f);
            }

            if(Targethealth == 0)
            {
              
            }
        }
        static public void spawnnewTarget()
        {
            Target Bot = new();
            Bot.Name = "Bot";
            Bot.SetModel("Bot");
            Bot.SetPosition(HelperRandom.GetRandomNumber(-2, 2),0, HelperRandom.GetRandomNumber(-2, 2));
            //Bot.SetColor(0, 1, 0.87f);
            //Bot.SetScale(0.3f, 0.3f, 0.3f);
            Bot.IsCollisionObject = true;
            CurrentWorld.AddGameObject(Bot);
        }
    }
}
