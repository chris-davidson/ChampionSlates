using Models.DbCrud;
using Models.Poco;

namespace Models.Contracts
{
    public interface IAppOperations
    {
        Task<ResponseDto> LookupFactionById(int id);
        Task<ResponseDto> LookupAlignmentById(int id);
    }
}
