using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWEngine3.GameObjects;

namespace Aimlabs.App.Classes
{
    public class Walls : GameObject
    {
        public override void Act()
        {

        }
        public Walls(string name1, float Positionx, float Positiony, float Positionz, string Texture, float Scalex, float Scaley, float Scalez, float Rotationx, float Rotationy, float Rotationz)
        {
            Name = name1;
            SetPosition(Positionx, Positiony, Positionz);
            SetTexture(Texture);
            SetScale(Scalex, Scaley, Scalez);
            SetRotation(Rotationx,Rotationy,Rotationz);
            IsCollisionObject = true;
        }
    }
}
