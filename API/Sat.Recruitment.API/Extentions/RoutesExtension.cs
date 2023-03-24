using Sat.Recruitment.API.Controllers;
using System.Diagnostics.CodeAnalysis;

namespace Sat.Recruitment.API.Extentions
{

    [ExcludeFromCodeCoverage]
    internal static class RoutesExtension
    {
        internal static void UseUserRoutes(this WebApplication app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UserRoutes();

        }
    }

}
