namespace ExpenseTrackerGrupo4.src.Aplication.Commands;

public class CommandInvoker
{
    public T Execute<T>(ICommand<T> command)
    {
        return command.Execute();
    }
}
