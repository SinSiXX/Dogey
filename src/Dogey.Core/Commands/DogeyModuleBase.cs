﻿using Discord;
using Discord.Commands;
using System.IO;
using System.Threading.Tasks;

namespace Dogey
{
    public class DogeyModuleBase : ModuleBase<DogeyCommandContext>
    {
        protected readonly RootController _root;

        public DogeyModuleBase(RootController root)
        {
            _root = root;
        }

        public async Task ReplySuccessAsync()
            => await Context.Message.AddReactionAsync(await _root.GetSuccessEmojiAsync(Context.Guild, Context.Channel as IGuildChannel));

        public Task ReplyEmbedAsync(Embed embed)
            => ReplyAsync("", false, embed, null);
        public Task ReplyEmbedAsync(EmbedBuilder builder)
            => ReplyAsync("", false, builder.Build(), null);

        public Task ReplyReactionAsync(IEmote emote)
            => Context.Message.AddReactionAsync(emote);

        public Task ReplyFileAsync(Stream stream, string fileName, string message = null)
            => Context.Channel.SendFileAsync(stream, fileName, message, false, null);
        
    }
}
