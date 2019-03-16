using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GitHubReposSearch.Models
{
    public class Owner
    {
        [JsonProperty("login")]
        [DisplayName("Owner name")]
        public string name { get; set; }
        [JsonProperty("avatar_url")]
        [DisplayName("Owner avatar")]
        public string avatar { get; set; }
    }
}