

using Newtonsoft.Json;

namespace Resturant.DBModels.DTO
{
    public class Global_Response_DTO<T>
        {
            public T Data { get; set; }
        [JsonProperty("Message")]    
        public string Message { get; set; }
            public bool Success { get; set; }
        }
}
