using Main.Services;

namespace Main.Views.Store
{
    public interface IApplicationStorekeeper : IApplication
    {
        void subMenuClient(IClientService clientService, IClientView clientView);
    }
}