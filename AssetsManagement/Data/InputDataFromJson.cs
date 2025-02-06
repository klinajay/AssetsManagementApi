using System.Text.Json;
using AssetsManagement.Contracts;
using AssetsManagement.Models;
using AssetsManagement.Respositories;
using MongoDB.Bson.IO;

namespace AssetsManagement.Data
{
    public class InputDataFromJson : IInputData
    {
        private readonly IMachinesRepository _machinesRepository;
        private readonly IAssetsRepository _assetsRepository;
        public class DataObjects
        {
            public string Machine { get; set; }
            public string Asset { get; set; }
            public string Version { get; set; }
        }
        public InputDataFromJson(IMachinesRepository machinesRepository, IAssetsRepository assetsRepository)
        {
            _machinesRepository = machinesRepository;
            _assetsRepository = assetsRepository;
        }
        public async Task<bool> InsertInputData()
        {
            string jsonFilePath = "./Data/data.json";
            SortedDictionary<string, Machines> machines = new SortedDictionary<string, Machines>();

            Dictionary<string, string> assets = new Dictionary<string, string>();

            string jsonString = await File.ReadAllTextAsync(jsonFilePath);
            Console.WriteLine(jsonString);
            List<DataObjects> deserializedData = JsonSerializer.Deserialize<List<DataObjects>>(jsonString);
            foreach(var data in deserializedData)
            {
                Console.WriteLine(data.Machine);
            }
            foreach(var data in deserializedData)
            {
                if (!machines.ContainsKey(data.Machine))
                {
                    machines.Add(data.Machine, new Machines { Name = data.Machine, Assets = new Dictionary<string, string>()});
                    machines[data.Machine].Assets.Add(data.Asset, data.Version);
                }
                else
                {
                    machines[data.Machine].Assets.Add(data.Asset, data.Version);
                }
                if (!assets.ContainsKey(data.Asset))
                {
                    assets.Add(data.Asset, data.Version);
                }
                else
                {

                    string asset = data.Version.Substring(1);
                    int.TryParse(asset, out int assetVersion);
                    int.TryParse(assets[data.Asset].Substring(1), out int existingAssetVersion);
                    if (assetVersion > existingAssetVersion)
                    {
                        assets[data.Asset] = data.Version;
                    }
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
