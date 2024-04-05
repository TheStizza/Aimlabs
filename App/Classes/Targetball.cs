using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWEngine3.GameObjects;
using KWEngine3;
using KWEngine3.Helper;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;


namespace Aimlabs.App.Classes
{
    public class Targetball : Target
    {
        public override void Act()
        {
           
        }
        public void spawnnewTargetball()
        {
            Targetball Sphereball = new();
            Name = "Sphereball";
            SetModel("KWSphere");
            SetPosition(HelperRandom.GetRandomNumber(1, 5), HelperRandom.GetRandomNumber(1, 3), HelperRandom.GetRandomNumber(1, 5));
            SetScale(0.3f, 0.3f, 0.3f);
            IsCollisionObject = true;
            CurrentWorld.AddGameObject(Sphereball);
        }
    }
}