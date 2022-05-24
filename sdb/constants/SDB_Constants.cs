using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.constants
{
    interface SDB_Constants
    {
        public static string CAMPAIGN_START_STATUS = "Start";
        public static string CAMPAIGN_RUNNING_STATUS = "Running";
        public static string CAMPAIGN_STOP_STATUS = "Stop";
        public static string CAMPAIGN_DELIVERED_STATUS = "Delivered";
        public static string CAMPAIGN_CLOSE_STATUS = "Close";
        public static string CAMPAIGN_PURPOSE = "DEFAULT";
        public static string USER_ROLE_NGO = "NGO";
        public static string USER_ROLE_DONOR = "DONOR";
        public static string USER_ROLE_ADMIN = "ADMIN";
    }
}
