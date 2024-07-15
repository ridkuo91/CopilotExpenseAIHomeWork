using GithubCopilotExpenseService.Entities;
using GithubCopilotExpenseService.Models;

namespace GithubCopilotExpenseService.Services;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetExpensesAsync(string title, DateTime start, DateTime end);
    Task<Expense> GetExpenseByIdAsync(int id);
    Task<Expense> AddExpenseAsync(ExpenseCreateEntity entity);
    Task<Expense> UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(int id);
}