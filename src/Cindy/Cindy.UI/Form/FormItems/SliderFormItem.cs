using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Form.FormItems
{

    [AddComponentMenu("Cindy/UI/Form/Items/SliderFormItem", 1)]
    public class SliderFormItem : AbstractSourceFormItem<Slider>
    {
        protected override string OnGetValue(Slider source, string key)
        {
            return source.value.ToString();
        }
    }
}
