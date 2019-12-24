using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Binder
{
    [AddComponentMenu("Cindy/UI/Binder/TextBinder", 1)]
    public class TextBinder : StringSourceBinder<Text>
    {
        protected override void OnBind(Text target,string value)
        {
            target.text = value;
        }
    }
}
