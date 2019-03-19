using System;
using System.IO;
using Renci.SshNet;
using SftpInfo.Result;
using System.Collections.Generic;
using System.Linq;
using Renci.SshNet.Sftp;

namespace SftpInfo
{
   public class SftpClientAdapter
   {
      private readonly SftpClient _renciSshSftpClient;

      public SftpClientAdapter(string ipAddress, string userName, string password)
      {
         _renciSshSftpClient = new SftpClient(ipAddress, userName, password);
      }

      public ReturnResult Connect()
      {
         try
         {
            _renciSshSftpClient.Connect();
         }
         catch (Exception exception)
         {
            return new FailResult(exception.Message);
         }

         return new SuccessResult();
      }

      public ReturnResult Disconnect()
      {
         try
         {
            _renciSshSftpClient.Disconnect();
         }
         catch (Exception exception)
         {
            return new FailResult(exception.Message);
         }

         return new SuccessResult();
      }

      public ReturnResult CreateDirectory(string path)
      {
         try
         {
            _renciSshSftpClient.CreateDirectory(path);
         }
         catch (Exception exception)
         {
            return new FailResult(exception.Message);
         }

         return new SuccessResult();
      }

      public ReturnResult UploadFile(Stream stream, string path)
      {
         try
         {
            _renciSshSftpClient.UploadFile(stream, path);
         }
         catch (Exception exception)
         {
            return new FailResult(exception.Message);
         }

         return new SuccessResult();
      }

      public ListDirectoryResult ListDirectory(string path)
      {
         List<SftpFile> directoryList;

         try
         {
            directoryList = _renciSshSftpClient.ListDirectory(path).ToList();
         }
         catch (Exception exception)
         {
            directoryList = new List<SftpFile>();

            return new ListDirectoryResult(directoryList, false, exception.Message);
         }

         return new ListDirectoryResult(directoryList, true);
      }

      public ReturnResult DeleteDirectory(string path)
      {
         try
         {
            _renciSshSftpClient.DeleteDirectory(path);
         }
         catch (Exception exception)
         {
            return new FailResult(exception.Message);
         }

         return new SuccessResult();
      }
   }
}
