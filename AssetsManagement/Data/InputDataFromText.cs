using System.Reflection.PortableExecutable;

using AssetsManagement.Contracts;
using AssetsManagement.Models;

namespace AssetsManagement.Data
{
    public class InputDataFromText : IInputData
    {
        
        private readonly IMachinesRepository _machinesRepository;
        private readonly IAssetsRepository _assetsRepository;

        public InputDataFromText(IMachinesRepository machinesRepository, IAssetsRepository assetsRepository)
        {
            _machinesRepository = machinesRepository;
            _assetsRepository = assetsRepository;
        }
        public async Task<bool> InsertInputData()
        {
            string filePath = "./Data/data.txt";
            SortedDictionary<string, Machines> machines = new SortedDictionary<string, Machines>();

            Dictionary<string, string> assets = new Dictionary<string, string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine(line);
                    SegregateDataFromLines(line, machines, assets);
                }
            }
            List<Machines> machinesList = new List<Machines>();
            foreach (var machine in machines)
            {
                machinesList.Add(machine.Value);
            }
            List<Assets> assetsList = new List<Assets>();
            foreach (var asset in assets)
            {
                assetsList.Add(new Assets { Name = asset.Key, LatestVersion = asset.Value });
            }
            
            return StoreToDataBase(machinesList, assetsList).Result;
        }
        public bool SegregateDataFromLines(string line, SortedDictionary<string, Machines> machines, Dictionary<string, string> assets)
        {
            string[] data = line.Split(",");


            if (data.Length == 3)
            {

                string machineName = data[0].Trim();
                string assetName = data[1].Trim();
                string assetValue = data[2].Trim();
                string parseData = assetValue.Substring(1).Trim();
                int.TryParse(parseData, out int latestVersion);
                Console.WriteLine($"asset Name = {assetName} , asset value{assetValue} , latestVersion = {latestVersion}");
                int currentVersion = 0;
                if (assets.ContainsKey(assetName))
                {
                    currentVersion = Convert.ToInt32(assets[assetName].Substring(1).Trim());
                    if (currentVersion < latestVersion)
                    {
                        assets[assetName] = assetValue;
                    }
                }
                else
                {
                    assets[assetName] = assetValue;
                }
                if (machines.ContainsKey(machineName))
                {
                    machines[machineName].Assets.Add(assetName, assetValue);
                    machines[machineName].Name = machineName;
                }
                else
                {
                    Machines newMachine = new Machines
                    {
                        Name = machineName,
                        Assets = new Dictionary<string, string> { { assetName, assetValue } }
                    };
                    machines.Add(machineName, newMachine);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> StoreToDataBase(List<Machines> machinesList, List<Assets> assetsList)
        {
            var responseForMachines = await _machinesRepository.AddMachines(machinesList);
            var responseForAssets = await _assetsRepository.AddAssets(assetsList);
            if (responseForMachines != null && responseForMachines != null)
            {
                return true;
            }
            else return false;

        }

    }

}
