using NekoBox.Client;
using NekoBox.Client.Message;
using System.Threading.Tasks;

namespace Nekonya.Example
{
    public class ApiController : MessageController
    {
        public ApiController(IGatewayBox gateway) : base(gateway) { }
        

        [GET("hello")]
        public Task SayHello(IMessageRequest req, IMessageResponse resp)
        {
            return resp.SendAsync(new { msg = "hello , meow ~~!" });
        }

        [POST("meow")] //按照约定，GET和POST的一大区别是：使用POST方法时，客户端会在消息中携带参数。
        public Task SayMeow(IMessageRequest req, IMessageResponse resp)
        {
            return resp.SendTextAsync(" meow~! meow~! " + req.GetTextMessage());
        }

    }
}
