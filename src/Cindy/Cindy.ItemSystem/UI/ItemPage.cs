﻿using Cindy.Logic.ReferenceValues;
using Cindy.UI.Pages;
using Cindy.Util;
using UnityEngine;

namespace Cindy.ItemSystem.UI
{

    [AddComponentMenu("Cindy/ItemSystem/UI/ItemPage", 1)]
    public class ItemPage : Page
    {
        public ReferenceString itemContainerName;

        public ItemListView ItemListView;

        protected virtual void Start()
        {
            ItemListView.source = Finder.Find<ItemContainer>(itemContainerName.Value);
            ItemListView.Refresh();
        }
    }
}
