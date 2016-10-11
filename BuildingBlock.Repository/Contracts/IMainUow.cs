using ModelExecuter.Model;

namespace ModelExecuter.Repository.Contracts
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

        IRepository<MetadataItemType> MetadataItemTypes { get; }
        IMetadataItemRepository MetadataItems { get; }
        IModelRepository Models { get; }
        IViewRepository Views { get; }

    }
}