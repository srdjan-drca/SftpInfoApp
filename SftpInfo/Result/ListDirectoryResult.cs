using System.Collections.Generic;
using Renci.SshNet.Sftp;

namespace SftpInfo.Result
{
   public class ListDirectoryResult : ReturnResult
   {
      public List<SftpFile> SftpFiles { get; set; }

      public ListDirectoryResult(List<SftpFile> sftpFiles, bool isSuccess, string errorMessage = "")
         : base(isSuccess, errorMessage)
      {
         SftpFiles = sftpFiles;
      }
   }
}
