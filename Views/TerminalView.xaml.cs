using AutomatedArduinoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutomatedArduinoApp.Views
{
    /// <summary>
    /// Interaction logic for TerminalView.xaml
    /// </summary>
    public partial class TerminalView : UserControl
    {
        public TerminalView()
        {
            InitializeComponent();
            this.DataContext = new TerminalViewModel();
        }

        // Handle the KeyDown event for the InputTextBox to trigger the submit command when Enter is pressed
        private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var viewModel = (TerminalViewModel)this.DataContext;
                viewModel.SubmitCommand.Execute(null);  // Trigger the command on Enter
            }
        }

        // Handle the Submit Button click
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TerminalViewModel)this.DataContext;
            viewModel.SubmitCommand.Execute(null);  // Trigger the command on button click
        }
    }
}
