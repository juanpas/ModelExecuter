using System;
using System.Collections.Generic;
using System.Data.Entity;
using BuildingBlock.Defs;
using BuildingBlock.Model;
using System.Data;
using System.Web;

namespace BuildingBlock.Repository.SampleData
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

            if (Defs.Utils.ReadAppSetting<bool>("LoadDummyProducts", false))
            {
                var productCategories = AddProductCategories(context);

                var productBrands = AddProductBrands(context);

                var productProviders = AddProductProviders(context);

                var products = AddProducts(context, productCategories, productBrands);

                var textResources = AddTextResources(context, languages, products, productCategories);

                var photos = AddPhotos(context);
            }

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


        private List<TextResource> AddTextResources(MainDbContext context, List<Language> languages, List<Product> products, List<ProductCategory> productCategories)
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
                        result.Add(new TextResource
                        {
                            Type = dr["Type"].ToString(),
                            LanguageId = languages.Find(a => a.Code == dr["Language"].ToString()).Id,
                            RelatedId = productCategories.Find(a => a.Code == dr["Related"].ToString()).Id,
                            Value = dr["Value"].ToString()
                        });
                        break;
                }

            }

            result.ForEach(a => context.TextResources.Add(a));

            context.SaveChanges();

            return result;
        }

        private List<ProductCategory> AddProductCategories(MainDbContext context)
        {
            var productCategories = new List<ProductCategory>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "ProductCategory");

            foreach(DataRow dr in dt.Rows)
            {
                productCategories.Add(new ProductCategory
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString(),
                    Description = dr["Description"].ToString(),
                    ParentCategoryId = null
                });
            }

            productCategories.ForEach(a => context.ProductCategories.Add(a));

            context.SaveChanges();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ParentCategory"].ToString().Length > 0)
                {
                    ProductCategory selected = productCategories.Find(a => a.Code == dr["Code"].ToString());
                    ProductCategory related = productCategories.Find(a => a.Code == dr["ParentCategory"].ToString());

                    selected.ParentCategoryId = related.Id;
                }
            }

            context.SaveChanges();

            return productCategories;
        }

        private List<ProductBrand> AddProductBrands(MainDbContext context)
        {
            var productBrands = new List<ProductBrand>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "ProductBrand");

            foreach (DataRow dr in dt.Rows)
            {
                productBrands.Add(new ProductBrand
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString()
                });
            }

            productBrands.ForEach(a => context.ProductBrands.Add(a));

            context.SaveChanges();

            return productBrands;
        }

        private List<ProductProvider> AddProductProviders(MainDbContext context)
        {
            var productProviders = new List<ProductProvider>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "ProductProvider");

            foreach (DataRow dr in dt.Rows)
            {
                productProviders.Add(new ProductProvider
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString()
                });
            }

            productProviders.ForEach(a => context.ProductProviders.Add(a));

            context.SaveChanges();

            return productProviders;
        }

        private List<Product> AddProducts(MainDbContext context, List<ProductCategory> productCategories, List<ProductBrand> productBrands)
        {
            var result = new List<Product>();

            DataTable dt = ExcelHelper.GetTableFromWorkSheet(excelFileName, "Product");

            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new Product
                {
                    Name = dr["Name"].ToString(),
                    Code = dr["Code"].ToString(),
                    ShortDescription = dr["ShortDescription"].ToString(),
                    Description = dr["Description"].ToString(),
                    Details = dr["Details"].ToString(),
                    TechnicalInfo = dr["TechnicalInfo"].ToString(),
                    DeliveryInfo = dr["DeliveryInfo"].ToString(),
                    Keywords = dr["Keywords"].ToString(),
                    Price = float.Parse(dr["Price"].ToString()),
                    CategoryId = productCategories.Find(a => a.Code == dr["Category"].ToString()).Id,
                    BrandId = productBrands.Find(a => a.Code == dr["Brand"].ToString()).Id
                });
            }

            result.ForEach(a => context.Products.Add(a));

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
