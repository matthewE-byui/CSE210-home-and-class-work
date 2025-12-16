using System;
using System.Runtime.InteropServices;

namespace FinalProject.Commands
{
    public class SpotifyControlCommand : Command
    {
        public SpotifyControlCommand()
            : base("spotifyctl", "Control Spotify playback (play, pause, next)")
        {
        }

        public override CommandResult Execute(string input)
        {
            string action = ExtractParameter(input).ToLower();

            if (string.IsNullOrWhiteSpace(action))
                return CommandResult.ErrorResult(
                    "Usage: spotifyctl <play|pause|next|prev>");

            switch (action)
            {
                case "play":
                case "pause":
                    MediaKey(VK_MEDIA_PLAY_PAUSE);
                    return CommandResult.SuccessResult("⏯️ Play/Pause toggled");

                case "next":
                    MediaKey(VK_MEDIA_NEXT_TRACK);
                    return CommandResult.SuccessResult("⏭️ Next track");

                case "prev":
                case "previous":
                    MediaKey(VK_MEDIA_PREV_TRACK);
                    return CommandResult.SuccessResult("⏮️ Previous track");

                default:
                    return CommandResult.ErrorResult(
                        "Unknown action. Use: play, pause, next, prev");
            }
        }

        private void MediaKey(byte key)
        {
            keybd_event(key, 0, 0, UIntPtr.Zero);
            keybd_event(key, 0, 2, UIntPtr.Zero);
        }

        private const byte VK_MEDIA_PLAY_PAUSE = 0xB3;
        private const byte VK_MEDIA_NEXT_TRACK = 0xB0;
        private const byte VK_MEDIA_PREV_TRACK = 0xB1;

        [DllImport("user32.dll")]
        private static extern void keybd_event(
            byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    }
}
