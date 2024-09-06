using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using ExpenseTrackAPI.Models;
using ExpenseTrackAPI.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTrackController : ControllerBase
    {
        private readonly ExpensesDbContext _context;

        public ExpenseTrackController(ExpensesDbContext context)
        {
            _context = context;
        }

        // POST: api/ExpenseTrack/AddExpense
        [HttpPost("AddExpense")]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            try
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
                return Ok("Expense added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ExpenseTrack/UpdateExpense
        [HttpPut("UpdateExpense")]
        public IActionResult UpdateExpense([FromBody] Expense expense)
        {
            try
            {
                var existingExpense = _context.Expenses.Find(expense.Id);
                if (existingExpense == null)
                {
                    return NotFound("Expense not found.");
                }

                existingExpense.Description = expense.Description;
                existingExpense.Amount = expense.Amount;
                existingExpense.DateUpdated = expense.DateUpdated;
                existingExpense.CategoryId = expense.CategoryId;
                existingExpense.Deleted = expense.Deleted;

                _context.SaveChanges();
                return Ok("Expense updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ExpenseTrack/GetExpenses
        [HttpGet("GetExpenses")]
        public IActionResult GetExpenses()
        {
            try
            {
                var expenses = _context.Expenses.Include(e => e.CategoryId).ToList();
                if (expenses == null || !expenses.Any())
                {
                    return NotFound("No expenses found.");
                }

                return Ok(expenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ExpenseTrack/GetExpensesByCategory/{categoryId}
        [HttpGet("GetExpensesByCategory/{categoryId}")]
        public IActionResult GetExpensesByCategory(int categoryId)
        {
            try
            {
                var expenses = _context.Expenses
                                       .Where(e => e.CategoryId == categoryId)
                                       .ToList();

                if (expenses == null || !expenses.Any())
                {
                    return NotFound($"No expenses found for category with ID {categoryId}.");
                }

                return Ok(expenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ExpenseTrack/AddCategory
        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Ok("Category added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ExpenseTrack/UpdateCategory
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] Category category)
        {
            try
            {
                var existingCategory = _context.Categories.Find(category.Id);
                if (existingCategory == null)
                {
                    return NotFound("Category not found.");
                }

                existingCategory.Description = category.Description;
                _context.SaveChanges();
                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ExpenseTrack/GetCategories
        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _context.Categories.ToList();
                if (categories == null || !categories.Any())
                {
                    return NotFound("No categories found.");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
