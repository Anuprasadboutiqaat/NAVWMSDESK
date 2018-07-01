using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NAVWMSDESK.ViewModel.Movement
{
    public class MovementViewModel
    {
        public string DOCNO { get; set; }
        public string WHNO { get; set; }
        public Nullable<System.DateTime> DOCDATE { get; set; }
        public Nullable<System.DateTime> DATE1{ get; set; }
        public Nullable<System.DateTime> DATE2{ get; set; }
        public int QTY { get; set; }
        public int USERID { get; set; }
        public string  USERNAME { get; set; }
        public string MOVSTATUS { get; set; }
        public string USER { get; set; }
        public string STATUSCNT { get; set; }
    }
}