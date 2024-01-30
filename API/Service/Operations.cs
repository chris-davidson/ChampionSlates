using Models.Contracts;
using Models.Poco;
using Models.Shared;

namespace API.Service
{
    public class Operations : IApiOperations
    {
        public Task<ResponseDto> Ping()
        {
            var response = new ResponseDto 
            { 
                Message = PingLines.Query,
                Success = true
            };
            return Task.FromResult(response);
        }

        // TODO
        public Task<ResponseDataDto<AlignmentDto>> RequestCreateAlignment(string name, string abbr)
        {
            var alignment = new AlignmentDto
            { 
                Id = 42,
                Name = name,
                Abbr = abbr           
            };
            var response = new ResponseDataDto<AlignmentDto>
            {
                Data = alignment,
                Response = new ResponseDto
                {
                    Message = "Create Alignment request called for {name}({abbr}). TODO Database needed.",
                    Success = true
                }
            };
            return Task.FromResult(response);
        }

        // TODO
        public Task<ResponseDataDto<ChampDto>> RequestCreateChamp(string title, string name, int factionId, int alignmentId)
        {
            throw new NotImplementedException();
        }

        // TODO
        public Task<ResponseDataDto<FactionDto>> RequestCreateFaction(string name, string abbr)
        {
            var faction = new FactionDto
            {
                Id = 42,
                Name = name,
                Abbr = abbr
            };
            var response = new ResponseDataDto<FactionDto>
            {
                Data = faction,
                Response = new ResponseDto
                {
                    Message = "Create Faction request called for {name}({abbr}). TODO Database needed.",
                    Success = true
                }
            };
            return Task.FromResult(response);
        }

        // TODO
        public Task<ResponseDataDto<ChampDto>> RequestChamp(int id)
        {
            throw new NotImplementedException();
        }
    }
}
