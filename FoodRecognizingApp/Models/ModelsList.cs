using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace FoodRecognizingApp.Models
{
    public class ModelsList
    {
        public List <Model> models { get; set; }
        public List<string> results { get; set; }
        public HttpPostedFileBase photo { get; set; }
        public Model checkedModel { get; set; }
        public string selected{ get; set; }

        public ModelsList()
        {
            this.models = new List<Model>();
            this.results = new List<string>();
        }
    }
}