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
        }

        private void UpdateMovementAndFirstPersonCamera()
        {
            // Initialisiere zwei Variablen:
            // forward == +1 -> Indikator für 'vorwärts'
            // forward == -1 -> Indikator für 'rückwärts'
            // strafe  == +1 -> Indikator für 'strafe rechts'
            // strafe  == -1 -> Indikator für 'strafe links'
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

            // Nutze die Informationen aus der Welt,
            // um die Kamera entsprechend der Mausbewegungen
            // rotieren zu lassen:
            CurrentWorld.AddCameraRotationFromMouseDelta();

            // Bewege das Objekt entlang der aktuellen Blickrichtung
            // mit Hilfe der Variablenwerte in 'forward' und 'strafe'.
            // Der dritte Parameter ist die Geschwindigkeit.
            MoveAndStrafeAlongCameraXZ(forward, strafe, 0.01f);

            // Für Free-Float-Verhalten die Tasten Q und E für Auf-/Absteigen:
            if (Keyboard.IsKeyDown(Keys.Q))
            {
                MoveAlongVector(CurrentWorld.CameraLookAtVectorLocalUp, -0.01f);
            }
            if (Keyboard.IsKeyDown(Keys.E))
            {
                MoveAlongVector(CurrentWorld.CameraLookAtVectorLocalUp, 0.01f);
            }
            // Aktualisiere die Kameraposition:
            // (Erneut wird die Kamera um 0.4f Einheiten weiter oben platziert)
            CurrentWorld.UpdateCameraPositionForFirstPersonView(Center, Player.CAM_OFFSET);
        }
    }
}
