using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiApp.Models
{
    public class Contact
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string User { get; set; }

        private string _extendedData;

        [NotMapped]
        public JObject contacts
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_extendedData) ? "{}" : _extendedData);
            }
            set
            {
                _extendedData = value.ToString();
            }
        }

        public Contact()
        {
            User = "";
            contacts = new JObject();
        }

        public Contact(string _User, JObject _contacts)
        {
            User = _User;
            contacts = _contacts;
        }

    }
}
