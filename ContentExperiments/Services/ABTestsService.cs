using ContentExperiments.WebUI.Repositories;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentExperiments.WebUI.Services
{
    public class ABTestsService : IABTestsService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IABTestsRepository abTestsRepository;
        public ABTestsService(IHostingEnvironment hostingEnvironment, IABTestsRepository abTestsRepository)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.abTestsRepository = abTestsRepository;
        }
        public string GetJSForUser(string userId)
        {
            string webRootPath = hostingEnvironment.WebRootPath;

            string result = System.IO.File.ReadAllText(webRootPath + "/JavaScript.js");
            var abtests = abTestsRepository.GetByUserId(userId).ToList();
            result = result.Replace("@abTests", JsonConvert.SerializeObject(abtests));
            return result;
        }
    }
}
