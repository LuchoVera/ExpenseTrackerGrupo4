namespace ExpenseTrackerGrupo4.src.Aplication.Commands;

public interface ICommand<T>
{
    T Execute();
}
