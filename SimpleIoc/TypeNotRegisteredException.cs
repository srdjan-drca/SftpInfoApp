using System;

namespace SimpleIoc {

   public class TypeNotRegisteredException : Exception {

      public TypeNotRegisteredException(string message)
          : base(message) {
      }
   }
}