using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Form
{
    [Obsolete]
    public abstract class AbstractStorageFrom<S> : AbstractSourceForm
    {
        [Tooltip("Storage target to save form.")]
        public S storage;

        protected override bool DoSubmit(IDictionary<string, object> form)
        {
            if (storage == null)
                return false;
            return OnSubmit(storage, form);
        }

        protected abstract bool OnSubmit(S storage, IDictionary<string, object> form);
    }
}
