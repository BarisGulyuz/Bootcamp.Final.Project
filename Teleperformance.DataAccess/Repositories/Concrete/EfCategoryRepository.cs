using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.DataAccess.Repository;
using Teleperformance.DataAccess.Contexts;
using Teleperformance.DataAccess.Repositories.Abstract;
using Teleperformance.Entities.Concrete;

namespace Teleperformance.DataAccess.Repositories.Concrete
{
    public class EfCategoryRepository : BaseEntityRepository<Category>, ICategoryRepository
    {
        public EfCategoryRepository(TeleperformanceDbContext context) : base(context)
        {

        }
    }
}
