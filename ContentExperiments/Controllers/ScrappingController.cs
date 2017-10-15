using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using ContentExperiments.HtmlParser;

namespace ContentExperiments.Controllers
{
    public class ScrappingController : Controller
    {
        public ActionResult GetUrl(string encoding, string url)
        {

            try
            {
                var uri = new Uri(url);
                string domain = string.Format("{0}://{1}:{2}", uri.Scheme, uri.Host, uri.Port);
                var handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; rv:14.0) Gecko/20100101 Firefox/14.0.1");
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = httpClient.SendAsync(request).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(responseString);

                var el = htmlDoc.DocumentNode.Descendants("script").Count();
                foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("script"))
                {
                    HtmlAttributeCollection atrib = script.Attributes;
                    HtmlAttribute src = atrib.FirstOrDefault(x => x.Name == "src");
                    if (src != null)
                    {
                        string oldAttr = src.Value;
                        if (!(oldAttr.StartsWith("//") || oldAttr.StartsWith("http://") || oldAttr.StartsWith("https://")))
                        {
                            if (oldAttr.StartsWith("."))
                            {
                                oldAttr = oldAttr.TrimStart('.');
                            }
                            if (!oldAttr.StartsWith("/"))
                            {
                                oldAttr = "/" + oldAttr;
                            }
                            var newAttr = string.Format("{0}{1}", domain, oldAttr);
                            src.Value = newAttr;

                        }
                    }
                }
                int el2;
                if (htmlDoc.DocumentNode.Descendants("link") != null)
                {
                    el2 = htmlDoc.DocumentNode.Descendants("link").Count();

                    foreach (HtmlNode link in htmlDoc.DocumentNode.Descendants("link"))
                    {
                        HtmlAttributeCollection atrib = link.Attributes;
                        HtmlAttribute href = atrib.FirstOrDefault(x => x.Name == "href");
                        if (href != null)
                        {
                            string oldAttr = href.Value;
                            if (!(oldAttr.StartsWith("//") || oldAttr.StartsWith("http://") || oldAttr.StartsWith("https://")))
                            {
                                if (oldAttr.StartsWith("."))
                                {
                                    oldAttr = oldAttr.TrimStart('.');
                                }
                                if (!oldAttr.StartsWith("/"))
                                {
                                    oldAttr = "/" + oldAttr;
                                }
                                var newAttr = string.Format("{0}{1}", domain, oldAttr);
                                href.Value = newAttr;
                            }
                        }
                    }
                };
                int el3;
                if (htmlDoc.DocumentNode.Descendants("img") != null)
                {
                    el3 = htmlDoc.DocumentNode.Descendants("img").Count();
                    foreach (HtmlNode img in htmlDoc.DocumentNode.Descendants("img"))
                    {
                        HtmlAttributeCollection atrib = img.Attributes;
                        HtmlAttribute src = atrib.FirstOrDefault(x => x.Name == "src");
                        if (src != null)
                        {
                            string oldAttr = src.Value;
                            if (!(oldAttr.StartsWith("//") || oldAttr.StartsWith("http://") || oldAttr.StartsWith("https://")))
                            {
                                if (oldAttr.StartsWith("."))
                                {
                                    oldAttr = oldAttr.TrimStart('.');
                                }
                                if (!oldAttr.StartsWith("/"))
                                {
                                    oldAttr = "/" + oldAttr;
                                }
                                var newAttr = string.Format("{0}{1}", domain, oldAttr);
                                src.Value = newAttr;
                            }
                        }
                    }
                }
                int el4;
                if (htmlDoc.DocumentNode.Descendants("a") != null)
                {
                    el4 = htmlDoc.DocumentNode.Descendants("//a").Count();
                    foreach (HtmlNode a in htmlDoc.DocumentNode.Descendants("a"))
                    {
                        HtmlAttributeCollection atrib = a.Attributes;
                        HtmlAttribute href = atrib.FirstOrDefault(x => x.Name == "href");
                        if (href != null)
                        {
                            string oldAttr = href.Value;
                            if (oldAttr.StartsWith("http://") || oldAttr.StartsWith("https://"))
                            {
                                var newAttr = string.Format("/Scrapping/GetUrl?encoding=" + encoding + "&url={0}", oldAttr);
                                href.Value = newAttr;
                            }
                            else
                            {
                                if (oldAttr.StartsWith("."))
                                {
                                    oldAttr = oldAttr.TrimStart('.');
                                }
                                if (!oldAttr.StartsWith("/"))
                                {
                                    oldAttr = "/" + oldAttr;
                                }
                                var newAttr = string.Format("/Scrapping/GetUrl?encoding=" + encoding + "&url={0}{1}", domain, oldAttr);
                                href.Value = newAttr;
                            }
                        }
                    }
                }

                var res = htmlDoc.DocumentNode.OuterHtml;
                return View("GetUrl", (object)res);
            }
            catch (Exception ex)
            {
                return View("GetUrlExeption", (object)ex.Message);
            }
        }
    }
}
