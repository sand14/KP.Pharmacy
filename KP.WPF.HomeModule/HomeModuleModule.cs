﻿using KP.WPF.HomeModule.ViewModels;
using KP.WPF.HomeModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace KP.WPF.HomeModule
{
    public class HomeModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Home>("Home");

        }
    }
}