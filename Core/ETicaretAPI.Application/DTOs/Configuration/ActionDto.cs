using ETicaretAPI.Application.Enums;

namespace ETicaretAPI.Application.DTOs.Configuration
{
    public class ActionDto
    {
        public ActionType ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
    }
}
