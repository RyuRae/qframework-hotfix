
using Framework;
using QFramework;

namespace HotfixDemo
{
    public class HotfixCodeEntry
    {

        public static void Entrance()
        {
            GameMainApp.Interface.SendCommand(new LaunchCommand());
        }

    }
}