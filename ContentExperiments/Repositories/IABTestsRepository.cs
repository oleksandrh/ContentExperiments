using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentExperiments.WebUI.Repositories
{
    public interface IABTestsRepository
    {
        void Save(ABTest abtest);
        void Update(ABTest abtest);
        ABTest Get(int id);
        IEnumerable<ABTest> GetByUserId(string userId);
        IEnumerable<ABTest> GetAll();
    }
}
