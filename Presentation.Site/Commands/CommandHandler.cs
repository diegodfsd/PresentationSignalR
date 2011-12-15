using System;
using System.Collections.Generic;
using System.Linq;

namespace Projector.Site.Commands
{
    public interface ICommandHandler<TModel>
        where TModel : class
    {
        ICommandHandler<TModel> Add(ICommand<TModel> command);
        void Process(TModel model, Action<IList<int>> @action);
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

        public void Process(TModel model, Action<IList<int>> @action)
        {
            ICommand<TModel> command;
            IList<int> returns = new List<int>();
            while (commands.Any())
            {
                command = commands.Dequeue();
                returns.Add(command.Execute(model));
            }

            @action(returns);
        }
    }
}