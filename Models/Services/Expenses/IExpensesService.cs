﻿using ExpanseTracker.Models.Categories;
using ExpanseTracker.Models.Expenses;

namespace ExpanseTracker.Models.Services.Expenses
{
    public interface IExpensesService
    {
        Task<List<ExpenseReadOnlyVM>> GetAllExpensesAnyc();
        Task Create(ExpenseCreateVM model);
        Task Delete(int id);
        Task Edit(ExpenseEditVM model);
        Task<T?> GetAsync<T>(int id) where T : class;
        bool ExpenseExists(int id);
    }
}
