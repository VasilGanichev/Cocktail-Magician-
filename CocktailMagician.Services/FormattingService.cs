using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CocktailMagician.Services.Contracts;
using Microsoft.AspNetCore.Routing;

namespace CocktailMagician.Services
{
    public class FormattingService : ViewExecutor, IFormattingService
    {
        private readonly IActionContextAccessor _ActionContextAccessor;
        private ITempDataProvider _TempDataProvider;

        public Byte[] HtmlStringToPDF(string html)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderHtmlAsPdf(html);

            return PDF.BinaryData;
        }

        public FormattingService(
                    IActionContextAccessor actionContextAccessor,
                    IOptions<MvcViewOptions> viewOptions,
                    IHttpResponseStreamWriterFactory writerFactory,
                    ICompositeViewEngine viewEngine,
                    ITempDataDictionaryFactory tempDataFactory,
                    DiagnosticSource diagnosticSource,
                    IModelMetadataProvider modelMetadataProvider,
                    ITempDataProvider tempDataProvider)
                    : base(viewOptions, writerFactory, viewEngine, tempDataFactory, diagnosticSource, modelMetadataProvider)
        {
            _ActionContextAccessor = actionContextAccessor;
            _TempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var context = GetActionContext();

            if (context == null) throw new ArgumentNullException(nameof(context));

            var result = new ViewResult()
            {
                ViewData = new ViewDataDictionary(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                {
                    Model = model,
                },
                TempData = new TempDataDictionary(
                        context.HttpContext,
                        _TempDataProvider),
                ViewName = viewName,
            };

            var viewEngineResult = FindView(context, result);
            viewEngineResult.EnsureSuccessful(originalLocations: null);

            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    context,
                    view,
                    new ViewDataDictionary(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        context.HttpContext,
                        _TempDataProvider),
                    output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                return output.ToString();
            }
        }
        private ActionContext GetActionContext()
        {
            return _ActionContextAccessor.ActionContext;

        }

        ViewEngineResult FindView(ActionContext actionContext, ViewResult viewResult)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            if (viewResult == null)
            {
                throw new ArgumentNullException(nameof(viewResult));
            }

            var viewEngine = viewResult.ViewEngine ?? ViewEngine;

            var viewName = viewResult.ViewName ?? GetActionName(actionContext);

            var result = viewEngine.GetView(viewName, viewName, false);
            var originalResult = result;
            if (!result.Success)
            {
                result = viewEngine.FindView(actionContext, viewName, false);
            }

            if (!result.Success)
            {
                if (originalResult.SearchedLocations.Any())
                {
                    if (result.SearchedLocations.Any())
                    {
                        var locations = new List<string>(originalResult.SearchedLocations);
                        locations.AddRange(result.SearchedLocations);
                        result = ViewEngineResult.NotFound(viewName, locations);
                    }
                    else
                    {
                        result = originalResult;
                    }
                }
            }

            if (!result.Success)
                throw new InvalidOperationException(string.Format("Couldn't find view '{0}'", viewName));

            return result;
        }


        private const string ActionNameKey = "action";
        private static string GetActionName(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.RouteData.Values.TryGetValue(ActionNameKey, out var routeValue))
            {
                return null;
            }

            var actionDescriptor = context.ActionDescriptor;
            string normalizedValue = null;
            if (actionDescriptor.RouteValues.TryGetValue(ActionNameKey, out var value) &&
                !string.IsNullOrEmpty(value))
            {
                normalizedValue = value;
            }

            var stringRouteValue = routeValue?.ToString();
            if (string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase))
            {
                return normalizedValue;
            }

            return stringRouteValue;
        }
    }

    public static class FormattingServiceExtensions
    {
        public static void AddViewToStringRendererService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IFormattingService, FormattingService>();
        }
    }
}