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
        static public void spawnnewTargetball()
        {
            Targetball Sphereball = new();
            Sphereball.Name = "Sphereball";
            Sphereball.SetModel("KWSphere");
            Sphereball.SetPosition(HelperRandom.GetRandomNumber(1, 5), HelperRandom.GetRandomNumber(1, 3), HelperRandom.GetRandomNumber(1, 5));
            Sphereball.SetScale(0.3f, 0.3f, 0.3f);
            Sphereball.IsCollisionObject = true;
            CurrentWorld.AddGameObject(Sphereball);
        }
    }
}