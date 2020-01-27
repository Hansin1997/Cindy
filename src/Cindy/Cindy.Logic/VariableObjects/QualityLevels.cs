using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    public class QualityLevel : IntObject
    {
        protected override void Start()
        {
            base.Start();
            QualitySettings.SetQualityLevel(value);
        }

        public override void Save()
        {
            QualitySettings.SetQualityLevel(value);
            base.Save();
        }
    }

    public class QualityLevelName : StringObject
    {
        protected override void Start()
        {
            value = QualitySettings.names[QualitySettings.GetQualityLevel()];
        }

        protected override void Update()
        {
            value = QualitySettings.names[QualitySettings.GetQualityLevel()];
            base.Update();
        }
    }
}
