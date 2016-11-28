using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private readonly List<byte> _serialPortReceiveBuffer = new List<byte>();

        private bool _isRead;

        private int _lastWriteLength;

        private bool _processWaiting;

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
                BtnOpenPort.Content = "关闭串口";
                _currentSerialPort.DataReceived += CurrentSerialPortOnDataReceived;
                CmbSerialPorts.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("打开串口失败，请检查串口是否可用或被占用。");
            }
        }

        private void CurrentSerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            if (!_processWaiting) return;
            lock (_serialPortReceiveBuffer)
            {
                _serialPortReceiveBuffer.AddRange(Encoding.GetEncoding("GBK").GetBytes(_currentSerialPort.ReadExisting()));
            }

            if (_isRead)
            {
                Task.Factory.StartNew(ReadCheck);
            }
            else
            {
                Task.Factory.StartNew(WriteCheck);
            }
        }

        private void ReadCheck()
        {
            Dispatcher.Invoke(() =>
            {
                Thread.Sleep(1000);
                _processWaiting = false;
                if (_serialPortReceiveBuffer.Count < 2
                    || (_serialPortReceiveBuffer[0] != 23 && _serialPortReceiveBuffer[1] != 23)
                    || _serialPortReceiveBuffer.Count - 2 != _lastWriteLength)
                {
                    MessageBox.Show("读取配置失败。");
                }
                else
                {
                    try
                    {
                        _serialPortReceiveBuffer.RemoveRange(0, 2);
                        var items = Encoding.GetEncoding("GBK")
                                .GetString(_serialPortReceiveBuffer.ToArray())
                                .Split(new[] {"\r\n"}, StringSplitOptions.None);
                        var config = items.Select(cfg => cfg.Split('=')).ToDictionary(values => values[0], values => values[1]);
                        Frequency.Text = config["Frequency"];
                        LineType.SelectedValue = config["LineType"];
                        CurrentModal.SelectedValue = config["CurrentModal"];
                        VoltageStep.SelectedValue = config["VoltageStep"];
                        TxtCurrentRestore.Text = config["CurrentRestore"];
                        TxtVoltageRestore.Text = config["VoltageRestore"];
                        Period.SelectedValue = config["Period"];
                        SampleRate.SelectedValue = config["SampleRate"];
                        TxtComment.Text = config["Comment"];
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("读取配置失败。");
                    }
                    
                }

            });
        }

        private void WriteCheck()
        {
            Dispatcher.Invoke(() =>
            {
                Thread.Sleep(1000);
                _processWaiting = false;
                if (_serialPortReceiveBuffer.Count < 2
                    || (_serialPortReceiveBuffer[0] != 23 && _serialPortReceiveBuffer[1] != 23)
                    || _serialPortReceiveBuffer.Count - 2 != _lastWriteLength)
                {
                    MessageBox.Show("写入配置失败。");
                }
                else
                {
                    MessageBox.Show("写入配置成功。");
                }
            });
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
            try
            {
                var configBuilder = new StringBuilder();
                configBuilder.Append("WriteConfig=\r\n");
                configBuilder.Append($"Frequency={Frequency.Text}\r\n");
                configBuilder.Append($"LineType={((ComboBoxItem) LineType.SelectedItem).Tag}\r\n");
                configBuilder.Append($"CurrentModal={((ComboBoxItem) CurrentModal.SelectedItem).Tag}\r\n");
                configBuilder.Append($"VoltageStep={((ComboBoxItem) VoltageStep.SelectedItem).Tag}\r\n");
                configBuilder.Append($"CurrentRestore={TxtCurrentRestore.Text}\r\n");
                configBuilder.Append($"VoltageRestore={TxtVoltageRestore.Text}\r\n");
                configBuilder.Append($"Period={((ComboBoxItem) Period.SelectedItem).Tag}\r\n");
                configBuilder.Append($"SampleRate={((ComboBoxItem) SampleRate.SelectedItem).Tag}\r\n");
                configBuilder.Append($"Comment={TxtComment.Text}\r\n");
                var protocolBytes = Encoding.GetEncoding("GBK").GetBytes(configBuilder.ToString());
                var crc = Crc(protocolBytes, protocolBytes.Length);
                var finalBytes = new byte[protocolBytes.Length + 2];
                Buffer.BlockCopy(protocolBytes, 0, finalBytes, 0, protocolBytes.Length);
                Buffer.BlockCopy(crc, 0, finalBytes, protocolBytes.Length, crc.Length);
                _currentSerialPort.Write(finalBytes, 0, finalBytes.Length);
                _lastWriteLength = finalBytes.Length;
                _serialPortReceiveBuffer.Clear();
                _processWaiting = true;
            }
            catch (Exception)
            {
                MessageBox.Show("发送失败，请检查串口连接。");
            }
        }

        private void ReadConfig(object sender, RoutedEventArgs e)
        {
            _serialPortReceiveBuffer.Clear();
            _isRead = true;
            _processWaiting = true;
            try
            {
                var configBuilder = new StringBuilder();
                configBuilder.Append("WriteConfig=\r\n");
                var protocolBytes = Encoding.GetEncoding("GBK").GetBytes(configBuilder.ToString());
                _currentSerialPort.Write(protocolBytes, 0, protocolBytes.Length);
            }
            catch (Exception)
            {
                MessageBox.Show("发送读取请求失败，请检查串口连接。");
            }
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
