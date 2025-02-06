using AssetsManagement.Contracts;
using AssetsManagement.Models;

namespace AssetsManagement.Services
{
    public class MachinesService
    {
        private readonly IMachinesRepository _machinesRepository;
        private readonly IAssetsRepository _assetsRepository;
        public MachinesService(IMachinesRepository machinesRepository , IAssetsRepository assetRepository)
        {
            _machinesRepository = machinesRepository;
            _assetsRepository = assetRepository;
        }
        public async Task<List<Machines>> AddMachine(List<Machines> machines)
        {
            return await _machinesRepository.AddMachines(machines);
        }
        public async Task<List<Machines>> GetMachines()
        {
            return await _machinesRepository.GetMachines();
        }
        public async Task<List<string>> GetAssetsUsedByMachines(string machineName)
        {
             var machines = await _machinesRepository.GetMachines();
            var SelectedMachine = machines.Where(x => x.Name == machineName).FirstOrDefault();
            List<string> assets = new List<string>();
            if (SelectedMachine != null)
            {
                foreach (var asset in SelectedMachine.Assets)
                {
                    assets.Add(asset.Key);
                }
                return assets;
            }
            else return null;
        }
        public async Task<List<string>> GetMachinesUsingLatestAssets()
        {
            var machines = await _machinesRepository.GetMachines();
            return await ShowLatestMachines(machines);
        }
        private async Task<List<string>> ShowLatestMachines(List<Machines> machines)
        {
            if (machines == null)
            {
                return null;
            }
            List<Assets> assets = await _assetsRepository.GetAssets();
            List<string> machinesUsingLatestAssets = new List<string>();
            foreach (var machine in machines)
            {
                int count = 0;
                foreach (var assetInMachine in machine.Assets)
                {
                    if (assets.Any(asset => asset.LatestVersion == assetInMachine.Value))
                    {
                        count++;
                    }
                }
                if (count == machine.Assets.Count)
                {

                    machinesUsingLatestAssets.Add(machine.Name);
                }
            }

            return machinesUsingLatestAssets;
        }

        public async Task<List<string>> GetMachineUsingAsset(string assetName)
        {
            var machines = await _machinesRepository.GetMachines();
            List<string> machinesUsingAssets = new List<string>();

            foreach(var machine in machines)
            {
                foreach(var assets in machine.Assets)
                {
                    if(assets.Key == assetName)
                    {
                        machinesUsingAssets.Add(machine.Name);
                    }
                }
            }
            return machinesUsingAssets;
        }
    }
}
