
using Newtonsoft.Json;
using System;

namespace Core.TableModels
{
    public class ExceptionModel
    {
        public string Id { get; set; }
        public string SerializedExceptionObject { get; set; }
        public DateTime TimeHappened { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }


        // Getter and Setter for the actual object
        public Exception ExceptionObject
        {
            get => JsonConvert.DeserializeObject<Exception>(SerializedExceptionObject);
            set => SerializedExceptionObject = JsonConvert.SerializeObject(value);
        }

        public string ExceptionMessage => ExceptionObject.Message;
    }
}
