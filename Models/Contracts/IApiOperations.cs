using Models.Poco;

namespace Models.Contracts
{
    public interface IApiOperations
    {
        Task<ResponseDto> Ping();
        Task<ResponseDataDto<FactionDto>> RequestCreateFaction(string name, string abbr);
        Task<ResponseDataDto<AlignmentDto>> RequestCreateAlignment(string name, string abbr);
        Task<ResponseDataDto<ChampDto>> RequestCreateChamp(string title, string name, int factionId, int alignmentId);

        Task<ResponseDataDto<ChampDto>> RequestChamp(int id);

    }
}
