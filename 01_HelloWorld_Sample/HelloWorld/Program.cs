using System;
using System.Threading.Tasks;

using NekoBox;
using NekoBox.Client;
using NekoBox.Http.Provider;
using NekoBox.WebSocket.Provider;
using NekoBox.Client.Message;

namespace Nekonya.Example
{
    class Program
    {
        static int Main()
        {
            Console.WriteLine("Hello World!");
            return DoMainAsync().Result;
                
        }

        static async Task<int> DoMainAsync()
        {
            var builder = new GatewayBuilder()
                .SetGatewayName("demo_gateway")
                .ConfigureCluster(options =>
                {
                    options.DisableClustering();    //不使用集群模式
                })
                .SetHttpProvider(provider =>
                {
                    provider.EnableSSL = false;
                    provider.GatewayUrl = "meow";
                    provider.ListenIP = System.Net.IPAddress.Any;
                    provider.Port = 80;
                    provider.UseJson(); //使用JSON格式解析、编码消息
                })
                .SetWebSocketProvider(provider =>
                {
                    provider.EnableSSL = false;
                    provider.HandleUrl = "meow";
                    provider.HostAddress = System.Net.IPAddress.Any;
                    provider.Port = 9002;
                    provider.UseLibuv = true;
                    provider.UseJson(); //使用JSON格式解析、编码消息
                })
                .AddMessageHandler(new MessageHandler("hello", MessageMethod.GET, (req, resp) =>
                {
                    return resp.SendAsync(new { msg = "hello" });
                }))
                .AddMessageHandler(new MessageHandler()
                {
                    ProtocolName = "player:*",
                    Method = MessageMethod.POST,
                    Handler = (req, resp) =>
                    {
                        return resp.SendAsync(new { msg = "hello", proto_name = req.ProtocolName });
                    }
                })
                .AddMessageController<ApiController>("api:");



            IGatewayBox gateway = await builder.RunAsync();

            ReadInput();
            return 0;
        }


        static void ReadInput()
        {
            Console.Write("Gateway>");
            var str = Console.ReadLine();

            //Do something

            ReadInput();
        }

    }
}
