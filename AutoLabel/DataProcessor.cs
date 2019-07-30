using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoLabel
{
    public class DataProcessor
    {

        public string sPort;
        // Delegate  
        public delegate void DataProcessorEventHandler(object sender, ProcEventArgs args);

        // Events  
        public event DataProcessorEventHandler DataProcessing;
        public event DataProcessorEventHandler DataProcessed;

        // Variable  
        //private DataStorage dataStorage;
        private ProcEventArgs procEventArgs;

        public void Process(DataStorage ds)
        {
            procEventArgs = new ProcEventArgs();
            procEventArgs.EvDataStorage = ds;
            procEventArgs.EvPercentCompleted = 0;

            Thread thread = new Thread(new ThreadStart(DoWork));
            Console.WriteLine("The thread #" +
                     Thread.CurrentThread.ManagedThreadId + " is started");
            thread.Start();
        }

        protected virtual void OnDataProcessing(ProcEventArgs procEventArgs)
        {
            DataProcessing(this, procEventArgs);
        }

        protected virtual void OnDataProcessed(ProcEventArgs procEventArgs)
        {
            if (DataProcessed != null)
            {   // Fire the OnDataProcessed event to subscripber(s)  
                Console.WriteLine(this.ToString() +
                    ": Sending the OnDataProcessed event to the subscripbers...");

                DataProcessed(this, procEventArgs);

                Console.WriteLine(this.ToString() +
                    ": The OnDataProcessed event is processed by the subscripbers");
            }
            else
            {
                Console.WriteLine("No subscripber registerd!");
            }
        }

        private void DoWork()
        {
            string returnData = "";
            Console.WriteLine("The thread #" +
                    Thread.CurrentThread.ManagedThreadId + " is started");
            ComPort cp = new ComPort();
            cp.Open(sPort);

            
            cp.SendData("hwid read_manuf");

            while(returnData == string.Empty)
            {
                returnData = cp.GetData();
                Thread.Sleep(1000);
                OnDataProcessing(procEventArgs);
            }

           

            //Random rand = new Random();
            //int T = 5;      // time in seconds  
            //int N = 100;     // number of loops  
            //for (int i = 0; i < N; i++)
            //{
            //    dataStorage.Data += rand.NextDouble();
            //    Console.WriteLine("Data=" + dataStorage.Data.ToString("0.000") +
            //                      " - " + (i + 1) * 100 / N + "% completed");

            //    OnDataProcessing();     // Fire the OnDataProcessing event  

            //    Thread.Sleep(T * 1000 / N);
            //}



            cp.Close();
            procEventArgs.EvDataStorage.Data = returnData;
            OnDataProcessed(procEventArgs);             // Fire the OnDataProcessed event  
        }
    }
}  
