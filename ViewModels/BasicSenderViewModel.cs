using AutomatedArduinoApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutomatedArduinoApp.ViewModels
{
    class BasicSenderViewModel : BaseViewModel
    {
        public ICommand Button1Command { get; set; }
        public ICommand Button2Command { get; set; }
        public ICommand Button3Command { get; set; }
        public ICommand Button4Command { get; set; }
        public ICommand Button5Command { get; set; }
        public ICommand Button6Command { get; set; }
        public ICommand ButtonUnconfigured { get; set; }

        public BasicSenderViewModel()
        {
            // Initialize commands
            Button1Command = new RelayCommand(ExecuteButton1);
            Button2Command = new RelayCommand(ExecuteButton2);
            Button3Command = new RelayCommand(ExecuteButton3);
            Button4Command = new RelayCommand(ExecuteButton4);
            Button5Command = new RelayCommand(ExecuteButton5);
            Button6Command = new RelayCommand(ExecuteButton6);
            ButtonUnconfigured = new RelayCommand(ExecuteButtonUnconfigured);
        }

        private void ExecuteButton1()
        {

            System.Windows.MessageBox.Show("Button 1 clicked");
        }

        private void ExecuteButton2()
        {

            System.Windows.MessageBox.Show("Button 2 clicked");
        }

        private void ExecuteButton3()
        {

            System.Windows.MessageBox.Show("Button 3 clicked");
        }

        private void ExecuteButton4()
        {

            System.Windows.MessageBox.Show("Button 4 clicked");
        }
        private void ExecuteButton5()
        {

            System.Windows.MessageBox.Show("Button 5 clicked");
        }
        private void ExecuteButton6()
        {

            System.Windows.MessageBox.Show("Button 6 clicked");
        }

        private void ExecuteButtonUnconfigured()
        {

            System.Windows.MessageBox.Show("Button not configured");
        }
    }
}
