using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Models
{
    public class CompaignModel
    {
        public int Compaign_Id { get; set; }
        public String Compaign_Name { get; set; }
        public String Compaign_image { get; set; }
        public int user_ID { get; set; }
        public String Compaign_Description { get; set; }
        public int Total_amount_needed { get; set; }
        public int Collected_amount { get; set; }
        public String status { get; set; }

    }
}
