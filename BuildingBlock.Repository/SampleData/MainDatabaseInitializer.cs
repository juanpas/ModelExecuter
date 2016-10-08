using System;
using System.Collections.Generic;
using System.Data.Entity;
using ModelExecuter.Defs;
using ModelExecuter.Model;
using System.Data;
using System.Web;

namespace ModelExecuter.Repository.SampleData
{
    //public class MainDatabaseInitializer :
    //    CreateDatabaseIfNotExists<MainDbContext>
    public class MainDatabaseInitializer:
        DropCreateDatabaseIfModelChanges<MainDbContext>
    {
        string excelFileName = HttpContext.Current.Server.MapPath("/bin/SampleData/") + "DataInitializer.xlsx";

        protected override void Seed(MainDbContext context)
        {
            var categories = AddParameterCategories(context);

            var countries = AddCountries(context);

            var cities = AddCities(context, countries);

            var parameters = AddParameters(context, categories);

            var languages = AddLanguages(context);

            var companies = AddCompanies(context);

        }

        private List<ParameterCategory> AddParameterCategories(MainDbContext context)
        {
            var categories = new List<ParameterCategory>();

            categories.Add(new ParameterCategory
            {
                Name = "Configuration parameters",
                Code = "CP",
                Description = "Configuration parameters",
            });

            categories.Add(new ParameterCategory
            {
                Name = "Resources",
                Code = "R",
                Description = "Resources",
            });

            categories.ForEach(a => context.ParameterCategories.Add(a));

            context.SaveChanges();

            return categories;
        }

        private List<Parameter> AddParameters(MainDbContext context, List<ParameterCategory> categories)
        {
            var parameters = new List<Parameter>();

            ParameterCategory CPCategory = categories.Find(a => a.Code == "CP");
            ParameterCategory RCategory = categories.Find(a => a.Code == "R");

            #region CPCategory

            parameters.Add(new Parameter
            {
                Name = "SystemEmail",
                StringValue = "pastranajc@hotmail.com",
                CategoryId = CPCategory.Id,
                TypeId = (int)Enums.ParameterType.String

            });

            parameters.Add(new Parameter
            {
                Name = "SystemSMTPHost",
                StringValue = "smtp.gmail.com",
                CategoryId = CPCategory.Id,
                TypeId = (int)Enums.ParameterType.String
            });

            parameters.Add(new Parameter
            {
                Name = "SystemSMTPUsername",
                StringValue = "pastranajc@gmail.com",
                CategoryId = CPCategory.Id,
                TypeId = (int)Enums.ParameterType.String
            });

            parameters.Add(new Parameter
            {
                Name = "SystemSMTPPassword",
                StringValue = "Adivine123",
                CategoryId = CPCategory.Id,
                TypeId = (int)Enums.ParameterType.String
            });

            parameters.Add(new Parameter
            {
                Name = "BaseRedirectURLForP2P",
                StringValue = "http://juanpastrana.bounceme.net:8083/",
                CategoryId = CPCategory.Id,
                TypeId = (int)Enums.ParameterType.String
            });

            parameters.Add(new Parameter
            {
                Name = "SystemSMTPPort",
                IntValue = 587,
                CategoryId = CPCategory.Id,
                TypeId = (int)Enums.ParameterType.Integer
            });

            #endregion

            #region RCategory

            #region XXX

            /*parameters.Add(new Parameter
                           {
                               Name = "XXX",
                               StringValue = "XXX",
                               CategoryId = RCategory.Id,
                               TypeId = (int) Enums.ParameterType.String
                           });*/

            #endregion

            #endregion

            parameters.ForEach(a => context.Parameters.Add(a));

            context.SaveChanges();

            return parameters;
        }


        private List<Country> AddCountries(MainDbContext context)
        {
            var countries = new List<Country>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "Country");

            foreach (DataRow dr in dt.Rows)
            {
                countries.Add(new Country
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString()
                });
            }

            countries.ForEach(a => context.Countries.Add(a));

            context.SaveChanges();

            return countries;
        }

        private List<City> AddCities(MainDbContext context, List<Country> countries)
        {
            var result = new List<City>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "City");

            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new City
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString(),
                    CountryId = countries.Find(a => a.Code == dr["Country"].ToString()).Id
                });
            }

            result.ForEach(a => context.Cities.Add(a));

            context.SaveChanges();

            return result;
        }


        private List<Language> AddLanguages(MainDbContext context)
        {
            var result = new List<Language>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "Language");

            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new Language
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString()
                });
            }

            result.ForEach(a => context.Languages.Add(a));

            context.SaveChanges();

            return result;
        }

        private List<Company> AddCompanies(MainDbContext context)
        {
            var result = new List<Company>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "Company");

            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new Company
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString()
                });
            }

            result.ForEach(a => context.Companies.Add(a));

            context.SaveChanges();

            return result;
        }


        private List<TextResource> AddTextResources(MainDbContext context, List<Language> languages)
        {
            var result = new List<TextResource>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "TextResource");

            foreach (DataRow dr in dt.Rows)
            {
                switch (dr["MainType"].ToString())
                {
                    case "PRODUCT":
                        //result.Add(new TextResource
                        //{
                        //    Type = dr["Type"].ToString(),
                        //    LanguageId = languages.Find(a => a.Code == dr["Language"].ToString()).Id,
                        //    RelatedId = products.Find(a => a.Code == dr["Related"].ToString()).Id,
                        //    Value = dr["Value"].ToString()
                        //});
                        break;
                    case "PRODUCTCATEGORY":
                        //result.Add(new TextResource
                        //{
                        //    Type = dr["Type"].ToString(),
                        //    LanguageId = languages.Find(a => a.Code == dr["Language"].ToString()).Id,
                        //    RelatedId = productCategories.Find(a => a.Code == dr["Related"].ToString()).Id,
                        //    Value = dr["Value"].ToString()
                        //});
                        break;
                }

            }

            result.ForEach(a => context.TextResources.Add(a));

            context.SaveChanges();

            return result;
        }

        private List<Photo> AddPhotos(MainDbContext context)
        {
            var result = new List<Photo>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "Photo");

            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new Model.Photo
                {
                    Title = dr["Title"].ToString(),
                    Description = dr["Description"].ToString(),
                    FileName = dr["FileName"].ToString(),
                    Alt = dr["Alt"].ToString(),
                    Index = Convert.ToInt32(dr["Index"].ToString()),
                    SubTypeId = Convert.ToInt32(dr["TypeId"].ToString()),
                    RelatedId = Convert.ToInt32(dr["RelatedId"].ToString())
            });
            }

            result.ForEach(a => context.Photos.Add(a));

            context.SaveChanges();

            return result;
        }

    }
}
