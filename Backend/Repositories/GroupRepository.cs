using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repositories
{
    public class GroupRepository
    {
        private readonly DatabaseContext _databaseContext;

        public GroupRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}