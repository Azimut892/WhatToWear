using System;
using System.Collections.Generic;
using WhatToWear.ViewModels;
using WhatToWear.Views;
using Xamarin.Forms;

namespace WhatToWear
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(Main), typeof(Main));
        }

    }
}
