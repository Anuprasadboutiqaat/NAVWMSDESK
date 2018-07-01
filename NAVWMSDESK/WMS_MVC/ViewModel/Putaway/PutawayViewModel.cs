using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NAVWMSDESK.ViewModel.Putaway
{
    public class PutawayViewModel
    {
        public DateTime DATE1 { get; set; }

        public DateTime DATE2 { get; set; }
        public Nullable<int> STATUS { get; set; }      
        public Nullable<bool> ASSIGNSTATUS { get; set; }
        public Nullable<int> PUTSTATUS { get; set; }
        public Nullable<int> USERID { get; set; }
        public Nullable<int> STATUS1 { get; set; }
        //public string PUTSTATUS { get; set; }
        public string DOCNO { get; set; }
        public string PUTNO { get; set; }
        public Nullable<System.DateTime> USERASSIGNDATE { get; set; }
        public Nullable<System.DateTime> TRANSDATE { get; set; }
        public Nullable<System.DateTime> RECINSERTED { get; set; }
        public string USER { get; set; }
        public string ZONEID { get; set; }
        public string ZONEDESC { get; set; }
        public string USERNAME { get; set; }
        public string STATUSCNT { get; set; }
        public string WHNO { get; set; }
        
    }
    
}