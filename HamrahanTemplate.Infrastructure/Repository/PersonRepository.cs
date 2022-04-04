using Contexts;
using FarshadTools;
using Hamrahan.Models;
using HamrahanTemplate.Application.Pagination;
using HamrahanTemplate.Infrastructure.Contract;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanTemplate.Infrastructure.Repository
{
    public class PersonRepository:Repository<Person>,IPersonRepository
    {
        private readonly HamrahanDbContext _db;

        public PersonRepository(HamrahanDbContext db):base(db)
        {
            _db = db;
        }



        public IQueryable<Person> FindById(string? id)
        {
            return _db.Person.Where(t => t.Id == id);
        }
        public Pagination<Person> FindAllByPagination(PersonPaginationParameters parameters)
        {
            Pagination<Person> p= new(_db.Person.OrderBy(n => n.RegisterDate), parameters.PageNumber, parameters.PageSize);
            return p;
        }
    }
}
