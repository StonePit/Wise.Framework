﻿using System;
using System.Windows.Input;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.ViewModel;
using Wise.Module.Commands;

namespace Wise.Module.ViewModel
{
    [MenuItem("Modules|DummyModule Other", "DummyModule Other")]
    public class OtherContentViewModel : ViewModelBase
    {
        private string label;

        public OtherContentViewModel(IMessanger messanger)
        {
            messanger.Subscribe<string>(OnMessageArrived).ExecuteOn(MessageProcessingThread.Dispatcher);

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