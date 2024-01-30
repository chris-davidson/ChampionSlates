using Models.DbCrud;

namespace Models.Contracts
{
    public interface ICrudOperations
    {
        Task<Response> Ping();
        Task<Response> CheckDb();
        Task<Response> DatabaseInit();

        //Task<Response> DatabaseDown();
        //Task<Response> DatabaseDelete();
        //Task<Response> DatabaseUpdate();
        //Task<Response> DatabaseSave();
        //Task<Response> DatabaseUpdateAll();
        //Task<Response> DatabaseDeleteAll();
        //Task<Response> DatabaseSaveAll();
        //Task<Response> DatabaseDeleteAllAll();
        //Task<Response> DatabaseUpdateAllAll();
    }
}
