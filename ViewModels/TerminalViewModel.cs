using AutomatedArduinoApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutomatedArduinoApp.ViewModels
{
    internal class TerminalViewModel : BaseViewModel
    {
        public ObservableCollection<string> TerminalOutput { get; set; }
        public string InputText { get; set; }
        public RelayCommand ButtonSendClick { get; set; }
        public ICommand SubmitCommand { get; }
        private SerialPort _serialPort;

        public TerminalViewModel()
        {
            ButtonSendClick = new RelayCommand(() => { ExecuteSubmitCommandAsync(); });
            TerminalOutput = new ObservableCollection<string>();
            InitSerialPort();
        }

        private void InitSerialPort() {
            _serialPort = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One)
            {
                ReadTimeout = 500,  // Timeout for reading
                WriteTimeout = 500  // Timeout for writing
            };

            _serialPort.DataReceived += OnDataReceived;
            OpenSerialPort();
        }

        private void OpenSerialPort()
        {
            if (!_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Open();
                    TerminalOutput.Add("Connected to Arduino...");
                }
                catch (Exception ex)
                {
                    TerminalOutput.Add($"Error: {ex.Message}");
                }
            }
        }

        // Command execution logic
        private async void ExecuteSubmitCommandAsync()
        {
            if (!string.IsNullOrEmpty(InputText))
            {
                await SendCommandToArduino(InputText);

                // Clear the input text
                InputText = string.Empty;
            }
        }

        private async Task SendCommandToArduino(string command)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    // Write the command to Arduino
                    _serialPort.WriteLine(command);
                    TerminalOutput.Add($"Sent: {command}");

                    // Wait for a response from Arduino
                    await Task.Delay(500); // Allow Arduino time to respond
                }
                catch (Exception ex)
                {
                    TerminalOutput.Add($"Error: {ex.Message}");
                }
            }
            else
            {
                TerminalOutput.Add("Error: Serial port not open.");
            }
        }

        // Event handler to read data from Arduino when it's received
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialDevice = sender as SerialPort;
            if (serialDevice == null)
                return;
            Thread.Sleep(10);
            // Get the number of bytes available to read
            int bytesToRead = serialDevice.BytesToRead;
            if (bytesToRead == 0)
                return; // No data to read

            // Create a buffer to hold the incoming data
            var buffer = new byte[bytesToRead];

            // Read the data into the buffer
            serialDevice.Read(buffer, 0, bytesToRead);

            // Convert the byte buffer to a string (assuming ASCII encoding)
            string receivedData = System.Text.Encoding.ASCII.GetString(buffer);

            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    Debug.WriteLine(receivedData);
                    TerminalOutput.Add($"Arduino: {receivedData}");
                }));
              
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    {
                        // If there's an error, update the ObservableCollection on the UI thread
                        TerminalOutput.Add($"Error: {ex.Message}");
                    }
                }));
            }
        }
    }
}
