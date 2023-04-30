namespace Resources.Services.ResourceService
{
    public interface IResourceService : IResourceUpdater, IResourceStorage
    {
        void LoadInitialValues();
    }
}