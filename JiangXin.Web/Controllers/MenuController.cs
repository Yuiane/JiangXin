using JiangXin.Common.Extensions;
using JiangXin.Web.Models;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JiangXin.Web.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取当前菜单
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenu()
        {
            var result = CommonApi.GetMenu(AccessTokenContainer.TryGetAccessToken("wxd4d7bd7f8fdc8156", "0482ace1cf1b9dcf702ba4366be65db8"));
            if (result.errcode == ReturnCode.请求成功)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 修改自定义菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UpdateMenu(string id)
        {
            try
            {
                var model = id.JsonToObj<MenuModel>();
                if (model != null)
                {
                    var bg = GetSendMenuJson(model.list);
                    var result = CommonApi.CreateMenu(AccessTokenContainer.TryGetAccessToken("wxd4d7bd7f8fdc8156", "0482ace1cf1b9dcf702ba4366be65db8"), bg);
                    if (result != null)
                    {
                        return Content(result.errcode.ToStringEx());
                    }
                }
                return Content("1");
            }
            catch (Exception err)
            {
                return Content(err.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ButtonGroup GetSendMenuJson(Menu_List[] list)
        {
            ButtonGroup bg = new ButtonGroup();
            foreach (Menu_List info in list)
            {
                if (info.menuList != null && info.menuList.Length != 0)
                {
                    //子级菜单
                    var subButton = new SubButton()
                    {
                        name = info.menu.name
                    };
                    foreach (Menu menuInfo in info.menuList)
                    {
                        subButton.sub_button.Add(GetButton(menuInfo));
                    }
                    bg.button.Add(subButton);
                }
                else
                {
                    //单级菜单
                    bg.button.Add(GetButton(info.menu));
                }
            }
            return bg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuInfo"></param>
        /// <returns></returns>
        public SingleButton GetButton(Menu menuInfo)
        {
            SingleButton model = null;
            var type = menuInfo.type.ToLower();
            if (type == ButtonType.click.ToStringEx())
            {
                model = new SingleClickButton()
                {
                    key = menuInfo.key,
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.location_select.ToStringEx())
            {
                model = new SingleLocationSelectButton()
                {
                    key = menuInfo.key,
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.miniprogram.ToStringEx())
            {
                model = new SingleMiniProgramButton()
                {
                    url = menuInfo.url,
                    appid = "",
                    pagepath = "",
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.pic_photo_or_album.ToStringEx())
            {
                model = new SinglePicPhotoOrAlbumButton()
                {
                    key = menuInfo.key,
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.pic_sysphoto.ToStringEx())
            {
                model = new SinglePicSysphotoButton()
                {
                    key = menuInfo.key,
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.pic_weixin.ToStringEx())
            {
                model = new SinglePicWeixinButton()
                {
                    key = menuInfo.key,
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.scancode_push.ToStringEx())
            {
                model = new SingleScancodePushButton()
                {
                    key = menuInfo.key,
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.scancode_waitmsg.ToStringEx())
            {
                model = new SingleScancodeWaitmsgButton()
                {
                    key = menuInfo.key,
                    name = menuInfo.name
                };
            }
            else if (type == ButtonType.view.ToStringEx())
            {
                model = new SingleViewButton()
                {
                    url = menuInfo.url,
                    name = menuInfo.name
                };
            }
            return model;
        }
    }
}