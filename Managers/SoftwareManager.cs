using System;
using System.Collections.Generic;
using System.Linq;
using Emergent.Code.Test.Helpers;
using Emergent.Code.Test.Models.DTOs;
using Emergent.Code.Test.Models.ViewModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Emergent.Code.Test.Managers
{
    internal class SoftwareManager : ISoftwareManager
    {
        private readonly ILogger<SoftwareManager> _logger;

        public SoftwareManager(ILogger<SoftwareManager> logger) => _logger = logger;

        public SoftwareViewModel Filter(SoftwareViewModel viewModel)
        {
            try
            {
                viewModel.Software = Software
                    .Where(x => x.Normalize().Compare(viewModel.Normalized) > 0)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"ISoftwareManager => Filter(SoftwareViewModel {JsonConvert.SerializeObject(new { Payload = viewModel.Version})})");
                throw;
            }

            return viewModel;
        }

        private static IEnumerable<SoftwareDTO> Software =>
            new List<SoftwareDTO>
            {
                new()
                {
                    Name = "MS Word",
                    Version = "13.2.1"
                },
                new()
                {
                    Name = "AngularJS",
                    Version = "1.7.1"
                },
                new()
                {
                    Name = "Angular",
                    Version = "8.1.13"
                },
                new()
                {
                    Name = "React",
                    Version = "0.0.5"
                },
                new()
                {
                    Name = "Vue.js",
                    Version = "2.6"
                },
                new()
                {
                    Name = "Visual Studio",
                    Version = "2017.0.1"
                },
                new()
                {
                    Name = "Visual Studio",
                    Version = "2019.1"
                },
                new()
                {
                    Name = "Visual Studio Code",
                    Version = "1.35"
                },
                new()
                {
                    Name = "Blazor",
                    Version = "0.7"
                }
            };
    }

    public interface ISoftwareManager
    {
        SoftwareViewModel Filter(SoftwareViewModel viewModel);
    }
}