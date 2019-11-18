using bpm.BPMonlineServiceReference;
using System;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace bpm.Models
{
    public class HTTPService
    {
        private static Uri serverUri = new Uri("http://shedko.beesender.com/0/serviceModel/entitydataservice.svc/");

        // Ссылки на пространства имен XML.
        private static readonly XNamespace ds = "http://schemas.microsoft.com/ado/2007/08/dataservices";
        private static readonly XNamespace dsmd = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
        private static readonly XNamespace atom = "http://www.w3.org/2005/Atom";
        
        public static void CreateBpmEntityByOdataWcfExample(Contact contact)
        {
            try
            {
                // Создание сообщения xml, содержащего данные о создаваемом объекте.
                var content = new XElement(dsmd + "properties",
                              new XElement(ds + "Name", contact.Name),
                              new XElement(ds + "MobilePhone", contact.MobilePhone),
                              new XElement(ds + "Dear", contact.Dear),
                              new XElement(ds + "JobTitle", contact.JobTitle),
                              new XElement(ds + "BirthDate", contact.BirthDate));

                var entry = new XElement(atom + "entry",
                            new XElement(atom + "content",
                            new XAttribute("type", "application/xml"), content));
                Console.WriteLine(entry.ToString());
                // Создание запроса к сервису, который будет добавлять новый объект в коллекцию контактов.
                var request = (HttpWebRequest)HttpWebRequest.Create(serverUri + "ContactCollection/");
                request.Credentials = new NetworkCredential("Supervisor", "Supervisor");
                request.Method = "POST";
                request.Accept = "application/atom+xml";
                request.ContentType = "application/atom+xml;type=entry";
                // Запись xml-сообщения в поток запроса.
                using (var writer = XmlWriter.Create(request.GetRequestStream()))
                {
                    entry.WriteTo(writer);
                }
                // Получение ответа от сервиса о результате выполнения операции.
                using (WebResponse response = request.GetResponse())
                {
                    if (((HttpWebResponse)response).StatusCode == HttpStatusCode.Created)
                    {
                        // Обработка результата выполнения операции.
                    }
                }
            }
            catch
            {

            }
        }

        public static void UpdateBpmEntityByOdatetWcfExample(Contact contact)
        {
            try
            {
                // Создание сообщения xml, содержащего данные об изменяемом объекте.
                var content = new XElement(dsmd + "properties",
                             new XElement(ds + "Name", contact.Name),
                             new XElement(ds + "MobilePhone", contact.MobilePhone),
                             new XElement(ds + "Dear", contact.Dear),
                             new XElement(ds + "JobTitle", contact.JobTitle),
                             new XElement(ds + "BirthDate", contact.BirthDate));

                var entry = new XElement(atom + "entry",
                        new XElement(atom + "content",
                        new XAttribute("type", "application/xml"), content));

                // Создание запроса к сервису, который будет изменять данные объекта.
                var request = (HttpWebRequest)HttpWebRequest.Create(serverUri
                        + "ContactCollection(guid'" + contact.Id + "')");
                request.Credentials = new NetworkCredential("Supervisor", "Supervisor");
                // или request.Method = "MERGE";
                request.Method = "PUT";
                request.Accept = "application/atom+xml";
                request.ContentType = "application/atom+xml;type=entry";
                // Запись сообщения xml в поток запроса.
                using (var writer = XmlWriter.Create(request.GetRequestStream()))
                {
                    entry.WriteTo(writer);
                }
                // Получение ответа от сервиса о результате выполнения операции.
                using (WebResponse response = request.GetResponse())
                {
                    // Обработка результата выполнения операции.
                }
            }
            catch { }
        }




        public static void DeleteBpmEntityByOdataWcfExample(Guid contactId)
        {                        
            try
            {
                var request = (HttpWebRequest)HttpWebRequest.Create(serverUri
                        + "ContactCollection(guid'" + contactId + "')");
                request.Credentials = new NetworkCredential("Supervisor", "Supervisor");
                request.Method = "DELETE";
                // Получение ответа от сервися о результате выполненя операции.
                using (WebResponse response = request.GetResponse())
                {
                    // Обработка результата выполнения операции.
                }
            }
            catch
            {
            }
        }
    }
}