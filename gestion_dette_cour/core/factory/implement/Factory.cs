namespace Main.Core.Factory.Implement
{
    public class Factory : IFactory
    {
        private static Factory instance;
        private IFactoryRepository factoryRepository;
        private IFactoryService factoryService;
        private IFactoryView factoryView;

        private Factory()
        {
            this.factoryRepository = new FactoryRepository();
            this.factoryService = new FactoryService(this.factoryRepository);
            this.factoryView = new FactoryView(this.factoryService);
        }

        public static Factory getInstance()
        {
            if (instance == null)
            {
                instance = new Factory();
            }
            return instance;
        }

        public IFactoryRepository getFactoryRepository()
        {
            return this.factoryRepository;
        }

        public IFactoryService getFactoryService()
        {
            return this.factoryService;
        }

        public IFactoryView getFactoryView()
        {
            return this.factoryView;
        }
    }
}