using System;

namespace Cindy.ItemSystem
{
    [Serializable]
    public class SceneItem
    {
        public SerializedItem item;
        public string scene;

        public SceneItem(SerializedItem item,string scene)
        {
            this.item = item;
            this.scene = scene;
        }
    }
}
