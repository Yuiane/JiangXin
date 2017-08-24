using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiangXin.Web.Models
{
    /// <summary>
    /// 自定义菜单设置提交参数Model
    /// </summary>
    public class MenuModel
    {
        public Menu_List[] list { get; set; }
    }

    public class Menu_List
    {
        public Menu menu { get; set; }
        public Menu[] menuList { get; set; }
    }

    public class Menu
    {
        public string name { get; set; }
        public string key { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }

}