using System.Collections.Generic;
using System.Linq;

namespace Projector.Site.Commands
{
    public interface ICommandHandler<TModel>
        where TModel : class
    {
        ICommandHandler<TModel> Add(ICommand<TModel> command);
        IEnumerable<int> Process(TModel model);
    }

    public class CommandHandler<TModel> : ICommandHandler<TModel> 
        where TModel : class
    {
        private readonly Queue<ICommand<TModel>> commands;

        public CommandHandler()
        {
            commands = new Queue<ICommand<TModel>>();
        }

        public ICommandHandler<TModel> Add(ICommand<TModel> command)
        {
            commands.Enqueue(command);
            return this;
        }

        public IEnumerable<int> Process(TModel model)
        {
            ICommand<TModel> command;
            while (commands.Any())
            {
                command = commands.Dequeue();
                yield return command.Execute(model);
            }
        }
    }
}