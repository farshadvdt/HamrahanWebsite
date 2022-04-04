using Hamrahan.Models;
using HamrahanTemplate.Application.Pagination;
using System.Linq;

namespace HamrahanTemplate.Infrastructure.Contract
{
    public interface IPersonRepository:IRepository<Person>
    {

        /// <summary>
        /// یافتن یوزر با شناسه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<Person> FindById(string? id);
        /// <summary>
        /// متد برای صفحه بندی موارد دریافتی
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        
        public Pagination<Person> FindAllByPagination(PersonPaginationParameters parameters);
    }

}
