﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Binder.Binders
{

    [AddComponentMenu("Cindy/UI/Binder/SliderBinder", 1)]
    public class SliderBinder : StorageBinder<Slider>
    {
        protected override void OnBind(Slider target, string value)
        {
            try
            {
                target.value = float.Parse(value);
            }catch(Exception e)
            {
                Debug.LogError(e, this);
            }
        }
    }
}
