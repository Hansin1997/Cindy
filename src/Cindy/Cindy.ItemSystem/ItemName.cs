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
            value = stringSource != null ? stringSource.GetString(itemKey.Value, itemKey.Value) : itemKey.Value;
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }
    }
}
