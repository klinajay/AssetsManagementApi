using AssetsManagement.Contracts;
using AssetsManagement.Models;

namespace AssetsManagement.Services
{
    public class MachinesService
    {
        private readonly IMachinesRepository _machinesRepository;
        public MachinesService(IMachinesRepository machinesRepository)
        {
            _machinesRepository = machinesRepository;
        }
        public async Task<List<Machines>> AddMachine(List<Machines> machines)
        {
            return await _machinesRepository.AddMachines(machines);
        }
        public async Task<List<Machines>> GetMachines()
        {
            return await _machinesRepository.GetMachines();
        }
    }
}
