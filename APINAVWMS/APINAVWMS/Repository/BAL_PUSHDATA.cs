using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APINAVWMS.Repository
{
  

        public class BAL_PUSHDATA
        {

            public class DOC_HEADER_CLS
            {

                public string H_DOCNO { get; set; }

                public string H_SUBDOCNO { get; set; }

                public string H_WHNO { get; set; }

                public List<RESPONSE_DETAIL> RESPONSE_LIST { get; set; }
                public List<DOC_DETAIL_CLS> DOC_DETAIL_LIST { get; set; }
            }
            public class DOC_DETAIL_CLS
            {

                public string D_DOCNO { get; set; }
                public string D_SUBDOCNO { get; set; }
                public string D_WHNO { get; set; }
                public string D_SERIALNO { get; set; }
                public string D_ZONEID { get; set; }
                public string D_ITEMNO { get; set; }
                public string D_BARCODE { get; set; }
                public string D_QTY { get; set; }
                public string D_TRANSDATE { get; set; }
                public string D_USERID { get; set; }
                public string D_UOM { get; set; }
                public string D_DESCRIPTION { get; set; }
                public string D_WAREHOUSENO { get; set; }
                public string D_BINID{ get; set; }
                public int D_SUCCESS { get; set; }
                public string D_MESSAGE { get; set; }

            }



            public class PUTAWAY_CLS
                {

                    public string H_DOCNO { get; set; }

                    public string H_PUTNO { get; set; }

                    public string H_WHNO { get; set; }

                    public List<RESPONSE_DETAIL> RESPONSE_LIST { get; set; }
                    public List<PUTAWAY_DETAIL> PUTAWAYDETAIL_LIST { get; set; }
                }
            public class PUTAWAY_DETAIL
                {

                    public string D_DOCNO { get; set; }
                    public string D_PUTNO { get; set; }
                    public string D_WHNO { get; set; }
                    public string D_BINID { get; set; }
                    public string D_SERIALNO { get; set; }
                    public string D_ZONEID { get; set; }
                    public string D_ITEMNO { get; set; }
                    public string D_BARCODE { get; set; }
                    public string D_QTY { get; set; }
                    public string D_TRANSDATE { get; set; }
                    public string D_USERID { get; set; }
                    public string D_UOM { get; set; }
                    public string D_DESCRIPTION { get; set; }
                    public string D_WAREHOUSENO { get; set; }
                    public string D_BINNO { get; set; }
                    public int D_SUCCESS { get; set; }
                    public string D_MESSAGE { get; set; }

                }


            public class PICKING_CLS
            {

                public string H_DOCNO { get; set; }

                public string H_PICKNO { get; set; }

                public string H_WHNO { get; set; }

                public List<RESPONSE_DETAIL> RESPONSE_LIST { get; set; }
                public List<PICKING_DETAIL> PICKINGDETAIL_LIST { get; set; }
            }
            public class PICKING_DETAIL
            {

                public string D_DOCNO { get; set; }
                public string D_PICKNO { get; set; }
                public string D_WHNO { get; set; }
                public string D_BINID { get; set; }
                public string D_SERIALNO { get; set; }
                public string D_ZONEID { get; set; }
                public string D_ITEMNO { get; set; }
                public string D_BARCODE { get; set; }
                public string D_EANCODE { get; set; }
            
                public string D_QTY { get; set; }
                public string D_TRANSDATE { get; set; }
                public string D_USERID { get; set; }
                public string D_UOM { get; set; }
                public string D_DESCRIPTION { get; set; }
                public string D_WAREHOUSENO { get; set; }
                public string D_BINNO { get; set; }
                public int D_SUCCESS { get; set; }
                public string D_MESSAGE { get; set; }

            }

            public class RESPONSE_DETAIL
            {
                public string D_SERIALNO { get; set; }
                public string D_ITEMNO { get; set; }
                public int D_SUCCESS { get; set; }
                public string D_MESSAGE { get; set; }

            }
        public class MOVEMENT_CLS
        {

            public string H_DOCNO { get; set; }
            
            public string H_WHNO { get; set; }

            public List<RESPONSE_DETAIL> RESPONSE_LIST { get; set; }
            public List<MOVEMENT_DETAIL> MOVEMENTDETAIL_LIST { get; set; }
        }
        public class MOVEMENT_DETAIL
        {
            public string D_BARCODE { get; set; }
            public string D_SUBBIN { get; set; }
            public string D_SUBZONE { get; set; }
            public string D_ITEMNO { get; set; }
            public string D_DESCRIPTION { get; set; }
            public string D_SERIALNO { get; set; }
            public string D_TRANSDATE { get; set; }
            public string D_USERID { get; set; }

        }
    }
  
}