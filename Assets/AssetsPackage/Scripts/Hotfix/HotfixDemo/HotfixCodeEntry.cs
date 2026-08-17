using System.Threading.Tasks;
using Framework;

namespace HotfixDemo
{
    public sealed class HotfixCodeEntry : IHotfixEntry
    {
        public Task StartAsync(HotfixContext context)
        {
            return GameMainApp.Interface.SendCommand(new LaunchCommand(context));
        }
    }
}
