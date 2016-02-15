using System;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public partial class CookiesTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CookieIndicator.Text = Request.Cookies.AllKeys.Contains("cookie1").ToString();
        }

        protected void SetCookies_OnClick(object sender, EventArgs e)
        {
            Response.Cookies.Add(new HttpCookie("cookie1", "asdasdasd"));
        }
    }
}