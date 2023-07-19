namespace ETicaretAPI.Application.DTOs.Configuration
{
    public class MenuDto
    {
        public string Name { get; set; }
        public List<ActionDto> Actions { get; set; } = new();
    }
}
