using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PowerQualityUploader.View
{
    /// <summary>
    /// Interaction logic for DeviceConfig.xaml
    /// </summary>
    public partial class DeviceConfig
    {
        private readonly List<string> _serialPortNames = new List<string>();

        private SerialPort _currentSerialPort;

        private List<byte> _serialPortReceiveBuffer = new List<byte>();

        public DeviceConfig()
        {
            InitializeComponent();
            ReadSystemSerialPorts();
        }

        private void ReadSystemSerialPorts()
        {
            _serialPortNames.AddRange(SerialPort.GetPortNames());
            foreach (var portName in _serialPortNames)
            {
                CmbSerialPorts.Items.Add(portName);
            }

            if (CmbSerialPorts.Items.Count > 0)
            {
                CmbSerialPorts.SelectedIndex = 0;
            }
        }

        private void OpenPort(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CmbSerialPorts.Text)) return;

            if (_currentSerialPort != null && _currentSerialPort.IsOpen)
            {
                _currentSerialPort.Close();
                _currentSerialPort.Dispose();
                BtnOpenPort.Content = "打开串口";
                CmbSerialPorts.IsEnabled = true;
                return;
            }
            _currentSerialPort = new SerialPort
            {
                BaudRate = 115200,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                PortName = CmbSerialPorts.Text
            };

            try
            {
                _currentSerialPort.Open();
                _currentSerialPort.DataReceived += CurrentSerialPortOnDataReceived;
                BtnOpenPort.Content = "关闭串口";
                CmbSerialPorts.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("打开串口失败，请检查串口是否可用或被占用。");
            }
        }

        private void CurrentSerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            lock (_serialPortReceiveBuffer)
            {
                _serialPortReceiveBuffer.AddRange(Encoding.GetEncoding("GBK").GetBytes(_currentSerialPort.ReadExisting()));
            }


        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (_currentSerialPort == null || !_currentSerialPort.IsOpen) return;
            try
            {
                _currentSerialPort.Close();
                _currentSerialPort.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void WriteConfig(object sender, RoutedEventArgs e)
        {
            var configBuilder = new StringBuilder();
            configBuilder.Append("WriteConfig=\r\n");
            configBuilder.Append($"Frequency={Frequency.Text}\r\n");
            configBuilder.Append($"LineType={((ComboBoxItem)LineType.SelectedItem).Tag}\r\n");
            configBuilder.Append($"CurrentModal={((ComboBoxItem)CurrentModal.SelectedItem).Tag}\r\n");
            configBuilder.Append($"VoltageStep={((ComboBoxItem) VoltageStep.SelectedItem).Tag}\r\n");
            configBuilder.Append($"CurrentRestore={TxtCurrentRestore.Text}\r\n");
            configBuilder.Append($"VoltageRestore={TxtVoltageRestore.Text}\r\n");
            configBuilder.Append($"Period={((ComboBoxItem)Period.SelectedItem).Tag}\r\n");
            configBuilder.Append($"SampleRate={((ComboBoxItem)SampleRate.SelectedItem).Tag}\r\n");
            configBuilder.Append($"Comment={TxtComment.Text}\r\n");
            var protocolBytes = Encoding.GetEncoding("GBK").GetBytes(configBuilder.ToString());
            var crc = Crc(protocolBytes, protocolBytes.Length);
            var finalBytes = new byte[protocolBytes.Length + 2];
            Buffer.BlockCopy(protocolBytes, 0, finalBytes, 0, protocolBytes.Length);
            Buffer.BlockCopy(crc, 0, finalBytes, protocolBytes.Length, crc.Length);
            _currentSerialPort.Write(finalBytes, 0, finalBytes.Length);
        }

        private void ReadConfig(object sender, RoutedEventArgs e)
        {
            
        }

        private byte[] Crc(byte[] sourceBytes, int dataLenth)
        {
            ushort regCrc = 0xffff;

            for (var i = 0; i < dataLenth; i++)
            {
                regCrc ^= sourceBytes[i];
                for (var j = 0; j < 8; j++)
                {
                    if ((regCrc & 0x0001) != 0)

                        regCrc = (ushort)(regCrc >> 1 ^ 0xA001);
                    else
                        regCrc >>= 1;
                }
            }

            return BitConverter.GetBytes(regCrc);
        }
    }
}
