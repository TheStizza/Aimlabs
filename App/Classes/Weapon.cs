using KWEngine3.GameObjects;

namespace Aimlabs.App.Classes
{
    public class Weapon : ViewSpaceGameObject
    {
        public override void Act()
        {
            UpdatePosition();
        }
    }
}
