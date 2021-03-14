using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeMachineGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }
    }
}
