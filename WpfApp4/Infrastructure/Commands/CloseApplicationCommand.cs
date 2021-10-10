﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp4.Infrastructure.Commands.Base;

namespace WpfApp4.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
