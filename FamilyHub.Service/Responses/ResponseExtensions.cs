using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, Exception ex)
        {
            response.DidError = true;
            if (response.Message == null) response.Message = ResponseMessageDisplay.Error;

            var cast = ex as FamilyHubException;

            if (cast == null)
            {
                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
            else
            {
                response.ErrorMessage = ex.Message;
            }
        }
    }
}
