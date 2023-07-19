using System.ComponentModel;

namespace ETicaretAPI.Application.Enums
{
    public enum ActionType
    {
        [Description("Reading")] Reading,
        [Description("Writing")] Writing,
        [Description("Updating")] Updating,
        [Description("Deleting")] Deleting,
    }
}
