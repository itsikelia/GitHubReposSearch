using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace GitHubReposSearch.Models
{
    public class GitRepository
    {
        [DisplayName("Repository name")]
        public string name { get; set; }
        [JsonProperty("owner")]
        public Owner owner { get; set; }
        [DisplayName("Bookmarked?")]
        public bool bookmarked { get; set; }

        public GitRepository(string name, Owner owner,Boolean bookmarked)
        {
            this.name = name;
            this.owner = owner;
            this.bookmarked = bookmarked;
        }
    }
}