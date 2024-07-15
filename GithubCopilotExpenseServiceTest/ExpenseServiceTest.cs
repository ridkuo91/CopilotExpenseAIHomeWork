using Xunit;
using Moq;
using GithubCopilotExpenseService.Services;
using GithubCopilotExpenseService.Repositories;
using GithubCopilotExpenseService.Entities;
using GithubCopilotExpenseService.Models;
using System.Threading.Tasks;

public class ExpenseServiceTests
{
    [Fact]
    public async Task AddExpenseAsync_ValidExpense_ReturnsExpense()
    {
        // Arrange
        var mockRepository = new Mock<IExpenseRepository>();
        var expenseService = new ExpenseService(mockRepository.Object);
        var expenseCreateEntity = new ExpenseCreateEntity
        {
            Amount = 100,
            DateTime = System.DateTime.Now,
            Category = "食"
        };
        var expectedExpense = new Expense
        {
            Amount = 100,
            DateTime = System.DateTime.Now,
            Category = "食"
        };
        mockRepository.Setup(repo => repo.AddExpenseAsync(It.IsAny<Expense>()))
                      .ReturnsAsync(expectedExpense);

        // Act
        var result = await expenseService.AddExpenseAsync(expenseCreateEntity);

        // Assert
        Assert.Equal(expectedExpense.Amount, result.Amount);
        Assert.Equal(expectedExpense.Category, result.Category);
        // Add more assertions as necessary
    }

    [Fact]
    public async Task GetExpensesAsync_ValidTitleAndDate_ReturnsExpenses()
    {
        // Arrange
        var mockRepository = new Mock<IExpenseRepository>();
        var expenseService = new ExpenseService(mockRepository.Object);
        var title = "食";
        var start = System.DateTime.Now;
        var end = System.DateTime.Now;
        var expectedExpenses = new[]
        {
            new Expense
            {
                Amount = 100,
                DateTime = System.DateTime.Now,
                Category = "食"
            },
            new Expense
            {
                Amount = 200,
                DateTime = System.DateTime.Now,
                Category = "食"
            }
        };
        mockRepository.Setup(repo => repo.GetExpensesAsync(title, start, end))
                      .ReturnsAsync(expectedExpenses);

        // Act
        var result = await expenseService.GetExpensesAsync(title, start, end);

        // Assert
        Assert.Collection(result,
            item =>
            {
                Assert.Equal(expectedExpenses[0].Amount, item.Amount);
                Assert.Equal(expectedExpenses[0].Category, item.Category);
            },
            item =>
            {
                Assert.Equal(expectedExpenses[1].Amount, item.Amount);
                Assert.Equal(expectedExpenses[1].Category, item.Category);
            });
    }
    
    [Fact]
    public async Task GetExpensesAsync_InvalidDate_ThrowsArgumentException()
    {
        // Arrange
        var mockRepository = new Mock<IExpenseRepository>();
        var expenseService = new ExpenseService(mockRepository.Object);
        var title = "食";
        var start = System.DateTime.Now;
        var end = System.DateTime.Now.AddDays(31);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => expenseService.GetExpensesAsync(title, start, end));
    }
}