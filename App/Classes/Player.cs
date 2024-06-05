using KWEngine3;
using KWEngine3.GameObjects;
using KWEngine3.Helper;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using KWEngine3.Audio;

namespace Aimlabs.App.Classes
{
    public class Player : GameObject
    {
        public const float CAM_OFFSET = 0.4f;
        public override void Act()
        {
            if(Mouse.IsButtonPressed(MouseButton.Left))
            {
                if(Stats.ballsspawned == true || Stats.botspawned == true || Stats.MovingBallspawned == true)
                {
                    Stats.leftmouseclicks++;
                }
            }
            if(GlobalSettings.IsPaused == false)
            {
                UpdateMovementAndFirstPersonCamera();
            }
            List<RayIntersection> rayObjects = HelperIntersection.RayTraceObjectsForViewVectorFast(
                CurrentWorld.CameraPosition,
                CurrentWorld.CameraLookAtVector,
                this,
                0,
                true,
                typeof(Player), 
                typeof(Walls), 
                typeof(Obstacle), 
                typeof(BotAttachment),
                typeof(Targetball), 
                typeof(Startbutton),
                typeof(MovingTargetBall)
                );
            if (rayObjects.Count > 0)
            {
                GameObject firstObjectHitbyRay = rayObjects[0].Object;
                //Console.WriteLine(firstObjectHitbyRay);
                if (firstObjectHitbyRay is Targetball && Mouse.IsButtonPressed(MouseButton.Left))
                {
                    //Console.WriteLine("ich bin drin");
                    CurrentWorld.RemoveGameObject(firstObjectHitbyRay);
                    Audio.PlaySound("./App/Sounds/targethit.wav", false, Stats.volume);
                    Stats.ballscore ++;
                    Targetball.spawnnewTargetball();
                    Stats.hit = true;
                }
                else if (firstObjectHitbyRay is BotAttachment && Mouse.IsButtonPressed(MouseButton.Left))
                {
                    GameObject actualBot = firstObjectHitbyRay.GetGameObjectThatIAmAttachedTo();

                    if(actualBot != null && actualBot is Target)
                    {
                        (actualBot as Target).DeleteBotAttachments();
                        CurrentWorld.RemoveGameObject(actualBot);
                        Audio.PlaySound("./App/Sounds/targethit.wav", false, Stats.volume);
                        Target.spawnnewTarget();
                        Stats.botscore ++;
                    }                    
                }
                else if (firstObjectHitbyRay is MovingTargetBall && Mouse.IsButtonPressed(MouseButton.Left))
                {
                    Console.WriteLine(firstObjectHitbyRay);
                    CurrentWorld.RemoveGameObject(firstObjectHitbyRay);
                    Audio.PlaySound("./App/Sounds/targethit.wav", false, Stats.volume);
                    MovingTargetBall.SpawnnewMovingTargetball();
                    Stats.MovingBallscore++;
                }
                else if (firstObjectHitbyRay is Startbutton && Mouse.IsButtonPressed(MouseButton.Left) && firstObjectHitbyRay.Name == "Ball")
                {
                    CurrentWorld.RemoveGameObject(firstObjectHitbyRay);
                    Stats.ballstart = true;
                }
                else if (firstObjectHitbyRay is Startbutton && Mouse.IsButtonPressed(MouseButton.Left) && firstObjectHitbyRay.Name == "Bot")
                {
                    CurrentWorld.RemoveGameObject(firstObjectHitbyRay);
                    Stats.botstart = true;
                }
                else if (firstObjectHitbyRay is Startbutton && Mouse.IsButtonPressed(MouseButton.Left) && firstObjectHitbyRay.Name == "MovingBall")
                {
                    CurrentWorld.RemoveGameObject(firstObjectHitbyRay);
                    Stats.MovingBallstart = true;
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
