namespace Main.Core.Factory
{
    public interface IFactory {
    IFactoryRepository getFactoryRepository();
    IFactoryService getFactoryService();
    IFactoryView getFactoryView();
}

}