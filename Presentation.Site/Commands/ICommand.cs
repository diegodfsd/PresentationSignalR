namespace Projector.Site.Commands
{
    public interface ICommand<TModel>
        where TModel : class
    {
        int Execute(TModel model);
    }
}