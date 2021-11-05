using System;
using System.Linq;
using Emergent.Code.Test.Models.DTOs;

namespace Emergent.Code.Test.Helpers
{
    public static class Extensions
    {
        public static (int Major, int Minor, int Patch) Normalize(this SoftwareDTO softwareDTO)
        {
            if (string.IsNullOrEmpty(softwareDTO.Version)) return default;

            int[] semantics = softwareDTO.Version.Split('.')
                .Where(x => int.TryParse(x, out int _))
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            return (semantics.Length > 0 ? semantics[0] : 0, semantics.Length > 1 ? semantics[1] : 0, semantics.Length > 2 ? semantics[2] : 0);
        }

        public static int Compare(this (int Major, int Minor, int Patch) version, (int Major, int Minor, int Patch) otherVersion)
        {
            if (version.Major < 0 || version.Minor < 0 || version.Patch < 0) 
                throw new ArgumentNullException(nameof(version));
            
            if (otherVersion.Major < 0 || otherVersion.Minor < 0 || otherVersion.Patch < 0)
                throw new ArgumentNullException(nameof(otherVersion));

            if (version.Major != otherVersion.Major)
                return version.Major > otherVersion.Major ? 1 : -1;

            if (version.Minor != otherVersion.Minor)
                return version.Minor > otherVersion.Minor ? 1 : -1;

            return version.Patch != otherVersion.Patch ? version.Patch > otherVersion.Patch ? 1 : -1 : 0;
        }
    }
}