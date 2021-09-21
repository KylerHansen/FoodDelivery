using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUnitofWork
    {
        public IGenericRepository<Category> Category { get; }
        public IGenericRepository<FoodType> FoodType { get; }
        public IGenericRepository<MenuItem> MenuItem { get; }

        /// <summary>
        /// Commits items. 
        /// </summary>
        /// <returns>Returns number of rows affected.</returns>
        int Commit();

        /// <summary>
        /// Commits asynchronously
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
