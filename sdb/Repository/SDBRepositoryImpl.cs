using Microsoft.EntityFrameworkCore;
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
                                                Id = campaign.Id,
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
                                            Id = campaign.Id,
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
        public  SdbCompaigns DeleteCampaign(int campaignID)
        {
            SdbCompaigns sdbCampaign = sdbDBContext.SdbCompaigns.Find(campaignID);
            sdbDBContext.SdbCompaigns.Remove(sdbCampaign);
            sdbDBContext.SaveChanges();
            return sdbCampaign;
        }

        public SdbCompaigns UpdateCampaign(SdbCompaigns sdbCampaign)
        {
            sdbDBContext.Entry(sdbCampaign).State = EntityState.Modified;
            sdbDBContext.SaveChanges();
            return sdbCampaign;
        }

        public  SdbCompaigns GetCampaignByID(int campaignID)
        {
            var campaignObj = sdbDBContext.SdbCompaigns.Where(camp => camp.Id == campaignID).FirstOrDefault();
            var systemUsers = sdbDBContext.SdbSystemUsers.Where(user => user.Id == campaignObj.sdbSystemUsersId).SingleOrDefault();
            campaignObj.sdbSystemUsers = systemUsers;
            return campaignObj;
        }

        public SdbTransaction AddNewTransaction(SdbTransaction sdbTransaction)
        {
            try
            {
                sdbDBContext.SdbTransaction.Add(sdbTransaction);
                sdbDBContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                sdbTransaction = null;
            }
            return sdbTransaction;
        }

        public List<SdbTransaction> GetAllTransactionsByUserId(int userId)
        {
            try
            {
                var allCampaignsData = sdbDBContext.SdbCompaigns.ToList<SdbCompaigns>();
                var systemUsersData = sdbDBContext.SdbSystemUsers.ToList<SdbSystemUsers>();
                List<SdbCompaigns> campaignInfoWithUser = new List<SdbCompaigns>();

                campaignInfoWithUser = (from campaign in allCampaignsData
                                        join user in systemUsersData on campaign.sdbSystemUsersId equals user.Id
                                        select new SdbCompaigns
                                        {
                                            Id = campaign.Id,
                                            Name = campaign.Name,
                                            Image = campaign.Image,
                                            Description = campaign.Description,
                                            TotalAmountNeeded = campaign.TotalAmountNeeded,
                                            CollectedAmount = campaign.CollectedAmount,
                                            Status = campaign.Status,
                                            Active = campaign.Active,
                                            CompaignPurpose = campaign.CompaignPurpose,
                                            sdbSystemUsers = user
                                        }).ToList();
                var transactionData = sdbDBContext.SdbTransaction.ToList<SdbTransaction>();
                List<SdbTransaction> transactionInfoWithCampaign = new List<SdbTransaction>();

                transactionInfoWithCampaign = (from campaign in allCampaignsData
                                               join trans in transactionData on campaign.Id equals trans.CompaignId
                                               where trans.DonorId == userId
                                               group trans by trans.CompaignId into g
                                               select new SdbTransaction
                                               {
                                                   Id = g.FirstOrDefault().Id,
                                                   DonationAmount = g.Sum(x=>x.DonationAmount),
                                                   Compaign = g.FirstOrDefault().Compaign
                                               }).ToList();

                return transactionInfoWithCampaign;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
            return null;
        }

        public SdbSystemUsers GetSdbSystemUsersByID(int userID)
        {
            var userObj = sdbDBContext.SdbSystemUsers.Where(camp => camp.Id == userID).FirstOrDefault();
            return userObj;
        }

        
        List<SdbSystemUsers> ISDBRepository.GetAllSdbSystemUsers()
        {
            try
            {
                var allUserData = sdbDBContext.SdbSystemUsers.ToList<SdbSystemUsers>();
                var systemUsersData = sdbDBContext.SdbSystemUsers.ToList<SdbSystemUsers>();
                List<SdbSystemUsers> UserInfo = new List<SdbSystemUsers>();

                UserInfo = (from user in allUserData

                            select new SdbSystemUsers
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Password = user.Password,
                                Address = user.Address,
                                Email = user.Email,
                                Phone = user.Phone,
                                Active = user.Active,
                                UserRole = user.UserRole

                            }
                                       ).ToList();

                return UserInfo;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
            return null;
        }

        public SdbSystemUsers AddNewUser(SdbSystemUsers sdbSystemUsers)
        {
            try
            {
                SdbSystemUsers userAlreadyRegistered = sdbDBContext.SdbSystemUsers.Where(user => user.Email == sdbSystemUsers.Email).FirstOrDefault();
                if (userAlreadyRegistered == null) // Not Found
                {
                    sdbSystemUsers.Active = 1;
                    sdbSystemUsers.CreatedAt = DateTime.Now;
                    sdbSystemUsers.CreatedBy = sdbSystemUsers.Name;
                    sdbSystemUsers.UpdatedAt = DateTime.Now;
                    sdbSystemUsers.UpdatedBy = sdbSystemUsers.Name;
                    sdbDBContext.SdbSystemUsers.Add(sdbSystemUsers);
                    sdbDBContext.SaveChanges();
                }
                else
                {
                    sdbSystemUsers.Id = 0;
                    sdbSystemUsers.Address = "User Already Registered with same email: " + sdbSystemUsers.Email;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                sdbSystemUsers.Id = 0;
                sdbSystemUsers.Address = e.Message.ToString();
            }
            return sdbSystemUsers;
        }

        public SdbSystemUsers UpdateUser(SdbSystemUsers sdbSystemUsers)
        {
            SdbSystemUsers existingUser = GetSdbSystemUsersByID(sdbSystemUsers.Id);
            existingUser.Name = sdbSystemUsers.Name;
            existingUser.Address = sdbSystemUsers.Address;
            existingUser.Email = sdbSystemUsers.Email;
            existingUser.Phone = sdbSystemUsers.Phone;

            sdbDBContext.Entry(existingUser).State = EntityState.Modified;
            sdbDBContext.SaveChanges();
            return existingUser;
        }

        public SdbSystemUsers DeleteUser(int userId)
        {
            SdbSystemUsers sdbSystemUsers = sdbDBContext.SdbSystemUsers.Find(userId); 
            sdbDBContext.SdbSystemUsers.Remove(sdbSystemUsers);
            sdbDBContext.SaveChanges();
            return sdbSystemUsers;
        }

        List<SdbTransaction> ISDBRepository.GetAllSdbTransactions()
        {
            //var allTransactionsData = sdbDBContext.SdbTransaction.ToList<SdbTransaction>();
            //var systemUsersData = sdbDBContext.SdbSystemUsers.ToList<SdbSystemUsers>();
            //List<SdbTransaction> transactionInfoWithUser = new List<SdbTransaction>();

            //transactionInfoWithUser = (from transaction in allTransactionsData
            //                           join user in systemUsersData on transaction.DonorId equals user.Id
            //                        select new SdbTransaction
            //                        {
            //                            Id = transaction.Id,
            //                            DonationAmount = transaction.DonationAmount,
            //                            Active = transaction.Active

            //                        }).ToList();

            //return transactionInfoWithUser;
            try
            {
                var allCampaignsData = sdbDBContext.SdbCompaigns.ToList<SdbCompaigns>();
                var systemUsersData = sdbDBContext.SdbSystemUsers.ToList<SdbSystemUsers>();
                List<SdbCompaigns> campaignInfoWithUser = new List<SdbCompaigns>();

                campaignInfoWithUser = (from campaign in allCampaignsData
                                        join user in systemUsersData on campaign.sdbSystemUsersId equals user.Id
                                        select new SdbCompaigns
                                        {
                                            Id = campaign.Id,
                                            Name = campaign.Name,
                                            Image = campaign.Image,
                                            Description = campaign.Description,
                                            TotalAmountNeeded = campaign.TotalAmountNeeded,
                                            CollectedAmount = campaign.CollectedAmount,
                                            Status = campaign.Status,
                                            Active = campaign.Active,
                                            CompaignPurpose = campaign.CompaignPurpose,
                                            sdbSystemUsers = user
                                        }).ToList();
                var transactionData = sdbDBContext.SdbTransaction.ToList<SdbTransaction>();
                List<SdbTransaction> transactionInfoWithCampaign = new List<SdbTransaction>();

                transactionInfoWithCampaign = (from campaign in allCampaignsData
                                               join trans in transactionData on campaign.Id equals trans.CompaignId
                                               select new SdbTransaction
                                               {
                                                   Id = trans.Id,
                                                   DonationAmount = trans.DonationAmount,
                                                   Compaign = trans.Compaign
                                               }).ToList();

                return transactionInfoWithCampaign;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
            return null;
        }
    }
    
}
