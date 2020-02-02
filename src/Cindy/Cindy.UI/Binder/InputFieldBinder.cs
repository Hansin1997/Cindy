using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Binder
{

    [AddComponentMenu("Cindy/UI/Binder/InputFieldBinder", 1)]
    class InputFieldBinder : StringSourceBinder<InputField>
    {
        protected override void OnBind(InputField target, string value)
        {
            if(value != null)
                target.text = value;
        }
    }
}
