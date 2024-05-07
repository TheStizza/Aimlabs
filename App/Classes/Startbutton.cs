using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWEngine3.GameObjects;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Aimlabs.App.Classes
{
    internal class Startbutton : GameObject
    {
        public override void Act()
        {
            Player p = CurrentWorld.GetGameObjectByName<Player>("Player #1");
            if (p != null )
            {
                Vector3 playerPosition = p.Position;
                TurnTowardsXZ(playerPosition);
            }
            IsCollisionObject = true;
        }
    }
}
