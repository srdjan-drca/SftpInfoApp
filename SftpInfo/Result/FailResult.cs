namespace SftpInfo.Result
{
   public class FailResult : ReturnResult
   {
      public FailResult(string errorMessage) : base(false, errorMessage)
      {
      }
   }
}