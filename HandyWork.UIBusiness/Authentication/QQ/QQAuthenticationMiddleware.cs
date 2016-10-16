using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using System.Net.Http;
using Owin;
using HandyWork.UIBusiness.Authentication.QQ.Provider;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.DataHandler;

namespace HandyWork.UIBusiness.Authentication.QQ
{
    public class QQAuthenticationMiddleware : AuthenticationMiddleware<QQAuthenticationOptions>
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public QQAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, QQAuthenticationOptions options)
            : base(next, options)
        {
            _logger = app.CreateLogger<QQAuthenticationOptions>();
            if (Options.Provider == null)
            {
                Options.Provider = new QQAuthenticationProvider();
            }
            if (Options.StateDataFormat == null)
            {
                var dataProtecter = app.CreateDataProtector(typeof(QQAuthenticationMiddleware).FullName, Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtecter);
            }
            _httpClient = new HttpClient(ResolveHttpMessageHandler(Options))
            {
                Timeout = Options.BackchannelTimeout,
                MaxResponseContentBufferSize = 1024 * 1024 * 10
            };
        }

        protected override AuthenticationHandler<QQAuthenticationOptions> CreateHandler()
        {
            return new QQAuthenticationHandler(_httpClient, _logger);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(QQAuthenticationOptions options)
        {
            HttpMessageHandler handler = options.BackchannelHttpHandler ?? new WebRequestHandler();
            if (options.BackchannelCertificateValidator != null)
            {
                WebRequestHandler webRequestHandler = handler as WebRequestHandler;
                if (webRequestHandler == null)
                {
                    throw new InvalidOperationException("An ICertificateValidator cannot be specified at the same time as an HttpMessageHandler unless it is a WebRequestHandler.");
                }
                webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;
            }
            return handler;
        }
    }
}
