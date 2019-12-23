using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Form.FormItems
{

    [AddComponentMenu("Cindy/UI/Form/Items/InputFieldFormItem", 1)]
    public class InputFieldFormItem : AbstractSourceFormItem<InputField>
    {
        protected override string OnGetValue(InputField source, string key)
        {
            return source.text;
        }
    }
}
