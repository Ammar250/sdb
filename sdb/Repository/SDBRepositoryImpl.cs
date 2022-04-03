using sdb.Data;
using sdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Repository
{
    public class SDBRepository : ISDBRepository
    {
        private readonly SdbDBContext sdbDBContext;

        public SDBRepository(SdbDBContext appDBContext)
        {
            sdbDBContext = appDBContext;
        }

        public SdbSystemUsers Add(SdbSystemUsers sdbSystemUsers)
        {
            // INSERT INTO tableName 
            sdbDBContext.SdbSystemUsers.Add(sdbSystemUsers);
            sdbDBContext.SaveChanges();
            return sdbSystemUsers;
        }

        public SdbSystemUsers GetSdbSystemUser(string userEmail, string password)
        {
            // Select * From table where
           SdbSystemUsers sdbSystemUsers = sdbDBContext.SdbSystemUsers.SingleOrDefault(systemUser => systemUser.Email.Equals(userEmail) && systemUser.Password.Equals(password));
            return sdbSystemUsers;
        }
    }
}
