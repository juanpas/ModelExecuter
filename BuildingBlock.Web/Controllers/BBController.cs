using BuildingBlock.IdentityManager;
using BuildingBlock.Model;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildingBlock.Web.Controllers
{
    public abstract class BBController : Controller
    {
        protected IMainUow Uow { get; set; }
        protected Utils.Utils Utils { get; set; }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                _roleManager = value;
            }
        }


        private List<KeyValuePair<int?, City>> _allCities = new List<KeyValuePair<int?, City>>();

        protected List<KeyValuePair<int?, City>> GetAllCities(bool addEmptyOption, string emptyValue = "null", string emptyText = "----------")
        {
            if (_allCities.Count == 0)
            {
                _allCities = Uow.Cities.GetAll().ToList().Select(c => new KeyValuePair<int?, City>(c.Id, c)).ToList();
            }

            List<KeyValuePair<int?, City>> allCities = _allCities;

            if (addEmptyOption)
            {
                switch (emptyValue)
                {
                    case "0":
                        allCities.Add(new KeyValuePair<int?, City>(0, new City() { Name = emptyText }));
                        break;
                    case "null":
                    default:
                        allCities.Add(new KeyValuePair<int?, City>(null, new City() { Name = emptyText }));

                        break;
                }
            }

            allCities = allCities.OrderBy(c => c.Key).ToList();
            return allCities;
        }


        private List<KeyValuePair<int?, Country>> _allCountries = new List<KeyValuePair<int?, Country>>();

        protected List<KeyValuePair<int?, Country>> GetAllCountries(bool addEmptyOption, string emptyValue = "null", string emptyText = "----------")
        {
            if (_allCountries.Count == 0)
            {
                _allCountries = Uow.Countries.GetAll().ToList().Select(c => new KeyValuePair<int?, Country>(c.Id, c)).ToList();
            }

            List<KeyValuePair<int?, Country>> allCountries = _allCountries;

            if (addEmptyOption)
            {
                switch (emptyValue)
                {
                    case "0":
                        allCountries.Add(new KeyValuePair<int?, Country>(0, new Country() { Name = emptyText }));
                        break;
                    case "null":
                    default:
                        allCountries.Add(new KeyValuePair<int?, Country>(null, new Country() { Name = emptyText }));

                        break;
                }
            }

            allCountries = allCountries.OrderBy(c => c.Key).ToList();
            return allCountries;
        }


        protected class URLComponents : Uri

        {
            public string Subdomain2 { get; set; }
            public string Subdomain1 { get; set; }

            public bool HasSubdomian1
            {
                get
                {
                    return !(string.IsNullOrEmpty(this.Subdomain1));
                }
            }

            public bool HasSubdomian2
            {
                get
                {
                    return !(string.IsNullOrEmpty(this.Subdomain2));
                }
            }

            public URLComponents(Uri uri) : this(uri.ToString())
            {
            }

            public URLComponents(string uriString) :  base(uriString)
            {
                string domain = this.Host;

                string[] hostSegments = domain.Split('.');

                switch (hostSegments.Length) {

                    case 4:
                        if (hostSegments[2].ToLower().Equals("azurewebsites"))
                        {
                            Subdomain1 = hostSegments[0].ToLower();
                        }
                        else
                        {
                            Subdomain2 = hostSegments[0].ToLower();
                            Subdomain1 = hostSegments[1].ToLower();
                        }
                        break;

                    case 3:
                        if (!(hostSegments[0].ToLower().Equals("www")))
                        {
                            Subdomain1 = hostSegments[0].ToLower();
                        }

                        break;
                    case 2:
                        if(hostSegments[1].ToLower().Equals("localhost"))
                        {
                            Subdomain1 = hostSegments[0].ToLower();
                        }
                        break;
                }
            }

        }

        protected City GetCityFromURL()
        {
            URLComponents urlComponents = new URLComponents(Request.Url);
            if (urlComponents.HasSubdomian1)
            {
                return GetAllCities(false)
                    .Where(c => c.Value.Code != null && c.Value.Code.ToUpper() == urlComponents.Subdomain1.ToUpper())
                    .Select(c => c.Value)
                    .FirstOrDefault();
            }

            return null;
        }


    }

}
