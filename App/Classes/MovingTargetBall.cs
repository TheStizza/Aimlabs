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
        // Anmerkung KAR:
        // - Richtung des Balls muss normalisiert werden damit der Vektor die Einheitslänge 1 hat
        // - Aktuell kann der MovingBall-Richtungsvektor noch so gewürfelt werden, dass er geradewegs nach oben zeigt:
        //   (Die Chance kann man aber stark verringern, indem man nicht zwischen -2 und +2 würfelt, sondern z.B. zwischen -100 und +100!
        //    Da der Vektor dann eh normalisiert wird, hat er danach trotzdem wieder die Länge 1.)

        //private Vector3 movedirection = new Vector3(HelperRandom.GetRandomNumber(-2, 2), HelperRandom.GetRandomNumber(-2, 2), HelperRandom.GetRandomNumber(-2, 2));
        private Vector3 movedirection = Vector3.Normalize(new Vector3(HelperRandom.GetRandomNumber(-100, 100), HelperRandom.GetRandomNumber(-100, 100), HelperRandom.GetRandomNumber(-100, 100)));


        public override void Act()
        {
            //Move(0.01f);
            MoveAlongVector(movedirection, 0.01f);
            IsCollisionObject = true;
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if (collider is Walls || collider is Floor)
                {
                    // NEU:
                    movedirection = Vector3.NormalizeFast(HelperVector.ReflectVector(movedirection, i.ColliderSurfaceNormal));
                    MoveAlongVector(movedirection, 0.01f);
                    // ACHTUNG: Die Bälle können aktuell noch so abprallen, dass sie durch die Deckenöffnung 'entkommen' können!

                    // ALT:
                    // Risiko: Die Bälle konnten so rotiert werden, dass sie in der nächsten Ausrichtung wieder in Richtung Wand blicken.
                    //         Dadurch konnte es passieren, dass ein Ball durch die Wand 'glitch'te.
                    //movedirection = HelperVector.RotateVector(movedirection, HelperRandom.GetRandomNumber(60, 180), Axis.X);
                }
            }
            
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