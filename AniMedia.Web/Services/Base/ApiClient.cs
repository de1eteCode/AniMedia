//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using AniMedia.Application.Exceptions;
using AniMedia.Application.Models.Identity;
using AniMedia.Web.Services.Contracts;

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"

namespace AniMedia.Web.Services.Base
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    internal partial class ApiClient : IApiClient
    {
        private System.Net.Http.HttpClient _httpClient;
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public ApiClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings { Converters = new Newtonsoft.Json.JsonConverter[] { new JsonExceptionConverter() } };
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        /// <exception cref="BadRequestException">A server side error occurred.</exception>
        public virtual System.Threading.Tasks.Task<AuthorizationResponce> ApiV1AccountLoginAsync(AuthorizationRequest request)
        {
            return ApiV1AccountLoginAsync(request, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        /// <exception cref="BadRequestException">A server side error occurred.</exception>
        public virtual async System.Threading.Tasks.Task<AuthorizationResponce> ApiV1AccountLoginAsync(AuthorizationRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("api/v1/Account/Login");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(request, _settings.Value);
                    var content_ = new System.Net.Http.StringContent(json_);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AuthorizationResponce>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiClientException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ == 400)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<BadRequestException>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiClientException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            var responseObject_ = objectResponse_.Object != null ? objectResponse_.Object : new BadRequestException();
                            responseObject_.Data.Add("HttpStatus", status_.ToString());
                            responseObject_.Data.Add("HttpHeaders", headers_);
                            responseObject_.Data.Add("HttpResponse", objectResponse_.Text);
                            throw responseObject_;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiClientException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        /// <exception cref="BadRequestException">A server side error occurred.</exception>
        /// <exception cref="IdentityException">A server side error occurred.</exception>
        public virtual System.Threading.Tasks.Task<RegistrationResponce> ApiV1AccountRegisterAsync(RegistrationRequest request)
        {
            return ApiV1AccountRegisterAsync(request, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        /// <exception cref="BadRequestException">A server side error occurred.</exception>
        /// <exception cref="IdentityException">A server side error occurred.</exception>
        public virtual async System.Threading.Tasks.Task<RegistrationResponce> ApiV1AccountRegisterAsync(RegistrationRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("api/v1/Account/Register");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(request, _settings.Value);
                    var content_ = new System.Net.Http.StringContent(json_);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<RegistrationResponce>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiClientException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ == 400)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<BadRequestException>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiClientException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            var responseObject_ = objectResponse_.Object != null ? objectResponse_.Object : new BadRequestException();
                            responseObject_.Data.Add("HttpStatus", status_.ToString());
                            responseObject_.Data.Add("HttpHeaders", headers_);
                            responseObject_.Data.Add("HttpResponse", objectResponse_.Text);
                            throw responseObject_;
                        }
                        else
                        if (status_ == 500)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<IdentityException>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiClientException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            var responseObject_ = objectResponse_.Object != null ? objectResponse_.Object : new IdentityException();
                            responseObject_.Data.Add("HttpStatus", status_.ToString());
                            responseObject_.Data.Add("HttpHeaders", headers_);
                            responseObject_.Data.Add("HttpResponse", objectResponse_.Text);
                            throw responseObject_;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiClientException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        public virtual System.Threading.Tasks.Task<System.Guid> ApiV1SecuredAsync()
        {
            return ApiV1SecuredAsync(System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        public virtual async System.Threading.Tasks.Task<System.Guid> ApiV1SecuredAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("api/v1/Secured");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<System.Guid>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiClientException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiClientException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        public virtual System.Threading.Tasks.Task<UpdateTokenResponce> ApiV1TokenAsync(UpdateTokenRequest request)
        {
            return ApiV1TokenAsync(request, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiClientException">A server side error occurred.</exception>
        public virtual async System.Threading.Tasks.Task<UpdateTokenResponce> ApiV1TokenAsync(UpdateTokenRequest request, System.Threading.CancellationToken cancellationToken)
        {
            if (request == null)
                throw new System.ArgumentNullException("request");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("api/v1/Token");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(request, _settings.Value);
                    var content_ = new System.Net.Http.StringContent(json_);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<UpdateTokenResponce>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiClientException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiClientException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiClientException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiClientException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute)) 
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool) 
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[]) value);
            }
            else if (value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array) value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return result == null ? "" : result;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    internal class JsonExceptionConverter : Newtonsoft.Json.JsonConverter
    {
        private readonly Newtonsoft.Json.Serialization.DefaultContractResolver _defaultContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        private readonly System.Collections.Generic.IDictionary<string, System.Reflection.Assembly> _searchedNamespaces;
        private readonly bool _hideStackTrace = false;
    
        public JsonExceptionConverter()
        {
            _searchedNamespaces = new System.Collections.Generic.Dictionary<string, System.Reflection.Assembly> { { typeof(BadRequestException).Namespace, System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(BadRequestException)).Assembly } };
        }
    
        public override bool CanWrite => true;
    
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var exception = value as System.Exception;
            if (exception != null)
            {
                var resolver = serializer.ContractResolver as Newtonsoft.Json.Serialization.DefaultContractResolver ?? _defaultContractResolver;
    
                var jObject = new Newtonsoft.Json.Linq.JObject();
                jObject.Add(resolver.GetResolvedPropertyName("discriminator"), exception.GetType().Name);
                jObject.Add(resolver.GetResolvedPropertyName("Message"), exception.Message);
                jObject.Add(resolver.GetResolvedPropertyName("StackTrace"), _hideStackTrace ? "HIDDEN" : exception.StackTrace);
                jObject.Add(resolver.GetResolvedPropertyName("Source"), exception.Source);
                jObject.Add(resolver.GetResolvedPropertyName("InnerException"),
                    exception.InnerException != null ? Newtonsoft.Json.Linq.JToken.FromObject(exception.InnerException, serializer) : null);
    
                foreach (var property in GetExceptionProperties(value.GetType()))
                {
                    var propertyValue = property.Key.GetValue(exception);
                    if (propertyValue != null)
                    {
                        jObject.AddFirst(new Newtonsoft.Json.Linq.JProperty(resolver.GetResolvedPropertyName(property.Value),
                            Newtonsoft.Json.Linq.JToken.FromObject(propertyValue, serializer)));
                    }
                }
    
                value = jObject;
            }
    
            serializer.Serialize(writer, value);
        }
    
        public override bool CanConvert(System.Type objectType)
        {
            return System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(System.Exception)).IsAssignableFrom(System.Reflection.IntrospectionExtensions.GetTypeInfo(objectType));
        }
    
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jObject = serializer.Deserialize<Newtonsoft.Json.Linq.JObject>(reader);
            if (jObject == null)
                return null;
    
            var newSerializer = new Newtonsoft.Json.JsonSerializer();
            newSerializer.ContractResolver = (Newtonsoft.Json.Serialization.IContractResolver)System.Activator.CreateInstance(serializer.ContractResolver.GetType());
    
            var field = GetField(typeof(Newtonsoft.Json.Serialization.DefaultContractResolver), "_sharedCache");
            if (field != null)
                field.SetValue(newSerializer.ContractResolver, false);
    
            dynamic resolver = newSerializer.ContractResolver;
            if (System.Reflection.RuntimeReflectionExtensions.GetRuntimeProperty(newSerializer.ContractResolver.GetType(), "IgnoreSerializableAttribute") != null)
                resolver.IgnoreSerializableAttribute = true;
            if (System.Reflection.RuntimeReflectionExtensions.GetRuntimeProperty(newSerializer.ContractResolver.GetType(), "IgnoreSerializableInterface") != null)
                resolver.IgnoreSerializableInterface = true;
    
            Newtonsoft.Json.Linq.JToken token;
            if (jObject.TryGetValue("discriminator", System.StringComparison.OrdinalIgnoreCase, out token))
            {
                var discriminator = Newtonsoft.Json.Linq.Extensions.Value<string>(token);
                if (objectType.Name.Equals(discriminator) == false)
                {
                    var exceptionType = System.Type.GetType("System." + discriminator, false);
                    if (exceptionType != null)
                        objectType = exceptionType;
                    else
                    {
                        foreach (var pair in _searchedNamespaces)
                        {
                            exceptionType = pair.Value.GetType(pair.Key + "." + discriminator);
                            if (exceptionType != null)
                            {
                                objectType = exceptionType;
                                break;
                            }
                        }
    
                    }
                }
            }
    
            var value = jObject.ToObject(objectType, newSerializer);
            foreach (var property in GetExceptionProperties(value.GetType()))
            {
                var jValue = jObject.GetValue(resolver.GetResolvedPropertyName(property.Value));
                var propertyValue = (object)jValue?.ToObject(property.Key.PropertyType);
                if (property.Key.SetMethod != null)
                    property.Key.SetValue(value, propertyValue);
                else
                {
                    field = GetField(objectType, "m_" + property.Value.Substring(0, 1).ToLowerInvariant() + property.Value.Substring(1));
                    if (field != null)
                        field.SetValue(value, propertyValue);
                }
            }
    
            SetExceptionFieldValue(jObject, "Message", value, "_message", resolver, newSerializer);
            SetExceptionFieldValue(jObject, "StackTrace", value, "_stackTraceString", resolver, newSerializer);
            SetExceptionFieldValue(jObject, "Source", value, "_source", resolver, newSerializer);
            SetExceptionFieldValue(jObject, "InnerException", value, "_innerException", resolver, serializer);
    
            return value;
        }
    
        private System.Reflection.FieldInfo GetField(System.Type type, string fieldName)
        {
            var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(type).GetDeclaredField(fieldName);
            if (field == null && System.Reflection.IntrospectionExtensions.GetTypeInfo(type).BaseType != null)
                return GetField(System.Reflection.IntrospectionExtensions.GetTypeInfo(type).BaseType, fieldName);
            return field;
        }
    
        private System.Collections.Generic.IDictionary<System.Reflection.PropertyInfo, string> GetExceptionProperties(System.Type exceptionType)
        {
            var result = new System.Collections.Generic.Dictionary<System.Reflection.PropertyInfo, string>();
            foreach (var property in System.Linq.Enumerable.Where(System.Reflection.RuntimeReflectionExtensions.GetRuntimeProperties(exceptionType), 
                p => p.GetMethod?.IsPublic == true))
            {
                var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute<Newtonsoft.Json.JsonPropertyAttribute>(property);
                var propertyName = attribute != null ? attribute.PropertyName : property.Name;
    
                if (!System.Linq.Enumerable.Contains(new[] { "Message", "StackTrace", "Source", "InnerException", "Data", "TargetSite", "HelpLink", "HResult" }, propertyName))
                    result[property] = propertyName;
            }
            return result;
        }
    
        private void SetExceptionFieldValue(Newtonsoft.Json.Linq.JObject jObject, string propertyName, object value, string fieldName, Newtonsoft.Json.Serialization.IContractResolver resolver, Newtonsoft.Json.JsonSerializer serializer)
        {
            var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(System.Exception)).GetDeclaredField(fieldName);
            var jsonPropertyName = resolver is Newtonsoft.Json.Serialization.DefaultContractResolver ? ((Newtonsoft.Json.Serialization.DefaultContractResolver)resolver).GetResolvedPropertyName(propertyName) : propertyName;
            var property = System.Linq.Enumerable.FirstOrDefault(jObject.Properties(), p => System.String.Equals(p.Name, jsonPropertyName, System.StringComparison.OrdinalIgnoreCase));
            if (property != null)
            {
                var fieldValue = property.Value.ToObject(field.FieldType, serializer);
                field.SetValue(value, fieldValue);
            }
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