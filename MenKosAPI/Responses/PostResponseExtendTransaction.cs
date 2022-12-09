using MenKosAPI.Models;

namespace MenKosAPI.Responses
{
    public class PostResponseExtendTransaction : BaseResponse
    {
        public Payment Post { get; set; }
    }
}
