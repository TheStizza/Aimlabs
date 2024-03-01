using KWEngine3;
using KWEngine3.GameObjects;
using KWEngine3.Helper;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace Aimlabs.App.Classes
{
    public class Player : GameObject
    {
        public const float CAM_OFFSET = 0.4f;

        public override void Act()
        {
            if(GlobalSettings.IsPaused == false)
            {
                UpdateMovementAndFirstPersonCamera();
            }
            Player p = CurrentWorld.GetGameObjectByName<Player>("p");
            if (p != null)
            {
                Vector3 myposition = this.Position;
                List<RayIntersection> rayObjects = HelperIntersection.RayTraceObjectsForViewVectorFast(
                    myposition,
                    LookAtVector,
                    this,
                    0,
                    true,
                    typeof(Player), typeof(Walls), typeof(Obstacle)
                    );
                if (rayObjects.Count > 0)
                {
                    foreach (RayIntersection r in rayObjects)
                    {

                    }
                    GameObject firstObjectHitbyRay = rayObjects[0].Object;
                    if (firstObjectHitbyRay is Targetball && Mouse.IsButtonPressed(MouseButton.Left))
                    {
                        CurrentWorld.RemoveGameObject(firstObjectHitbyRay);
                        Stats.score = Stats.score + 1;
                    }
                }
            }
        }

        private void UpdateMovementAndFirstPersonCamera()
        {
            int forward = 0;
            int strafe = 0;
            if (Keyboard.IsKeyDown(Keys.A))
                strafe -= 1;
            if (Keyboard.IsKeyDown(Keys.D))
                strafe += 1;
            if (Keyboard.IsKeyDown(Keys.W))
                forward += 1;
            if (Keyboard.IsKeyDown(Keys.S))
                forward -= 1;
            CurrentWorld.AddCameraRotationFromMouseDelta();
            MoveAndStrafeAlongCameraXZ(forward, strafe, 0.01f);
            if (Keyboard.IsKeyDown(Keys.Q))
            {
                MoveAlongVector(CurrentWorld.CameraLookAtVectorLocalUp, -0.01f);
            }
            if (Keyboard.IsKeyDown(Keys.E))
            {
                MoveAlongVector(CurrentWorld.CameraLookAtVectorLocalUp, 0.01f);
            }
            CurrentWorld.UpdateCameraPositionForFirstPersonView(Center, Player.CAM_OFFSET);
            List<Intersection> intersections = GetIntersections();
            foreach (Intersection i in intersections)
            {
                GameObject collider = i.Object;
                if(collider is Walls)
                {
                    Vector3 mtv = i.MTV;
                    MoveOffset(mtv);
                }
            }
        }
    }
}
