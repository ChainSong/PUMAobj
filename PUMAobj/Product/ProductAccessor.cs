﻿using PUMAobj.Common;
using PUMAobj.Log;
using PUMAobj.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Product
{
    public class ProductAccessor : BaseAccessor
    {
        public string AddProduct(List<string> txtlists, out string externumber)
        {
            externumber = "";
            try
            {
                List<ProductModel> productModels = new List<ProductModel>();
                if (txtlists[0].TxtSubstring(1, 10) == "WMSSKU" && txtlists[0].TxtSubstring(11, 12) == "O")//PUMA SKU
                {
                    int linenumber = 1;
                    for (int i = 0; i < txtlists.Count; i++)
                    {
                        if (txtlists[i].TxtSubstring(1, 10) == "WMSSKU")//文档头
                        {
                            continue;
                        }
                        if (txtlists[i].TxtSubstring(1, 5) == "SKUDT" && txtlists[i].TxtSubstring(6, 6) == "A")//订单头
                        {
                            //string loadkey = txtlists[i].TxtSubstring(4166, 10);
                            //if (string.IsNullOrEmpty(loadkey))
                            //{
                            //    return ReturnTxtError("文档中存在LoadKey为空的订单");
                            //}
                            productModels.Add(new ProductModel
                            {
                                HeaderFlag = txtlists[i].TxtSubstring(1, 5),
                                InterfaceActionFlag = txtlists[i].TxtSubstring(6, 6),
                                StorerKey = txtlists[i].TxtSubstring(7, 21),
                                Sku = txtlists[i].TxtSubstring(22, 41),
                                DESCR = txtlists[i].TxtSubstring(42, 101),
                                SUSR1 = txtlists[i].TxtSubstring(102, 119),
                                SUSR2 = txtlists[i].TxtSubstring(120, 137),
                                SUSR3 = txtlists[i].TxtSubstring(138, 155),
                                SUSR4 = txtlists[i].TxtSubstring(156, 173),
                                SUSR5 = txtlists[i].TxtSubstring(174, 191),
                                MANUFACTURERSKU = txtlists[i].TxtSubstring(192, 211),
                                RETAILSKU = txtlists[i].TxtSubstring(212, 231),
                                ALTSKU = txtlists[i].TxtSubstring(232, 251),
                                PACKKey = txtlists[i].TxtSubstring(252, 261),
                                STDGROSSWGT = txtlists[i].TxtSubstring(262, 277),
                                STDNETWGT = txtlists[i].TxtSubstring(278, 293),
                                STDCUBE = txtlists[i].TxtSubstring(294, 309),
                                TARE = txtlists[i].TxtSubstring(310, 325),
                                CLASS = txtlists[i].TxtSubstring(326, 335),
                                ACTIVE = txtlists[i].TxtSubstring(336, 345),
                                SKUGROUP = txtlists[i].TxtSubstring(346, 355),
                                Style = txtlists[i].TxtSubstring(3053, 3072),
                                Color = txtlists[i].TxtSubstring(3073, 3082),
                                Size = txtlists[i].TxtSubstring(3083, 3087),


                            });
                        }
                    }
                }
                //订单获取结束之后，开始准备插入数据库
                if (productModels.Count > 0)
                {
                    //筛选已经插入数据库的SKU信息
                    StringBuilder sbCheck = new StringBuilder();
                    sbCheck.Append(" select SKU from WMS_Product where SKU in (");
                    sbCheck.Append(string.Join(",", productModels.Select(a => "'" + a.Sku + "'")));
                    var sbCheckData = this.ScanDataTable(sbCheck.ToString());
                    foreach (DataRow item in sbCheckData.Rows)
                    {
                        if (productModels.Where(a => a.Sku == item["SKU"].ToString()).Count() > 0)
                        {
                            productModels.RemoveAll(a => a.Sku == item["SKU"].ToString());
                        }
                    }

                    if (productModels.Count > 0)
                    {
                        StringBuilder sbBack = new StringBuilder();
                        StringBuilder sbProduct = new StringBuilder();
                        sbBack.Append(@" insert into WMS_Product ([StorerID]
                    ,[SKU]
                    ,[Status]
                    ,[GoodsName]
                    ,[GoodsType]
                    ,[SKUClassification]
                    ,[SKUGroup]
                    ,[Creator]
                    ,[CreateTime]
                    ,[Str5]
                    ,[Str9]
                    ,[Str10]
                    ,[Str11]
                    ,[Str12]
                    ) values  ");

                        int i = 0;
                        foreach (var item in productModels)
                        {
                            i++;
                            sbBack.Append("( 107," +
                                "'" + item.Sku + "'," +
                                "'1'," +
                                "'" + item.DESCR + "'," +
                                "'类型1'," +
                                "'组1'," +
                                "'dbo'," +
                                "'getdate()'," +
                                "'" + item.Size + "'," +
                                "'" + item.Size + "'," +
                                "'" + item.Size + "'," +
                                "'" + item.Style + "'," +
                                "'" + item.Color + "'," +
                                ")");
                            if (i > 500)
                            {
                                i = 0;
                                this.ScanExecuteNonQuery(sbBack.ToString());
                                sbBack = new StringBuilder();
                                sbBack.Append(@" insert into WMS_Product ([StorerID]
                            ,[SKU]
                            ,[Status]
                            ,[GoodsName]
                            ,[GoodsType]
                            ,[SKUClassification]
                            ,[SKUGroup]
                            ,[Creator]
                            ,[CreateTime]
                            ,[Str5]
                            ,[Str9]
                            ,[Str10]
                            ,[Str11]
                            ,[Str12]
                            ) values  ");
                                //sbBack=
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog(Type, ex);
                LogHelper.WriteLog(typeof(string), "AddProduct:" + ex.ToString(), LogHelper.LogLevel.Error);
                //throw;
            }
            //PreOrderDetail
            return "";
        }
    }
}
