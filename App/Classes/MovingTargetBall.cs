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
    public class MovingTargetBall : Target
    {
        private Vector3 movedirection = Vector3.Normalize(new Vector3(HelperRandom.GetRandomNumber(-100, 100), HelperRandom.GetRandomNumber(-100, 100), HelperRandom.GetRandomNumber(-100, 100)));


        public override void Act()
        {
            MoveAlongVector(movedirection, 0.01f);
            IsCollisionObject = true;
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if (collider is Walls || collider is Floor)
                {
                    movedirection = Vector3.NormalizeFast(HelperVector.ReflectVector(movedirection, i.ColliderSurfaceNormal));
                    MoveAlongVector(movedirection, 0.01f);
                }
            }
            
        }
        static public void SpawnnewMovingTargetball()
        {
            MovingTargetBall Sphereball = new()
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