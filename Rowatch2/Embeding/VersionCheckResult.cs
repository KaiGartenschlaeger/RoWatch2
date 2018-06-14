using System;

namespace Rowatch2.Embeding
{
    internal class VersionCheckResult
    {
        public Version CurrentVersion { get; set; }
        public bool Succeeded { get; set; }
    }
}