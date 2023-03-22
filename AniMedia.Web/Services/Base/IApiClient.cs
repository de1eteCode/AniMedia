//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Requests;
using AniMedia.Domain.Models.Responses;

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"

namespace AniMedia.Web.Services.Contracts
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface IApiClient
    {
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthAuthorizationAsync(string token);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthAuthorizationAsync(string token, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<SessionDto> ApiV1AuthSessionsGetAsync(string accessToken);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<SessionDto> ApiV1AuthSessionsGetAsync(string accessToken, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<SessionDto> ApiV1AuthSessionsGetAsync(System.Guid sessionUid);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<SessionDto> ApiV1AuthSessionsGetAsync(System.Guid sessionUid, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.ICollection<SessionDto>> ApiV1AuthSessionsGetAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.ICollection<SessionDto>> ApiV1AuthSessionsGetAsync(System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthRefreshAsync(System.Guid refreshToken, string userAgent);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthRefreshAsync(System.Guid refreshToken, string userAgent, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthRegistrationAsync(string userAgent, RegistrationRequest request);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthRegistrationAsync(string userAgent, RegistrationRequest request, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthLoginAsync(string userAgent, LoginRequest request);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AuthorizationResponse> ApiV1AuthLoginAsync(string userAgent, LoginRequest request, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Guid> ApiV1SecuredAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Guid> ApiV1SecuredAsync(System.Threading.CancellationToken cancellationToken);

    }

    



    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiClientException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiClientException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiClientException<TResult> : ApiClientException
    {
        public TResult Result { get; private set; }

        public ApiClientException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore  472
#pragma warning restore  114
#pragma warning restore  108
#pragma warning restore 3016
#pragma warning restore 8603