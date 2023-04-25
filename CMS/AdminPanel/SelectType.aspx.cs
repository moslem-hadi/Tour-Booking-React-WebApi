using CMS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft;
using Newtonsoft.Json;

namespace CMS.AdminPanel
{
    public partial class SelectType : System.Web.UI.Page
    {
        public List<ProductTypesJson> ProductTypes;
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductTypes = new List<ProductTypesJson>();
            using (DataAccessDataContext _data= new DataAccessDataContext())
            {
                var jsons = _data.GlobalValues.Where(a => a.Type == (int)GlobalValueTypes.ProductType)
                    .Select(a => a.Value).ToList();
                foreach (var item in jsons)
                {
                    ProductTypes.Add(JsonConvert.DeserializeObject<ProductTypesJson>(item));
                }
            }

        }
    }
}