using Contexts;
using Hamrahan.Models;
using HamrahanTemplate.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
