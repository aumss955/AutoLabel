using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLabel
{
    public class ComPort
    {
        SerialPort SP = new SerialPort();
        List<string> RevData = new List<string>();
        public ComPort()
        {
        }
        ~ComPort()
        {
            SP.Close();
        }
        private void SP_DataRecieved(Object sender, SerialDataReceivedEventArgs e)
        {
            RevData.Add(SP.ReadExisting());
            //Console.WriteLine(SP.ReadExisting());

         
        }
        private void SP_ErrorRecieved(Object sender, SerialErrorReceivedEventArgs e)
        {
            RevData.Add("Error while receiving data. " + e.ToString());
        }

        public string GetData()
        {
            string RevStr;
            if (RevData.Count == 0) return "";
            RevStr = RevData[0];
            RevData.RemoveAt(0);
            return RevStr;
        }
        public bool SendData(string Data)
        {
            if (SP.IsOpen)
            {
                SP.WriteLine(Data);
                return true;
            }
            else return false;
        }
        public bool Open(string PortName, int BaudRate = 115200, int DataBits = 8)
        {

            SP.PortName = PortName;
            SP.BaudRate = BaudRate;
            SP.DataBits = DataBits;
            SP.Parity = Parity.None;
            SP.StopBits = StopBits.One;
            SP.ReadTimeout = (int)500;
            SP.WriteTimeout = (int)500;
            SP.ReadBufferSize = 4;
            SP.WriteBufferSize = 4;
            SP.DtrEnable = true;
            SP.RtsEnable = true;
            SP.DataReceived += new SerialDataReceivedEventHandler(SP_DataRecieved);
            SP.ErrorReceived += new SerialErrorReceivedEventHandler(SP_ErrorRecieved);
            SP.Open();

            if (SP.IsOpen) return true; else return false;
        }
        public void Close()
        {
            SP.Close();
        }

        public static string[] getPortsname()
        {
            string[] ports = SerialPort.GetPortNames();

            return ports;
        }
    }
}
