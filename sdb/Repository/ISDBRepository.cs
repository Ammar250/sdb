using sdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Repository
{
    public interface ISDBRepository
    {
        // Login and user authentication query method
        SdbSystemUsers GetSdbSystemUser(string userEmail, string pasword);
        
        // User Registration query methods
        SdbSystemUsers Add(SdbSystemUsers sdbSystemUsers);
        SdbSystemUsers GetSdbSystemUser(SdbSystemUsers sdbSystemUsers);
        
        // Campaign create,update and delete query methods
        List<SdbCompaigns> GetAllCampaigns();
        List<SdbCompaigns> GetAllCampaignsByNGOId(int ngoID);
        SdbCompaigns AddNewCampaign(SdbCompaigns sdbCompaigns);
        SdbCompaigns UpdateCampaign(SdbCompaigns sdbCompaigns);
        SdbCompaigns DeleteCampaign(int campaignID);
        SdbCompaigns GetCampaignByID(int campaignID);
        SdbSystemUsers GetSdbSystemUsersByID(int userID);
        List<SdbSystemUsers> GetAllSdbSystemUsers();
        SdbSystemUsers AddNewUser(SdbSystemUsers sdbSystemUsers);
        SdbSystemUsers UpdateUser(SdbSystemUsers sdbSystemUsers);
        SdbSystemUsers DeleteUser(int userID);

        List<SdbTransaction> GetAllSdbTransactions();




        // Transaction for record in SDB query methods
        SdbTransaction AddNewTransaction(SdbTransaction sdbTransaction);
        List<SdbTransaction> GetAllTransactionsByUserId(int userId);
    }
}
