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
        public SdbSystemUsers GetSdbSystemUser(SdbSystemUsers sdbSystemUsers)
        {
            try
            {
                // Select * From table where
                
                return sdbSystemUsers;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
            return null;
        }

        List<SdbCompaigns> ISDBRepository.GetAllCampaigns()
        {
            try
            {
                var allCampaignsData = sdbDBContext.SdbCompaigns.ToList<SdbCompaigns>();
                var systemUsersData = sdbDBContext.SdbSystemUsers.ToList<SdbSystemUsers>();
                List<SdbCompaigns> campaignInfoWithUser = new List<SdbCompaigns>();

                campaignInfoWithUser = (from campaign in allCampaignsData
                                            join user in systemUsersData
                                            on campaign.sdbSystemUsersId equals user.Id
                                            select new SdbCompaigns
                                            {
                                                Name = campaign.Name,
                                                Image = campaign.Image,
                                                Description = campaign.Description,
                                                TotalAmountNeeded = campaign.TotalAmountNeeded,
                                                CollectedAmount = campaign.CollectedAmount,
                                                Status = campaign.Status,
                                                Active = campaign.Active,
                                                CompaignPurpose = campaign.CompaignPurpose,
                                                sdbSystemUsers = user
                                            }
                                         ).ToList();

                return campaignInfoWithUser;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
            return null;
        }

        List<SdbCompaigns> ISDBRepository.GetAllCampaignsByNGOId(int ngoID)
        {
            try
            {
                var allCampaignsData = sdbDBContext.SdbCompaigns.ToList<SdbCompaigns>();
                var systemUsers = sdbDBContext.SdbSystemUsers.Where(user => user.Id == ngoID).SingleOrDefault();
                List<SdbCompaigns> campaignInfoWithUser = new List<SdbCompaigns>();

                campaignInfoWithUser = (from campaign in allCampaignsData
                                        where campaign.sdbSystemUsersId == systemUsers.Id
                                        select new SdbCompaigns
                                        {
                                            Name = campaign.Name,
                                            Image = campaign.Image,
                                            Description = campaign.Description,
                                            TotalAmountNeeded = campaign.TotalAmountNeeded,
                                            CollectedAmount = campaign.CollectedAmount,
                                            Status = campaign.Status,
                                            Active = campaign.Active,
                                            CompaignPurpose = campaign.CompaignPurpose,
                                            sdbSystemUsers = systemUsers
                                        }).ToList();

                return campaignInfoWithUser;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
            return null;
        }

        public SdbCompaigns AddNewCampaign(SdbCompaigns sdbCompaigns)
        {
            try
            {
                sdbDBContext.SdbCompaigns.Add(sdbCompaigns);
                sdbDBContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                sdbCompaigns = null;
            }
            return sdbCompaigns;
        }
    }
}
