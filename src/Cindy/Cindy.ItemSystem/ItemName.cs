using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using Cindy.Strings;
using UnityEngine;

namespace Cindy.ItemSystem
{
    [AddComponentMenu("Cindy/ItemSystem/ItemName", 1)]
    public class ItemName : StringObject
    {
        public ReferenceString itemKey;
        public StringSource stringSource;

        public override string GetValue()
        {
            value = itemKey.Value;
            if (stringSource != null)
                stringSource.Get(itemKey.Value, this, (v, e, s) =>
                 {
                     if (s)
                         value = v;
                     else
                         Debug.LogWarning(e);
                 });
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }
    }
}
