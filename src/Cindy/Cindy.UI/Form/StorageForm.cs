﻿using Cindy.Storages;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Form
{

    [AddComponentMenu("Cindy/UI/Form/StorageForm", 99)]
    public class StorageForm : AbstractStorageFrom<AbstractStorage>
    {
        protected override bool OnSubmit(AbstractStorage storage, IDictionary<string, object> form)
        {
            storage.PutMultiple(form);
            return true;
        }
    }
}