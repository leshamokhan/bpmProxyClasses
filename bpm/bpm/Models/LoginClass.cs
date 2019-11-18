using System;
using System.IO;
using System.Net;

namespace bpm.Models
{
    public class LoginClass
    {
        class ResponseStatus
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Exception { get; set; }
            public object PasswordChangeUrl { get; set; }
            public object RedirectUrl { get; set; }
        }

        public const string authServiceUri = "http://shedko.beesender.com/ServiceModel/AuthService.svc/Login";

        public static CookieContainer AuthCookie = new CookieContainer();

        public static bool TryLogin(string userName = "Supervisor", string userPassword = "Supervisor")
        {

            var authRequest = HttpWebRequest.Create(authServiceUri) as HttpWebRequest;
            authRequest.Method = "POST";
            authRequest.ContentType = "application/json";
            // Включение использования cookie в запросе.
            authRequest.CookieContainer = AuthCookie;
            // Получение потока, ассоциированного с запросом на аутентификацию.
            using (var requestStream = authRequest.GetRequestStream())
            {
                // Запись в поток запроса учетных данных пользователя BPMonline и дополнительных параметров запроса.
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(@"{
                                ""UserName"":""" + userName + @""",
                                ""UserPassword"":""" + userPassword + @""",
                                ""SolutionName"":""TSBpm"",
                                ""TimeZoneOffset"":-120,
                                ""Language"":""Ru-ru""
                                }");
                }
            }

            ResponseStatus status = null;

            // Получение ответа от сервера. Если аутентификация проходит успешно, в объекте bpmCookieContainer будут 
            // помещены cookie, которые могут быть использованы для последующих запросов.
            using (var response = (HttpWebResponse)authRequest.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    // Десериализация HTTP-ответа во вспомогательный объект.
                    string responseText = reader.ReadToEnd();
                    status = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ResponseStatus>(responseText);
                }
            }

            // Проверка статуса аутентификации.
            if (status != null)
            {
                // Успешная аутентификация.
                if (status.Code == 0)
                {
                    return true;
                }
                // Сообщение о неудачной аутентификации.
                Console.WriteLine(status.Message);
            }
            return false;
        }
    }
}