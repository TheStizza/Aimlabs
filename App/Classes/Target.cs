using System;
using System.Collections.Generic;
using KWEngine3;
using KWEngine3.GameObjects;
using KWEngine3.Helper;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Aimlabs.App.Classes
{
    public class Target : GameObject
    {
        private int Targethealth = 160;
        public override void Act()
        {
            if (HasAnimations)
            {
                SetAnimationID(0);
                SetAnimationPercentageAdvance(0.001f);
            }
            if(Targethealth == 0)
            {
                
            }
        }

        public void CreateBotAttachments()
        {
            BotAttachment headattachment = new()
            {
                IsCollisionObject = true
            };
            headattachment.SetOpacity(0.5f);
            headattachment.SetColor(0f, 1f, 0f);
            CurrentWorld.AddGameObject(headattachment);
            this.AttachGameObjectToBone(headattachment, "mixamorig:Head");
            HelperGameObjectAttachment.SetScaleForAttachment(headattachment, 17.0f, 22.5f, 20.0f);
            HelperGameObjectAttachment.SetRotationForAttachment(headattachment, 0f, 0f, 0f);
            HelperGameObjectAttachment.SetPositionOffsetForAttachment(headattachment, 0.00f, 0.05f, 0.0f);
            headattachment.Name = "head";
            /*
            // BODY
            BotAttachment bodyattachment = new()
            {
                IsCollisionObject = true
            };
            bodyattachment.SetOpacity(0.5f);
            bodyattachment.SetColor(0f, 1f, 0f);
            CurrentWorld.AddGameObject(bodyattachment);
            this.AttachGameObjectToBone(bodyattachment, "mixamorig:Hips");
            HelperGameObjectAttachment.SetScaleForAttachment(bodyattachment, 27.5f, 75.0f, 27.0f);
            HelperGameObjectAttachment.SetRotationForAttachment(bodyattachment, 0f, 0f, 0f);
            HelperGameObjectAttachment.SetPositionOffsetForAttachment(bodyattachment, 0.01f, 0.10f, 0f);
            bodyattachment.Name = "body";

            
            // LEFT ARM
            BotAttachment leftarmattachment = new()
            {
                IsCollisionObject = true
            };
            leftarmattachment.SetOpacity(0.5f);
            leftarmattachment.SetColor(0f, 1f, 0f);
            CurrentWorld.AddGameObject(leftarmattachment);
            this.AttachGameObjectToBone(leftarmattachment, "mixamorig:LeftArm");
            HelperGameObjectAttachment.SetScaleForAttachment(leftarmattachment, 12.5f, 30.0f, 12.5f);
            HelperGameObjectAttachment.SetRotationForAttachment(leftarmattachment, 0f, 0f, 0f);
            HelperGameObjectAttachment.SetPositionOffsetForAttachment(leftarmattachment, -0.00f, 0.1f, 0.00f);

            
            // RIGHT ARM
            BotAttachment rightarmattachment = new()
            {
                IsCollisionObject = true
            };
            rightarmattachment.SetOpacity(0.5f);
            rightarmattachment.SetColor(0f, 1f, 0f);
            CurrentWorld.AddGameObject(rightarmattachment);
            this.AttachGameObjectToBone(rightarmattachment, "mixamorig:RightArm");
            HelperGameObjectAttachment.SetScaleForAttachment(rightarmattachment, 12.5f, 30.0f, 12.5f);
            HelperGameObjectAttachment.SetRotationForAttachment(rightarmattachment, 0f, 0f, 0f);
            HelperGameObjectAttachment.SetPositionOffsetForAttachment(rightarmattachment, 0f, 0.1f, 0f);*/            
        }

        public void DeleteBotAttachments()
        {
            // NEU: Löse alle Attachments vom Objekt und lösche die Attachments danach (Parameter = true):
            DetachAllBoneAttachments(true);
        }
        static public void spawnnewTarget()
        {
            Target Bot = new();
            Bot.Name = "Bot";
            Bot.SetModel("Bot");
            Bot.SetRotation(0, 90, 0);
            Bot.SetPosition(HelperRandom.GetRandomNumber(-2, 2),0, HelperRandom.GetRandomNumber(-2, 2));
            Bot.IsCollisionObject = true;
            CurrentWorld.AddGameObject(Bot);
            Bot.CreateBotAttachments();
        }
    }
}
