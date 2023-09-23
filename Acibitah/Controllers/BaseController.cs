using Microsoft.AspNetCore.Mvc;

namespace Acibitah.Controllers
{
    public class BaseController : Controller
    {
        public const string KEY_ERROR_MESSAGE = "ErrorMessage";
        public const string KEY_SUCCESS_MESSAGE = "SuccessMessage";
        public const string SUCCESSFULLY_DELETED = "Deleted.";
        public const string ERROR_RETRIEVING = "Problem with retrieving data.";

        public const string ERROR_SAVING = "Problem with saving. Please try again later.";
        public const string SUCCESS_SAVED = "Success.";
    }
}
