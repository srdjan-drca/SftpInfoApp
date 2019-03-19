using System;
using System.IO;
using Renci.SshNet;

namespace SftpClientExample
{
   class Program
   {
      static void Main(string[] args)
      {
         string ipAddress = "10.150.57.6";
         string userName = "tester";
         string password = "password";
         string keyFilePath = @"d:\Users\drs\Desktop\Firmware\10.150.57.6 - khr\1_SCP_35_2.0.0.2L_auf_SCP2.0.0.3.KEY";
         string firmwareFilePath = @"d:\Users\drs\Desktop\Firmware\10.150.57.6 - khr\1_SCP_2.0.0.3_FPGA_5.frm";

         try
         {
            var mSftpClient = new SftpClient(ipAddress, userName, password);

            mSftpClient.Connect();

            //Console.WriteLine($"Host = {mSftpClient.ConnectionInfo.Host}");
            //Console.WriteLine($"ClientVersion = {mSftpClient.ConnectionInfo.ClientVersion}");
            //Console.WriteLine($"ServerVersion = {mSftpClient.ConnectionInfo.ServerVersion}");
            //Console.WriteLine($"MaxSessions = {mSftpClient.ConnectionInfo.MaxSessions}");
            //Console.WriteLine($"ProxyHost = {mSftpClient.ConnectionInfo.ProxyHost}");
            //Console.WriteLine($"ProxyType = {mSftpClient.ConnectionInfo.ProxyType}");
            //Console.WriteLine($"ProxyPort = {mSftpClient.ConnectionInfo.ProxyPort}");
            //Console.WriteLine($"ProxyUsername = {mSftpClient.ConnectionInfo.ProxyUsername}");
            //Console.WriteLine($"ProxyPassword = {mSftpClient.ConnectionInfo.ProxyPassword}");

            const string rootFolder = @"/tmp/";
            string dataId = DateTime.Now.ToString(@"dd-MM-yyyy-HH-mm-ss");
            string folder = rootFolder + dataId + "/";

            mSftpClient.CreateDirectory(folder);

            using (Stream fs = File.OpenRead(keyFilePath))
            {
               mSftpClient.UploadFile(fs, folder + "key");
            }

            using (Stream fs = File.OpenRead(firmwareFilePath))
            {
               mSftpClient.UploadFile(fs, folder + "data");
            }

            mSftpClient.Disconnect();
         }
         catch(Exception exception)
         {
            Console.WriteLine(exception.Message);
         }

         Console.ReadKey();
      }
   }
}
