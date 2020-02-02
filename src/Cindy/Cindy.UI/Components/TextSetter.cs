using Cindy.Logic.VariableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Components
{

    [AddComponentMenu("Cindy/UI/Components/TextSetter")]
    public class TextSetter : MonoBehaviour
    {
        public Text text;
        public StringObject stringObject;

        protected virtual void Start()
        {
            if (text == null)
                text = GetComponent<Text>();
            if(text != null & stringObject != null)
            {
                text.text = stringObject.GetValue();
                stringObject.valueChangedEvent.AddListener(OnStirngChange);
            }
        }

        protected virtual void OnStirngChange()
        {
            if (text != null & stringObject != null)
            {
                text.text = stringObject.GetValue() ;
            }
        }

        protected virtual void OnDestroy()
        {
            if (stringObject != null)
                stringObject.valueChangedEvent.RemoveListener(OnStirngChange);
        }
    }
}
