using AutomatedArduinoApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutomatedArduinoApp.ViewModels
{
    internal class TerminalViewModel : BaseViewModel
    {
        public ObservableCollection<string> TerminalOutput { get; }
        public string InputText { get; set; }
        public ICommand SubmitCommand { get; }

        public TerminalViewModel()
        {
            TerminalOutput = new ObservableCollection<string>();
            SubmitCommand = new RelayCommand(ExecuteSubmitCommand);
        }

        // Command execution logic
        private void ExecuteSubmitCommand()
        {
            if (!string.IsNullOrEmpty(InputText))
            {
                // Append the command to the terminal output
                TerminalOutput.Add("Input: " + InputText);

                // For demonstration, we'll simulate an output response.
                TerminalOutput.Add("Output: " + "Echo: " + InputText);

                // Clear the input text
                InputText = string.Empty;
            }
        }
    }
}
