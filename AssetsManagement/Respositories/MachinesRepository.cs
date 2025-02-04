using AssetsManagement.Contracts;
using AssetsManagement.DB;
using AssetsManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace AssetsManagement.Respositories
{
    public class MachinesRepository : IMachinesRepository
    {
        private DbContext _context;
        public MachinesRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<Machines>> AddMachines(List<Machines> machines)
        {
            await _context.Machines.InsertManyAsync(machines);
            return machines;
        }
        public async Task<List<Machines>> GetMachines()
        {
            var filter = Builders<Machines>.Filter.Empty;
            var allMachines = await _context.Machines.Find(filter).ToListAsync();
            return allMachines;
        }
    }
}
