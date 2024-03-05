using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWEngine3.GameObjects;

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
    }
}
