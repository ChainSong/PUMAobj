using PUMAobj.Common;
using PUMAobj.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class ASNAccessor : BaseAccessor
    {
        /// <summary>
        /// Inbound-PURDT
        /// </summary>
        /// <param name="txtlists"></param>
        /// <param name="externumber"></param>
        /// <returns></returns>
        public string InboundPUR(List<string> txtlists, out string externumber)
        {
            List<ASNModel> aSNModels = new List<ASNModel>();
            List<ASNDetailModel> aSNDetailModels = new List<ASNDetailModel>();
            externumber = "";
            if (txtlists[0].TxtSubstring(1, 10) == "WMSPUR" && txtlists[0].TxtSubstring(11, 12) == "I")//PUMA入库单
            {
                int linenumber = 1;
                for (int i = 0; i < txtlists.Count; i++)
                {
                    if (txtlists[i].TxtSubstring(1, 10) == "PURHD")//文档头
                    {
                        continue;
                    }
                    if (txtlists[i].TxtSubstring(1, 5) == "WMSPUR" && txtlists[i].TxtSubstring(6, 6) == "A")//订单头添加订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        aSNModels.Add(new ASNModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            POKey = txtlists[i].TxtSubstring(7, 24),
                            ExternPOKey = txtlists[i].TxtSubstring(25, 44),
                            PoGroup = txtlists[i].TxtSubstring(45, 64),
                            StorerKey = txtlists[i].TxtSubstring(65, 79),
                            PODate = txtlists[i].TxtSubstring(80, 93),
                            SellersReference = txtlists[i].TxtSubstring(94, 111),
                            BuyersReference = txtlists[i].TxtSubstring(112, 129),
                            OtherReference = txtlists[i].TxtSubstring(130, 147),
                            POType = txtlists[i].TxtSubstring(148, 157),
                            SellerName = txtlists[i].TxtSubstring(158, 202),
                            SellerAddress1 = txtlists[i].TxtSubstring(203, 247),
                            SellerAddress2 = txtlists[i].TxtSubstring(248, 292),
                            SellerAddress3 = txtlists[i].TxtSubstring(293, 337),
                            SellerAddress4 = txtlists[i].TxtSubstring(338, 382),
                            SellerCity = txtlists[i].TxtSubstring(383, 427),
                            SellerState = txtlists[i].TxtSubstring(428, 429),
                            SellerZip = txtlists[i].TxtSubstring(430, 447),
                            SellerPhone = txtlists[i].TxtSubstring(448, 465),
                        });
                    }

                    if (txtlists[i].TxtSubstring(1, 5) == "WMSPUR" && txtlists[i].TxtSubstring(6, 6) == "C")//订单头修改订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        aSNModels.Add(new ASNModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            POKey = txtlists[i].TxtSubstring(7, 24),
                            ExternPOKey = txtlists[i].TxtSubstring(25, 44),
                            PoGroup = txtlists[i].TxtSubstring(45, 64),
                            StorerKey = txtlists[i].TxtSubstring(65, 79),
                            PODate = txtlists[i].TxtSubstring(80, 93),
                            SellersReference = txtlists[i].TxtSubstring(94, 111),
                            BuyersReference = txtlists[i].TxtSubstring(112, 129),
                            OtherReference = txtlists[i].TxtSubstring(130, 147),
                            POType = txtlists[i].TxtSubstring(148, 157),
                            SellerName = txtlists[i].TxtSubstring(158, 202),
                            SellerAddress1 = txtlists[i].TxtSubstring(203, 247),
                            SellerAddress2 = txtlists[i].TxtSubstring(248, 292),
                            SellerAddress3 = txtlists[i].TxtSubstring(293, 337),
                            SellerAddress4 = txtlists[i].TxtSubstring(338, 382),
                            SellerCity = txtlists[i].TxtSubstring(383, 427),
                            SellerState = txtlists[i].TxtSubstring(428, 429),
                            SellerZip = txtlists[i].TxtSubstring(430, 447),
                            SellerPhone = txtlists[i].TxtSubstring(448, 465),
                        });
                    }


                    if (txtlists[i].TxtSubstring(1, 5) == "PURDT")//订单头修改订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        aSNDetailModels.Add(new ASNDetailModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            POKey = txtlists[i].TxtSubstring(7, 24),
                            POLineNumber = txtlists[i].TxtSubstring(25, 29),
                            StorerKey = txtlists[i].TxtSubstring(30, 44),
                            PODetailKey = txtlists[i].TxtSubstring(45, 54),
                            ExternPOKey = txtlists[i].TxtSubstring(55, 74),
                            ExternLineNo = txtlists[i].TxtSubstring(75, 94),
                            MarksContainer = txtlists[i].TxtSubstring(95, 112),
                            Sku = txtlists[i].TxtSubstring(113, 132),
                            SKUDescription = txtlists[i].TxtSubstring(133, 192),
                            ManufacturerSku = txtlists[i].TxtSubstring(193, 212),
                            RetailSku = txtlists[i].TxtSubstring(213, 232),
                            AltSku = txtlists[i].TxtSubstring(233, 252),
                            QtyOrdered = txtlists[i].TxtSubstring(253, 262),
                            QtyAdjusted = txtlists[i].TxtSubstring(263, 272),
                            QtyReceived = txtlists[i].TxtSubstring(273, 282),
                            PackKey = txtlists[i].TxtSubstring(283, 292),
                            UnitPrice = txtlists[i].TxtSubstring(293, 308),
                            UOM = txtlists[i].TxtSubstring(309, 318),
                        });
                    }

                }
            }
            //订单获取结束之后，开始准备插入数据库
            if (aSNModels.Count > 0)
            {
                //筛选已经插入数据库的SKU信息
                StringBuilder sbCheck = new StringBuilder();
                sbCheck.Append(" select ExternReceiptNumber from WMS_ASN where ExternReceiptNumber in (");
                sbCheck.Append(string.Join(",", aSNModels.Select(a => "'" + a.ExternPOKey + "'")));
                var sbCheckData = this.ScanDataTable(sbCheck.ToString());
                foreach (DataRow item in sbCheckData.Rows)
                {
                    if (aSNModels.Where(a => a.ExternPOKey == item["ExternReceiptNumber"].ToString()).Count() > 0)
                    {
                        aSNModels.RemoveAll(a => a.ExternPOKey == item["ExternReceiptNumber"].ToString());
                    }
                }

                if (aSNModels.Count > 0)
                {
                    StringBuilder sbBack = new StringBuilder();
                    StringBuilder sbProduct = new StringBuilder();
                    sbBack.Append(" insert into ");

                }
            }



            //PreOrderDetail

            return "";
        }




        /// <summary>
        /// Inbound-ASNHD
        /// </summary>
        /// <param name="txtlists"></param>
        /// <param name="externumber"></param>
        /// <returns></returns>
        public string InboundASN(List<string> txtlists, out string externumber)
        {
            List<InboundASNHDModel> inboundASNHDs = new List<InboundASNHDModel>();
            List<InboundASNDTModel> inboundASNDTs = new List<InboundASNDTModel>();
            externumber = "";
            if (txtlists[0].TxtSubstring(1, 10) == "WMSASN" && txtlists[0].TxtSubstring(11, 12) == "I")//PUMA入库单
            {
                int linenumber = 1;
                for (int i = 0; i < txtlists.Count; i++)
                {
                    if (txtlists[i].TxtSubstring(1, 10) == "ASNHD")//文档头
                    {
                        continue;
                    }
                    if (txtlists[i].TxtSubstring(1, 5) == "ASNHD" && txtlists[i].TxtSubstring(6, 6) == "A")//订单头添加订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        inboundASNHDs.Add(new InboundASNHDModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            ReceiptKey = txtlists[i].TxtSubstring(7, 16),
                            ExternReceiptKey = txtlists[i].TxtSubstring(17, 36),
                            ReceiptGroup = txtlists[i].TxtSubstring(37, 56),
                            StorerKey = txtlists[i].TxtSubstring(57, 71),
                            ReceiptDate = txtlists[i].TxtSubstring(72, 85),
                            POKey = txtlists[i].TxtSubstring(86, 103),
                            CarrierKey = txtlists[i].TxtSubstring(104, 118),
                            CarrierName = txtlists[i].TxtSubstring(119, 148),
                            CarrierAddress1 = txtlists[i].TxtSubstring(149, 193),
                            CarrierAddress2 = txtlists[i].TxtSubstring(194, 238),
                            CarrierCity = txtlists[i].TxtSubstring(239, 283),
                            CarrierState = txtlists[i].TxtSubstring(284, 285),
                            CarrierZip = txtlists[i].TxtSubstring(286, 295),
                            CarrierReference = txtlists[i].TxtSubstring(296, 313),
                            WarehouseReference = txtlists[i].TxtSubstring(314, 331),
                            OriginCountry = txtlists[i].TxtSubstring(332, 361),
                            DestinationCountry = txtlists[i].TxtSubstring(362, 391),
                            VehicleNumber = txtlists[i].TxtSubstring(392, 409)
                        });
                    }

                    if (txtlists[i].TxtSubstring(1, 5) == "ASNHD" && txtlists[i].TxtSubstring(6, 6) == "C")//订单头修改订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        inboundASNHDs.Add(new InboundASNHDModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            ReceiptKey = txtlists[i].TxtSubstring(7, 16),
                            ExternReceiptKey = txtlists[i].TxtSubstring(17, 36),
                            ReceiptGroup = txtlists[i].TxtSubstring(37, 56),
                            StorerKey = txtlists[i].TxtSubstring(57, 71),
                            ReceiptDate = txtlists[i].TxtSubstring(72, 85),
                            POKey = txtlists[i].TxtSubstring(86, 103),
                            CarrierKey = txtlists[i].TxtSubstring(104, 118),
                            CarrierName = txtlists[i].TxtSubstring(119, 148),
                            CarrierAddress1 = txtlists[i].TxtSubstring(149, 193),
                            CarrierAddress2 = txtlists[i].TxtSubstring(194, 238),
                            CarrierCity = txtlists[i].TxtSubstring(239, 283),
                            CarrierState = txtlists[i].TxtSubstring(284, 285),
                            CarrierZip = txtlists[i].TxtSubstring(286, 295),
                            CarrierReference = txtlists[i].TxtSubstring(296, 313),
                            WarehouseReference = txtlists[i].TxtSubstring(314, 331),
                            OriginCountry = txtlists[i].TxtSubstring(332, 361),
                            DestinationCountry = txtlists[i].TxtSubstring(362, 391),
                            VehicleNumber = txtlists[i].TxtSubstring(392, 409)
                        });
                    }


                    if (txtlists[i].TxtSubstring(1, 5) == "ASNDT")//订单头修改订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        inboundASNDTs.Add(new InboundASNDTModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            ReceiptKey = txtlists[i].TxtSubstring(7, 16),
                            ReceiptLineNumber = txtlists[i].TxtSubstring(17, 21),
                            ExternReceiptKey = txtlists[i].TxtSubstring(22, 41),
                            ExternLineNo = txtlists[i].TxtSubstring(42, 61),
                            StorerKey = txtlists[i].TxtSubstring(62, 76),
                            POKey = txtlists[i].TxtSubstring(77, 94),
                            Sku = txtlists[i].TxtSubstring(95, 114),
                            AltSku = txtlists[i].TxtSubstring(115, 134),
                            Id = txtlists[i].TxtSubstring(135, 152),
                            Status = txtlists[i].TxtSubstring(153, 162),
                            DateReceived = txtlists[i].TxtSubstring(163, 176),
                            QtyExpected = txtlists[i].TxtSubstring(177, 186),
                            QtyAdjusted = txtlists[i].TxtSubstring(187, 196),
                            QtyReceived = txtlists[i].TxtSubstring(197, 206),
                            UOM = txtlists[i].TxtSubstring(207, 216),
                            PackKey = txtlists[i].TxtSubstring(217, 226),
                            VesselKey = txtlists[i].TxtSubstring(227, 244),
                            VoyageKey = txtlists[i].TxtSubstring(245, 262)
                        });
                    }

                }
            }
            //订单获取结束之后，开始准备插入数据库
            if (inboundASNHDs.Count > 0)
            {
                //筛选已经插入数据库的SKU信息
                //StringBuilder sbCheck = new StringBuilder();
                //sbCheck.Append(" select ExternReceiptNumber from WMS_ASN where ExternReceiptNumber in (");
                //sbCheck.Append(string.Join(",", inboundASNHDs.Select(a => "'" + a.ExternPOKey + "'")));
                //var sbCheckData = this.ScanDataTable(sbCheck.ToString());
                //foreach (DataRow item in sbCheckData.Rows)
                //{
                //    if (inboundASNHDs.Where(a => a.ExternPOKey == item["ExternReceiptNumber"].ToString()).Count() > 0)
                //    {
                //        inboundASNHDs.RemoveAll(a => a.ExternPOKey == item["ExternReceiptNumber"].ToString());
                //    }
                //}

                //if (inboundASNHDs.Count > 0)
                //{
                //    StringBuilder sbBack = new StringBuilder();
                //    StringBuilder sbProduct = new StringBuilder();
                //    sbBack.Append(" insert into ");

                //}
            }



            //PreOrderDetail

            return "";
        }



        /// <summary>
        /// Inbound-ORDHD
        /// </summary>
        /// <param name="txtlists"></param>
        /// <param name="externumber"></param>
        /// <returns></returns>
        public string InboundORDHD(List<string> txtlists, out string externumber)
        {
            List<InboundORDHDModel> inboundORDHDs = new List<InboundORDHDModel>();
            List<InboundORDDTModel> inboundORDDTs = new List<InboundORDDTModel>();
            externumber = "";
            if (txtlists[0].TxtSubstring(1, 10) == "WMSORD" && txtlists[0].TxtSubstring(11, 12) == "I")//PUMA入库单
            {
                int linenumber = 1;
                for (int i = 0; i < txtlists.Count; i++)
                {
                    if (txtlists[i].TxtSubstring(1, 10) == "WMSORD")//文档头
                    {
                        continue;
                    }
                    if (txtlists[i].TxtSubstring(1, 5) == "ORDHD" && txtlists[i].TxtSubstring(6, 6) == "A")//订单头添加订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        inboundORDHDs.Add(new InboundORDHDModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            OrderKey = txtlists[i].TxtSubstring(7, 16),
                            StorerKey = txtlists[i].TxtSubstring(17, 31),
                            ExternOrderKey = txtlists[i].TxtSubstring(32, 51),
                            Reserved = txtlists[i].TxtSubstring(52, 61),
                            OrderDate = txtlists[i].TxtSubstring(62, 75),
                            DeliveryDate = txtlists[i].TxtSubstring(76, 89),
                            Priority = txtlists[i].TxtSubstring(90, 99),
                            ConsigneeKey = txtlists[i].TxtSubstring(100, 114),
                            C_contact1 = txtlists[i].TxtSubstring(115, 144),
                            C_Contact2 = txtlists[i].TxtSubstring(145, 174),
                            C_Company = txtlists[i].TxtSubstring(175, 219),
                            C_Address1 = txtlists[i].TxtSubstring(220, 264),
                            C_Address2 = txtlists[i].TxtSubstring(265, 309),
                            C_Address3 = txtlists[i].TxtSubstring(310, 354),
                            C_Address4 = txtlists[i].TxtSubstring(355, 399),
                            C_City = txtlists[i].TxtSubstring(400, 444),
                            C_State = txtlists[i].TxtSubstring(445, 446),
                            C_Zip = txtlists[i].TxtSubstring(447, 464)

                        });
                    }

                    if (txtlists[i].TxtSubstring(1, 5) == "ORDHD" && txtlists[i].TxtSubstring(6, 6) == "C")//订单头修改订单
                    {
                        //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                        //if (string.IsNullOrEmpty(loadkey))
                        //{
                        //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                        //}
                        inboundORDHDs.Add(new InboundORDHDModel
                        {
                            HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                            InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                            OrderKey = txtlists[i].TxtSubstring(7, 16),
                            StorerKey = txtlists[i].TxtSubstring(17, 31),
                            ExternOrderKey = txtlists[i].TxtSubstring(32, 51),
                            Reserved = txtlists[i].TxtSubstring(52, 61),
                            OrderDate = txtlists[i].TxtSubstring(62, 75),
                            DeliveryDate = txtlists[i].TxtSubstring(76, 89),
                            Priority = txtlists[i].TxtSubstring(90, 99),
                            ConsigneeKey = txtlists[i].TxtSubstring(100, 114),
                            C_contact1 = txtlists[i].TxtSubstring(115, 144),
                            C_Contact2 = txtlists[i].TxtSubstring(145, 174),
                            C_Company = txtlists[i].TxtSubstring(175, 219),
                            C_Address1 = txtlists[i].TxtSubstring(220, 264),
                            C_Address2 = txtlists[i].TxtSubstring(265, 309),
                            C_Address3 = txtlists[i].TxtSubstring(310, 354),
                            C_Address4 = txtlists[i].TxtSubstring(355, 399),
                            C_City = txtlists[i].TxtSubstring(400, 444),
                            C_State = txtlists[i].TxtSubstring(445, 446),
                            C_Zip = txtlists[i].TxtSubstring(447, 464)
                        });
                    }
                }
            }
            //订单获取结束之后，开始准备插入数据库
            if (inboundORDHDs.Count > 0)
            {
                //筛选已经插入数据库的SKU信息
                StringBuilder sbCheck = new StringBuilder();
                sbCheck.Append(" select ExternReceiptNumber from WMS_ASN where ExternReceiptNumber in (");
                sbCheck.Append(string.Join(",", inboundORDHDs.Select(a => "'" + a.OrderKey + "'")));
                var sbCheckData = this.ScanDataTable(sbCheck.ToString());
                foreach (DataRow item in sbCheckData.Rows)
                {
                    if (inboundORDHDs.Where(a => a.OrderKey == item["ExternReceiptNumber"].ToString()).Count() > 0)
                    {
                        inboundORDHDs.RemoveAll(a => a.OrderKey == item["ExternReceiptNumber"].ToString());
                    }
                }

                if (inboundORDHDs.Count > 0)
                {
                    StringBuilder sbBack = new StringBuilder();
                    StringBuilder sbProduct = new StringBuilder();
                    sbBack.Append(" insert into ");

                }
            } 

            //PreOrderDetail

            return "";
        }





    }
}
