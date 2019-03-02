﻿using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Dogey.Modules.Inspect
{
    [Name("Inspect"), Group("user")]
    public class UserInspectModule : DogeyModuleBase
    {
        [Command]
        [Summary("Inspect the properties of the current user")]
        public Task UserAsync()
            => UserAsync(Context.User);
        [Command]
        [Summary("Inspect the properties of a specified user")]
        public async Task UserAsync([Remainder]SocketUser user)
        {
            var embed = new EmbedBuilder()
                .WithTitle(user.ToString() + $" ({user.Id})")
                .WithThumbnailUrl(user.GetAvatarUrl())
                .WithFooter("Created At")
                .WithTimestamp(user.CreatedAt);

            if (user.Activity != null)
                embed.AddField("Activity", user.Activity.Type.ToString().ToLower() + " " + user.Activity.Name, true);

            if (user is IGuildUser guildUser)
            {
                var roles = Context.Guild.Roles.Where(x => guildUser.RoleIds.Any(id => id == x.Id));
                var roleNames = roles.OrderByDescending(x => x.Position).Select(x => x.IsMentionable ? x.Mention : x.Name);

                embed.WithDescription(string.Join(", ", roleNames));
                embed.AddField("Joined At", guildUser.JoinedAt, true);
                if (guildUser.VoiceChannel != null)
                    embed.AddField("Voice", guildUser.VoiceChannel.Name, true);
            }

            await ReplyEmbedAsync(embed);
        }
    }
}
