using System;
using System.Diagnostics;

namespace FinalProject.Commands
{
    /// <summary>
    /// Controls Spotify using URI commands.
    /// Demonstrates encapsulation, inheritance, and polymorphism.
    /// </summary>
    public class SpotifyCommand : Command
    {
        public SpotifyCommand()
            : base("spotify", "Control Spotify playback")
        {
        }

        public override CommandResult Execute(string input)
        {
            // Examples:
            // spotify play
            // spotify pause
            // spotify next
            // spotify previous

            string action = ExtractParameter(input).ToLower();

            if (string.IsNullOrWhiteSpace(action))
                return CommandResult.ErrorResult(
                    "Usage: spotify <play|pause|next|previous>");

            try
            {
                string uri = action switch
                {
                    "play" => "spotify:play",
                    "pause" => "spotify:pause",
                    "next" => "spotify:next",
                    "previous" => "spotify:previous",
                    _ => null
                };

                if (uri == null)
                    return CommandResult.ErrorResult(
                        "Unknown spotify command. Use: play, pause, next, previous");

                Process.Start(new ProcessStartInfo
                {
                    FileName = uri,
                    UseShellExecute = true
                });

                return CommandResult.SuccessResult($"🎵 Spotify {action}");
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Spotify error: {ex.Message}");
            }
        }
    }
}
