using Common;
using sanfengli.Model.WeiXin;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bitauto.Mall.Aop;
using Newtonsoft.Json;
using sanfengli.Bll.WeChatMp;
using sanfengli.Bll.WeChat;
using sanfengli.Common;

namespace sanfengli.Web.home.ajax
{
    public partial class uploadHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResponseModel response = new ResponseModel();
            string op = RequestHelper.GetFormString("op");
            var user = new Model.WeiXin.wp_user();
            switch (op)
            {
                case "save":
                    #region 保存信息
                    try
                    {
                        string src = HttpUtility.UrlDecode(RequestHelper.GetFormString("src"));
                        string name = HttpUtility.UrlDecode(RequestHelper.GetFormString("name"));
                        string phone = HttpUtility.UrlDecode(RequestHelper.GetFormString("phone"));
                        string content = HttpUtility.UrlDecode(RequestHelper.GetFormString("content"));
                        string postion = HttpUtility.UrlDecode(RequestHelper.GetFormString("postion"));
                        string addr = HttpUtility.UrlDecode(RequestHelper.GetFormString("addr"));
                        int typeId = RequestHelper.GetFormInt("typeId", 1);
                        string typeName = HttpUtility.UrlDecode(RequestHelper.GetFormString("typeName"));
                        string openId = RequestHelper.GetFormString("openId");

                        int userId = new Bll.WeChat.wp_userbll().GetUserIdByOpenId(openId);

                        //LogHandler.Info(src+ content+openId+ userId);
                        if (string.IsNullOrEmpty(src) && string.IsNullOrEmpty(content))
                        {
                            response.IsSuccess = false;
                        }
                        else
                        {
                            feedback model = new feedback();
                            model.Content = content;
                            model.CreateOn = DateTime.Now;
                            model.Image = src;
                            model.UserId = userId;
                            model.name = name;
                            model.phone = phone;
                            model.addr = postion + " " + addr;
                            model.typeid = typeId;
                            model.typename = typeName;

                            response.IsSuccess = new FeedBackBll().SaveItem(model);
                        }

                        response.Msg = response.IsSuccess ? "成功" : "失败";
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Msg = "信息输入有误,只能输入文字和数字";
                        LogHandler.Error(ex);
                    }
                    #endregion
                    break;
                case "uploadImg":
                    #region 上传图片
                    string path = RequestHelper.GetFormString("path");
                    string requsetKey = "fileImage";
                    var result = ImageHelper.UpLoadImg(path, requsetKey, false);
                    response.IsSuccess = result[0] == "true";
                    response.Msg = result[1];
                    #endregion
                    break;
                case "joinvote":
                    #region 参加投票
                    try
                    {
                        string src = HttpUtility.UrlDecode(RequestHelper.GetFormString("src"));
                        string introduce = HttpUtility.UrlDecode(RequestHelper.GetFormString("introduce"));
                        string manifesto = HttpUtility.UrlDecode(RequestHelper.GetFormString("manifesto"));
                        string addr = HttpUtility.UrlDecode(RequestHelper.GetFormString("addr"));
                        int voteId = RequestHelper.GetFormInt("voteId", 0);
                        string phone = RequestHelper.GetFormString("phone");
                        string token = RequestHelper.GetFormString("token");
                        string openId = RequestHelper.GetFormString("openId");
                        user = new Bll.WeChat.wp_userbll().GetUserInfoByOpenId(openId);
                        int userId = user == null ? 0 : user.Id;

                        //LogHandler.Info(src+ content+openId+ userId);
                        if (string.IsNullOrEmpty(src) || voteId == 0)
                        {
                            response.IsSuccess = false;
                            response.Msg = "请上传图片";
                        }
                        else
                        {
                            int count = new Bll.WeChat.wp_shop_vote_optionbll().GetOptionCountByVoteId(voteId);
                            wp_shop_vote vote = new Bll.WeChat.wp_shop_votebll().GetModel(voteId);
                            if (vote == null || vote.count <= count)
                            {
                                response.IsSuccess = false;
                                response.Msg = "当前报名人数已满";
                            }
                            else
                            {
                                wp_picture pic = new wp_picture();
                                pic.url = src;
                                pic.path = src.Replace(Common.BaseClass.CurrentDomin, "/");
                                pic.create_time = (uint)BaseClass.ConvertDataTimeToLong(DateTime.Now);
                                pic.system = 0;
                                pic.md5 = "";
                                pic.sha1 = "";
                                pic.status = 1;
                                pic.system = 1;

                                var pic_id = new Bll.WeChat.wp_picturebll().InsertItem(pic);

                                if (pic_id > 0)
                                {
                                    wp_shop_vote_option model = new wp_shop_vote_option();
                                    model.image = (uint)pic_id;
                                    model.vote_id = voteId;
                                    model.ctime = (int)pic.create_time;
                                    model.uid = userId;
                                    model.truename = user == null ? "匿名" : user.nickname;
                                    model.introduce = introduce;
                                    model.manifesto = manifesto;
                                    model.number = 0;
                                    model.token = token;
                                    model.opt_count = 0;
                                    model.phone = phone;
                                    model.option_status = 0;
                                    model.phone = phone;
                                    model.option_status = 0;
                                    model.address = addr;
                                    response.IsSuccess = new wp_shop_vote_optionbll().InsertItem(model) > 0;
                                }
                                else
                                {
                                    response.IsSuccess = false;
                                }
                                response.Msg = response.IsSuccess ? "成功" : "失败";
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Msg = "失败，请重试";
                        LogHandler.Error(ex);
                    }
                    #endregion
                    break;
                case "dovote":
                    try
                    {
                        #region 投票
                        int option_id = RequestHelper.GetFormInt("option_id", 0);
                        int vote_id = RequestHelper.GetFormInt("vote_id", 0);
                        int uid = RequestHelper.GetFormInt("uid", 0);
                        user = new Bll.WeChat.wp_userbll().GetItem(uid);
                        var vote = new Bll.WeChat.wp_shop_votebll().GetItem(vote_id);
                        var vote_log_byvote = new List<wp_shop_vote_log>();
                        var vote_log_count_byoption = 0;
                        if (vote == null || user == null)
                        {
                            response.IsSuccess = false;
                            if (vote == null)
                            {
                                response.Msg = "未找到当前活动";
                            }
                            else
                            {
                                LogHandler.Info($"未找到用户uid:{uid}");
                                response.Msg = "未找到用户";
                            }

                        }
                        else if (BaseClass.ConvertDataTimeToLong(DateTime.Now) > vote.end_time)
                        {
                            response.IsSuccess = false;
                            response.Msg = "投票活动已经结束";
                        }
                        else
                        {
                            //vote_log_byvote = new Bll.WeChat.wp_shop_vote_logbll().GetVoteLogTodayByVoteId(uid, vote_id);
                            vote_log_byvote = new Bll.WeChat.wp_shop_vote_logbll().GetVoteLogByVoteId(uid, vote_id);
                            int user_vote_count = vote_log_byvote == null ? 0 : vote_log_byvote.Count;
                            if (user_vote_count >= vote.multi_num&& vote.multi_num>0)
                            {
                                response.IsSuccess = false;
                                response.Msg = "票已投完";
                            }
                            else
                            {
                                #region 限制每日每人(被投票人)一票
                                //vote_log_count_byoption = new Bll.WeChat.wp_shop_vote_logbll().GetVoteCountTodayByOptionId(uid, option_id);
                                //if (vote_log_count_byoption > 0)
                                //{
                                //    response.IsSuccess = false;
                                //    response.Msg = "已投票";
                                //}
                                //else
                                //{
                                //    wp_shop_vote_log model = new wp_shop_vote_log();
                                //    model.uid = uid;
                                //    model.vote_id = vote_id;
                                //    model.option_id = option_id;
                                //    model.token = vote.token;
                                //    model.ctime = (int)BaseClass.ConvertDataTimeToLong(DateTime.Now);
                                //    response.IsSuccess = new Bll.WeChat.wp_shop_vote_logbll().InsertModel(model) > 0;
                                //    response.Msg = response.IsSuccess ? "投票成功" : "投票失败，请重试";
                                //} 
                                #endregion
                                #region 不限制每日每人(被投票人)一票
                                wp_shop_vote_log model = new wp_shop_vote_log();
                                model.uid = uid;
                                model.vote_id = vote_id;
                                model.option_id = option_id;
                                model.token = vote.token;
                                model.ctime = (int)BaseClass.ConvertDataTimeToLong(DateTime.Now);
                                response.IsSuccess = new Bll.WeChat.wp_shop_vote_logbll().InsertModel(model) > 0;
                                response.Msg = response.IsSuccess ? "投票成功" : "投票失败，请重试";
                                #endregion
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Msg = response.IsSuccess ? "投票成功" : "投票失败，请重试";
                    }
                    break;
                default:
                    break;
            }


            Response.Write(JsonConvert.SerializeObject(response));
        }
    }
}