using System.Net.Http.Headers;
using AssetsManagement.DB;
using AssetsManagement.Models;

namespace AssetsManagement.Contracts
{
    public interface IMachinesRepository
    {

        public Task<List<Machines>> AddMachines(List<Machines> machines);
        public Task<List<Machines>> GetMachines();
    }
}
