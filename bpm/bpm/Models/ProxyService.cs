using bpm.BPMonlineServiceReference;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;

namespace bpm.Models
{
    public class ProxyService
    {
        private static Uri serverUri = new Uri("http://shedko.beesender.com/0/serviceModel/entitydataservice.svc/");

        static void OnSendingRequestCookie(object sender, SendingRequestEventArgs e)
        {
            // Вызов метода класса LoginClass, реализующего аутентификацию переданного в параметрах метода пользователя.
            LoginClass.TryLogin("Supervisor", "Supervisor");
            var req = e.Request as HttpWebRequest;
            // Добавление полученных аутентификационных cookie в запрос на получение данных.
            req.CookieContainer = LoginClass.AuthCookie;
            e.Request = req;
        }

        public static IEnumerable<Contact> GetOdataCollectioByLinqWcfExample()
        {

            IEnumerable<Contact> allContacts = null;

            // Создание контекста приложения BPMonline.
            var context = new BPMonline(serverUri);
            // Определение метода, который добавляет аутентификационные cookie при создании нового запроса.
            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(OnSendingRequestCookie);
            try
            {
                // Построение запроса LINQ для получение коллекции контактов.                
                allContacts = from contacts in context.ContactCollection
                                  select contacts; 
            }
            catch (Exception ex)
            {
                // Обработка ошибок.
            }
            return allContacts;
        }         
    }
}