using Hamrahan.Models;
using HamrahanTemplate.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanTemplate.Infrastructure.Contract
{
    public interface IPostRepository:IRepository<Post>
    {
        /// <summary>
        /// SoftDelete متد 
        /// </summary>
        bool SoftDelete(Post entity);
 
        IQueryable<Post> FindById(int? id);
    }
}
