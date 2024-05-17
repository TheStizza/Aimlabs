using KWEngine3.GameObjects;

namespace Aimlabs.App.Classes
{
    public class Floor : GameObject
    {
        public override void Act()
        {
            IsCollisionObject = true;
        }
    }
}
