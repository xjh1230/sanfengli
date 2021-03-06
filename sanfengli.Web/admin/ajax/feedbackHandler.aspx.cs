﻿using Common;
using sanfengli.Model.WeiXin;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin.ajax
{
    public partial class feedbackHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResponseModel responseModel = new ResponseModel();

            string op = RequestHelper.GetQueryString("op");
            try
            {
                switch (op)
                {
                    #region 编辑备注
                    case "edit":
                        int editId = RequestHelper.GetQueryInt("id", 0);
                        string remark = RequestHelper.GetQueryString("remark");
                        if (editId == 0)
                        {
                            responseModel.IsSuccess = false;
                            responseModel.Msg = "请选择";
                            return;
                        }
                        responseModel.IsSuccess = new Bll.WeChat.FeedBackBll().UpdateRemarkById(editId, remark);
                        responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
                        break;
                    #endregion
                    #region 删除
                    case "delete":
                        int id = RequestHelper.GetFormInt("id", 0);
                        if (id <= 0)
                        {
                            responseModel.IsSuccess = false;
                            responseModel.Msg = "请输入有效ID";
                        }
                        else
                        {
                            responseModel.IsSuccess = new Bll.WeChat.FeedBackBll().DeleteById(id);
                            responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
                        }
                        break;
                    #endregion
                    #region 添加分类
                    //case "addType":
                    //    string typeName = RequestHelper.GetFormString("typeName");
                    //    if (!string.IsNullOrWhiteSpace(typeName))
                    //    {
                    //        feedback_type model = new feedback_type();
                    //        model.Name = typeName;
                    //        responseModel.IsSuccess = new Bll.WeChat.feedback_typebll().SaveMode(model);
                    //        responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
                    //    }
                    //    else
                    //    {
                    //        responseModel.IsSuccess = false;
                    //        responseModel.Msg = "请输入有效值";
                    //    }


                    //    break;
                    #endregion
                    #region 编辑分类
                    case "editType":
                        string editTypeName = RequestHelper.GetFormString("typeName");
                        int editTypeId = RequestHelper.GetFormInt("typeId", 0);
                        if (!string.IsNullOrWhiteSpace(editTypeName))
                        {
                            feedback_type model = new feedback_type();
                            if (editTypeId > 0)
                            {
                                model.Id = editTypeId;
                            }
                            model.Name = editTypeName;
                            responseModel.IsSuccess = new Bll.WeChat.feedback_typebll().SaveMode(model);
                            responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
                        }
                        else
                        {
                            responseModel.IsSuccess = false;
                            responseModel.Msg = "请输入有效值";
                        }


                        break;
                    #endregion
                    #region 删除分类
                    case "deleteType":
                        int typeid = RequestHelper.GetFormInt("id", 0);
                        if (typeid <= 0)
                        {
                            responseModel.IsSuccess = false;
                            responseModel.Msg = "请输入有效ID";
                        }
                        else
                        {
                            responseModel.IsSuccess = new Bll.WeChat.feedback_typebll().DeleteById(typeid);
                            responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
                        }
                        break;
                        #endregion
                }
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Msg = "异常,请重试";
            }
            Response.Write(JsonHelper.Serialize(responseModel));
            Response.End();
        }
    }
}