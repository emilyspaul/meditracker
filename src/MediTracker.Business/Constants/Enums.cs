using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MediTracker.Business.Constants
{
    public enum Frequency
    {
        Daily,
        Weekly,
        Monthly,
        [Description("Twice Daily")]
        TwiceDaily,
        [Description("Every Other Day")] 
        EveryOtherDay,
        [Description("Specific Day")]
        SpecificDay
    }

    public enum ReminderType
    {
        Email,
        Text
    }
}
