using GitHubReposSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace GitHubReposSearch.Controllers
{
    public class GitSearchController : Controller
    {
        
        // GET: GitSearch
        public ActionResult Index()
        {
            if(Session["bookMarked"]==null)
                Session["bookMarked"]=new List<GitRepository>();
            return View();
        }

        public ActionResult ShowBookmarked()
        {
            List<GitRepository> temp = (List<GitRepository>)Session["bookMarked"];
            return View(temp==null||temp.Count==0?null:temp);
        }


        public async Task<ActionResult> SearchInfo(string data)
        {
            string apiurl = $@"https://api.github.com/search/repositories?q={data}";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0");
            client.BaseAddress = new Uri(apiurl);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = await client.GetAsync(apiurl);

            var repos = response.Content.ReadAsStringAsync().Result;
            JToken token = JObject.Parse(repos);
            List<GitRepository> SearchResult = new List<GitRepository>();
            foreach (JToken jt in token["items"])
            {
                GitRepository gr = new GitRepository(jt["name"].ToString(),new Owner { name = jt["owner"]["login"].ToString(),
                    avatar = jt["owner"]["avatar_url"].ToString()},checkIfBookmarked(jt["name"].ToString()));
                SearchResult.Add(gr);
            }
            Session["SearchResult"] = SearchResult;
            return View(SearchResult);
        }

        private bool checkIfBookmarked(string v)
        {
            List<GitRepository> bookmarked= (List<GitRepository>)Session["bookMarked"];
            if(bookmarked !=null)
            foreach (GitRepository gr in bookmarked)
                if (gr.name.Equals(v))
                    return true;
            return false;
        }

        public void addBookMark(string repoName,string owner)
        {
            List<GitRepository> temp = (List<GitRepository>)Session["bookMarked"];
            if (temp is null)
                temp = new List<GitRepository>();
            List<GitRepository> searchResult= (List<GitRepository>)Session["SearchResult"];
            foreach (var item in searchResult)
            {
                if (item.name.Equals(repoName) && item.owner.name.Equals(owner))
                {
                    item.bookmarked = true;
                    temp.Add(item);
                    break;
                }

            }
            Session["bookMarked"] = temp;

        }
        public void removeBookMark(string repoName, string owner)
        {
            List<GitRepository> temp = (List<GitRepository>)Session["bookMarked"];
            if (temp is null)
                temp = new List<GitRepository>();
            //List<GitRepository> searchResult = (List<GitRepository>)Session["SearchResult"];
            GitRepository toRemove = null;
            foreach (var item in temp)
            {
                if (item.name.Equals(repoName) && item.owner.name.Equals(owner))
                {
                    item.bookmarked = false;
                    toRemove = item;
                    break;
                }

            }
            if (toRemove != null) temp.Remove(toRemove);
            Session["bookMarked"] = temp;

        }
    }
}