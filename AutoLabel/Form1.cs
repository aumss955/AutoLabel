using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AutoLabel
{
    public partial class Form1 : Form
    {

        


        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private string sSN,sRev,sPN;
        private string printerPath;
        private string sScanLabel;

        public string SScanLabel
        {
            get
            {
                return sScanLabel;
            }

            set
            {
                sScanLabel = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            lbStatus.Text = "Status";
            lbStatus.BackColor = Color.White;

            string sPort = comboBox1.SelectedItem.ToString();
       

            
            if (textBox3.Text != "" || textBox4.Text != "")
            {
                ScanLabel scan = new ScanLabel(this);

                scan.ShowDialog();

                textBox5.Text = sScanLabel;
                textBox1.Text = "";
                //textBox2.Text = "";

                DataStorage dataStorage = new DataStorage() { Data = "test" };
                DataProcessor dataProcessor = new DataProcessor();

                dataProcessor.sPort = sPort;
                // Subscripber A, the SubscripberClass  


                // Subscripber B, this form  
                dataProcessor.DataProcessing += this.OnDataProcessing;
                dataProcessor.DataProcessed += this.OnDataProcessed;

                // Start process the data  
                dataProcessor.Process(dataStorage);

                

            }
            else
            {
                MessageBox.Show("Please input Rev. and PN. !!!");
            }

        }



        private void OnDataProcessing(object sender, ProcEventArgs args)
        {
            Invoke((MethodInvoker)(() =>
            {
                textBox2.AppendText("TESTING \n");
            }));
        }

        private void OnDataProcessed(object sender, ProcEventArgs args)
        {
            string dataRecive = args.EvDataStorage.Data;
            string xmlString = System.IO.File.ReadAllText("C:\\Users\\AumHey\\Desktop\\XML_format.xml");

            Console.WriteLine(dataRecive.Length);

            if (dataRecive.Length > 90)
            {
                sRev = textBox3.Text;
                sPN = textBox4.Text;

                XmlDocument xm = new XmlDocument();

                Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText(args.EvDataStorage.Data + Environment.NewLine);
                });


                Regex regex = new Regex(@"[A-Z]\d+");
                Match match = regex.Match(dataRecive);
                if (match.Success)
                {
                    Console.WriteLine("MATCH VALUE: " + match.Value);
                    sSN = match.Value;


                    Invoke((MethodInvoker)delegate ()
                    {
                        textBox1.Text = sSN;

                    });

                    xmlString = xmlString.Replace("{{REV}}", sRev);
                    xmlString = xmlString.Replace("{{PN}}", sPN);
                    xmlString = xmlString.Replace("{{SN}}", sSN);
                    xmlString = xmlString.Replace("{{PRINTER}}", tbIP.Text);

                    Invoke((MethodInvoker)delegate ()
                    {
                        textBox2.AppendText("==> Write File to Result folder" + Environment.NewLine);

                    });

                    File.WriteAllText(Path.Combine("Result\\", "TEST.xml"), xmlString);




             


                    if (sSN == SScanLabel)
                    {


                        Invoke((MethodInvoker)delegate ()
                        {
                            textBox2.AppendText("==> Match Found !!! HWID กับ Label S/N เหมือนกัน ไม่จำเป็นต้องปิ้นใหม่" + Environment.NewLine);
                            lbStatus.Text = "ไม่จำเป็นต้องปริ้น Label ใหม่";
                            lbStatus.BackColor = Color.GreenYellow;

                        });
                    }
                    else
                    {
                        Invoke((MethodInvoker)delegate ()
                        {
                            textBox2.AppendText("==> HWID and Scanlabel S/N are not match. จำเป็นต้องปิ้น Label ใหม่" + Environment.NewLine);
                            lbStatus.Text = "ปริ้น Label ใหม่";
                            lbStatus.BackColor = Color.Red;
                            button2.Enabled = true;
                        });
                    }
                }
                else
                {
                    Invoke((MethodInvoker)delegate ()
                    {
                        textBox2.AppendText("==> Error กรูณา insert ตัวงานอีกครั้ง"+Environment.NewLine);
                      
                    });

                }

                // xm.LoadXml(xmlString);

                //XML.WriteToXmlFile("TEST.xml", xm,true);
                //.WriteAllText("C:\\Test.xml", xmlString);
            }
            else
            {
                Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText("==> Error กรูณา insert ตัวงานอีกครั้ง" + Environment.NewLine);

                });

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = ComPort.getPortsname();

            comboBox1.Items.AddRange(ports);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sSN != string.Empty || sSN != null)
            {
                textBox2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool checkCON;
            string sourceFile = Path.Combine("Result\\", "TEST.xml");
            string backupFile = Path.Combine("Backup\\", "TEST.xml");
            string destFile = @"D:\AUM.xml";
            printerPath = Path.Combine(tbPath.Text);

            if (tbIP.Text != "" || tbPath.Text != "")
            {
                checkCON = checkConThread(tbIP.Text);
                if(checkCON == true)
                {
                    try
                    {
                        textBox2.AppendText("==> Writing File to " + printerPath + Environment.NewLine);
                        System.IO.File.Copy(sourceFile, printerPath, true);
                        textBox2.AppendText("==> Finished writing" + Environment.NewLine);
                        System.IO.File.Copy(sourceFile, backupFile, true);
                        System.IO.File.Delete(sourceFile);
                        button2.Enabled = false;
                    }
                    catch (ArgumentNullException ex)
                    {
                        textBox2.AppendText("==> Error writing" + Environment.NewLine);
                        MessageBox.Show("Please input directory path");
                    }
                }
                else
                {
                    MessageBox.Show("Connection has error, Please check printer!!!");
                    textBox2.AppendText("==> Connection has error, Please check printer!!!" + Environment.NewLine);
                }
            }
            else
            {
                MessageBox.Show("Please input printer IP or path!!!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                    tbPath.Text = fbd.SelectedPath + "\\test.xml";
                    

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public static bool checkConThread(string sIP)
        {
            
            
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(sIP, 1000);
                if (reply != null)
                {
                    Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);
                    //Console.WriteLine(reply.ToString());

                }

                return true;
            }
            catch
            {
                Console.WriteLine("ERROR: You have Some TIMEOUT issue");
                return false;
            }
           
        }

       
    }


        //comport.Close();
    }
    

