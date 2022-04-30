using Conversion.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversion.Services
{
    public class UsersRepository : IRepository<Users>
    {
        private readonly Context _context;

        public UsersRepository(Context context)
        {
            _context = context;
        }

        public Task DeleteAsync(Users item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetAll(Func<Users, bool> filters = null)
        {

            var query = _context.Users.AsEnumerable();

            if (filters != null)
                query = query.Where(filters);

            return query.ToList();
        }



        public async Task<Users> InsertAsync(Users item)  //Insert the users values ​​into the database.
        {

            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Users> UpdateAsync(Users item)
        {
            _context.Users.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
