using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public OperationResult()
        {
            IsSuccess=false;
        }
        public OperationResult Succeeded(string message="عملیات با موفقیت انجام شد")
        {
            IsSuccess=true;
            Message=message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSuccess = false;
            Message = message;
            return this;
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
        public new OperationResult<T> Failed(string message)
        {
            IsSuccess = false;
            Message = message;
            return this;
        }

        public new OperationResult<T> Succeeded(string message = "عملیات با موفقیت انجام شد")
        {
            IsSuccess = true;
            Message = message;
            return this;
        }
    }
}
