using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NAVWMSDESK.ViewModel.Cyclecount
{
    public class CyclecountViewModel
    {
        public DateTime DATE1 { get; set; }
        public Nullable<int> REQNO { get; set; }
        public string ZONEID { get; set; }
        public string ZONEDESC { get; set; }
        public string BINNO { get; set; }
        public string USER { get; set; }
        public IEnumerable<SelectListItem> Options { set; get; }
        public string[] SelectedOptions { set; get; }
        public string USERHOLD { get; set; }

        
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
        public string STATUSCNT { get; set; }

        public Boolean statuscheck { get; set; }

        public string USERNAME { get; set; }
        public string ZONENBIN { get; set; }

        public string WHNO { get; set; }

        public string labelvalue { get; set; }
    }
    public class CYCLECOUNT_CLS
    {

        public string REQUESTID { get; set; }

        public string LOCATION { get; set; }
        
        public List<lstZONEID> lstZONEID { get; set; }
        public List<lstBINID> lstBINID { get; set; }

    }
    public class lstZONEID
    {

        public string ZONEID { get; set; }
        

    }
    public class lstBINID
    {

        public string BINID { get; set; }
        
    }
}