﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Dynamic;
using System.Collections.Specialized;

/// <summary>
/// Summary description for Test
/// </summary>
public static class Pages
{
	public static void Init()
	{
        SiteEngine.AddHook("get_homepage", (data) => 
        {            
            return "~/Pages";  
        });

        SiteEngine.AddHook("get_menu", (data) => 
        {
            var filename = HttpContext.Current.Server.MapPath("~/App_Data/Rabbit/Menu");
            return File.Exists(filename) ? File.ReadAllText(filename) : data; //TODO: cache it
        });
        
        SiteEngine.AddHook("save_menu", (data) =>
        {
            var filename = HttpContext.Current.Server.MapPath("~/App_Data/Rabbit/Menu");
            File.WriteAllText(filename, data.Menu);
            return data;
        });

        SiteEngine.AddHook("get_module_admin_menu", (data) =>
        {
            ((IList<string>)data).Add("Manage Pages|~/Pages/List");
            ((IList<string>)data).Add("Manage Menus|~/Pages/EditMenu");
            return data;
        });

        SiteEngine.AddHook("get_pages_page_list", (data) =>
        {
            return new PageModel().List(data).Value;
        });

        SiteEngine.AddHook("get_pages_page", (data) =>
        {
            return new PageModel().Load(data).Value;
        });

        SiteEngine.AddHook("save_pages_page", (data) =>
        {
            return new PageModel(data).Save().Value;
        });

        SiteEngine.AddHook("delete_pages_page", (data) =>
        {
            return new PageModel().Load(data).Delete().Value;
        });
	}
}