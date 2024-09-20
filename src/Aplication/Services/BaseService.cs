using ExpenseTrackerGrupo4.src.Aplication.Commands;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class BaseService(CommandInvoker commandInvoker)
{
    protected CommandInvoker CommandInvoker { get; } = commandInvoker;
}

