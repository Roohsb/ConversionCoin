using Conversion.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversion.Services
{
    public class ConvertRepository : IRepository<Converts>
    {
        private readonly Context _context;

        public  ConvertRepository(Context context)
        {
            _context = context;
        }

        public Task DeleteAsync(Converts item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Converts> GetAll(Func<Converts, bool> filters = null)
        {

            var query = _context.Transaction.AsEnumerable();

            if (filters != null)
                query = query.Where(filters);

            return query.ToList();
        }



        public async Task<Converts> InsertAsync(Converts item) //Insert the conversion values ​​into the database.
        {

            await _context.Transaction.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Converts> UpdateAsync(Converts item)
        {
            _context.Transaction.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
