using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/QualityLevel")]
    public class QualityLevel : IntObject
    {
        protected override void OnValueLoad(int val)
        {
            base.OnValueLoad(val);
            if(val != default)
                QualitySettings.SetQualityLevel(val); 
        }

        protected override void Update()
        {
            base.Update();
            value = QualitySettings.GetQualityLevel();
        }

        public override int GetValue()
        {
            value = QualitySettings.GetQualityLevel();
            return base.GetValue();
        }

        public override void SetValue(int value)
        {
            base.SetValue(value);
            QualitySettings.SetQualityLevel(value);
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/QualityLevelCount")]
    public class QualityLevelCount : IntObject
    {
        public int offset = 0;
        protected override void Start()
        {
            value = QualitySettings.names.Length;
            OnValueChanged();
        }

        protected override void Update()
        {
            value = QualitySettings.names.Length;
            base.Update();
        }

        public override void SetValue(int value)
        {

        }

        public override int GetValue()
        {
            value = QualitySettings.names.Length;
            return base.GetValue() + offset;
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/QualityLevelName")]
    public class QualityLevelName : StringObject
    {
        protected override void Start()
        {
            value = QualitySettings.names[QualitySettings.GetQualityLevel()];
            OnValueChanged();
        }

        protected override void Update()
        {
            value = QualitySettings.names[QualitySettings.GetQualityLevel()];
            base.Update();
        }

        public override void SetValue(string value)
        {

        }

        public override string GetValue()
        {
            value = QualitySettings.names[QualitySettings.GetQualityLevel()];
            return base.GetValue();
        }
    }
}
