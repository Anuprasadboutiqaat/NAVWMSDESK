//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NAVWMSDESK.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WMS_MOVE_DETAIL
    {
        public decimal SRNO { get; set; }
        public string DOCNO { get; set; }
        public string FROMBINNO { get; set; }
        public string SUGGBINNO { get; set; }
        public string SUGGZONEID { get; set; }
        public string SCANBINNO { get; set; }
        public string BARCODE { get; set; }
        public string ITEMNO { get; set; }
        public string SERIALNO { get; set; }
        public string ITEMDESC { get; set; }
        public Nullable<int> USERID { get; set; }
        public Nullable<System.DateTime> PICKDATE { get; set; }
        public Nullable<System.DateTime> PUTDATE { get; set; }
        public Nullable<System.DateTime> INTEGRATED { get; set; }
        public string WAREHOUSENO { get; set; }
        public Nullable<int> MOVSTATUS { get; set; }
        public Nullable<bool> ARCHIVE { get; set; }
    }
}
