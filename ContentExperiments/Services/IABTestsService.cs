using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentExperiments.WebUI.Services
{
    public interface IABTestsService
    {
        string GetJSForUser(string userId);
    }
}
