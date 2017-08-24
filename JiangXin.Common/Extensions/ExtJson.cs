using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JiangXin.Common.Extensions
{
    public static class ExtJson
    {
        /// <summary>
        /// Json 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Json 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonRemoveNull(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }


        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObj<T>(this string json) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(json);
        }


        /// <summary>
        /// api接口获取请求参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Param(this HttpRequestMessage request, string name)
        {
            try
            {
                name = name.ToLower();
                if (request.Method == HttpMethod.Post)
                {
                    var jsonStr = request.Content.ReadAsStringAsync().Result;//{"Name":"tongl","Age":22}
                    var json = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonStr);
                    if (json != null && json.Count > 0)
                    {
                        foreach (var item in json)
                        {
                            if (item.Key.ToLower() == name)
                            {
                                return item.Value.Trim();
                            }
                        }
                    }
                }
                else if (request.Method == HttpMethod.Get)
                {
                    if (!string.IsNullOrEmpty(request.RequestUri.Query) && request.RequestUri.Query.ToLower().Contains(name))
                    {
                        string queryValue = request.RequestUri.Query.ToLower().Replace("?", "");
                        if (!string.IsNullOrEmpty(queryValue))
                        {
                            if (queryValue.Contains("&"))
                            {
                                string[] queryParameter = queryValue.Split('&');
                                foreach (string queryInfo in queryParameter)
                                {
                                    if (queryInfo.Contains(name))
                                    {
                                        string[] parameter = queryInfo.Split('=');
                                        foreach (string parameterInfo in parameter)
                                        {
                                            if (parameterInfo != name) return parameterInfo;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] parameter = queryValue.Split('=');
                                foreach (string parameterInfo in parameter)
                                {
                                    if (parameterInfo != name) return parameterInfo;
                                }
                            }
                        }
                    }
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }
    }
}
