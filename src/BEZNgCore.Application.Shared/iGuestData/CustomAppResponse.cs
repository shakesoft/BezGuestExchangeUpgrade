using BEZNgCore.iGuestData.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore.iGuestData
{
    public class CustomAppResponse<T>
    {
        public bool Success { get; set; }
        //public T Data { get; set; }
        public object Error { get; set; }

        
        public static CustomAppResponse<T> SuccessResponse(T data)
        {
            //return new CustomAppResponse<T> { Success = true, Data = data };
            return new CustomAppResponse<T> { Success = true, Error = null };
        }

        public static CustomAppResponse<T> ErrorResponse(string statusCode)
        {
            return new CustomAppResponse<T> { Success = false, Error = statusCode };
        }


    }
}
