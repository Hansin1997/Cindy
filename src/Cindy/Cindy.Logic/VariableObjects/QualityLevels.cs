using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 画质等级
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Util/QualityLevel (Int)")]
    public class QualityLevel : IntObject
    {
        protected override void OnValueLoad(int val)
        {
            base.OnValueLoad(val);
            if(val != default)
                QualitySettings.SetQualityLevel(val); 
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
