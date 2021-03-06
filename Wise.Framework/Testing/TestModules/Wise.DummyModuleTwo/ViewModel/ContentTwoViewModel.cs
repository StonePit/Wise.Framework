﻿using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using Wise.DummyModuleTwo.Commands;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.DummyModuleTwo.ViewModel
{
    [MenuItem("Modules|Dummy Module Two", "Content Two View")]
    public class ContentTwoViewModel : ViewModelBase
    {
        private string label;
        private IMessanger messanger;

        public ContentTwoViewModel(IMessanger messanger)
        {
            this.messanger = messanger;
            messanger.Subscribe<string>(OnMessageArrived).ExecuteOn(MessageProcessingThread.Dispatcher);

            base.Title = "asd";
            Button = new DummyCommandTwo(this, messanger);
        }

        public ICommand Button { get; set; }

        public string Label
        {
            get { return label; }
            protected set
            {
                label = value;
                OnPropertyChanged("Label");
            }
        }

        private void OnMessageArrived(string o)
        {
            Label += DateTime.Now + " HELLO: " + o;
        }

    }
}