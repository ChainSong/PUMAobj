using PUMAobj.Common;
using PUMAobj.Log;
using PUMAobj.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Product
{
    public class ProductAccessor : BaseAccessor
    {
        public string AddProduct(List<string> txtlists, out string externumber)
        {
            string msg = string.Empty;
            externumber = "";
            try
            {
                List<ProductModel> productModels = new List<ProductModel>();
                if (txtlists[0].TxtSubstring(1, 10) == "WMSSKU" && txtlists[0].TxtSubstring(11, 12) == "I")//PUMA SKU
                {
                    int linenumber = 1;
                    for (int i = 0; i < txtlists.Count; i++)
                    {
                        if (txtlists[i].TxtSubstring(1, 10) == "WMSSKU")//文档头
                        {
                            continue;
                        }
                        if (txtlists[i].TxtSubstring(1, 5) == "SKUDT")//订单头//&& txtlists[i].TxtSubstring(6, 6) == "A"我建议你忽略这个字段，仅用SKU作为BK
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
                                Price = txtlists[i].TxtSubstring(1073, 1088),
                                itemclass = txtlists[i].TxtSubstring(1434, 1443),
                                CaseCnt = txtlists[i].TxtSubstring(1714, 1443),
                                Style = txtlists[i].TxtSubstring(3053, 3072),
                                Color = txtlists[i].TxtSubstring(3073, 3082),
                                Size = txtlists[i].TxtSubstring(3083, 3087),
                                Gender = txtlists[i].TxtSubstring(3817, 3846),
                                RBU = txtlists[i].TxtSubstring(3847, 3886),
                                ProductLine = txtlists[i].TxtSubstring(3887, 3936),
                            });
                        }
                    }
                }
                //订单获取结束之后，开始准备插入数据库
                if (productModels.Count > 0)
                {

                    //筛选已经插入数据库的SKU信息我建议你忽略这个字段，仅用SKU作为BK(存在就更新，不存在就不更新)
                    //StringBuilder sbCheck = new StringBuilder();
                    //sbCheck.Append(" select SKU from WMS_Product where SKU in (");
                    //sbCheck.Append(string.Join(",", productModels.Select(a => "'" + a.MANUFACTURERSKU + "'")));
                    //var sbCheckData = this.ScanDataTable(sbCheck.ToString());
                    //foreach (DataRow item in sbCheckData.Rows)
                    //{
                    //    if (productModels.Where(a => a.MANUFACTURERSKU == item["SKU"].ToString()).Count() > 0)
                    //    {
                    //        productModels.RemoveAll(a => a.MANUFACTURERSKU == item["SKU"].ToString());
                    //    }
                    //}

                    if (productModels.Count > 0)
                    {
                        List<ProductStorerInfo> productStorerInfos = new List<ProductStorerInfo>();
                        ProductStorerInfo productStorerInfo = new ProductStorerInfo();
                        foreach (var item in productModels)
                        {
                            productStorerInfos.Add(new ProductStorerInfo()
                            {
                                StorerID = 108,
                                SKU = item.MANUFACTURERSKU.Trim(),
                                Status = 1,
                                GoodsName = item.DESCR,
                                GoodsType = 1,
                                SKUClassification = "类型1",
                                SKUGroup = "组1",
                                ManufacturerSKU = item.Sku.Trim(),
                                //Creator = "API",
                                //o.CreateTime = DateTime.Now,
                                Str2 = item.Price,
                                Str5 = item.Size,
                                Str8 = "01",
                                Str9 = item.Size,
                                Str10 = item.Style,
                                Str11 = item.Gender,
                                Str12 = item.SUSR1,
                                Str13 = item.ACTIVE,
                                Str14 = item.Size + item.Color,

                            });
                        }

                        using (SqlConnection conn = new SqlConnection(BaseAccessor._dataBase.ConnectionString))
                        {
                            string message = "";
                            SqlCommand cmd = new SqlCommand("[Proc_WMS_UpdateProduct]", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Productdata", productStorerInfos.Select(p => new ProductStorerInfoToDB(p)));
                            cmd.Parameters[0].SqlDbType = SqlDbType.Structured;
                            cmd.Parameters.AddWithValue("@message", message);
                            cmd.Parameters[1].SqlDbType = SqlDbType.NVarChar;
                            cmd.Parameters[1].Size = 10;
                            cmd.Parameters[1].Direction = ParameterDirection.Output;
                            cmd.CommandTimeout = 300;
                            conn.Open();

                            DataSet ds = new DataSet();
                            SqlDataAdapter sda = new SqlDataAdapter();
                            sda.SelectCommand = cmd;
                            sda.Fill(ds);
                            message = sda.SelectCommand.Parameters["@message"].Value.ToString();
                            conn.Close();
                            if (message != "有重复")
                            {
                                msg = "有重复";
                            }
                            //return new ProductStorerInfo();
                        }

                        //记录到备份表
                        StringBuilder sbBack = new StringBuilder();
                        StringBuilder sbProduct = new StringBuilder();
                        sbBack.Append(@" insert into [WMS_Product_PUMA] ( ,[HeaderFlag]
                        ,[Sku]
                        ,[DESCR]
                        ,[SUSR1]
                        ,[SUSR2]
                        ,[MANUFACTURERSKU]
                        ,[RETAILSKU]
                        ,[STDGROSSWGT]
                        ,[STDCUBE]
                        ,[SKUGROUP]
                        ,[Price]
                        ,[itemclass]
                        ,[CaseCnt]
                        ,[Style]
                        ,[Color]
                        ,[Size]
                        ,[Gender]
                        ,[RBU]
                        ,[ProductLine]
                        ) values  ");

                        int i = 0;
                        foreach (var item in productModels)
                        {
                            i++;
                            sbBack.Append("(" +
                                "'" + item.HeaderFlag + "'," +
                                "'" + item.Sku.Trim() + "'," +
                                "'" + item.DESCR.Trim('\'') + "'," +
                                "'" + item.SUSR1 + "'," +
                                "'" + item.SUSR2 + "'," +
                                "'" + item.MANUFACTURERSKU + "'," +
                                "'" + item.RETAILSKU + "'," +
                                "'" + item.STDGROSSWGT + "'," +
                                "'" + item.STDCUBE + "'," +
                                "'" + item.SKUGROUP + "'," +
                                "'" + item.Price + "'," +
                                "'" + item.itemclass + "'," +
                                "'" + item.CaseCnt + "'," +
                                "'" + item.Style + "'," +
                                "'" + item.Color + "'," +
                                "'" + item.Size + "'," +
                                "'" + item.Gender + "'," +
                                "'" + item.RBU + "'," +
                                "'" + item.ProductLine + "'," +

                                "),");
                            if (i > 500)
                            {
                                i = 0;
                                //s.Substring(0, s.Length - 1)
                                //this.ScanExecuteNonQuery(sbBack.ToString());
                                this.ScanExecuteNonQuery(sbBack.ToString().Substring(0, sbBack.ToString().Length - 1));

                                sbBack = new StringBuilder();
                                sbBack.Append(@" insert into [WMS_Product_PUMA] ( ,[HeaderFlag]
                                ,[Sku]
                                ,[DESCR]
                                ,[SUSR1]
                                ,[SUSR2]
                                ,[MANUFACTURERSKU]
                                ,[RETAILSKU]
                                ,[STDGROSSWGT]
                                ,[STDCUBE]
                                ,[SKUGROUP]
                                ,[Price]
                                ,[itemclass]
                                ,[CaseCnt]
                                ,[Style]
                                ,[Color]
                                ,[Size]
                                ,[Gender]
                                ,[RBU]
                                ,[ProductLine]
                                ) values  ");
                                //sbBack=
                            }
                        }

                        this.ScanExecuteNonQuery(sbBack.ToString().Substring(0, sbBack.ToString().Length - 1));
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                //LogHelper.WriteLog(Type, ex);
                LogHelper.WriteLog(typeof(string), "AddProduct:" + ex.ToString(), LogHelper.LogLevel.Error);
                //throw;
            }
            //PreOrderDetail
            return msg;
        }
    }
}
