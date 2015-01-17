﻿using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Interface.Modularity;
using Wise.Framework.Presentation.Modularity;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Commands
{
    public class NavigateToViewModelCommand : BaseCommand
    {
        private readonly OpenItemsViewModel openItemsViewModel;
        public NavigateToViewModelCommand(OpenItemsViewModel openItemsViewModel)
        {
            this.openItemsViewModel = openItemsViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = parameter as ViewModelInfo;
            var messanger = DependencyInjection.Container.Current.Resolve<IMessanger>();
            messanger.Publish(new NavigationRequest() { ViewModelType = vm.ViewModelType});

            var manager = DependencyInjection.Container.Current.Resolve<INavigationManager>();
            manager.CloseItem(openItemsViewModel);
        }

    }
}