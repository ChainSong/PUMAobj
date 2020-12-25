using PUMAobj.Common;
using PUMAobj.Model;
using PUMAobj.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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



        public string ASNReadTXT()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            dir = Path.GetFullPath("..");
            dir= Path.GetFullPath("../..");
            string filepath = dir + "/DownFile/WMSASN_202010090917430000000049490757.txt";     //文件路径

            TxtTable txtTable = new TxtTable();//文件表头标识
            List<InboundASNHDModel> header = new List<InboundASNHDModel>();//asn 主订单信息
            List<InboundASNDTModel> details = new List<InboundASNDTModel>();//asn 订单详细信息
            List<string> thdata = new List<string>();
            if (System.IO.File.Exists(filepath))
            {
                int index = 0;
                foreach (string str in System.IO.File.ReadAllLines(filepath, Encoding.Default))
                {
                    if (index == 0)
                    {
                        txtTable.TableID = str.TxtSubstring(1, 10);
                        txtTable.InterfaceType = str.TxtSubstring(11, 12);
                        txtTable.CreateDateTime = str.TxtSubstring(13, 26);
                        txtTable.ClientID = str.TxtSubstring(27, 46);
                        txtTable.ClientCountry = str.TxtSubstring(47, 51);
                        txtTable.InterfaceName = str.TxtSubstring(52, 81);
                        txtTable.Reserved = str.TxtSubstring(82, 101);
                        index++;
                    }else {
                        if (str.Length < 71)
                        {
                            continue;
                        }
                        ASNDetail detail = new ASNDetail();
                        detail.HeaderFlag = str.TxtSubstring(1, 5);
                        detail.InterfaceActionFlag= str.TxtSubstring(6,6);
                        //detail.ReceiptKey= str.TxtSubstring(7,16);
                        detail.ExternReceiptKey= str.TxtSubstring(17,36);
                        //detail.ReceiptGroup= str.TxtSubstring(37,56);
                        detail.StorerKey= str.TxtSubstring(57,71);
                        //detail.ReceiptDate= str.TxtSubstring(72,85);
                        //detail.POKey= str.TxtSubstring(86,103);
                        //detail.CarrierKey= str.TxtSubstring(104,118);
                        //detail.CarrierName= str.TxtSubstring(119,148);
                        //detail.CarrierAddress1= str.TxtSubstring(149,193);
                        //detail.CarrierAddress2= str.TxtSubstring(194,238);
                        //detail.CarrierCity= str.TxtSubstring(239,283);
                        //detail.CarrierState= str.TxtSubstring(284,285);
                        //detail.CarrierZip= str.TxtSubstring(286,295);
                        //detail.CarrierReference= str.TxtSubstring(296,313);
                        //detail.WarehouseReference= str.TxtSubstring(314,331);
                        //detail.OriginCountry= str.TxtSubstring(332,361);
                        //detail.DestinationCountry= str.TxtSubstring(362,391);
                        //detail.VehicleNumber= str.TxtSubstring(392,409);
                        //detail.VehicleDate= str.TxtSubstring(410,427);
                        //detail.PlaceOfLoading= str.TxtSubstring(428,445);
                        //detail.PlaceOfDischarge= str.TxtSubstring(446,463);
                        //detail.PlaceofDelivery= str.TxtSubstring(464,481);
                        //detail.IncoTerms= str.TxtSubstring(482,491);
                        //detail.TermsNote= str.TxtSubstring(492,509);
                        //detail.ContainerKey= str.TxtSubstring(510,527);
                        //detail.Signatory= str.TxtSubstring(528,545);
                        //detail.PlaceofIssue= str.TxtSubstring(546,563);
                        //detail.OpenQty= str.TxtSubstring(564,573);
                        //detail.Status= str.TxtSubstring(574,583);
                        //detail.Notes= str.TxtSubstring(584,798);
                        //detail.EffectiveDate= str.TxtSubstring(799,812);
                        //detail.ContainerType= str.TxtSubstring(813,832);
                        //detail.ContainerQty= str.TxtSubstring(833,842);
                        //detail.BilledContainerQty= str.TxtSubstring(843,852);
                        //detail.RECType= str.TxtSubstring(853,862);
                        //detail.ASNStatus= str.TxtSubstring(863,872);
                        //detail.ASNReason= str.TxtSubstring(873,882);
                        //detail.Facility= str.TxtSubstring(883,887);
                        //detail.Reserved= str.TxtSubstring(888,897);
                        //detail.MBOLKey= str.TxtSubstring(898,907);
                        //detail.Appointment_No= str.TxtSubstring(908,917);
                        //detail.LoadKey= str.TxtSubstring(918,927);
                        //detail.xDockFlag= str.TxtSubstring(928,928);
                        //detail.UserDefine01= str.TxtSubstring(929,958);
                        //detail.PROCESSTYPE= str.TxtSubstring(959,959);
                        //detail.UserDefine02= str.TxtSubstring(960,989);
                        //detail.UserDefine03= str.TxtSubstring(990,1019);
                        //detail.UserDefine04= str.TxtSubstring(1020,1049);
                        //detail.UserDefine05= str.TxtSubstring(1050,1079);
                        //detail.UserDefine06= str.TxtSubstring(1080,1093);
                        //detail.UserDefine07= str.TxtSubstring(1094,1107);
                        //detail.UserDefine08= str.TxtSubstring(1108,1137);
                        //detail.UserDefine09= str.TxtSubstring(1138,1167);
                        //detail.UserDefine10= str.TxtSubstring(1168,1197);
                        //detail.DOCTYPE= str.TxtSubstring(1198,1198);
                        //detail.RoutingTool= str.TxtSubstring(1199,1228);
                        //detail.CTNTYPE1= str.TxtSubstring(1229,1258);
                        //detail.CTNQTY1= str.TxtSubstring(1259,1268);
                        //detail.CTNTYPE2= str.TxtSubstring(1269,1298);
                        //detail.CTNQTY2= str.TxtSubstring(1299,1308);
                        //detail.CTNTYPE3= str.TxtSubstring(1309,1338);
                        //detail.CTNQTY3= str.TxtSubstring(1339,1348);
                        //detail.CTNTYPE4= str.TxtSubstring(1349,1378);
                        //detail.CTNQTY4= str.TxtSubstring(1379,1388);
                        //detail.CTNTYPE5= str.TxtSubstring(1389,1418);
                        //detail.CTNQTY5= str.TxtSubstring(1419,1428);
                        //detail.CTNTYPE6= str.TxtSubstring(1429,1458);
                        //detail.CTNQTY6= str.TxtSubstring(1459,1468);
                        //detail.CTNTYPE7= str.TxtSubstring(1469,1498);
                        //detail.CTNQTY7= str.TxtSubstring(1499,1508);
                        //detail.CTNTYPE8= str.TxtSubstring(1509,1538);
                        //detail.CTNQTY8= str.TxtSubstring(1539,1548);
                        //detail.CTNTYPE9= str.TxtSubstring(1549,1578);
                        //detail.CTNQTY9= str.TxtSubstring(1579,1588);
                        //detail.CTNTYPE10= str.TxtSubstring(1589,1618);
                        //detail.CTNQTY10= str.TxtSubstring(1619,1628);
                        //detail.NoOfBulkCtn= str.TxtSubstring(1629,1638);
                        //detail.NoOfMiniPacks= str.TxtSubstring(1639,1648);
                        //detail.NoOfPallet= str.TxtSubstring(1649,1658);
                        //detail.Weight= str.TxtSubstring(1659,1668);
                        //detail.WeightUnit= str.TxtSubstring(1669,1688);
                        //detail.Cube= str.TxtSubstring(1689,1698);
                        //detail.CubeUnit= str.TxtSubstring(1699,1718);
                        //details.Add(detail);
                    }
                }
            }
            return "";
        }
    }

    public class ASNDetail {
        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string ReceiptKey { get; set; }
        public string ExternReceiptKey { get; set; }
        public string ReceiptGroup { get; set; }
        public string StorerKey { get; set; }
        public string ReceiptDate { get; set; }
        public string POKey { get; set; }
        public string CarrierKey { get; set; }
        public string CarrierName { get; set; }
        public string CarrierAddress1 { get; set; }
        public string CarrierAddress2 { get; set; }
        public string CarrierCity { get; set; }
        public string CarrierState { get; set; }
        public string CarrierZip { get; set; }
        public string CarrierReference { get; set; }
        public string WarehouseReference { get; set; }
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleDate { get; set; }
        public string PlaceOfLoading { get; set; }
        public string PlaceOfDischarge { get; set; }
        public string PlaceofDelivery { get; set; }
        public string IncoTerms { get; set; }
        public string TermsNote { get; set; }
        public string ContainerKey { get; set; }
        public string Signatory { get; set; }
        public string PlaceofIssue { get; set; }
        public string OpenQty { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string EffectiveDate { get; set; }
        public string ContainerType { get; set; }
        public string ContainerQty { get; set; }
        public string BilledContainerQty { get; set; }
        public string RECType { get; set; }
        public string ASNStatus { get; set; }
        public string ASNReason { get; set; }
        public string Facility { get; set; }
        public string Reserved { get; set; }
        public string MBOLKey { get; set; }
        public string Appointment_No { get; set; }
        public string LoadKey { get; set; }
        public string xDockFlag { get; set; }
        public string UserDefine01 { get; set; }
        public string PROCESSTYPE { get; set; }
        public string UserDefine02 { get; set; }
        public string UserDefine03 { get; set; }
        public string UserDefine04 { get; set; }
        public string UserDefine05 { get; set; }
        public string UserDefine06 { get; set; }
        public string UserDefine07 { get; set; }
        public string UserDefine08 { get; set; }
        public string UserDefine09 { get; set; }
        public string UserDefine10 { get; set; }
        public string DOCTYPE { get; set; }
        public string RoutingTool { get; set; }
        public string CTNTYPE1 { get; set; }
        public string CTNQTY1 { get; set; }
        public string CTNTYPE2 { get; set; }
        public string CTNQTY2 { get; set; }
        public string CTNTYPE3 { get; set; }
        public string CTNQTY3 { get; set; }
        public string CTNTYPE4 { get; set; }
        public string CTNQTY4 { get; set; }
        public string CTNTYPE5 { get; set; }
        public string CTNQTY5 { get; set; }
        public string CTNTYPE6 { get; set; }
        public string CTNQTY6 { get; set; }
        public string CTNTYPE7 { get; set; }
        public string CTNQTY7 { get; set; }
        public string CTNTYPE8 { get; set; }
        public string CTNQTY8 { get; set; }
        public string CTNTYPE9 { get; set; }
        public string CTNQTY9 { get; set; }
        public string CTNTYPE10 { get; set; }
        public string CTNQTY10 { get; set; }
        public string NoOfBulkCtn { get; set; }
        public string NoOfMiniPacks { get; set; }
        public string NoOfPallet { get; set; }
        public string Weight { get; set; }
        public string WeightUnit { get; set; }
        public string Cube { get; set; }
        public string CubeUnit { get; set; }
    }
}
