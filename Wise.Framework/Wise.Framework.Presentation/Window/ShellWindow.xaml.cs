﻿using System;
using System.Linq;
using Wise.Framework.Interface.Window;
using Wise.Framework.Presentation.Interface.Shell;
using Wise.Framework.Presentation.Interface.ViewModel;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Window
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class ShellWindow : WindowBase, IShellWindow
    {
        /// <summary>
        ///     Ctor for shell window object
        /// </summary>
        /// <param name="shellViewModel">default shell window view model</param>
        /// c
        public ShellWindow(IShellViewModel shellViewModel)
        {
            DataContext = shellViewModel;

            InitializeComponent();
        }

    }
}