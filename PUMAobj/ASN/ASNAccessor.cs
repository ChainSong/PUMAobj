using PUMAobj.Common;
using PUMAobj.Log;
using PUMAobj.Model;
using PUMAobj.Model.WMS;
using PUMAobj.SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class ASNAccessor : BaseAccessor
    {
        public string putpath = "";
        public readonly static string sftpip = GetConfigValue("sftpip");//ip
        public readonly static string sftpport = GetConfigValue("sftpport");//端口
        public readonly static string sftpuser = GetConfigValue("sftpuser");//用户名
        public readonly static string sftppwd = GetConfigValue("sftppwd");//密码
        public readonly static string sftpfilepath = GetConfigValue("sftpfilepath");//lf接收文件地址

        public static string GetConfigValue(string key)
        {
            return string.IsNullOrEmpty(key) ? string.Empty : ConfigurationManager.AppSettings[key].ToString();
        }

        SFTPHelper sFTP = new SFTPHelper(sftpip, sftpport, sftpuser, sftppwd);
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


        /// <summary>
        /// 预入库ASN  来自于门店退货  经销商退货
        /// </summary>
        /// <returns></returns>
        public string GetInbound_ASNHD(List<string> thdata, out string externumber)
        {
            string msg = string.Empty;
            string Error = "";
            externumber = "";
            try
            {
                ////读取文件
                //List<string> thdata = new List<string>();
                //string dir = AppDomain.CurrentDomain.BaseDirectory;
                //dir = Path.GetFullPath("..");
                //dir = Path.GetFullPath("../..");
                //string filepath = dir + "/DownFile/WMSASN_202010090917430000000049490757.txt";     //文件路径
                //if (System.IO.File.Exists(filepath))
                //{
                //    foreach (string str in System.IO.File.ReadAllLines(filepath, Encoding.Default))
                //    {
                //        thdata.Add(str);
                //    }
                //}

                //实体赋值
                List<Inbound_ASNHD> header = new List<Inbound_ASNHD>();//asn 主订单信息
                List<Inbound_ASNDT> details = new List<Inbound_ASNDT>();//asn 订单详细信息
                AddASNandASNDetailRequest request = new AddASNandASNDetailRequest();
                for (int i = 0; i < thdata.Count(); i++)
                {
                    if (thdata[0].TxtSubstring(1, 10) == "WMSASN")//判断txt表头类型
                    {
                        if (thdata[i].TxtSubstring(1, 5) == "ASNHD")//取出主订单信息
                        {
                            header.Add(new Inbound_ASNHD
                            {
                                HeaderFlag = thdata[i].TxtSubstring(1, 5),
                                InterfaceActionFlag = thdata[i].TxtSubstring(6, 6),
                                ReceiptKey = thdata[i].TxtSubstring(7, 16),
                                ExternReceiptKey = thdata[i].TxtSubstring(17, 36),
                                ReceiptGroup = thdata[i].TxtSubstring(37, 56),
                                StorerKey = thdata[i].TxtSubstring(57, 71),
                                ReceiptDate = thdata[i].TxtSubstring(72, 85).TxtConvertTime(),
                                POKey = thdata[i].TxtSubstring(86, 103),
                                CarrierKey = thdata[i].TxtSubstring(104, 118),
                                CarrierName = thdata[i].TxtSubstring(119, 148),
                                CarrierAddress1 = thdata[i].TxtSubstring(149, 193),
                                CarrierAddress2 = thdata[i].TxtSubstring(194, 238),
                                CarrierCity = thdata[i].TxtSubstring(239, 283),
                                CarrierState = thdata[i].TxtSubstring(284, 285),
                                CarrierZip = thdata[i].TxtSubstring(286, 295),
                                CarrierReference = thdata[i].TxtSubstring(296, 313),
                                WarehouseReference = thdata[i].TxtSubstring(314, 331),
                                OriginCountry = thdata[i].TxtSubstring(332, 361),
                                DestinationCountry = thdata[i].TxtSubstring(362, 391),
                                VehicleNumber = thdata[i].TxtSubstring(392, 409),
                                VehicleDate = thdata[i].TxtSubstring(410, 427),
                                PlaceOfLoading = thdata[i].TxtSubstring(428, 445),
                                PlaceOfDischarge = thdata[i].TxtSubstring(446, 463),
                                PlaceofDelivery = thdata[i].TxtSubstring(464, 481),
                                IncoTerms = thdata[i].TxtSubstring(482, 491),
                                TermsNote = thdata[i].TxtSubstring(492, 509),
                                ContainerKey = thdata[i].TxtSubstring(510, 527),
                                Signatory = thdata[i].TxtSubstring(528, 545),
                                PlaceofIssue = thdata[i].TxtSubstring(546, 563),
                                OpenQty = thdata[i].TxtSubstring(564, 573).TxtConvertInt(),
                                Status = thdata[i].TxtSubstring(574, 583),
                                Notes = thdata[i].TxtSubstring(584, 798),
                                EffectiveDate = thdata[i].TxtSubstring(799, 812),
                                ContainerType = thdata[i].TxtSubstring(813, 832),
                                ContainerQty = thdata[i].TxtSubstring(833, 842).TxtConvertInt(),
                                BilledContainerQty = thdata[i].TxtSubstring(843, 852).TxtConvertInt(),
                                RECType = thdata[i].TxtSubstring(853, 862),
                                ASNStatus = thdata[i].TxtSubstring(863, 872),
                                ASNReason = thdata[i].TxtSubstring(873, 882),
                                Facility = thdata[i].TxtSubstring(883, 887),
                                Reserved = thdata[i].TxtSubstring(888, 897),
                                MBOLKey = thdata[i].TxtSubstring(898, 907),
                                Appointment_No = thdata[i].TxtSubstring(908, 917),
                                LoadKey = thdata[i].TxtSubstring(918, 927),
                                xDockFlag = thdata[i].TxtSubstring(928, 928),
                                UserDefine01 = thdata[i].TxtSubstring(929, 958),
                                PROCESSTYPE = thdata[i].TxtSubstring(959, 959),
                                UserDefine02 = thdata[i].TxtSubstring(960, 989),
                                UserDefine03 = thdata[i].TxtSubstring(990, 1019),
                                UserDefine04 = thdata[i].TxtSubstring(1020, 1049),
                                UserDefine05 = thdata[i].TxtSubstring(1050, 1079),
                                UserDefine06 = thdata[i].TxtSubstring(1080, 1093).TxtConvertTime(),
                                UserDefine07 = thdata[i].TxtSubstring(1094, 1107).TxtConvertTime(),
                                UserDefine08 = thdata[i].TxtSubstring(1108, 1137),
                                UserDefine09 = thdata[i].TxtSubstring(1138, 1167),
                                UserDefine10 = thdata[i].TxtSubstring(1168, 1197),
                                DOCTYPE = thdata[i].TxtSubstring(1198, 1198),
                                RoutingTool = thdata[i].TxtSubstring(1199, 1228),
                                CTNTYPE1 = thdata[i].TxtSubstring(1229, 1258),
                                CTNQTY1 = thdata[i].TxtSubstring(1259, 1268).TxtConvertInt(),
                                CTNTYPE2 = thdata[i].TxtSubstring(1269, 1298),
                                CTNQTY2 = thdata[i].TxtSubstring(1299, 1308).TxtConvertInt(),
                                CTNTYPE3 = thdata[i].TxtSubstring(1309, 1338),
                                CTNQTY3 = thdata[i].TxtSubstring(1339, 1348).TxtConvertInt(),
                                CTNTYPE4 = thdata[i].TxtSubstring(1349, 1378),
                                CTNQTY4 = thdata[i].TxtSubstring(1379, 1388).TxtConvertInt(),
                                CTNTYPE5 = thdata[i].TxtSubstring(1389, 1418),
                                CTNQTY5 = thdata[i].TxtSubstring(1419, 1428).TxtConvertInt(),
                                CTNTYPE6 = thdata[i].TxtSubstring(1429, 1458),
                                CTNQTY6 = thdata[i].TxtSubstring(1459, 1468).TxtConvertInt(),
                                CTNTYPE7 = thdata[i].TxtSubstring(1469, 1498),
                                CTNQTY7 = thdata[i].TxtSubstring(1499, 1508).TxtConvertInt(),
                                CTNTYPE8 = thdata[i].TxtSubstring(1509, 1538),
                                CTNQTY8 = thdata[i].TxtSubstring(1539, 1548).TxtConvertInt(),
                                CTNTYPE9 = thdata[i].TxtSubstring(1549, 1578),
                                CTNQTY9 = thdata[i].TxtSubstring(1579, 1588).TxtConvertInt(),
                                CTNTYPE10 = thdata[i].TxtSubstring(1589, 1618),
                                CTNQTY10 = thdata[i].TxtSubstring(1619, 1628).TxtConvertInt(),
                                NoOfBulkCtn = thdata[i].TxtSubstring(1629, 1638).TxtConvertInt(),
                                NoOfMiniPacks = thdata[i].TxtSubstring(1639, 1648).TxtConvertInt(),
                                NoOfPallet = thdata[i].TxtSubstring(1649, 1658).TxtConvertInt(),
                                Weight = thdata[i].TxtSubstring(1659, 1668).TxtConvertFloat(),
                                WeightUnit = thdata[i].TxtSubstring(1669, 1688),
                                Cube = thdata[i].TxtSubstring(1689, 1698).TxtConvertFloat(),
                                CubeUnit = thdata[i].TxtSubstring(1699, 1718)
                            });
                        }
                        if (thdata[i].TxtSubstring(1, 5) == "ASNDT")//取出主订单详细信息
                        {
                            details.Add(new Inbound_ASNDT
                            {
                                HeaderFlag = thdata[i].TxtSubstring(1, 5),
                                InterfaceActionFlag = thdata[i].TxtSubstring(6, 6),
                                ReceiptKey = thdata[i].TxtSubstring(7, 16),
                                ReceiptLineNumber = thdata[i].TxtSubstring(17, 21),
                                ExternReceiptKey = thdata[i].TxtSubstring(22, 41),
                                ExternLineNo = thdata[i].TxtSubstring(42, 61),
                                StorerKey = thdata[i].TxtSubstring(62, 76),
                                POKey = thdata[i].TxtSubstring(77, 94),
                                Sku = thdata[i].TxtSubstring(95, 114),
                                AltSku = thdata[i].TxtSubstring(115, 134),
                                Id = thdata[i].TxtSubstring(135, 152),
                                Status = thdata[i].TxtSubstring(153, 162),
                                DateReceived = thdata[i].TxtSubstring(163, 176).TxtConvertTime(),
                                QtyExpected = thdata[i].TxtSubstring(177, 186).TxtConvertInt(),
                                QtyAdjusted = thdata[i].TxtSubstring(187, 196).TxtConvertInt(),
                                QtyReceived = thdata[i].TxtSubstring(197, 206).TxtConvertInt(),
                                UOM = thdata[i].TxtSubstring(207, 216),
                                PackKey = thdata[i].TxtSubstring(217, 226),
                                VesselKey = thdata[i].TxtSubstring(227, 244),
                                VoyageKey = thdata[i].TxtSubstring(245, 262),
                                XdockKey = thdata[i].TxtSubstring(263, 280),
                                ContainerKey = thdata[i].TxtSubstring(281, 298),
                                ToLoc = thdata[i].TxtSubstring(299, 308),
                                ToLot = thdata[i].TxtSubstring(309, 318),
                                ToId = thdata[i].TxtSubstring(319, 336),
                                ConditionCode = thdata[i].TxtSubstring(337, 346),
                                Lottable01 = thdata[i].TxtSubstring(347, 364),
                                Lottable02 = thdata[i].TxtSubstring(365, 382),
                                Lottable03 = thdata[i].TxtSubstring(383, 400),
                                Lottable04 = thdata[i].TxtSubstring(401, 414).TxtConvertTime(),
                                Lottable05 = thdata[i].TxtSubstring(415, 428).TxtConvertTime(),
                                CaseCnt = thdata[i].TxtSubstring(429, 438).TxtConvertInt(),
                                InnerPack = thdata[i].TxtSubstring(439, 448).TxtConvertInt(),
                                Pallet = thdata[i].TxtSubstring(449, 458).TxtConvertInt(),
                                Cube = thdata[i].TxtSubstring(459, 468).TxtConvertFloat(),
                                GrossWgt = thdata[i].TxtSubstring(469, 478).TxtConvertFloat(),
                                NetWgt = thdata[i].TxtSubstring(479, 488).TxtConvertFloat(),
                                OtherUnit1 = thdata[i].TxtSubstring(489, 498).TxtConvertFloat(),
                                OtherUnit2 = thdata[i].TxtSubstring(499, 508).TxtConvertFloat(),
                                UnitPrice = thdata[i].TxtSubstring(509, 524).TxtConvertFloat(),
                                ExtendedPrice = thdata[i].TxtSubstring(525, 540).TxtConvertFloat(),
                                EffectiveDate = thdata[i].TxtSubstring(541, 554).TxtConvertTime(),
                                TariffKey = thdata[i].TxtSubstring(555, 564),
                                FreeGoodQtyExpected = thdata[i].TxtSubstring(565, 574).TxtConvertInt(),
                                FreeGoodQtyReceived = thdata[i].TxtSubstring(575, 584).TxtConvertInt(),
                                SubReasonCode = thdata[i].TxtSubstring(585, 594),
                                FinalizeFlag = thdata[i].TxtSubstring(595, 595),
                                DuplicateFrom = thdata[i].TxtSubstring(596, 600),
                                BeforeReceivedQty = thdata[i].TxtSubstring(601, 610).TxtConvertInt(),
                                PutawayLoc = thdata[i].TxtSubstring(611, 620),
                                ExportStatus = thdata[i].TxtSubstring(621, 621),
                                SplitPalletFlag = thdata[i].TxtSubstring(622, 622),
                                POLineNumber = thdata[i].TxtSubstring(623, 627),
                                LoadKey = thdata[i].TxtSubstring(628, 637),
                                ExternPoKey = thdata[i].TxtSubstring(638, 657),
                                UserDefine01 = thdata[i].TxtSubstring(658, 687),
                                UserDefine02 = thdata[i].TxtSubstring(688, 717),
                                UserDefine03 = thdata[i].TxtSubstring(718, 747),
                                UserDefine04 = thdata[i].TxtSubstring(748, 777),
                                UserDefine05 = thdata[i].TxtSubstring(778, 807),
                                UserDefine06 = thdata[i].TxtSubstring(808, 821).TxtConvertTime(),
                                UserDefine07 = thdata[i].TxtSubstring(822, 835).TxtConvertTime(),
                                UserDefine08 = thdata[i].TxtSubstring(836, 865),
                                UserDefine09 = thdata[i].TxtSubstring(866, 895),
                                UserDefine10 = thdata[i].TxtSubstring(896, 925)
                            });
                        }
                        if (thdata[i].TxtSubstring(1, 5) == "ASNTR")
                        {
                            break;
                        }
                    }
                }

                //ans 需要更新(如果asn能够查到数据并且，数据的状态为1就可以更新)
                for (int i = 0; i < header.Count(); i++)
                {
                    string questr = "SELECT * FROM [dbo].[WMS_ASN] WHERE ExternReceiptNumber='" + header[i].ExternReceiptKey + "'";
                    DataTable SuccessCount = this.ExecuteDataTableBySqlString(questr);
                    if (SuccessCount.Rows.Count <= 0 || SuccessCount.Rows[0]["Status"].ToString() == "1")
                    {
                        Error = header[i].ExternReceiptKey.ToString();
                        //先删除 在新增
                        string sql_1 = "DELETE Inbound_ASNHD WHERE ExternReceiptKey='" + header[i].ExternReceiptKey + "';";
                        sql_1 += "DELETE Inbound_ASNDT WHERE ExternReceiptKey='" + header[i].ExternReceiptKey + "';INSERT INTO Inbound_ASNHD VALUES('" + header[i].HeaderFlag + "'";
                        sql_1 += ",'" + header[i].InterfaceActionFlag + "'";
                        sql_1 += ",'" + header[i].ReceiptKey + "'";
                        sql_1 += ",'" + header[i].ExternReceiptKey + "'";
                        sql_1 += ",'" + header[i].ReceiptGroup + "'";
                        sql_1 += ",'" + header[i].StorerKey + "'";
                        sql_1 += ",'" + header[i].ReceiptDate + "'";
                        sql_1 += ",'" + header[i].POKey + "'";
                        sql_1 += ",'" + header[i].CarrierKey + "'";
                        sql_1 += ",'" + header[i].CarrierName + "'";
                        sql_1 += ",'" + header[i].CarrierAddress1 + "'";
                        sql_1 += ",'" + header[i].CarrierAddress2 + "'";
                        sql_1 += ",'" + header[i].CarrierCity + "'";
                        sql_1 += ",'" + header[i].CarrierState + "'";
                        sql_1 += ",'" + header[i].CarrierZip + "'";
                        sql_1 += ",'" + header[i].CarrierReference + "'";
                        sql_1 += ",'" + header[i].WarehouseReference + "'";
                        sql_1 += ",'" + header[i].OriginCountry + "'";
                        sql_1 += ",'" + header[i].DestinationCountry + "'";
                        sql_1 += ",'" + header[i].VehicleNumber + "'";
                        sql_1 += ",'" + header[i].VehicleDate + "'";
                        sql_1 += ",'" + header[i].PlaceOfLoading + "'";
                        sql_1 += ",'" + header[i].PlaceOfDischarge + "'";
                        sql_1 += ",'" + header[i].PlaceofDelivery + "'";
                        sql_1 += ",'" + header[i].IncoTerms + "'";
                        sql_1 += ",'" + header[i].TermsNote + "'";
                        sql_1 += ",'" + header[i].ContainerKey + "'";
                        sql_1 += ",'" + header[i].Signatory + "'";
                        sql_1 += ",'" + header[i].PlaceofIssue + "'";
                        sql_1 += ",'" + header[i].OpenQty + "'";
                        sql_1 += ",'" + header[i].Status + "'";
                        sql_1 += ",'" + header[i].Notes + "'";
                        sql_1 += ",'" + header[i].EffectiveDate + "'";
                        sql_1 += ",'" + header[i].ContainerType + "'";
                        sql_1 += ",'" + header[i].ContainerQty + "'";
                        sql_1 += ",'" + header[i].BilledContainerQty + "'";
                        sql_1 += ",'" + header[i].RECType + "'";
                        sql_1 += ",'" + header[i].ASNStatus + "'";
                        sql_1 += ",'" + header[i].ASNReason + "'";
                        sql_1 += ",'" + header[i].Facility + "'";
                        sql_1 += ",'" + header[i].Reserved + "'";
                        sql_1 += ",'" + header[i].MBOLKey + "'";
                        sql_1 += ",'" + header[i].Appointment_No + "'";
                        sql_1 += ",'" + header[i].LoadKey + "'";
                        sql_1 += ",'" + header[i].xDockFlag + "'";
                        sql_1 += ",'" + header[i].UserDefine01 + "'";
                        sql_1 += ",'" + header[i].PROCESSTYPE + "'";
                        sql_1 += ",'" + header[i].UserDefine02 + "'";
                        sql_1 += ",'" + header[i].UserDefine03 + "'";
                        sql_1 += ",'" + header[i].UserDefine04 + "'";
                        sql_1 += ",'" + header[i].UserDefine05 + "'";
                        sql_1 += ",'" + header[i].UserDefine06 + "'";
                        sql_1 += ",'" + header[i].UserDefine07 + "'";
                        sql_1 += ",'" + header[i].UserDefine08 + "'";
                        sql_1 += ",'" + header[i].UserDefine09 + "'";
                        sql_1 += ",'" + header[i].UserDefine10 + "'";
                        sql_1 += ",'" + header[i].DOCTYPE + "'";
                        sql_1 += ",'" + header[i].RoutingTool + "'";
                        sql_1 += ",'" + header[i].CTNTYPE1 + "'";
                        sql_1 += ",'" + header[i].CTNQTY1 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE2 + "'";
                        sql_1 += ",'" + header[i].CTNQTY2 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE3 + "'";
                        sql_1 += ",'" + header[i].CTNQTY3 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE4 + "'";
                        sql_1 += ",'" + header[i].CTNQTY4 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE5 + "'";
                        sql_1 += ",'" + header[i].CTNQTY5 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE6 + "'";
                        sql_1 += ",'" + header[i].CTNQTY6 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE7 + "'";
                        sql_1 += ",'" + header[i].CTNQTY7 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE8 + "'";
                        sql_1 += ",'" + header[i].CTNQTY8 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE9 + "'";
                        sql_1 += ",'" + header[i].CTNQTY9 + "'";
                        sql_1 += ",'" + header[i].CTNTYPE10 + "'";
                        sql_1 += ",'" + header[i].CTNQTY10 + "'";
                        sql_1 += ",'" + header[i].NoOfBulkCtn + "'";
                        sql_1 += ",'" + header[i].NoOfMiniPacks + "'";
                        sql_1 += ",'" + header[i].NoOfPallet + "'";
                        sql_1 += ",'" + header[i].Weight + "'";
                        sql_1 += ",'" + header[i].WeightUnit + "'";
                        sql_1 += ",'" + header[i].Cube + "'";
                        sql_1 += ",'" + header[i].CubeUnit + "'";

                        sql_1 += ",'0'";
                        sql_1 += ",Null";
                        sql_1 += ",'" + DateTime.Now + "'";
                        sql_1 += ",Null";
                        sql_1 += ") SELECT @@IDENTITY AS Inbound_ASNHD;";

                        int id_1 = this.ScanExecuteNonQueryRID(sql_1);
                        if (id_1 > 0)
                        {
                            //CarrierKey
                            string ASNType = "";
                            if (header[i].CarrierKey != "")
                            {
                                ASNType = "门店入库";
                            }
                            else
                            {
                                ASNType = "经销商入库";
                            }
                            List<ASNH> aSNHs = new List<ASNH>();
                            aSNHs.Add(new ASNH
                            {
                                ExternReceiptNumber = header[i].ExternReceiptKey,
                                CustomerID = 108,
                                CustomerName = "PUMA_SH",
                                WarehouseID = 3,
                                WarehouseName = "PUMA上海仓",
                                ExpectDate = DateTime.Now.AddDays(3),
                                Status = 1,
                                ASNType = ASNType,
                                Creator = "API",
                                CreateTime = DateTime.Now,
                                str3 = "PUMA",
                                str1 = header[i].UserDefine02,
                            });
                            request.asn = aSNHs;
                            List<ASNDetail> aSNDetails = new List<ASNDetail>();
                            for (int m = 0; m < details.Count(); m++)
                            {
                                ASNDetail detail = new ASNDetail();
                                if (header[i].ExternReceiptKey == details[m].ExternReceiptKey)
                                {
                                    detail.ExternReceiptNumber = header[i].ExternReceiptKey;
                                    detail.CustomerID = 108;
                                    detail.CustomerName = "PUMA_SH";
                                    detail.LineNumber = details[m].ExternLineNo;
                                    detail.SKU = EANQuery(details[m].Sku);
                                    detail.QtyExpected = details[m].QtyExpected;
                                    detail.QtyReceived = 0.000;
                                    detail.QtyDiff = 0.000;
                                    detail.GoodsName = detail.SKU;
                                    detail.Creator = "API";
                                    detail.CreateTime = DateTime.Now;
                                    aSNDetails.Add(detail);

                                    //删除原数据  更新新数据
                                    string sql_2 = "INSERT INTO Inbound_ASNDT VALUES('" + id_1 + "'";
                                    sql_2 += ",'" + details[m].HeaderFlag + "'";
                                    sql_2 += ",'" + details[m].InterfaceActionFlag + "'";
                                    sql_2 += ",'" + details[m].ReceiptKey + "'";
                                    sql_2 += ",'" + details[m].ReceiptLineNumber + "'";
                                    sql_2 += ",'" + details[m].ExternReceiptKey + "'";
                                    sql_2 += ",'" + details[m].ExternLineNo + "'";
                                    sql_2 += ",'" + details[m].StorerKey + "'";
                                    sql_2 += ",'" + details[m].POKey + "'";
                                    sql_2 += ",'" + details[m].Sku + "'";
                                    sql_2 += ",'" + details[m].AltSku + "'";
                                    sql_2 += ",'" + details[m].Id + "'";
                                    sql_2 += ",'" + details[m].Status + "'";
                                    sql_2 += ",'" + details[m].DateReceived + "'";
                                    sql_2 += ",'" + details[m].QtyExpected + "'";
                                    sql_2 += ",'" + details[m].QtyAdjusted + "'";
                                    sql_2 += ",'" + details[m].QtyReceived + "'";
                                    sql_2 += ",'" + details[m].UOM + "'";
                                    sql_2 += ",'" + details[m].PackKey + "'";
                                    sql_2 += ",'" + details[m].VesselKey + "'";
                                    sql_2 += ",'" + details[m].VoyageKey + "'";
                                    sql_2 += ",'" + details[m].XdockKey + "'";
                                    sql_2 += ",'" + details[m].ContainerKey + "'";
                                    sql_2 += ",'" + details[m].ToLoc + "'";
                                    sql_2 += ",'" + details[m].ToLot + "'";
                                    sql_2 += ",'" + details[m].ToId + "'";
                                    sql_2 += ",'" + details[m].ConditionCode + "'";
                                    sql_2 += ",'" + details[m].Lottable01 + "'";
                                    sql_2 += ",'" + details[m].Lottable02 + "'";
                                    sql_2 += ",'" + details[m].Lottable03 + "'";
                                    sql_2 += ",'" + details[m].Lottable04 + "'";
                                    sql_2 += ",'" + details[m].Lottable05 + "'";
                                    sql_2 += ",'" + details[m].CaseCnt + "'";
                                    sql_2 += ",'" + details[m].InnerPack + "'";
                                    sql_2 += ",'" + details[m].Pallet + "'";
                                    sql_2 += ",'" + details[m].Cube + "'";
                                    sql_2 += ",'" + details[m].GrossWgt + "'";
                                    sql_2 += ",'" + details[m].NetWgt + "'";
                                    sql_2 += ",'" + details[m].OtherUnit1 + "'";
                                    sql_2 += ",'" + details[m].OtherUnit2 + "'";
                                    sql_2 += ",'" + details[m].UnitPrice + "'";
                                    sql_2 += ",'" + details[m].ExtendedPrice + "'";
                                    sql_2 += ",'" + details[m].EffectiveDate + "'";
                                    sql_2 += ",'" + details[m].TariffKey + "'";
                                    sql_2 += ",'" + details[m].FreeGoodQtyExpected + "'";
                                    sql_2 += ",'" + details[m].FreeGoodQtyReceived + "'";
                                    sql_2 += ",'" + details[m].SubReasonCode + "'";
                                    sql_2 += ",'" + details[m].FinalizeFlag + "'";
                                    sql_2 += ",'" + details[m].DuplicateFrom + "'";
                                    sql_2 += ",'" + details[m].BeforeReceivedQty + "'";
                                    sql_2 += ",'" + details[m].PutawayLoc + "'";
                                    sql_2 += ",'" + details[m].ExportStatus + "'";
                                    sql_2 += ",'" + details[m].SplitPalletFlag + "'";
                                    sql_2 += ",'" + details[m].POLineNumber + "'";
                                    sql_2 += ",'" + details[m].LoadKey + "'";
                                    sql_2 += ",'" + details[m].ExternPoKey + "'";
                                    sql_2 += ",'" + details[m].UserDefine01 + "'";
                                    sql_2 += ",'" + details[m].UserDefine02 + "'";
                                    sql_2 += ",'" + details[m].UserDefine03 + "'";
                                    sql_2 += ",'" + details[m].UserDefine04 + "'";
                                    sql_2 += ",'" + details[m].UserDefine05 + "'";
                                    sql_2 += ",'" + details[m].UserDefine06 + "'";
                                    sql_2 += ",'" + details[m].UserDefine07 + "'";
                                    sql_2 += ",'" + details[m].UserDefine08 + "'";
                                    sql_2 += ",'" + details[m].UserDefine09 + "'";
                                    sql_2 += ",'" + details[m].UserDefine10 + "'";
                                    sql_2 += ",'" + DateTime.Now + "'";
                                    sql_2 += ",Null";
                                    sql_2 += ") SELECT @@IDENTITY AS Inbound_ASNDT;";

                                    int id_2 = this.ScanExecuteNonQueryRID(sql_2);
                                    if (id_2 < 0)
                                    {
                                        LogHelper.WriteLog(typeof(string), "ASN-Inbound_ASNDT数据写入错误[" + Error + "]:" + sql_2, LogHelper.LogLevel.Error);
                                        msg = "ASN-Inbound_ASNDT数据写入错误";
                                        return msg;
                                    }
                                }
                            }
                            request.asnDetails = aSNDetails;
                            int isresult;
                            string wmsmsg = AddasnAndasnDetail(request, out isresult);
                            if (isresult != 200)
                            {
                                LogHelper.WriteLog(typeof(string), "ASN-Inbound_ASNDT入库单写入WMS失败[" + Error + "]:" + wmsmsg, LogHelper.LogLevel.Error);
                                msg = "ASN-Inbound_ASNDT入库单写入WMS失败";
                                return msg;
                            }
                        }
                        else
                        {
                            LogHelper.WriteLog(typeof(string), "ASN-Inbound_ASNHD数据写入错误[" + Error + "]:" + sql_1, LogHelper.LogLevel.Error);
                            msg = "ASN-Inbound_ASNHD数据写入错误";
                            return msg;
                        }
                    }
                    else
                    {


                    }
                }
                msg = "200";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "ASN-Inbound_ASNHD接口错误[" + Error + "]:" + ex.Message, LogHelper.LogLevel.Error);
            }

            return msg;
        }
        /// <summary>
        /// 入库订单
        /// </summary>
        /// <returns></returns>
        public string GetInbound_ORDHD(List<string> thdata, out string externumber)
        {
            string msg = string.Empty;
            string Error = "";
            externumber = "";
            try
            {
                //读取文件
                //List<string> thdata = new List<string>();
                //string dir = AppDomain.CurrentDomain.BaseDirectory;
                //dir = Path.GetFullPath("..");
                //dir = Path.GetFullPath("../..");
                //string filepath = dir + "/DownFile/WMSORD_202010091020160000000049490828.txt";     //文件路径

                //if (System.IO.File.Exists(filepath))
                //{
                //    foreach (string str in System.IO.File.ReadAllLines(filepath, Encoding.Default))
                //    {
                //        thdata.Add(str);
                //    }
                //}

                //实体赋值
                List<Inbound_ORDHD> header = new List<Inbound_ORDHD>();//asn 主订单信息
                List<Inbound_ORDDT> details = new List<Inbound_ORDDT>();//asn 订单详细信息
                for (int i = 0; i < thdata.Count(); i++)
                {
                    if (thdata[0].TxtSubstring(1, 10) == "WMSORD")//判断txt表头类型
                    {
                        if (thdata[i].TxtSubstring(1, 5) == "ORDHD")//取出主订单信息
                        {
                            header.Add(new Inbound_ORDHD
                            {
                                HeaderFlag = thdata[i].TxtSubstring(1, 5),
                                InterfaceActionFlag = thdata[i].TxtSubstring(6, 6),
                                OrderKey = thdata[i].TxtSubstring(7, 16),
                                StorerKey = thdata[i].TxtSubstring(17, 31),
                                ExternOrderKey = thdata[i].TxtSubstring(32, 51),
                                Reserved = thdata[i].TxtSubstring(52, 61),
                                OrderDate = thdata[i].TxtSubstring(62, 75).TxtConvertTime(),
                                DeliveryDate = thdata[i].TxtSubstring(76, 89).TxtConvertTime(),
                                Priority = thdata[i].TxtSubstring(90, 99),
                                ConsigneeKey = thdata[i].TxtSubstring(100, 114),
                                C_contact1 = thdata[i].TxtSubstring(115, 144),
                                C_Contact2 = thdata[i].TxtSubstring(145, 174),
                                C_Company = thdata[i].TxtSubstring(175, 219),
                                C_Address1 = thdata[i].TxtSubstring(220, 264),
                                C_Address2 = thdata[i].TxtSubstring(265, 309),
                                C_Address3 = thdata[i].TxtSubstring(310, 354),
                                C_Address4 = thdata[i].TxtSubstring(355, 399),
                                C_City = thdata[i].TxtSubstring(400, 444),
                                C_State = thdata[i].TxtSubstring(445, 446),
                                C_Zip = thdata[i].TxtSubstring(447, 464),
                                C_Country = thdata[i].TxtSubstring(465, 494),
                                C_ISOCntryCode = thdata[i].TxtSubstring(495, 504),
                                C_Phone1 = thdata[i].TxtSubstring(505, 522),
                                C_Phone2 = thdata[i].TxtSubstring(523, 540),
                                C_Fax1 = thdata[i].TxtSubstring(541, 558),
                                C_Fax2 = thdata[i].TxtSubstring(559, 576),
                                C_vat = thdata[i].TxtSubstring(577, 594),
                                BuyerPO = thdata[i].TxtSubstring(595, 614),
                                BillToKey = thdata[i].TxtSubstring(615, 629),
                                B_contact1 = thdata[i].TxtSubstring(630, 659),
                                B_Contact2 = thdata[i].TxtSubstring(660, 689),
                                B_Company = thdata[i].TxtSubstring(690, 734),
                                B_Address1 = thdata[i].TxtSubstring(735, 779),
                                B_Address2 = thdata[i].TxtSubstring(780, 824),
                                B_Address3 = thdata[i].TxtSubstring(825, 869),
                                B_Address4 = thdata[i].TxtSubstring(870, 914),
                                B_City = thdata[i].TxtSubstring(915, 959),
                                B_State = thdata[i].TxtSubstring(960, 961),
                                B_Zip = thdata[i].TxtSubstring(962, 979),
                                B_Country = thdata[i].TxtSubstring(980, 1009),
                                B_ISOCntryCode = thdata[i].TxtSubstring(1010, 1019),
                                B_Phone1 = thdata[i].TxtSubstring(1020, 1037),
                                B_Phone2 = thdata[i].TxtSubstring(1038, 1055),
                                B_Fax1 = thdata[i].TxtSubstring(1056, 1073),
                                B_Fax2 = thdata[i].TxtSubstring(1074, 1091),
                                B_Vat = thdata[i].TxtSubstring(1092, 1109),
                                IncoTerm = thdata[i].TxtSubstring(1110, 1119),
                                PmtTerm = thdata[i].TxtSubstring(1120, 1129),
                                OpenQty = thdata[i].TxtSubstring(1130, 1139).TxtConvertInt(),
                                Status = thdata[i].TxtSubstring(1140, 1149),
                                DischargePlace = thdata[i].TxtSubstring(1150, 1179),
                                DeliveryPlace = thdata[i].TxtSubstring(1180, 1209),
                                IntermodalVehicle = thdata[i].TxtSubstring(1210, 1239),
                                CountryOfOrigin = thdata[i].TxtSubstring(1240, 1269),
                                CountryDestination = thdata[i].TxtSubstring(1270, 1299),
                                UpdateSource = thdata[i].TxtSubstring(1300, 1309),
                                Type = thdata[i].TxtSubstring(1310, 1319),
                                OrderGroup = thdata[i].TxtSubstring(1320, 1339),
                                Door = thdata[i].TxtSubstring(1340, 1349),
                                Route = thdata[i].TxtSubstring(1350, 1359),
                                Stop = thdata[i].TxtSubstring(1360, 1369),
                                Notes = thdata[i].TxtSubstring(1370, 1494),
                                EffectiveDate = thdata[i].TxtSubstring(1495, 1508).TxtConvertTime(),
                                ContainerType = thdata[i].TxtSubstring(1509, 1528),
                                ContainerQty = thdata[i].TxtSubstring(1529, 1538).TxtConvertInt(),
                                BilledContainerQty = thdata[i].TxtSubstring(1539, 1548).TxtConvertInt(),
                                SOStatus = thdata[i].TxtSubstring(1549, 1558),
                                MBOLKey = thdata[i].TxtSubstring(1559, 1568),
                                InvoiceNo = thdata[i].TxtSubstring(1569, 1578),
                                InvoiceAmount = thdata[i].TxtSubstring(1579, 1594).TxtConvertFloat(),
                                Salesman = thdata[i].TxtSubstring(1595, 1624),
                                GrossWeight = thdata[i].TxtSubstring(1625, 1640).TxtConvertFloat(),
                                WgtUnit = thdata[i].TxtSubstring(1641, 1645),
                                Capacity = thdata[i].TxtSubstring(1646, 1661).TxtConvertFloat(),
                                CubeUnit = thdata[i].TxtSubstring(1662, 1666),
                                PrintFlag = thdata[i].TxtSubstring(1667, 1667),
                                LoadKey = thdata[i].TxtSubstring(1668, 1677),
                                Rdd = thdata[i].TxtSubstring(1678, 1707),
                                Notes2 = thdata[i].TxtSubstring(1708, 1832),
                                SequenceNo = thdata[i].TxtSubstring(1833, 1842).TxtConvertInt(),
                                Rds = thdata[i].TxtSubstring(1843, 1843),
                                SectionKey = thdata[i].TxtSubstring(1844, 1853),
                                Facility = thdata[i].TxtSubstring(1854, 1858),
                                PrintDocDate = thdata[i].TxtSubstring(1859, 1872).TxtConvertTime(),
                                LabelPrice = thdata[i].TxtSubstring(1873, 1877),
                                POKey = thdata[i].TxtSubstring(1878, 1887),
                                ExternPOKey = thdata[i].TxtSubstring(1888, 1907),
                                XDockFlag = thdata[i].TxtSubstring(1908, 1908),
                                UserDefine01 = thdata[i].TxtSubstring(1909, 1928),
                                UserDefine02 = thdata[i].TxtSubstring(1929, 1948),
                                UserDefine03 = thdata[i].TxtSubstring(1949, 1968),
                                UserDefine04 = thdata[i].TxtSubstring(1969, 1988),
                                UserDefine05 = thdata[i].TxtSubstring(1989, 2008),
                                UserDefine06 = thdata[i].TxtSubstring(2009, 2022).TxtConvertTime(),
                                UserDefine07 = thdata[i].TxtSubstring(2023, 2036).TxtConvertTime(),
                                UserDefine08 = thdata[i].TxtSubstring(2037, 2046),
                                UserDefine09 = thdata[i].TxtSubstring(2047, 2056),
                                UserDefine10 = thdata[i].TxtSubstring(2057, 2066),
                                Issued = thdata[i].TxtSubstring(2067, 2067),
                                DeliveryNote = thdata[i].TxtSubstring(2068, 2077),
                                PODCust = thdata[i].TxtSubstring(2078, 2091).TxtConvertTime(),
                                PODArrive = thdata[i].TxtSubstring(2092, 2105).TxtConvertTime(),
                                PODReject = thdata[i].TxtSubstring(2106, 2119).TxtConvertTime(),
                                PODUser = thdata[i].TxtSubstring(2120, 2137),
                                xdockpokey = thdata[i].TxtSubstring(2138, 2157),
                                SpecialHandling = thdata[i].TxtSubstring(2158, 2158),
                                RoutingTool = thdata[i].TxtSubstring(2159, 2188),
                                MarkforKey = thdata[i].TxtSubstring(2189, 2203),
                                M_Contact1 = thdata[i].TxtSubstring(2204, 2233),
                                M_Contact2 = thdata[i].TxtSubstring(2234, 2263),
                                M_Company = thdata[i].TxtSubstring(2264, 2308),
                                M_Address1 = thdata[i].TxtSubstring(2309, 2353),
                                M_Address2 = thdata[i].TxtSubstring(2354, 2398),
                                M_Address3 = thdata[i].TxtSubstring(2399, 2443),
                                M_Address4 = thdata[i].TxtSubstring(2444, 2488),
                                M_City = thdata[i].TxtSubstring(2489, 2533),
                                M_State = thdata[i].TxtSubstring(2534, 2535),
                                M_Zip = thdata[i].TxtSubstring(2536, 2553),
                                M_Country = thdata[i].TxtSubstring(2554, 2583),
                                M_ISOCntryCode = thdata[i].TxtSubstring(2584, 2593),
                                M_Phone1 = thdata[i].TxtSubstring(2594, 2611),
                                M_Phone2 = thdata[i].TxtSubstring(2612, 2629),
                                M_Fax1 = thdata[i].TxtSubstring(2630, 2647),
                                M_Fax2 = thdata[i].TxtSubstring(2648, 2665),
                                M_vat = thdata[i].TxtSubstring(2666, 2683),
                                //C_State_Long = thdata[i].TxtSubstring(2684,2728)
                                C_State_Long = thdata[i].TxtSubstring(2684, 2688)
                            });
                        }
                        if (thdata[i].TxtSubstring(1, 5) == "ORDDT")//取出主订单详细信息
                        {
                            details.Add(new Inbound_ORDDT
                            {
                                HeaderFlag = thdata[i].TxtSubstring(1, 5),
                                InterfaceActionFlag = thdata[i].TxtSubstring(6, 6),
                                OrderLineNumber = thdata[i].TxtSubstring(7, 11),
                                OrderDetailSysId = thdata[i].TxtSubstring(12, 21).TxtConvertInt(),
                                ExternOrderKey = thdata[i].TxtSubstring(22, 41),
                                ExternLineNo = thdata[i].TxtSubstring(42, 51),
                                Sku = thdata[i].TxtSubstring(52, 71),
                                StorerKey = thdata[i].TxtSubstring(72, 86),
                                ManufacturerSku = thdata[i].TxtSubstring(87, 106),
                                RetailSku = thdata[i].TxtSubstring(107, 126),
                                AltSku = thdata[i].TxtSubstring(127, 146),
                                OriginalQty = thdata[i].TxtSubstring(147, 156).TxtConvertInt(),
                                OpenQty = thdata[i].TxtSubstring(157, 166).TxtConvertInt(),
                                ShippedQty = thdata[i].TxtSubstring(167, 176).TxtConvertInt(),
                                AdjustedQty = thdata[i].TxtSubstring(177, 186).TxtConvertInt(),
                                QtyPreAllocated = thdata[i].TxtSubstring(187, 196).TxtConvertInt(),
                                QtyAllocated = thdata[i].TxtSubstring(197, 206).TxtConvertInt(),
                                QtyPicked = thdata[i].TxtSubstring(207, 216).TxtConvertInt(),
                                UOM = thdata[i].TxtSubstring(217, 226),
                                PackKey = thdata[i].TxtSubstring(227, 236),
                                PickCode = thdata[i].TxtSubstring(237, 246),
                                CartonGroup = thdata[i].TxtSubstring(247, 256),
                                Lot = thdata[i].TxtSubstring(257, 266),
                                ID = thdata[i].TxtSubstring(267, 284).TxtConvertInt(),
                                Facility = thdata[i].TxtSubstring(285, 289),
                                Status = thdata[i].TxtSubstring(290, 299),
                                UnitPrice = thdata[i].TxtSubstring(300, 315).TxtConvertFloat(),
                                Tax01 = thdata[i].TxtSubstring(316, 331).TxtConvertFloat(),
                                Tax02 = thdata[i].TxtSubstring(332, 347).TxtConvertFloat(),
                                ExtendedPrice = thdata[i].TxtSubstring(348, 363).TxtConvertFloat(),
                                Reserved = thdata[i].TxtSubstring(364, 400),
                                UpdateSource = thdata[i].TxtSubstring(401, 410),
                                Lottable01 = thdata[i].TxtSubstring(411, 428),
                                Lottable02 = thdata[i].TxtSubstring(429, 446),
                                Lottable03 = thdata[i].TxtSubstring(447, 464),
                                Lottable04 = thdata[i].TxtSubstring(465, 478).TxtConvertTime(),
                                Lottable05 = thdata[i].TxtSubstring(479, 492).TxtConvertTime(),
                                EffectiveDate = thdata[i].TxtSubstring(493, 506).TxtConvertTime(),
                                TariffKey = thdata[i].TxtSubstring(507, 516),
                                FreeGoodQty = thdata[i].TxtSubstring(517, 526).TxtConvertInt(),
                                GrossWeight = thdata[i].TxtSubstring(527, 542).TxtConvertFloat(),
                                WgtUnit = thdata[i].TxtSubstring(543, 547),
                                Capacity = thdata[i].TxtSubstring(548, 563).TxtConvertFloat(),
                                CubeUnit = thdata[i].TxtSubstring(564, 568),
                                LoadKey = thdata[i].TxtSubstring(569, 578),
                                MBOLKey = thdata[i].TxtSubstring(579, 588),
                                QtyToProcess = thdata[i].TxtSubstring(589, 598).TxtConvertInt(),
                                MinShelfLife = thdata[i].TxtSubstring(599, 608).TxtConvertInt(),
                                UserDefine01 = thdata[i].TxtSubstring(609, 626),
                                UserDefine02 = thdata[i].TxtSubstring(627, 644),
                                UserDefine03 = thdata[i].TxtSubstring(645, 662),
                                UserDefine04 = thdata[i].TxtSubstring(663, 680),
                                UserDefine05 = thdata[i].TxtSubstring(681, 698),
                                UserDefine06 = thdata[i].TxtSubstring(699, 716),
                                UserDefine07 = thdata[i].TxtSubstring(717, 734),
                                UserDefine08 = thdata[i].TxtSubstring(735, 752),
                                UserDefine09 = thdata[i].TxtSubstring(753, 770),
                                POkey = thdata[i].TxtSubstring(771, 790),
                                ExternPOKey = thdata[i].TxtSubstring(791, 810),
                                OrgExternLineNo = thdata[i].TxtSubstring(811, 820),
                            });
                        }
                        if (thdata[i].TxtSubstring(1, 5) == "ORDTR")
                        {
                            break;
                        }
                    }
                }

                //保存原数据 并转换成订单数据
                for (int i = 0; i < header.Count(); i++)
                {
                    IEnumerable<PreOrder> PreOrderList;
                    IEnumerable<PreOrderDetail> PreDetail;
                    PreOrderList = null;
                    PreDetail = null;
                    Error = header[i].ExternOrderKey;
                    string sql_1 = "DELETE Inbound_ORDHD WHERE ExternOrderKey='" + header[i].ExternOrderKey + "';DELETE Inbound_ORDDT WHERE ExternOrderKey='" + header[i].ExternOrderKey + "';";
                    sql_1 += "INSERT INTO Inbound_ORDHD VALUES('" + header[i].HeaderFlag + "'";
                    sql_1 += ",'" + header[i].InterfaceActionFlag + "'";
                    sql_1 += ",'" + header[i].OrderKey + "'";
                    sql_1 += ",'" + header[i].StorerKey + "'";
                    sql_1 += ",'" + header[i].ExternOrderKey + "'";
                    sql_1 += ",'" + header[i].Reserved + "'";
                    sql_1 += ",'" + header[i].OrderDate + "'";
                    sql_1 += ",'" + header[i].DeliveryDate + "'";
                    sql_1 += ",'" + header[i].Priority + "'";
                    sql_1 += ",'" + header[i].ConsigneeKey + "'";
                    sql_1 += ",'" + header[i].C_contact1 + "'";
                    sql_1 += ",'" + header[i].C_Contact2 + "'";
                    sql_1 += ",'" + header[i].C_Company + "'";
                    sql_1 += ",'" + header[i].C_Address1 + "'";
                    sql_1 += ",'" + header[i].C_Address2 + "'";
                    sql_1 += ",'" + header[i].C_Address3 + "'";
                    sql_1 += ",'" + header[i].C_Address4 + "'";
                    sql_1 += ",'" + header[i].C_City + "'";
                    sql_1 += ",'" + header[i].C_State + "'";
                    sql_1 += ",'" + header[i].C_Zip + "'";
                    sql_1 += ",'" + header[i].C_Country + "'";
                    sql_1 += ",'" + header[i].C_ISOCntryCode + "'";
                    sql_1 += ",'" + header[i].C_Phone1 + "'";
                    sql_1 += ",'" + header[i].C_Phone2 + "'";
                    sql_1 += ",'" + header[i].C_Fax1 + "'";
                    sql_1 += ",'" + header[i].C_Fax2 + "'";
                    sql_1 += ",'" + header[i].C_vat + "'";
                    sql_1 += ",'" + header[i].BuyerPO + "'";
                    sql_1 += ",'" + header[i].BillToKey + "'";
                    sql_1 += ",'" + header[i].B_contact1 + "'";
                    sql_1 += ",'" + header[i].B_Contact2 + "'";
                    sql_1 += ",'" + header[i].B_Company + "'";
                    sql_1 += ",'" + header[i].B_Address1 + "'";
                    sql_1 += ",'" + header[i].B_Address2 + "'";
                    sql_1 += ",'" + header[i].B_Address3 + "'";
                    sql_1 += ",'" + header[i].B_Address4 + "'";
                    sql_1 += ",'" + header[i].B_City + "'";
                    sql_1 += ",'" + header[i].B_State + "'";
                    sql_1 += ",'" + header[i].B_Zip + "'";
                    sql_1 += ",'" + header[i].B_Country + "'";
                    sql_1 += ",'" + header[i].B_ISOCntryCode + "'";
                    sql_1 += ",'" + header[i].B_Phone1 + "'";
                    sql_1 += ",'" + header[i].B_Phone2 + "'";
                    sql_1 += ",'" + header[i].B_Fax1 + "'";
                    sql_1 += ",'" + header[i].B_Fax2 + "'";
                    sql_1 += ",'" + header[i].B_Vat + "'";
                    sql_1 += ",'" + header[i].IncoTerm + "'";
                    sql_1 += ",'" + header[i].PmtTerm + "'";
                    sql_1 += ",'" + header[i].OpenQty + "'";
                    sql_1 += ",'" + header[i].Status + "'";
                    sql_1 += ",'" + header[i].DischargePlace + "'";
                    sql_1 += ",'" + header[i].DeliveryPlace + "'";
                    sql_1 += ",'" + header[i].IntermodalVehicle + "'";
                    sql_1 += ",'" + header[i].CountryOfOrigin + "'";
                    sql_1 += ",'" + header[i].CountryDestination + "'";
                    sql_1 += ",'" + header[i].UpdateSource + "'";
                    sql_1 += ",'" + header[i].Type + "'";
                    sql_1 += ",'" + header[i].OrderGroup + "'";
                    sql_1 += ",'" + header[i].Door + "'";
                    sql_1 += ",'" + header[i].Route + "'";
                    sql_1 += ",'" + header[i].Stop + "'";
                    sql_1 += ",'" + header[i].Notes + "'";
                    sql_1 += ",'" + header[i].EffectiveDate + "'";
                    sql_1 += ",'" + header[i].ContainerType + "'";
                    sql_1 += ",'" + header[i].ContainerQty + "'";
                    sql_1 += ",'" + header[i].BilledContainerQty + "'";
                    sql_1 += ",'" + header[i].SOStatus + "'";
                    sql_1 += ",'" + header[i].MBOLKey + "'";
                    sql_1 += ",'" + header[i].InvoiceNo + "'";
                    sql_1 += ",'" + header[i].InvoiceAmount + "'";
                    sql_1 += ",'" + header[i].Salesman + "'";
                    sql_1 += ",'" + header[i].GrossWeight + "'";
                    sql_1 += ",'" + header[i].WgtUnit + "'";
                    sql_1 += ",'" + header[i].Capacity + "'";
                    sql_1 += ",'" + header[i].CubeUnit + "'";
                    sql_1 += ",'" + header[i].PrintFlag + "'";
                    sql_1 += ",'" + header[i].LoadKey + "'";
                    sql_1 += ",'" + header[i].Rdd + "'";
                    sql_1 += ",'" + header[i].Notes2 + "'";
                    sql_1 += ",'" + header[i].SequenceNo + "'";
                    sql_1 += ",'" + header[i].Rds + "'";
                    sql_1 += ",'" + header[i].SectionKey + "'";
                    sql_1 += ",'" + header[i].Facility + "'";
                    sql_1 += ",'" + header[i].PrintDocDate + "'";
                    sql_1 += ",'" + header[i].LabelPrice + "'";
                    sql_1 += ",'" + header[i].POKey + "'";
                    sql_1 += ",'" + header[i].ExternPOKey + "'";
                    sql_1 += ",'" + header[i].XDockFlag + "'";
                    sql_1 += ",'" + header[i].UserDefine01 + "'";
                    sql_1 += ",'" + header[i].UserDefine02 + "'";
                    sql_1 += ",'" + header[i].UserDefine03 + "'";
                    sql_1 += ",'" + header[i].UserDefine04 + "'";
                    sql_1 += ",'" + header[i].UserDefine05 + "'";
                    sql_1 += ",'" + header[i].UserDefine06 + "'";
                    sql_1 += ",'" + header[i].UserDefine07 + "'";
                    sql_1 += ",'" + header[i].UserDefine08 + "'";
                    sql_1 += ",'" + header[i].UserDefine09 + "'";
                    sql_1 += ",'" + header[i].UserDefine10 + "'";
                    sql_1 += ",'" + header[i].Issued + "'";
                    sql_1 += ",'" + header[i].DeliveryNote + "'";
                    sql_1 += ",'" + header[i].PODCust + "'";
                    sql_1 += ",'" + header[i].PODArrive + "'";
                    sql_1 += ",'" + header[i].PODReject + "'";
                    sql_1 += ",'" + header[i].PODUser + "'";
                    sql_1 += ",'" + header[i].xdockpokey + "'";
                    sql_1 += ",'" + header[i].SpecialHandling + "'";
                    sql_1 += ",'" + header[i].RoutingTool + "'";
                    sql_1 += ",'" + header[i].MarkforKey + "'";
                    sql_1 += ",'" + header[i].M_Contact1 + "'";
                    sql_1 += ",'" + header[i].M_Contact2 + "'";
                    sql_1 += ",'" + header[i].M_Company + "'";
                    sql_1 += ",'" + header[i].M_Address1 + "'";
                    sql_1 += ",'" + header[i].M_Address2 + "'";
                    sql_1 += ",'" + header[i].M_Address3 + "'";
                    sql_1 += ",'" + header[i].M_Address4 + "'";
                    sql_1 += ",'" + header[i].M_City + "'";
                    sql_1 += ",'" + header[i].M_State + "'";
                    sql_1 += ",'" + header[i].M_Zip + "'";
                    sql_1 += ",'" + header[i].M_Country + "'";
                    sql_1 += ",'" + header[i].M_ISOCntryCode + "'";
                    sql_1 += ",'" + header[i].M_Phone1 + "'";
                    sql_1 += ",'" + header[i].M_Phone2 + "'";
                    sql_1 += ",'" + header[i].M_Fax1 + "'";
                    sql_1 += ",'" + header[i].M_Fax2 + "'";
                    sql_1 += ",'" + header[i].M_vat + "'";
                    sql_1 += ",'" + header[i].C_State_Long + "'";

                    sql_1 += ",'0'";
                    sql_1 += ",Null";
                    sql_1 += ",'" + DateTime.Now + "'";
                    sql_1 += ",Null";
                    sql_1 += ") SELECT @@IDENTITY AS Inbound_ORDHD;";
                    int id_1 = this.ScanExecuteNonQueryRID(sql_1);
                    if (id_1 > 0)
                    {
                        string type = "";
                        if (header[i].Type == "SH")
                        {
                            type = "销售订单";
                        }
                        else
                        {
                            type = "转仓订单";
                        }
                        #region 原始数据新增成功之后,wms主订单赋值
                        PreOrder preOrders = new PreOrder();
                        preOrders.ExternOrderNumber = header[i].ExternOrderKey;//外部单号
                        preOrders.CustomerID = 108;//客户ID
                        preOrders.CustomerName = "PUMA_SH";//客户名称
                        preOrders.Warehouse = "PUMA上海仓";//仓库名称
                        preOrders.OrderType = type;//出库类型Type 销售订单 转仓订单
                        preOrders.Status = 1;//订单状态
                        preOrders.OrderTime = DateTime.Now;//订单时间
                        preOrders.DetailCount = 0;//明细行数
                        preOrders.Creator = "API";//创建人
                        preOrders.CreateTime = DateTime.Now;//创建时间
                        preOrders.str4 = "PUMA";//默认
                        List<PreOrder> pres = new List<PreOrder>();
                        pres.Add(preOrders);
                        PreOrderList = pres;
                        #endregion

                        List<PreOrderDetail> detaillist = new List<PreOrderDetail>();
                        for (int m = 0; m < details.Count(); m++)
                        {
                            string goodtype = "";
                            if (header[i].Facility == "D6001")
                            {
                                goodtype = "A品";
                            }
                            else if (header[i].Facility == "D7001")
                            {
                                goodtype = "C品";
                            }
                            else if (header[i].Facility == "D7002")
                            {
                                goodtype = "D品";
                            }
                            else
                            {
                                goodtype = header[i].Facility;
                            }
                            if (header[i].ExternOrderKey == details[m].ExternOrderKey)
                            {
                                #region wms明细订单赋值
                                PreOrderDetail detail = new PreOrderDetail();
                                detail.ExternOrderNumber = details[m].ExternOrderKey;//外部单号
                                detail.CustomerID = 108;
                                detail.CustomerName = "PUMA_SH";
                                detail.LineNumber = details[m].ExternLineNo;
                                detail.WarehouseId = 3;//
                                detail.Warehouse = "PUMA上海仓";
                                detail.SKU = EANQuery(details[m].Sku);
                                detail.GoodsName = detail.SKU;
                                detail.GoodsType = goodtype;//Facility 6001 7001 7002
                                detail.OriginalQty = details[m].OpenQty;//
                                detail.Creator = "API";//
                                detail.CreateTime = DateTime.Now;//
                                detaillist.Add(detail);
                                #endregion

                                string sql_2 = "INSERT INTO Inbound_ORDDT VALUES('" + id_1 + "'";
                                sql_2 += ",'" + details[m].HeaderFlag + "'";
                                sql_2 += ",'" + details[m].InterfaceActionFlag + "'";
                                sql_2 += ",'" + details[m].OrderLineNumber + "'";
                                sql_2 += ",'" + details[m].OrderDetailSysId + "'";
                                sql_2 += ",'" + details[m].ExternOrderKey + "'";
                                sql_2 += ",'" + details[m].ExternLineNo + "'";
                                sql_2 += ",'" + details[m].Sku + "'";
                                sql_2 += ",'" + details[m].StorerKey + "'";
                                sql_2 += ",'" + details[m].ManufacturerSku + "'";
                                sql_2 += ",'" + details[m].RetailSku + "'";
                                sql_2 += ",'" + details[m].AltSku + "'";
                                sql_2 += ",'" + details[m].OriginalQty + "'";
                                sql_2 += ",'" + details[m].OpenQty + "'";
                                sql_2 += ",'" + details[m].ShippedQty + "'";
                                sql_2 += ",'" + details[m].AdjustedQty + "'";
                                sql_2 += ",'" + details[m].QtyPreAllocated + "'";
                                sql_2 += ",'" + details[m].QtyAllocated + "'";
                                sql_2 += ",'" + details[m].QtyPicked + "'";
                                sql_2 += ",'" + details[m].UOM + "'";
                                sql_2 += ",'" + details[m].PackKey + "'";
                                sql_2 += ",'" + details[m].PickCode + "'";
                                sql_2 += ",'" + details[m].CartonGroup + "'";
                                sql_2 += ",'" + details[m].Lot + "'";
                                sql_2 += ",'" + details[m].ID + "'";
                                sql_2 += ",'" + details[m].Facility + "'";
                                sql_2 += ",'" + details[m].Status + "'";
                                sql_2 += ",'" + details[m].UnitPrice + "'";
                                sql_2 += ",'" + details[m].Tax01 + "'";
                                sql_2 += ",'" + details[m].Tax02 + "'";
                                sql_2 += ",'" + details[m].ExtendedPrice + "'";
                                sql_2 += ",'" + details[m].Reserved + "'";
                                sql_2 += ",'" + details[m].UpdateSource + "'";
                                sql_2 += ",'" + details[m].Lottable01 + "'";
                                sql_2 += ",'" + details[m].Lottable02 + "'";
                                sql_2 += ",'" + details[m].Lottable03 + "'";
                                sql_2 += ",'" + details[m].Lottable04 + "'";
                                sql_2 += ",'" + details[m].Lottable05 + "'";
                                sql_2 += ",'" + details[m].EffectiveDate + "'";
                                sql_2 += ",'" + details[m].TariffKey + "'";
                                sql_2 += ",'" + details[m].FreeGoodQty + "'";
                                sql_2 += ",'" + details[m].GrossWeight + "'";
                                sql_2 += ",'" + details[m].WgtUnit + "'";
                                sql_2 += ",'" + details[m].Capacity + "'";
                                sql_2 += ",'" + details[m].CubeUnit + "'";
                                sql_2 += ",'" + details[m].LoadKey + "'";
                                sql_2 += ",'" + details[m].MBOLKey + "'";
                                sql_2 += ",'" + details[m].QtyToProcess + "'";
                                sql_2 += ",'" + details[m].MinShelfLife + "'";
                                sql_2 += ",'" + details[m].UserDefine01 + "'";
                                sql_2 += ",'" + details[m].UserDefine02 + "'";
                                sql_2 += ",'" + details[m].UserDefine03 + "'";
                                sql_2 += ",'" + details[m].UserDefine04 + "'";
                                sql_2 += ",'" + details[m].UserDefine05 + "'";
                                sql_2 += ",'" + details[m].UserDefine06 + "'";
                                sql_2 += ",'" + details[m].UserDefine07 + "'";
                                sql_2 += ",'" + details[m].UserDefine08 + "'";
                                sql_2 += ",'" + details[m].UserDefine09 + "'";
                                sql_2 += ",'" + details[m].POkey + "'";
                                sql_2 += ",'" + details[m].ExternPOKey + "'";
                                sql_2 += ",'" + details[m].OrgExternLineNo + "'";

                                sql_2 += ",'" + DateTime.Now + "'";
                                sql_2 += ",Null";
                                sql_2 += ") SELECT @@IDENTITY AS Inbound_ORDDT;";

                                int id_2 = this.ScanExecuteNonQueryRID(sql_2);
                                if (id_2 < 0)
                                {
                                    LogHelper.WriteLog(typeof(string), "Order-GetInbound_ORDHD数据写入错误[" + Error + "]:" + sql_2, LogHelper.LogLevel.Error);
                                    msg = "Order-GetInbound_ORDHD数据写入错误[" + Error + "]";
                                    return msg;
                                }
                            }
                        }
                        PreDetail = detaillist;
                        int isresult;
                        string wmsmsg = AddPreOrderAndPreOrderDetail(PreOrderList, PreDetail, out isresult);
                        if (isresult != 200)
                        {
                            LogHelper.WriteLog(typeof(string), "Order-GetInbound_ORDHD入库单写入WMS失败[" + Error + "]:" + wmsmsg, LogHelper.LogLevel.Error);
                            msg = "Order-GetInbound_ORDHD入库单写入WMS失败[" + Error + "]";
                            return msg;
                        }
                    }
                    else
                    {
                        LogHelper.WriteLog(typeof(string), "Order-GetInbound_ORDHD数据写入错误[" + Error + "]:" + sql_1, LogHelper.LogLevel.Error);
                        msg = "Order-GetInbound_ORDHD数据写入错误[" + Error + "]";
                        return msg;
                    }
                }
                msg = "200";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "Order-GetInbound_ORDHD数据写入错误[" + Error + "]:" + ex.Message, LogHelper.LogLevel.Error);
            }
            return msg;
        }

        //wms 写入asn数据
        public string AddasnAndasnDetail(AddASNandASNDetailRequest rece, out int msg)
        {

            using (SqlConnection conn = new SqlConnection(BaseAccessor._dataBase.ConnectionString))
            {
                string message = "";
                msg = 400;
                try
                {
                    SqlCommand cmd = new SqlCommand("Proc_WMS_AddASNANDASNDetali", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Asn", rece.asn.Select(ASN => new WMSASNToDb(ASN)));
                    cmd.Parameters[0].SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.AddWithValue("@AsnDetali", rece.asnDetails.Select(asnDetali => new WMSASNDetailToDb(asnDetali)));
                    cmd.Parameters[1].SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.Parameters[2].SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters[2].Direction = ParameterDirection.Output;
                    cmd.Parameters[2].Size = 500;
                    cmd.CommandTimeout = 300;
                    conn.Open();

                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    message = sda.SelectCommand.Parameters["@message"].Value.ToString();
                    conn.Close();
                    if (message.IndexOf("添加成功") > -1)
                    {
                        msg = 200;
                    }
                    else
                    {
                        LogHelper.WriteLog(typeof(string), "AddasnAndasnDetail执行错误:" + message, LogHelper.LogLevel.Error);
                    }
                    return message;
                }
                catch (Exception ex)
                {
                    msg = 400;
                    throw ex;
                }
            }
        }

        //wms 预出库订单写入数据
        public string AddPreOrderAndPreOrderDetail(IEnumerable<PreOrder> PreOrderList, IEnumerable<PreOrderDetail> PreDetail, out int msg)
        {
            using (SqlConnection conn = new SqlConnection(BaseAccessor._dataBase.ConnectionString))
            {
                msg = 400;
                try
                {
                    string message = "";
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Proc_WMS_AddPreOrderANDPreOrderDetali", conn);//默认
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Po", PreOrderList.Select(p => new WMSPreOrderInfoToDb(p)));
                    cmd.Parameters[0].SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.AddWithValue("@Pod", PreDetail.Select(p => new WMSPreOrderDetailInfoToDb(p)));
                    cmd.Parameters[1].SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.AddWithValue("@Creator", "API");
                    cmd.Parameters[2].SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.Parameters[3].SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters[3].Direction = ParameterDirection.Output;
                    cmd.Parameters[3].Size = 500;
                    cmd.CommandTimeout = 300;//超时时间
                    conn.Open();

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    message = sda.SelectCommand.Parameters["@message"].Value.ToString();
                    conn.Close();
                    if (message == "")
                    {
                        msg = 200;
                    }
                    else
                    {
                        LogHelper.WriteLog(typeof(string), "AddPreOrderAndPreOrderDetail执行错误:" + message, LogHelper.LogLevel.Error);
                    }
                    return message;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 反馈入库完成的接口
        /// </summary>
        /// <returns></returns>
        public string wms_receipt()
        {
            string msg = string.Empty;
            try
            {
                //查询所有入库完成的单号  查询条件 STATUS=0，ExternReceiptNumber未反馈
                string sqllist = " SELECT * FROM wms_receipt WHERE STATUS=9 AND ExternReceiptNumber IN(SELECT ExternReceiptKey FROM Inbound_ASNHD WHERE ISReturn='0')";
                DataTable ReceiptCount = this.ExecuteDataTableBySqlString(sqllist);
                if (ReceiptCount.Rows.Count > 0)//有需要反馈的 入库订单
                {
                    for (int i = 0; i < ReceiptCount.Rows.Count; i++)
                    {

                        //经销商 门店入库 反馈查询
                        if (ReceiptCount.Rows[i]["ReceiptType"].ToString() == "经销商入库" || ReceiptCount.Rows[i]["ReceiptType"].ToString() == "门店入库")
                        {
                            string sql1_hd = "SELECT Top 1 * FROM Inbound_ASNHD WHERE ExternReceiptKey='" + ReceiptCount.Rows[i]["ExternReceiptKey"].ToString() + "' AND ISReturn='0'";
                            string sql1_dt = "SELECT * FROM Inbound_ASNDT WHERE ExternReceiptKey='" + ReceiptCount.Rows[i]["ExternReceiptKey"].ToString() + "'";
                            DataTable data1_hd = this.ExecuteDataTableBySqlString(sql1_hd);
                            DataTable data1_dt = this.ExecuteDataTableBySqlString(sql1_dt);
                            if (data1_hd.Rows.Count > 0 && data1_dt.Rows.Count > 0)
                            {
                                string istrue = "";//是否创建成功
                                string txtaddress = Create_RECHD_TXT1(data1_hd, data1_dt, out istrue);
                                if (istrue == "200")//创建成功 更新状态
                                {
                                    string upstr = "UPDATE Inbound_ASNHD SET ISReturn=1,ReturnDate=GETDATE() WHERE ID='" + ReceiptCount.Rows[i]["ID"].ToString() + "'";
                                    int upid = this.ScanExecuteNonQueryRID(upstr);
                                    if (upid > 0)
                                    {
                                        //反馈更新成功
                                    }
                                    else
                                    {
                                        LogHelper.WriteLog(typeof(string), "wms_receipt反馈入库更新接口失败:" + upstr, LogHelper.LogLevel.Error);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "wms_receipt反馈入库接口错误:" + msg, LogHelper.LogLevel.Error);
            }
            return msg;
        }

        /// <summary>
        /// 生成入库反馈文件   门店经销商
        /// </summary>
        /// <param name="hd">订单头部</param>
        /// <param name="dt">订单详细</param>
        /// <returns></returns>
        public string Create_RECHD_TXT1(DataTable hd, DataTable dt, out string msg)
        {
            Thread.Sleep(1000);
            string txtaddress = string.Empty;
            try
            {
                //string sql1_hd = "SELECT Top 1 * FROM Inbound_ASNHD WHERE ExternReceiptKey='3400559973' AND ISReturn='0'";
                //string sql1_dt = "SELECT * FROM Inbound_ASNDT WHERE ExternReceiptKey='3400559973'";
                //hd = this.ExecuteDataTableBySqlString(sql1_hd);
                //dt = this.ExecuteDataTableBySqlString(sql1_dt);

                string dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = Path.GetFullPath("..");
                dir = Path.GetFullPath("../..");
                string filepath = dir + "/UploadFile";     //文件路径
                if (Directory.Exists(filepath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename = "WMSREC_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                txtaddress = filepath;

                FileStream file = new FileStream(filepath + "/" + filename, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine("WMSREC    O " + DateTime.Now.ToString("yyyyMMddhhmmss") + "PUMA                CN   Receipt Outbound                                  ");
                string header = "RECHD";
                header += "A";
                header += "".TxtStrPush(10);
                header += hd.Rows[0]["ExternReceiptKey"].ToString().TxtStrPush(20);
                header += "".TxtStrPush(20);
                header += hd.Rows[0]["StorerKey"].ToString().TxtStrPush(15);
                header += DateTime.Now.ToString("yyyyMMddhhmmss").TxtStrPush(14);
                header += hd.Rows[0]["CarrierKey"].ToString().TxtStrPush(15);
                header += hd.Rows[0]["CarrierName"].ToString().TxtStrPush(30);
                header += "".TxtStrPush(45);
                header += "".TxtStrPush(45);
                header += "".TxtStrPush(45);
                header += "".TxtStrPush(2);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(18);
                header += "".TxtStrPush(125);
                header += "".TxtStrPush(14);
                header += "".TxtStrPush(20);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(10);
                header += hd.Rows[0]["Facility"].ToString().TxtStrPush(5);
                header += "".TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".TxtStrPush(1);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(1);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(14);
                header += "".TxtStrPush(14);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(30);
                header += "".TxtStrPush(1);
                writer.WriteLine(header);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dtstr = "RECDTA";
                    dtstr += "A";
                    dtstr += dt.Rows[i]["ExternReceiptKey"].ToString().TxtStrPush(20);
                    dtstr += dt.Rows[i]["ExternLineNo"].ToString().TxtStrPush(20);
                    dtstr += dt.Rows[i]["StorerKey"].ToString().TxtStrPush(15);
                    dtstr += dt.Rows[i]["Sku"].ToString().TxtStrPush(20);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(10);
                    dtstr += dt.Rows[i]["QtyReceived"].ToString().TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(14);
                    dtstr += DateTime.Now.ToString("yyyyMMddhhmmss").TxtStrPush(14);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(16);
                    dtstr += "".TxtStrPush(16);
                    dtstr += "".TxtStrPush(16);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(30);
                    dtstr += "".TxtStrPush(30);
                    dtstr += dt.Rows[i]["UserDefine03"].ToString().TxtStrPush(30);
                    dtstr += dt.Rows[i]["UserDefine04"].ToString().TxtStrPush(30);
                    dtstr += "".TxtStrPush(30);
                    dtstr += "".TxtStrPush(14);
                    dtstr += "".TxtStrPush(14);
                    dtstr += "".TxtStrPush(30);
                    dtstr += "".TxtStrPush(30);
                    dtstr += "".TxtStrPush(30);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(5);
                    dtstr += "".TxtStrPush(5);
                    dtstr += "".TxtStrPush(10);
                    writer.WriteLine(dtstr);
                }
                //结束标识
                string fotstr = "RECTR" + (dt.Rows.Count + 1).ToString().PadLeft(10, '0');
                writer.WriteLine(fotstr);

                writer.Close();
                file.Close();
                bool issuccess = sFTP.PUMAPut(filepath, filename, sftpfilepath);
                if (issuccess)
                {
                    msg = "200";
                    //入库反馈成功之后 执行品级自动调整
                    CreatIQC(hd.Rows[0]["ExternReceiptKey"].ToString());
                }
                else
                {
                    msg = "500";
                    LogHelper.WriteLog(typeof(string), "Create_RECHD_TXT1入库文件上传失败[" + hd.Rows[0]["ExternReceiptKey"].ToString() + "]:" + msg, LogHelper.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "Create_RECHD_TXT1生成入库文件失败[" + hd.Rows[0]["ExternReceiptKey"].ToString() + "]:" + msg, LogHelper.LogLevel.Error);
            }
            return txtaddress;
        }

        /// <summary>
        /// 入库完成反馈之后根据 外部订单号查询 上架信息 自动生成品级调整单
        /// </summary>
        /// <returns></returns>
        public string CreatIQC(string ExternReceiptNumber)
        {
            string msg = string.Empty;
            try
            {
                string querstr = "SELECT * FROM [dbo].[WMS_ReceiptReceiving] WHERE CustomerID='108' AND ExternReceiptNumber='" + ExternReceiptNumber + "' AND GoodsType IN('C品','D品') AND (Int2 IS NULL OR Int2!=6)";
                DataTable ReceiptReceiving = this.ExecuteDataTableBySqlString(querstr);
                if (ReceiptReceiving.Rows.Count > 0)
                {
                    int CID = 0;
                    int DID = 0;
                    string CNumber = "";
                    string DNumber = "";
                    for (int i = 0; i < ReceiptReceiving.Rows.Count; i++)
                    {
                        if (ReceiptReceiving.Rows[i]["GoodsType"].ToString() == "C品")
                        {
                            if (CID == 0)
                            {
                                CNumber = "ADJ" + DateTime.Now.ToString("yyyyMMddhhmmss") + "C";
                                string hc = "INSERT INTO  WMS_Adjustment VALUES('" + CNumber + "',108,'PUMA_SH','PUMA上海仓',9,'库存品级调整单','入库完成系统根据WMS_ReceiptReceiving移库'";
                                hc += ", GETDATE(),0,'API',GETDATE(),NULL,NULL,'D6001品级C品区分',NULL,NULL,'PUMA'";
                                hc += ",NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)SELECT @@IDENTITY AS WMS_Adjustment;";
                                CID = this.ScanExecuteNonQueryRID(hc);
                            }
                            string sql_C = "";
                            sql_C += "INSERT INTO WMS_AdjustmentDetail VALUES(" + CID + ",'" + CNumber + "',108,'PUMA_SH',NULL,NULL,'" + ReceiptReceiving.Rows[i]["SKU"] + "',NULL,NULL,NULL,'" + ReceiptReceiving.Rows[i]["SKU"] + "','PUMA上海仓','PUMA上海仓'";
                            sql_C += ",NULL,NULL,NULL,NULL,NULL," + ReceiptReceiving.Rows[i]["QtyReceived"] + ",'A品','C品',NULL,NULL,0,NULL,'API',GETDATE(),NULL,NULL";
                            sql_C += ",NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)SELECT @@IDENTITY AS WMS_AdjustmentDetail;";
                            int thint = this.ScanExecuteNonQueryRID(sql_C);
                        }
                        if (ReceiptReceiving.Rows[i]["GoodsType"].ToString() == "D品")
                        {
                            if (DID == 0)
                            {
                                DNumber = "ADJ" + DateTime.Now.ToString("yyyyMMddhhmmss") + "D";
                                string hc = "INSERT INTO  WMS_Adjustment VALUES('" + DNumber + "',108,'PUMA_SH','PUMA上海仓',9,'库存品级调整单','入库完成系统根据WMS_ReceiptReceiving移库'";
                                hc += ", GETDATE(),0,'API',GETDATE(),NULL,NULL,'D6001品级D品区分',NULL,NULL,'PUMA'";
                                hc += ",NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)SELECT @@IDENTITY AS WMS_Adjustment;";
                                DID = this.ScanExecuteNonQueryRID(hc);
                            }
                            string sql_D = "";
                            sql_D += "INSERT INTO WMS_AdjustmentDetail VALUES(" + DID + ",'" + DNumber + "',108,'PUMA_SH',NULL,NULL,'" + ReceiptReceiving.Rows[i]["SKU"] + "',NULL,NULL,NULL,'" + ReceiptReceiving.Rows[i]["SKU"] + "','PUMA上海仓','PUMA上海仓'";
                            sql_D += ",NULL,NULL,NULL,NULL,NULL," + ReceiptReceiving.Rows[i]["QtyReceived"] + ",'A品','D品',NULL,NULL,0,NULL,'API',GETDATE(),NULL,NULL";
                            sql_D += ",NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)SELECT @@IDENTITY AS WMS_AdjustmentDetail;";
                            int thint = this.ScanExecuteNonQueryRID(sql_D);
                        }
                    }



                    string upstr = "UPDATE [WMS_ReceiptReceiving] SET Int2='6' WHERE ExternReceiptNumber='" + ExternReceiptNumber + "'";
                    this.ScanExecuteNonQueryRID(upstr);
                    msg = "200";
                }
                else
                {
                    LogHelper.WriteLog(typeof(string), "CreatIQC接口外部单号[" + ExternReceiptNumber + "]没有查询到数据：" + querstr, LogHelper.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "CreatIQC接口错误[" + ExternReceiptNumber + "]:" + msg, LogHelper.LogLevel.Error);
            }
            return msg;
        }

        /// <summary>
        /// 订单出库  出库反馈
        /// </summary>
        /// <returns></returns>
        public string Create_SHPPK()
        {
            string msg = string.Empty;
            try
            {
                string Querstr = "SELECT * FROM [dbo].[WMS_Order] WHERE STATUS=9 AND ExternOrderNumber IN(SELECT ExternOrderKey FROM Inbound_ORDHD WHERE ISReturn=0)";
                DataTable OrderCount = this.ExecuteDataTableBySqlString(Querstr);
                if (OrderCount.Rows.Count > 0)//有需要反馈的 出库订单
                {
                    for (int i = 0; i < OrderCount.Rows.Count; i++)
                    {
                        string sql1 = "SELECT * FROM Inbound_ORDHD WHERE ExternOrderKey='" + OrderCount.Rows[i]["ExternOrderNumber"] + "'";
                        string sql2 = "SELECT * FROM Inbound_ORDDT WHERE ExternOrderKey='" + OrderCount.Rows[i]["ExternOrderNumber"] + "'";
                        DataTable hd = this.ExecuteDataTableBySqlString(sql1);
                        DataTable dt = this.ExecuteDataTableBySqlString(sql1);
                        string ispick = "";
                        string picktxt = Create_PICKTXT(OrderCount.Rows[i]["OrderNumber"].ToString(), hd, dt, out ispick);
                        if (ispick == "200")//拣货回传成功
                        {
                            string isshp = "";
                            string shptxt = Create_SHPTXT(hd, dt, out isshp);
                            if (isshp == "200")
                            {

                            }
                            else
                            {
                                LogHelper.WriteLog(typeof(string), "Create_SHPPK生成出库完成反馈文件失败[" + OrderCount.Rows[i]["ExternOrderNumber"] + "]:" + msg, LogHelper.LogLevel.Error);
                            }
                        }
                        else
                        {
                            LogHelper.WriteLog(typeof(string), "Create_SHPPK生成拣货反馈文件失败[" + OrderCount.Rows[i]["ExternOrderNumber"] + "]:" + msg, LogHelper.LogLevel.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "Create_SHPPK接口错误:" + msg, LogHelper.LogLevel.Error);
            }

            return msg;
        }

        //生成拣货反馈文件
        public string Create_PICKTXT(string WmsNo, DataTable hd, DataTable dt, out string msg)
        {
            string txtaddress = string.Empty;
            try
            {

                //string sql1_hd = "SELECT Top 1 * FROM Inbound_ORDHD WHERE ExternOrderKey='3400525580' AND ISReturn='0'";
                //string sql1_dt = "SELECT * FROM Inbound_ORDDT WHERE ExternOrderKey='3400525580'";
                //hd = this.ExecuteDataTableBySqlString(sql1_hd);
                //dt = this.ExecuteDataTableBySqlString(sql1_dt);

                string dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = Path.GetFullPath("..");
                dir = Path.GetFullPath("../..");
                string filepath = dir + "/UploadFile";     //文件路径
                if (Directory.Exists(filepath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename= "WMSSHP_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_PICK.txt";

                txtaddress = filepath;

                FileStream file = new FileStream(filepath+"/"+filename, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine("WMSSHP    O " + DateTime.Now.ToString("yyyyMMddhhmmss") + "PUMA                CN   Shipment Outbound                                 ");
                string header = "SHPHDA";
                header += hd.Rows[0]["StorerKey"].ToString().TxtStrPush(15);
                header += hd.Rows[0]["ExternOrderKey"].ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(125);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(125);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(1);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(1);
                header += DateTime.Now.ToString("yyyyMMddhhmmss").ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(125);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(2);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(16);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(16);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(16);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(2);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(2);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                writer.WriteLine(header);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string detstr = "SHPPKA";
                    detstr += dt.Rows[i]["StorerKey"].ToString().TxtStrPush(20);
                    detstr += dt.Rows[i]["ExternOrderKey"].ToString().TxtStrPush(20);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(1);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(20);
                    detstr += "".TxtStrPush(5);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(10);
                    detstr += dt.Rows[i]["SKU"].ToString().TxtStrPush(20);
                    detstr += "".TxtStrPush(10);
                    detstr += dt.Rows[i]["OpenQty"].ToString().TxtStrPush(10);
                    detstr += WmsNo.TxtStrPush(20);
                    detstr += "".TxtStrPush(18);
                    detstr += DateTime.Now.ToString("yyyyMMddhhmmss").ToString().TxtStrPush(14);
                    detstr += "".TxtStrPush(30);
                    writer.WriteLine(detstr);
                }
                //结束标识
                string fotstr = "SHPTR" + (dt.Rows.Count + 1).ToString().PadLeft(10, '0');
                writer.WriteLine(fotstr);

                writer.Close();
                file.Close();
                bool issuccess = sFTP.PUMAPut(filepath, filename, sftpfilepath);
                if (issuccess)
                {
                    msg = "200";
                }
                else
                {
                    msg = "500";
                    LogHelper.WriteLog(typeof(string), "Create_PICKTX拣货文件上传失败[" + hd.Rows[0]["ExternOrderKey"].ToString() + "]:" + msg, LogHelper.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "Create_PICKTXT生成拣货文件失败[" + hd.Rows[0]["ExternOrderKey"].ToString() + "]:" + msg, LogHelper.LogLevel.Error);
            }
            return txtaddress;
        }

        //生成订单出库文件
        public string Create_SHPTXT(DataTable hd, DataTable dt, out string msg)
        {
            string txtaddress = string.Empty;
            try
            {

                //string sql1_hd = "SELECT Top 1 * FROM Inbound_ORDHD WHERE ExternOrderKey='3400525580' AND ISReturn='0'";
                //string sql1_dt = "SELECT * FROM Inbound_ORDDT WHERE ExternOrderKey='3400525580'";
                //hd = this.ExecuteDataTableBySqlString(sql1_hd);
                //dt = this.ExecuteDataTableBySqlString(sql1_dt);

                string dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = Path.GetFullPath("..");
                dir = Path.GetFullPath("../..");
                string filepath = dir + "/UploadFile";     //文件路径
                if (Directory.Exists(filepath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename = "WMSSHP_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_SHP.txt";
   
                txtaddress = filepath;

                FileStream file = new FileStream(filepath+"/"+ filename, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine("WMSSHP    O " + DateTime.Now.ToString("yyyyMMddhhmmss") + "PUMA                CN   Shipment Outbound                                 ");
                string header = "SHPHDA";
                header += hd.Rows[0]["StorerKey"].ToString().TxtStrPush(15);
                header += hd.Rows[0]["ExternOrderKey"].ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(125);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(125);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(1);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(1);
                header += DateTime.Now.ToString("yyyyMMddhhmmss").ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(125);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(20);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(14);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(2);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(16);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(16);
                header += "".ToString().TxtStrPush(5);
                header += "".ToString().TxtStrPush(16);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(2);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(15);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(45);
                header += "".ToString().TxtStrPush(2);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(30);
                header += "".ToString().TxtStrPush(10);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                header += "".ToString().TxtStrPush(18);
                writer.WriteLine(header);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string detstr = "SHPDTA";
                    detstr += dt.Rows[i]["ExternOrderKey"].ToString().TxtStrPush(20);
                    detstr += dt.Rows[i]["ExternLineNo"].ToString().TxtStrPush(10);
                    detstr += dt.Rows[i]["Sku"].ToString().TxtStrPush(20);
                    detstr += "".TxtStrPush(20);
                    detstr += "".TxtStrPush(20);
                    detstr += "".TxtStrPush(20);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(14);
                    detstr += "".TxtStrPush(14);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(18);
                    detstr += "".TxtStrPush(20);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(30);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(20);
                    detstr += "".TxtStrPush(10);
                    detstr += "".TxtStrPush(5);
                    detstr += "".TxtStrPush(5);
                    detstr += "".TxtStrPush(10);
                    writer.WriteLine(detstr);
                }
                //结束标识
                string fotstr = "SHPTR" + (dt.Rows.Count + 1).ToString().PadLeft(10, '0');
                writer.WriteLine(fotstr);

                writer.Close();
                file.Close();
                bool issuccess = sFTP.PUMAPut(filepath, filename, sftpfilepath);
                if (issuccess)
                {
                    msg = "200";
                }
                else
                {
                    msg = "500";
                    LogHelper.WriteLog(typeof(string), "Create_SHPTXT出库反馈文件上传失败[" + hd.Rows[0]["ExternOrderKey"].ToString() + "]:" + msg, LogHelper.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "Create_SHPTXT生成出库反馈文件失败[" + hd.Rows[0]["ExternOrderKey"].ToString() + "]:" + msg, LogHelper.LogLevel.Error);
            }
            return txtaddress;
        }

        /// <summary>
        /// 生成库存快照 并且生成txt
        /// </summary>
        /// <returns></returns>
        public string WMSInventory()
        {
            string msg = string.Empty;
            try
            {
                string execstr = "EXEC [pro_wms_InventoryPUMA]";
                this.ExecuteDataTableBySqlString(execstr);

                string querstr = "SELECT * FROM WMS_InventoryPUMA WHERE Str10 IS NULL";
                DataTable INV = this.ExecuteDataTableBySqlString(querstr);

                if (INV.Rows.Count > 0)
                {
                    string istrue = "";
                    string txtaddress = TxtWMSInventory(INV, out istrue);
                    if (istrue == "200")
                    {

                    }
                    else
                    {
                        LogHelper.WriteLog(typeof(string), "WMSInventory生成库存快照失败[" + DateTime.Now.ToString() + "]", LogHelper.LogLevel.Error);
                    }
                }
                else
                {
                    LogHelper.WriteLog(typeof(string), "WMSInventory查询库存表无数据[" + DateTime.Now.ToString() + "]:" + querstr, LogHelper.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "WMSInventory执行错误[" + DateTime.Now.ToString() + "]:" + msg, LogHelper.LogLevel.Error);
            }
            return msg;
        }

        /// <summary>
        /// 生成库存快照txt文件
        /// </summary>
        /// <param name="hd"></param>
        /// <param name="msg"></param>
        public string TxtWMSInventory(DataTable hd, out string msg)
        {
            string TxtAddress = string.Empty;
            try
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = Path.GetFullPath("..");
                dir = Path.GetFullPath("../..");
                string filepath = dir + "/UploadFile";     //文件路径
                if (Directory.Exists(filepath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename= "WMSSOH_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                TxtAddress = filepath;
                FileStream file = new FileStream(filepath+"/"+ filename, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine("WMSSOHO " + DateTime.Now.ToString("yyyyMMddhhmmss") + "PUMA                CN   SOH Outbound");
                for (int i = 0; i < hd.Rows.Count; i++)
                {
                    string content = "SOHDTA";
                    content += DateTime.Now.ToString("yyyyMMddhhmmss").TxtStrPush(14);
                    content += "PUMA".TxtStrPush(15);
                    if (hd.Rows[i]["GoodsType"].ToString() == "A品")
                    {
                        content += "D6001".TxtStrPush(5);
                    }
                    else if (hd.Rows[i]["GoodsType"].ToString() == "B品")
                    {
                        content += "D7001".TxtStrPush(5);
                    }
                    else if (hd.Rows[i]["GoodsType"].ToString() == "C品")
                    {
                        content += "D7001".TxtStrPush(5);
                    }
                    else
                    {
                        content += "".TxtStrPush(5);
                    }

                    content += "".TxtStrPush(10);
                    content += SKUQuery(hd.Rows[i]["SKU"].ToString()).TxtStrPush(20);
                    content += "".TxtStrPush(18);
                    content += "".TxtStrPush(18);
                    content += "".TxtStrPush(18);
                    content += "".TxtStrPush(18);
                    content += DateTime.Now.ToString("yyyyMMddhhmmss").TxtStrPush(14);
                    content += DateTime.Now.ToString("yyyyMMddhhmmss").TxtStrPush(14);
                    content += "".TxtStrPush(10);
                    string qty = hd.Rows[i]["Qty"].ToString();
                    qty = qty.Substring(0, qty.Length - 3);
                    qty = qty.PadLeft(10, '0');
                    content += qty.TxtStrPush(10);
                    content += "".TxtStrPush(10);
                    content += "".TxtStrPush(10);
                    content += "".TxtStrPush(10);
                    content += "".TxtStrPush(10);
                    content += "".TxtStrPush(30);
                    content += "".TxtStrPush(30);
                    content += "".TxtStrPush(30);
                    content += "".TxtStrPush(30);
                    content += "".TxtStrPush(30);
                    content += "".TxtStrPush(20);
                    content += "".TxtStrPush(20);
                    content += "".TxtStrPush(10);
                    content += "".TxtStrPush(5);
                    content += "".TxtStrPush(5);
                    writer.WriteLine(content);
                }
                string fotstr = "SOHTR" + (hd.Rows.Count + 1).ToString().PadLeft(10, '0');
                writer.WriteLine(fotstr);

                writer.Close();
                file.Close();
                bool issuccess = sFTP.PUMAPut(filepath, filename, sftpfilepath);
                if (issuccess)
                {
                    msg = "200";
                }
                else
                {
                    msg = "500";
                    LogHelper.WriteLog(typeof(string), "TxtWMSInventory库存快照文件上传失败:" + msg, LogHelper.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "TxtWMSInventory执行错误:" + msg, LogHelper.LogLevel.Error);
            }
            return TxtAddress;
        }


        /// <summary>
        /// 库存调整 反馈
        /// </summary>
        /// <returns></returns>
        public string WMSAdjustment()
        {
            string msg = string.Empty;
            try
            {
                //INT2=6 回传成功 INT2=2回传失败
                string sql_h = "SELECT * FROM [dbo].[WMS_Adjustment] WHERE CustomerID='108' AND  AdjustmentType IN('库存调整单', '库存品级调整单')AND Status = '9' AND(INT2 IS NULL OR INT2 != '6')";
                DataTable AdjustmentCount = this.ExecuteDataTableBySqlString(sql_h);
                if (AdjustmentCount.Rows.Count > 0)
                {
                    for (int i = 0; i < AdjustmentCount.Rows.Count; i++)
                    {

                        string sql_d = "SELECT * FROM [WMS_AdjustmentDetail] WHERE AID='" + AdjustmentCount.Rows[i]["ID"] + "'";
                        DataTable de = this.ExecuteDataTableBySqlString(sql_d);
                        if (de.Rows.Count > 0)
                        {
                            string isresult = "";
                            string txtaddress = TxtAdjustment(AdjustmentCount.Rows[i], de, out isresult);

                            if (isresult == "200")//生成文件 回传成功  更新状态
                            {
                                update_Adjustment(AdjustmentCount.Rows[i]["ID"].ToString(), "6");
                            }
                        }
                        else
                        {
                            LogHelper.WriteLog(typeof(string), "WMSAdjustment没有查询到明细信息:" + sql_d, LogHelper.LogLevel.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "WMSAdjustment执行错误:" + msg, LogHelper.LogLevel.Error);
            }
            return msg;
        }

        /// <summary>
        /// 生成库存调整文件
        /// </summary>
        /// <param name="hd"></param>
        /// <param name="dt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string TxtAdjustment(DataRow hd, DataTable dt, out string msg)
        {
            Thread.Sleep(1000);
            string TxtAddress = string.Empty;
            string type = hd["AdjustmentType"].ToString();//调整类型
            string from = Grade(dt.Rows[0]["FromGoodsType"].ToString()); //调整前 库区
            string to = Grade(dt.Rows[0]["ToGoodsType"].ToString());//调整后 库区
            try
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = Path.GetFullPath("..");
                dir = Path.GetFullPath("../..");
                string filepath = dir + "/UploadFile";     //文件路径
                string filename = "";
                if (Directory.Exists(filepath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(filepath);
                }
                if (type == "库存调整单")
                {
                    filename = "WMSITR_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_ADJ.txt";
                }
                else if (type == "库存品级调整单")
                {
                    filename = "WMSITR_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_IQC.txt";
                }
                else
                {
                    msg = "400";
                    LogHelper.WriteLog(typeof(string), "TxtAdjustment执行错误:调整单类型错误[AdjustmentType]", LogHelper.LogLevel.Error);
                    return "";
                }
                TxtAddress = filepath;
                FileStream file = new FileStream(filepath+"/"+ filename, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine("WMSITR    O " + DateTime.Now.ToString("yyyyMMddhhmmss") + "PUMA                CN   Inventory Transaction Outbound");
                string header = "ITRHDA";
                header += "".TxtStrPush(10);
                if (type == "库存调整单")
                {
                    header += "ADJ".TxtStrPush(3);
                }
                else
                {
                    header += "IQC".TxtStrPush(3);
                }
                header += hd["ID"].ToString().PadLeft(10, '0').TxtStrPush(10);//wms单号直接ID补0
                header += "PUMA".TxtStrPush(15);

                if (type == "库存调整单")
                {
                    header += from.TxtStrPush(5);
                    header += "".TxtStrPush(10);
                }
                else
                {
                    header += from.TxtStrPush(5);
                    header += to.TxtStrPush(10);
                }
                header += DateTime.Now.ToString("yyyyMMddhhmmss").TxtStrPush(14);
                header += "".TxtStrPush(10);
                header += hd["ID"].ToString().PadLeft(10, '0').TxtStrPush(10);//wms单号直接ID补0
                if (type == "库存调整单")
                {
                    header += "ADJ".TxtStrPush(3);
                }
                else
                {
                    header += "IQC".TxtStrPush(3);
                }
                header += "01".TxtStrPush(10);
                header += "".TxtStrPush(200);
                header += "".TxtStrPush(20);
                header += "".TxtStrPush(20);
                header += "".TxtStrPush(20);
                header += "".TxtStrPush(20);
                header += "".TxtStrPush(20);
                header += "".TxtStrPush(14);
                header += "".TxtStrPush(14);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(10);
                header += "".TxtStrPush(10);

                writer.WriteLine(header);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //如果同一个调整单 from  和 to明细又不一样，就需要打标识退出
                    if (from != Grade(dt.Rows[i]["FromGoodsType"].ToString()) || to != Grade(dt.Rows[0]["ToGoodsType"].ToString()))
                    {
                        string upstr = "UPDATE [WMS_Adjustment] SET Int2=2 WHERE ID='" + hd["ID"] + "'";
                        int upid = this.ScanExecuteNonQueryRID(upstr);
                        msg = "500";
                        return "";
                    }
                    string dtstr = "ITRDTA";
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(3);
                    if (type == "库存调整单")
                    {
                        dtstr += "ADJ".TxtStrPush(3);
                    }
                    else
                    {
                        dtstr += "IQC".TxtStrPush(3);
                    }
                    dtstr += dt.Rows[i]["ID"].ToString().PadLeft(10, '0').TxtStrPush(10);
                    dtstr += "".TxtStrPush(5);
                    dtstr += "PUMA".TxtStrPush(15);
                    dtstr += SKUQuery(dt.Rows[i]["SKU"].ToString()).TxtStrPush(20);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(18);

                    string qty = dt.Rows[i]["ToQty"].ToString();
                    qty = qty.Substring(0, qty.Length - 3);//去掉小数点 和后两位
                    if (Convert.ToInt32(qty) <= 0)
                    {//如果位-数就去掉-号
                        qty = qty.Replace("-", "");
                    }
                    qty = qty.PadLeft(10, '0');//根据格式左边不足位数补0

                    if (type == "库存调整单")
                    {
                        float sign = Convert.ToSingle(dt.Rows[i]["ToQty"]);
                        if (sign > 0)
                        {
                            dtstr += "+".TxtStrPush(5);
                        }
                        else
                        {
                            dtstr += "-".TxtStrPush(5);
                        }
                    }
                    else
                    {
                        dtstr += "+".TxtStrPush(5);
                    }
                    dtstr += qty.TxtStrPush(10);
                    dtstr += "".ToString().TxtStrPush(10);
                    dtstr += "".ToString().TxtStrPush(18);
                    dtstr += "".ToString().TxtStrPush(18);
                    dtstr += "".ToString().TxtStrPush(18);
                    dtstr += "".ToString().TxtStrPush(14);
                    dtstr += "".ToString().TxtStrPush(14);
                    dtstr += DateTime.Now.ToString("yyyyMMddhhmmss").TxtStrPush(14);
                    dtstr += "01".TxtStrPush(10);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(14);
                    dtstr += "".TxtStrPush(14);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(18);
                    dtstr += "".TxtStrPush(14);
                    dtstr += "".TxtStrPush(30);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(20);
                    dtstr += "".TxtStrPush(10);
                    dtstr += "".TxtStrPush(5);
                    dtstr += "".TxtStrPush(5);
                    writer.WriteLine(dtstr);
                }

                string fotstr = "ITRTR" + (dt.Rows.Count + 1).ToString().PadLeft(10, '0');
                writer.WriteLine(fotstr);

                writer.Close();
                file.Close();
                bool issuccess = sFTP.PUMAPut(filepath, filename, sftpfilepath);
                if (issuccess)
                {
                    msg = "200";
                }
                else
                {
                    msg = "500";
                    LogHelper.WriteLog(typeof(string), "TxtAdjustment库存调整文件上传失败:" + msg, LogHelper.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogHelper.WriteLog(typeof(string), "TxtAdjustment执行错误:" + msg, LogHelper.LogLevel.Error);
            }
            return TxtAddress;
        }

        //更新库存调整状态  6成功 2失败
        public string update_Adjustment(string ID, string Int2)
        {
            string upstr = "UPDATE [WMS_Adjustment] SET Int2=" + Int2 + " WHERE ID='" + ID + "'";
            int upid = this.ScanExecuteNonQueryRID(upstr);
            if (upid > 0)
            {

            }
            else
            {
                LogHelper.WriteLog(typeof(string), "update_Adjustment执行错误更新库存调整状态失败:" + upstr, LogHelper.LogLevel.Error);
            }
            return "";
        }

        /// <summary>
        /// 根据SKU查询EAN
        /// </summary>
        /// <param name="SKU"></param>
        /// <returns></returns>
        public string EANQuery(string SKU)
        {
            string EAN = "";
            string questr = "SELECT * FROM WMS_Product WHERE ManufacturerSKU='" + SKU + "'";
            DataTable SuccessCount = this.ExecuteDataTableBySqlString(questr);
            if (SuccessCount.Rows.Count > 0)
            {
                EAN = SuccessCount.Rows[0]["SKU"].ToString();
            }
            else
            {
                LogHelper.WriteLog(typeof(string), "EANQuery没有查询到对应EAN,查询条件ManufacturerSKU等于" + SKU + "]", LogHelper.LogLevel.Error);
            }
            return EAN;
        }

        /// <summary>
        /// 根据EAN查询SKU
        /// </summary>
        /// <param name="EAN"></param>
        /// <returns></returns>
        public string SKUQuery(string EAN)
        {
            string SKU = "";
            string questr = "SELECT * FROM WMS_Product WHERE SKU='" + EAN + "'";
            DataTable SuccessCount = this.ExecuteDataTableBySqlString(questr);
            if (SuccessCount.Rows.Count > 0)
            {
                SKU = SuccessCount.Rows[0]["ManufacturerSKU"].ToString();
            }
            else
            {
                LogHelper.WriteLog(typeof(string), "SKUQuery没有查询到对应SKU,查询条件EAN等于" + EAN + "]", LogHelper.LogLevel.Error);
            }
            return SKU;
        }

        /// <summary>
        /// 品级  和 库区的转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Grade(string str)
        {
            string value = "";
            switch (str)
            {
                case "A品":
                    value = "D6001";
                    break;
                case "C品":
                    value = "D7001";
                    break;
                case "D品":
                    value = "D7002";
                    break;
                case "D6001":
                    value = "A品";
                    break;
                case "D7001":
                    value = "C品";
                    break;
                case "D7002":
                    value = "D品";
                    break;
            }
            return value;
        }
    }
}
