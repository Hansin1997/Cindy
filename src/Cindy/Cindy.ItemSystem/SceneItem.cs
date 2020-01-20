using Cindy.Util.Serializables;
using System;

namespace Cindy.ItemSystem
{
    [Serializable]
    public class SceneItem
    {
        public SerializedItem item;
        public string scene;
        public SerializedTransform transform;

        public SceneItem(SerializedItem item,string scene,SerializedTransform transform = null)
        {
            this.item = item;
            this.scene = scene;
            this.transform = transform;
        }
    }
}
