using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
   public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get an item by Id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Item with Id</returns>
        T GetById(int id);   

        /// <summary>
        /// Used to get items similar to (SELECT/WHERE) in SQL.
        /// </summary>
        /// <param name="predicate">A function that takes an object of type T and returns a boolean value.</param>
        /// <param name="asNoTracking">If set to false its ReadOnly. If you intend to modify them in the database set it to true. the  It won't track changes made to the results after they have been returned. </param>
        /// <param name="includes">Used to join tables. Includes multiple tables together.</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);

        /// <summary>
        /// Returns an Enumerable list of results.
        /// </summary>
        /// <returns>List of results.</returns>
        IEnumerable<T> List();

        /// <summary>
        /// Takes a query and returns a Enumerable List
        /// </summary>
        /// <param name="predicate">Takes a expression and returns a boolean value</param>
        /// <param name="orderBy"> Default value is null.</param>
        /// <param name="includes">Include other tables.</param>
        /// <returns>Returns IEnumerable List</returns>
        IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);

        /// <summary>
        /// Takes a query and returns a Enumerable List but does it asynchronously. 
        /// </summary>
        /// <param name="predicate">Takes a expression and returns a boolean value</param>
        /// <param name="orderBy"> Default value is null.</param>
        /// <param name="includes">Include other tables.</param>
        /// <returns>Returns a Task</returns>
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="entity">The item to be inserted.</param>
        void Add(T entity);

        /// <summary>
        /// Delete a single entity
        /// </summary>
        /// <param name="entity">A single item in the database</param>
        void Delete(T entity);

        /// <summary>
        /// Delete several entities
        /// </summary>
        /// <param name="entities">Items in the database</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity"> Single Item in the database</param>
        void Update(T entity);
    }
}
