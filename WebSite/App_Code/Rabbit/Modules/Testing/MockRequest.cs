﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Collections.Specialized;

/// <summary>
/// Summary description for MockAction
/// </summary>
public class MockRequest : Mock
{
    public dynamic Request { get; set; }
    public dynamic Context { get; set; }
    public dynamic Page { get; set; }
    public bool IsPost { get; set; }
    public IList<string> UrlData { get; set; }

	public MockRequest(string[] urlData, bool isPost=false)
	{
        Request = new ExpandoObject();
        Context = new ExpandoObject();
        Page = new ExpandoObject();
        Context.IsDebuggingEnabled = true;
        IsPost = isPost;
        UrlData = urlData;
        //Setup("Write", new object[] { It.IsAny<string>()}, null);
	}
}

public class MockGet : MockRequest
{
    public MockGet(string[] UrlData) : base(UrlData, false)
    {
    }
}

public class MockPost : MockRequest
{
    public NameValueCollection Form { get; set; }

    public MockPost(string[] UrlData, NameValueCollection form)
        : base(UrlData, true)
    {
        Form = form;
    }
}