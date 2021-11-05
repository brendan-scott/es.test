using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Emergent.Code.Test.Managers;
using Emergent.Code.Test.Models.ViewModels;

namespace Emergent.Code.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISoftwareManager _softwareManager;

        public HomeController(ILogger<HomeController> logger, ISoftwareManager softwareManager)
        {
            _logger = logger;
            _softwareManager = softwareManager;
        }

        [HttpGet]
        public ViewResult Index() => View(new SoftwareViewModel());

        [HttpPost("{viewModel}")]
        public PartialViewResult Filter([FromBody] SoftwareViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            return ModelState.IsValid ? base.PartialView("_Software", _softwareManager.Filter(viewModel)) : base.PartialView("_Software", viewModel);
        }

        [HttpGet]
        public ViewResult Privacy() => View();

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ViewResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}