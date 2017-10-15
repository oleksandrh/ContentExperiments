using ContentExperiments.WebUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentExperiments.WebUI.Repositories
{
    public class ABTestsRepository : IABTestsRepository
    {
        private ABContext context;
        private DbSet<ABTest> abtestEntity;
        public ABTestsRepository(ABContext context)
        {
            this.context = context;
            abtestEntity = context.Set<ABTest>();
        }
        public ABTest Get(int id)
        {
            return abtestEntity.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ABTest> GetByUserId(string userId)
        {
            return abtestEntity.Where(x => x.User.Id == userId).AsEnumerable();
        }

        public void Save(ABTest abtest)
        {
            context.Entry(abtest).State = EntityState.Added;
            context.SaveChanges();
        }
        public IEnumerable<ABTest> GetAll()
        {
            return abtestEntity.AsEnumerable();
        }

        public void Update(ABTest abtest)
        {
            context.SaveChanges();
        }
    }
}
