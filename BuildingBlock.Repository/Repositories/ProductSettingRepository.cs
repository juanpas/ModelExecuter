using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class ProductSettingRepository : EFRepository<ProductSetting>, IProductSettingRepository
    {
        public ProductSettingRepository(DbContext context) : base(context) { }

        public void OptimizeProductSetting(ProductSetting productSetting)
        {
            // Seller User
            ProductSetting bestProductSetting = GetBestProductSetting(1, productSetting);

            if (bestProductSetting != null)
            {
                productSetting.SellerId = bestProductSetting.SellerId;
            }


            // Discount Percentage
            bestProductSetting = GetBestProductSetting(2, productSetting);

            if (bestProductSetting != null)
            {
                productSetting.DiscountPercentage = bestProductSetting.DiscountPercentage;
            }
            else
            {
                productSetting.DiscountPercentage = "0";
            }

            // Product Description
            bestProductSetting = GetBestProductSetting(3, productSetting);

            if (bestProductSetting != null)
            {
                productSetting.ProductDescription = bestProductSetting.ProductDescription;
            }
        }


        private ProductSetting GetBestProductSetting(int adminPropertyId, ProductSetting productSetting)
        {
            IQueryable <ProductSetting> baseBestProductSetting = DbSet.Where(p => p.AdminPropertyId == adminPropertyId);

            IQueryable<ProductSetting> bestProductSetting = baseBestProductSetting
                .Where(p =>
                    p.BrandId == productSetting.BrandId &&
                    p.CategoryId == productSetting.CategoryId &&
                    p.CityId == productSetting.CityId);

            if (bestProductSetting.Count() == 0)
            {
                bestProductSetting = baseBestProductSetting
                .Where(p =>
                    p.BrandId == productSetting.BrandId &&
                    p.CategoryId == productSetting.CategoryId &&
                    p.CityId == null);

                if (bestProductSetting.Count() == 0)
                {
                    bestProductSetting = baseBestProductSetting
                    .Where(p =>
                        p.BrandId == productSetting.BrandId &&
                        p.CategoryId == null &&
                        p.CityId == productSetting.CityId);
                }

                if (bestProductSetting.Count() == 0)
                {
                    bestProductSetting = baseBestProductSetting
                    .Where(p =>
                        p.BrandId == productSetting.BrandId &&
                        p.CategoryId == null &&
                        p.CityId == null);
                }

                if (bestProductSetting.Count() == 0)
                {
                    bestProductSetting = baseBestProductSetting
                    .Where(p =>
                        p.BrandId == null &&
                        p.CategoryId == null &&
                        p.CityId == productSetting.CityId);
                }

                if (bestProductSetting.Count() == 0)
                {
                    bestProductSetting = baseBestProductSetting
                    .Where(p =>
                        p.BrandId == null &&
                        p.CategoryId == null &&
                        p.CityId == null);
                }
            }

            return bestProductSetting.FirstOrDefault();
        }
    }



}
