using MenKosAPI.Models;

namespace MenKosAPI.Responses
{
    public class PostResponseNewTransaction : BaseResponse
    {
        public Payment Post { get; set; }
    }
}
