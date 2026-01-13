using System.ComponentModel;

namespace DigitalOOH.API.Models.Shared.Enum
{
    public enum ResponseEnum
    {
        [Description("The request is complete!")]
        Sucess,
        [Description("The request is unauthorized!")]
        Unauthorized,
        [Description("An unexpected error occured!")]
        UnexpectedError,
        [Description("No record found!")]
        NoRecordFound,
        [Description("Invalid Credentials!")]
        InvalidCredential
    }
}
