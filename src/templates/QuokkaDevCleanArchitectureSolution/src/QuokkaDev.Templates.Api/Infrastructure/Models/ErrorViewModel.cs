﻿namespace QuokkaDev.Templates.Api.Infrastructure.Models
{
    public class ErrorViewModel
    {
        public InnerErrorViewModel Error { get; }

        public ErrorViewModel(string message, int code, string[] errors)
        {
            Error = new InnerErrorViewModel(message, code, errors);
        }

        public ErrorViewModel(Exception ex) : this(ex.Message, ex.HResult, GetInnerExceptions(ex))
        {
        }

        private static string[] GetInnerExceptions(Exception ex)
        {
            List<string> errors = new();
            Exception? innerError = ex.InnerException;
            while (innerError != null)
            {
                errors.Add(innerError.Message);
                innerError = innerError.InnerException;
            }
            return errors.ToArray();
        }
    }

    public class InnerErrorViewModel
    {
        public string Message { get; }
        public int Code { get; }
        public string[] Errors { get; }

        public InnerErrorViewModel(string message, int code, string[] errors)
        {
            Message = message;
            Code = code;
            Errors = errors;
        }
    }
}
