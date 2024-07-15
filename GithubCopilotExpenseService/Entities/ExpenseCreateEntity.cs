namespace GithubCopilotExpenseService.Entities;

public class ExpenseCreateEntity
{
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Category { get; set; }
}