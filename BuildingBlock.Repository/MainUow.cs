﻿using System;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    /// <summary>
    /// The "Unit of Work"
    ///     1) decouples the repos from the controllers
    ///     2) decouples the DbContext and EF from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular
    /// root entity type such as a <see cref="User"/>.
    /// A repository typically exposes "Get" methods for querying and
    /// will offer add, update, and delete methods if those features are supported.
    /// The repositories rely on their parent UoW to provide the interface to the
    /// data layer (which is the EF DbContext in 360Portal).
    /// </remarks>
    public class MainUow : IMainUow, IDisposable
    {
        public MainUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        // Repositories
        public IFileRepository Files { get { return GetRepo<IFileRepository>(); } }
        public IPhotoRepository Photos { get { return GetRepo<IPhotoRepository>(); } }

        public IRepository<ParameterCategory> ParameterCategories { get { return GetStandardRepo<ParameterCategory>(); } }
        public IParameterRepository Parameters { get { return GetRepo<IParameterRepository>(); } }

        public IRepository<Language> Languages { get { return GetStandardRepo<Language>(); } }
        public ITextResourceRepository TextResources { get { return GetRepo<ITextResourceRepository>(); } }

        public IRepository<Country> Countries { get { return GetStandardRepo<Country>(); } }
        public ICityRepository Cities { get { return GetRepo<ICityRepository>(); } }
        public ICompanyRepository Companies { get { return GetRepo<ICompanyRepository>(); } }
        public IModuleRepository Modules { get { return GetRepo<IModuleRepository>(); } }

        public IRepository<ServiceCategory> ServiceCategories { get { return GetStandardRepo<ServiceCategory>(); } }
        public IServiceRepository Services { get { return GetRepo<IServiceRepository>(); } }

        public IRepository<ProductSeller> ProductSellers { get { return GetStandardRepo<ProductSeller>(); } }
        public IRepository<ProductCategory> ProductCategories { get { return GetStandardRepo<ProductCategory>(); } }
        public IRepository<ProductBrand> ProductBrands { get { return GetStandardRepo<ProductBrand>(); } }
        public IRepository<ProductProvider> ProductProviders { get { return GetStandardRepo<ProductProvider>(); } }
        public IProductRepository Products { get { return GetRepo<IProductRepository>(); } }
        public IProductSettingRepository ProductSettings { get { return GetRepo<IProductSettingRepository>(); } }
        public IProductQuotationRepository ProductQuotations { get { return GetRepo<IProductQuotationRepository>(); } }
        public IProductQuotationActivityRepository ProductQuotationActivities { get { return GetRepo<IProductQuotationActivityRepository>(); } }
        public IRepository<ExtendedProduct> ExtendedProducts { get { return GetStandardRepo<ExtendedProduct>(); } }

        public IRepository<LeadCategory> LeadCategories { get { return GetStandardRepo<LeadCategory>(); } }
        public ILeadRepository Leads { get { return GetRepo<ILeadRepository>(); } }


        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new MainDbContext();

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        private MainDbContext DbContext { get; set; }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}