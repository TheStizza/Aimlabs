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
    public class MovingTargetball : Target
    {
        private Vector3 movedirection = new Vector3(HelperRandom.GetRandomNumber(-2, 2), HelperRandom.GetRandomNumber(-2, 2), HelperRandom.GetRandomNumber(-2, 2));
        public override void Act()
        {
            Move(0.01f);
            IsCollisionObject = true;
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if (collider is Walls || collider is Floor)
                {
                    movedirection = HelperVector.RotateVector(movedirection, HelperRandom.GetRandomNumber(60, 180), Axis.X);
                }
            }
            MoveAlongVector(movedirection, 0.01f);
        }
        static public void SpawnnewMovingTargetball()
        {
            MovingTargetball Sphereball = new()
            {
                Name = "Sphereball"
            };
            Sphereball.SetModel("KWSphere");
            Sphereball.SetPosition(HelperRandom.GetRandomNumber(-2, 2), HelperRandom.GetRandomNumber(1, 2), HelperRandom.GetRandomNumber(-2, 2));
            Sphereball.SetColor(0, 1, 0.87f);
            Sphereball.SetScale(0.3f, 0.3f, 0.3f);
            Sphereball.IsCollisionObject = true;
            CurrentWorld.AddGameObject(Sphereball);
        }
    }
}