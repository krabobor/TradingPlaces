using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TradingPlaces.Models;
using System.Linq.Expressions;

namespace TradingPlaces.Helpers
{
    public static class Paging
    {
        private static int pageCount = 3;

        /// <summary>
        /// create multipage links in Sale.cshtml
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="itemsCount"></param>
        /// <returns></returns>
        public static MvcHtmlString PagingNavigator(this HtmlHelper helper, int pageNum, int pageSize, int itemsCount)
        {
            StringBuilder sb = new StringBuilder();
            int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);
            int delta = (int)pageSize / pageCount;
            if (delta < 1)
            {
                delta = 1;
            }
            int countStart = pageNum - delta;
            int countEnd = pageNum + delta;

            if (countStart < 0)
            {
                countEnd = countEnd - countStart;
                countStart = 0;
            }
            if (countEnd >= pagesCount)
            { 
                countEnd = pagesCount - 1;
                if (pagesCount - pageCount < 0)
                {
                    countStart = 0;
                }
                else
                {
                    countStart = pagesCount - pageCount;
                }
            }
            
            
            if (countStart > 0)
            {
                sb.Append("<li>");
                sb.Append(helper.ActionLink("«", "Sale", new { pageNum = countStart-1, pageSize = pageSize }));
                sb.Append("</li>");
                sb.Append(" ");
            }

            for (int i = countStart; i <= countEnd; i++)
            {
                if (pageNum == i)
                {
                    sb.Append("<li>");
                    sb.Append(HttpUtility.HtmlEncode(Convert.ToString(i + 1)));
                    sb.Append("</li>");
                }
                else
                {
                    sb.Append("<li>");
                    sb.Append(helper.ActionLink(Convert.ToString(i+1), "Sale", new { pageNum = i, pageSize = pageSize }));
                    sb.Append("</li>");
                }
                sb.Append(" ");
            }

            if (countEnd < pagesCount-1)
            {
                sb.Append("<li>");
                sb.Append(helper.ActionLink("»", "Sale", new { pageNum = countEnd + 1, pageSize = pageSize }));
                sb.Append("</li>");
            }
            
            return MvcHtmlString.Create(sb.ToString()); 
        }

        /// <summary>
        /// create span label in Contact.cshtml
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="title"></param>
        /// <param name="titleClass"></param>
        /// <param name="spanText"></param>
        /// <returns></returns>
        public static MvcHtmlString SpanTitle(this HtmlHelper helper, string title, string titleClass, string spanText)
        {

            return MvcHtmlString.Create(String.Format("<label for=\"{0}\">{1} <span class=\"{2}\">{3}</span></label>",title,title,titleClass,spanText));
        }    
    }
}