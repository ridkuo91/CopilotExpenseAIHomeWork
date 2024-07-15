using GithubCopilotExpenseService.Models;
using GithubCopilotExpenseService.Repositories;
using Microsoft.EntityFrameworkCore;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _context;

    public ExpenseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Expense>> GetExpensesAsync(string title, DateTime? start, DateTime? end)
    {
        return await _context.Expenses
            .Where(e => string.IsNullOrEmpty(title) || e.Title.Contains(title))
            .Where(e => !start.HasValue || e.DateTime >= start.Value)
            .Where(e => !end.HasValue || e.DateTime <= end.Value)
            .ToListAsync();
    }

    public async Task<Expense> GetExpenseByIdAsync(int id)
    {
        return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Expense> AddExpenseAsync(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> UpdateExpenseAsync(Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task DeleteExpenseAsync(int id)
    {
        var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}