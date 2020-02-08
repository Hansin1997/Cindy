using Cindy.UI.Pages;

namespace Cindy.Logic.VariableObjects
{
    public class PageCount : IntObject
    {
        public override void SetValue(int value)
        {

        }

        protected override void OnValueLoad(int val)
        {
            GetValue();
        }

        protected override void OnValueLoadEmpty()
        {
            GetValue();
        }

        protected override void OnValueChanged()
        {
            GetValue();
            base.OnValueChanged();
        }

        public override int GetValue()
        {
            value = PageContainer.Count;
            return base.GetValue();
        }
    }
}
