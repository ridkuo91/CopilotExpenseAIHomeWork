using GithubCopilotExpenseService.Models;

namespace GithubCopilotExpenseService.Repositories;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetExpensesAsync(string title, DateTime? start, DateTime? end);
    Task<Expense> GetExpenseByIdAsync(int id);
    Task<Expense> AddExpenseAsync(Expense expense);
    Task<Expense> UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(int id);
}
