using GithubCopilotExpenseService.Entities;

namespace GithubCopilotExpenseService.Services;

using GithubCopilotExpenseService.Models;
using GithubCopilotExpenseService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }
    
    public async Task<IEnumerable<Expense>> GetExpensesAsync(string title, DateTime start, DateTime end)
    {
        //檢查起訖時間需在30天內
        if (end.Subtract(start).Days > 30)
        {
            throw new ArgumentException("查詢區間需在30天內");
        }
        return await _expenseRepository.GetExpensesAsync(title, start, end);
    }
    
    public async Task<Expense> GetExpenseByIdAsync(int id)
    {
        return await _expenseRepository.GetExpenseByIdAsync(id);
    }
    
    public async Task DeleteExpenseAsync(int id)
    {
        await _expenseRepository.DeleteExpenseAsync(id);
    }

    public async Task<Expense> AddExpenseAsync(ExpenseCreateEntity entity)
    {
        var expense = new Expense
        {
            Title = entity.Title,
            Amount = entity.Amount,
            DateTime = entity.DateTime,
            Category = entity.Category
        };

        ValidateExpense(expense);
        return await _expenseRepository.AddExpenseAsync(expense);
    }

    public async Task<Expense> UpdateExpenseAsync(Expense expense)
    {
        ValidateExpense(expense);
        return await _expenseRepository.UpdateExpenseAsync(expense);
    }

    private void ValidateExpense(Expense expense)
    {
        if (expense.Amount < 0)
            throw new ArgumentException("金額不能小於0");

        if (expense.DateTime < DateTime.Now.AddYears(-1))
            throw new ArgumentException("發生時間不能晚於一年前");

        var validCategories = new HashSet<string> { "食", "衣", "住", "行" };
        if (!validCategories.Contains(expense.Category))
            throw new ArgumentException("分類必須為 食/衣/住/行 四種");
    }
}