namespace SftpInfo.Result
{
   public abstract class ReturnResult
   {
      public bool IsSuccess { get; set; }

      public string ErrorMessage { get; set; }

      protected ReturnResult(bool isSuccess, string errorMessage)
      {
         IsSuccess = isSuccess;
         ErrorMessage = errorMessage;
      }
   }
}