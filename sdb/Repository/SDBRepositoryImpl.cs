using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SDBRepository> _logger;


        public SDBRepository(SdbDBContext appDBContext, ILogger<SDBRepository> logger)
        {
            sdbDBContext = appDBContext;
            _logger = logger;
        }

        public SdbSystemUsers Add(SdbSystemUsers sdbSystemUsers)
        {
            try
            {
                // INSERT INTO tableName 
                sdbSystemUsers.Active = 1;
                sdbSystemUsers.CreatedAt = DateTime.Now;
                sdbSystemUsers.CreatedBy = sdbSystemUsers.Name;
                sdbSystemUsers.UpdatedAt = DateTime.Now;
                sdbSystemUsers.UpdatedBy = sdbSystemUsers.Name;
                sdbDBContext.SdbSystemUsers.Add(sdbSystemUsers);
                sdbDBContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                sdbSystemUsers = null;
            }
            return sdbSystemUsers;
        }

        public SdbSystemUsers GetSdbSystemUser(string userEmail, string password)
        {
            try
            {
                // Select * From table where
                SdbSystemUsers sdbSystemUsers = sdbDBContext.SdbSystemUsers.SingleOrDefault(systemUser => systemUser.Email.Equals(userEmail) && systemUser.Password.Equals(password));
                return sdbSystemUsers;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
            return null;
        }
    }
}
