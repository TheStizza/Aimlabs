using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWEngine3.GameObjects;

namespace Aimlabs.App.Classes
{
    public class Weapon : ViewSpaceGameObject
    {
        public override void Act()
        {
            UpdatePosition();
        }
    }
}
