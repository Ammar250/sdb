using sdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Repository
{
    public interface ISDBRepository
    {
        SdbSystemUsers GetSdbSystemUser(string userEmail, string pasword);
        SdbSystemUsers Add(SdbSystemUsers sdbSystemUsers);
        SdbSystemUsers GetSdbSystemUser(SdbSystemUsers sdbSystemUsers);
        List<SdbCompaigns> GetAllCampaigns();
        List<SdbCompaigns> GetAllCampaignsByNGOId(int ngoID);
        SdbCompaigns AddNewCampaign(SdbCompaigns sdbCompaigns);
        SdbCompaigns UpdateCampaign(SdbCompaigns sdbCompaigns);
        SdbCompaigns DeleteCampaign(int campaignID);
        SdbCompaigns GetCampaignByID(int campaignID);

    }
}
