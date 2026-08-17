using System;
using System.Threading.Tasks;
using Framework;

namespace HotfixDemo
{
    public sealed class HotfixCodeEntry : IHotfixEntry, IHotfixResourcePreloader
    {
        public Task PreloadAsync(
            HotfixContext context,
            IProgress<HotfixPreloadProgress> progress)
        {
            return GameMainApp.Interface.SendCommand(new PreloadConfigCommand(context, progress));
        }

        public Task StartAsync(HotfixContext context)
        {
            return GameMainApp.Interface.SendCommand(new LaunchCommand(context));
        }
    }
}
