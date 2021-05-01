using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Ofx.Battleship.Controller
{
    public class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
    {
        private readonly IExceptionFilter _exceptionFilter;

        public ConfigureMvcOptions(IExceptionFilter exceptionFilter) => _exceptionFilter = exceptionFilter;

        public void Configure(MvcOptions options) => options.Filters.Add(_exceptionFilter);
    }
}
