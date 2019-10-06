using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodRecognizingApp.Models
{
    public class Model
    {
        public string name { get; set; }
        public string path { get; set; }

        public Model(string name, string path)
        {
            this.name = name;
            this.path = path;
        }
    }
}