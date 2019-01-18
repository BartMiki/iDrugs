using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace WebApp.Controllers
{
    public class LoggerController : BaseController
    {
        private readonly ILogger<LoggerController> _logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Errors()
        {
            DisplayErrorFromRedirectIfNecessary();

            var errors = Try(() => 
            {
                return _logger.ErrorLogs();
            }, typeof(LoggerController));

            if (errors.IsSuccess) return View(errors.Value);

            AddLocalError(errors.FailureMessage);
            return View(Enumerable.Empty<ErrorLogModel>());
        }

        public IActionResult Infos()
        {
            DisplayErrorFromRedirectIfNecessary();

            var infos = Try(() =>
            {
                return _logger.InfoLogs();
            }, typeof(LoggerController));

            if (infos.IsSuccess) return View(infos.Value);

            AddLocalError(infos.FailureMessage);
            return View(Enumerable.Empty<BaseLogModel>());
        }

        public IActionResult ErrorDetails(string objectId)
        {
            var info = Try(() =>
            {
                return _logger.GetErrorLog(objectId);
            }, typeof(LoggerController));

            if (info.IsSuccess) return View(info.Value);

            AddErrorForRedirect(info.FailureMessage);
            return RedirectToAction(nameof(Errors));
        }

        public IActionResult InfoDetails(string objectId)
        {
            var info = Try(() =>
            {
                return _logger.GetInfoLog(objectId);
            }, typeof(LoggerController));

            if (info.IsSuccess) return View(info.Value);

            AddErrorForRedirect(info.FailureMessage);
            return RedirectToAction(nameof(Infos));
        }

        public IActionResult ClearAllInfo()
        {
            _logger.ClearAllInfoLogs();

            return RedirectToAction(nameof(Infos));
        }

        public IActionResult ClearAllError()
        {
            _logger.ClearAllErrorLogs();

            return RedirectToAction(nameof(Errors));
        }

        public IActionResult DeleteError(string objectId)
        {
            _logger.RemoveErrorLog(objectId);

            return RedirectToAction(nameof(Errors));
        }

        public IActionResult DeleteInfo(string objectId)
        {
            _logger.RemoveInfoLog(objectId);

            return RedirectToAction(nameof(Infos));
        }
    }
}