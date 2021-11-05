using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Emergent.Code.Test.Helpers;
using Emergent.Code.Test.Models.DTOs;

namespace Emergent.Code.Test.Models.ViewModels
{
    public class SoftwareViewModel
    {
        private string _version;

        [Version]
        [DisplayName("Software Version")]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Software Version field is required, please adjust and then try again.")]
        public string Version { get => _version; set => _version = string.IsNullOrEmpty(value) ? default : value.Trim(); }

        public (int Major, int Minor, int Patch) Normalized
        {
            get
            {
                if (string.IsNullOrEmpty(Version)) return default;

                int[] semantics = Version.Split('.')
                    .Where(x => int.TryParse(x, out int _))
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();

                return (semantics.Length > 0 ? semantics[0] : 0, semantics.Length > 1 ? semantics[1] : 0, semantics.Length > 2 ? semantics[2] : 0);
            }
        }

        public List<SoftwareDTO> Software { get; set; } = new();
    }
}