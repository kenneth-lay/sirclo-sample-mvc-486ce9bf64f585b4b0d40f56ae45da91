using WeightLogging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WeightLogging.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcForm BeginForm<TModel>(this HtmlHelper<TModel> html, string htmlClass = null, Dictionary<string, object> otherHtmlAttributes = null, bool searchForm = false)
        {
            Dictionary<string, object> htmlAttributes = otherHtmlAttributes ?? new Dictionary<string, object>();
            htmlAttributes.Add("class", "container-fluid " + htmlClass);
            return html.BeginForm(actionName: null, controllerName: null, method: searchForm ? FormMethod.Get : FormMethod.Post, htmlAttributes: htmlAttributes);
        }

        public static IHtmlString PageTitle<TModel>(this HtmlHelper<TModel> html, string title, string subText = null)
        {
            return new HtmlString("<h1>" + title + "</h1>");
        }

        private static MvcTag BeginTag<TModel>(this HtmlHelper<TModel> html, string tagStart, string tagEnd)
        {
            return new MvcTag(html.ViewContext, tagStart, tagEnd);
        }

        public static MvcTag BeginRow<TModel>(this HtmlHelper<TModel> html, string htmlClass = null)
        {
            return BeginTag(html, "<div class=\"form-group row " + htmlClass + "\">", "</div>");
        }

        public static MvcTag BeginColumnFull<TModel>(this HtmlHelper<TModel> html, string htmlClass = null)
        {
            return BeginTag(html, "<div class=\"col-sm-12 " + htmlClass + "\">", "</div>");
        }

        private static TagBuilder GetTagBuilder(string tagName, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            Dictionary<string, string> htmlAttributes = otherHtmlAttributes ?? new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(htmlClass))
            {
                if (htmlAttributes.ContainsKey("class"))
                {
                    htmlAttributes["class"] = htmlClass;
                }
                else
                {
                    htmlAttributes.Add("class", htmlClass);
                }
            }

            TagBuilder builder = new TagBuilder(tagName);

            foreach (KeyValuePair<string, string> pair in htmlAttributes)
            {
                builder.MergeAttribute(pair.Key, pair.Value);
            }

            return builder;
        }

        private static IHtmlString Tag(string tagName, IHtmlString content, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            TagBuilder builder = GetTagBuilder(tagName, htmlClass, otherHtmlAttributes);
            builder.InnerHtml = content.ToHtmlString();
            return new HtmlString(builder.ToString(TagRenderMode.Normal));
        }

        private static IHtmlString Tag(string tagName, string content, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            TagBuilder builder = GetTagBuilder(tagName, htmlClass, otherHtmlAttributes);
            builder.SetInnerText(content);
            return new HtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null, bool readOnly = false)
        {
            var editForm = html.ViewData["editForm"];
            IHtmlString content = html.DisplayNameFor(expression);

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            if (metadata.IsRequired && (bool)editForm == true && !readOnly)
            {
                htmlClass += " must";
            }

            return Tag("label", content, "col-lg-2 col-sm-3 col-form-label " + htmlClass, otherHtmlAttributes);
        }

        public static IHtmlString Label(this HtmlHelper html, string content, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            return Tag("label", content, "col-lg-2 col-sm-3 col-form-label " + htmlClass, otherHtmlAttributes);
        }

        public static IHtmlString InputColumn<TModel>(this HtmlHelper<TModel> html, IHtmlString content, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            return Tag("div", content, "col-lg-4 col-sm-9 " + htmlClass, otherHtmlAttributes);
        }

        public static MvcTag BeginInputColumn<TModel>(this HtmlHelper<TModel> html, string htmlClass = null)
        {
            return BeginTag(html, "<div class=\"col-lg-4 col-sm-9 " + htmlClass + "\">", "</div>");
        }

        public static IHtmlString InputColumnFull<TModel>(this HtmlHelper<TModel> html, IHtmlString content, string htmlClass = null)
        {
            return Tag("div", content, "col-lg-10 col-sm-9 " + htmlClass);
        }

        public static MvcTag BeginInputColumnFull<TModel>(this HtmlHelper<TModel> html, string htmlClass = null)
        {
            return BeginTag(html, "<div class=\"col-lg-10 col-sm-9 " + htmlClass + "\">", "</div>");
        }

        public static IHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string htmlClass = null)
        {
            IHtmlString content = html.DisplayFor(expression);
            return Tag("span", content, "form-control-plaintext " + htmlClass);
        }

        public static IHtmlString Display(this HtmlHelper html, string content, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            return Tag("span", content, "form-control-plaintext " + htmlClass);
        }

        public static IHtmlString PlainText<TModel>(this HtmlHelper<TModel> html, IHtmlString content, string htmlClass = null)
        {
            return Tag("span", content, htmlClass);
        }

        public static IHtmlString ErrorText<TModel>(this HtmlHelper<TModel> html, IHtmlString content, string htmlClass = null)
        {
            return PlainText(html, content, "text-danger");
        }

        public static IHtmlString ButtonInline<TModel>(this HtmlHelper<TModel> html, string name, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            Dictionary<string, string> htmlAttributes = otherHtmlAttributes ?? new Dictionary<string, string>();
            otherHtmlAttributes.Add("type", "button");
            otherHtmlAttributes.Add("value", name);
            return Tag("input", "", htmlClass, otherHtmlAttributes);
        }

        public static IHtmlString Button<TModel>(this HtmlHelper<TModel> html, string name, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            Dictionary<string, string> htmlAttributes = otherHtmlAttributes ?? new Dictionary<string, string>();
            return Tag("button", name, "btn space-right " + htmlClass, otherHtmlAttributes);
        }

        public static IHtmlString ButtonMain<TModel>(this HtmlHelper<TModel> html, string name, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            return Button<TModel>(html, name, "btn btn-primary " + htmlClass, otherHtmlAttributes);
        }

        public static IHtmlString ButtonWarn<TModel>(this HtmlHelper<TModel> html, string name, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            return Button<TModel>(html, name, "btn btn-warning " + htmlClass, otherHtmlAttributes);
        }

        public static IHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string htmlClass = null)
        {
            return html.TextBoxFor(expression, new { @class = "form-control " + htmlClass });
        }

        public static IHtmlString DateFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string htmlClass = null)
        {
            return html.TextBoxFor(expression, new { @class = "form-control datefield" + htmlClass, @type = "date" });
        }

        public static IHtmlString TextFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string htmlClass = null, Dictionary<string, string> otherHtmlAttributes = null)
        {
            IHtmlString content = html.DisplayTextFor(expression);

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            return Tag("label", content, "col-lg-2 col-sm-3 col-form-label " + htmlClass, otherHtmlAttributes);
        }

        public static MvcTag BeginTable<TModel>(this HtmlHelper<TModel> html, string htmlClass = null)
        {
            return BeginTag(html, "<table class=\"table " + htmlClass + "\">", "</table>");
        }

        public static MvcTag BeginTr<TModel>(this HtmlHelper<TModel> html, string htmlClass = null)
        {
            return BeginTag(html, "<tr class=\"" + htmlClass + "\">", "</tr>");
        }

        public static MvcTag BeginTd<TModel>(this HtmlHelper<TModel> html, string htmlClass = null)
        {
            return BeginTag(html, "<td class=\"" + htmlClass + "\">", "</td>");
        }

        public static IHtmlString Th<TModel>(this HtmlHelper<TModel> html, IHtmlString content, string htmlClass = null)
        {
            return Tag("th", content, htmlClass, new Dictionary<string, string> { { "scope", "col" } });
        }

        public static IHtmlString Td<TModel>(this HtmlHelper<TModel> html, IHtmlString content, string htmlClass = null)
        {
            return Tag("td", content, htmlClass);
        }

        public static IHtmlString Link<TModel>(this HtmlHelper<TModel> html, string value, string link = "#", string htmlClass = null)
        {
            return Tag("a", value, htmlClass, new Dictionary<string, string> { { "href", link } });
        }

        public static IHtmlString SubTitle<TModel>(this HtmlHelper<TModel> html, string title, string subText = "")
        {
            var markup = new StringBuilder();
            markup.Append(title);
            if (!string.IsNullOrEmpty(subText))
            {
                markup.Append(Tag("small", subText, "text-muted"));
            }
            return Tag("h2", new HtmlString(markup.ToString()), "");
        }

        public static IHtmlString SearchSubTitle<TModel>(this HtmlHelper<TModel> html, PagedList.IPagedList pagedList, object Model) where TModel : class
        {
            int PageNumber = pagedList.PageNumber;
            int PageSize = 3;
            int PageCount = pagedList.PageCount;

            if (PageCount == 0)
            {
                PageNumber = 0;
                PageSize = 1;
            }

            return SubTitle(html, "", "Showing " + ((PageNumber - 1) * PageSize + 1).ToString() 
                + " to " + (PageNumber * PageSize > pagedList.TotalItemCount ? pagedList.TotalItemCount : PageNumber * PageSize).ToString() 
                + " of " + (pagedList.TotalItemCount).ToString() + " entries");
        }

        public static IHtmlString RedValidationMessageFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.ValidationMessageFor(expression, "", new { @class = "text-danger" });
        }

        public static IHtmlString RecordDateFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string htmlClass = null)
        {
            return html.TextBoxFor(expression, new { @class = "form-control datefield" + htmlClass, @type = "date" });
        }

        public static IHtmlString ButtonSubmit<TModel>(this HtmlHelper<TModel> html)
        {
            return Tag("input", "", "btn btn-primary", new Dictionary<string, string> { { "name", "Submit" }, { "type", "submit" }, { "value", "Submit" } });
        }

        public static IHtmlString ButtonSave<TModel>(this HtmlHelper<TModel> html)
        {
            return Tag("input", "", "btn btn-primary", new Dictionary<string, string> { { "name", "Save" }, { "type", "submit" }, { "value", "Save" } });
        }
    }
}
