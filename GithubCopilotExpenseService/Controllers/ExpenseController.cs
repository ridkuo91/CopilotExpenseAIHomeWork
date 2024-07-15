using GithubCopilotExpenseService.Entities;
using GithubCopilotExpenseService.Models;
using GithubCopilotExpenseService.Services;
using Microsoft.AspNetCore.Mvc;

namespace GithubCopilotExpenseService.Controllers;

public class ExpenseController : ControllerBase
{
    ///寫一個CRUD API
    ///GET /api/expense 取得所有支出
    ///POST /api/expense 新增支出
    ///GET /api/expense/{id} 取得單一支出
    ///PUT /api/expense/{id} 更新支出
    ///DELETE /api/expense/{id} 刪除支出
    private readonly IExpenseService _expenseService;
    
    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }
    
    //q:有進階需求，需要讓查詢功能可帶入條件
    //標題->title，可模糊查詢，發生時間->datetime，可查詢區間，但起訖需在30天內
    [HttpGet("/api/expense")]
    public async Task<IEnumerable<Expense>> GetExpensesAsync(string title, DateTime start, DateTime end)
    {
        return await _expenseService.GetExpensesAsync(title, start, end);
    }
    
    [HttpGet("/api/expense/{id}")]
    public async Task<Expense> GetExpenseByIdAsync(int id)
    {
        return await _expenseService.GetExpenseByIdAsync(id);
    }
    
    [HttpPost("/api/expense")]
    public async Task AddExpenseAsync([FromBody]ExpenseCreateEntity expense)
    {
        await _expenseService.AddExpenseAsync(expense);
    }
    
    [HttpPut("/api/expense/{id}")]
    public async Task<Expense> UpdateExpenseAsync(int id, [FromBody]ExpenseCreateEntity request)
    {
        var expense = new Expense
        {
            Id = id,
            Title = request.Title,
            Amount = request.Amount,
            DateTime = request.DateTime,
            Category = request.Category
        };
        return await _expenseService.UpdateExpenseAsync(expense);
    }
    
    [HttpDelete("/api/expense/{id}")]
    public async Task DeleteExpenseAsync(int id)
    {
        await _expenseService.DeleteExpenseAsync(id);
    }
}