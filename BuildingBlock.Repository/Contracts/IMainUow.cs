using BuildingBlock.Model;

namespace BuildingBlock.Repository.Contracts
{
    /// <summary>
    /// Interface for the PhotoFeels "Unit of Work"
    /// </summary>
    public interface IMainUow
    {
        // Save pending changes to the data store.
        void Commit();

        // Repositories
        IFileRepository Files { get; }
        IPhotoRepository Photos { get; }
        IRepository<ParameterCategory> ParameterCategories { get; }
        IParameterRepository Parameters { get; }
        IRepository<Language> Languages { get; }
        ITextResourceRepository TextResources { get; }
        IRepository<Country> Countries { get; }
        ICityRepository Cities { get; }
        ICompanyRepository Companies { get; }
        IModuleRepository Modules { get; }
        IRepository<ProductSeller> ProductSellers { get; }
        IRepository<ProductCategory> ProductCategories { get; }
        IRepository<ProductBrand> ProductBrands { get; }
        IRepository<ProductProvider> ProductProviders { get; }
        IProductRepository Products { get; }
        IProductSettingRepository ProductSettings { get; }
        IProductQuotationRepository ProductQuotations { get; }
        IRepository<ExtendedProduct> ExtendedProducts { get; }
        IRepository<ServiceCategory> ServiceCategories { get; }
        IServiceRepository Services { get; }
        IRepository<LeadCategory> LeadCategories { get; }
        ILeadRepository Leads { get; }

    }
}