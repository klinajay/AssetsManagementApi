using AssetsManagement.Models;

namespace AssetsManagement.Contracts
{
    public interface IInputData
    {
        public Task<bool> InsertInputData();
        public Task<bool> StoreToDataBase(List<Machines> machinesList, List<Assets> assetsList);

    }
}
