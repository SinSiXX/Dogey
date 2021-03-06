﻿using Discord.Commands;
using System.Threading.Tasks;

namespace Dogey.Modules
{
    [RequireOwner]
    public class CSharpModule : DogeyModuleBase
    {
        private readonly RoslynService _roslyn;

        public CSharpModule(RoslynService roslyn, RootController root)
            : base(root)
        {
            _roslyn = roslyn;
        }

        [Command("csharp")]
        public async Task CSharpAsync([Remainder]string script)
        {
            var result = await _roslyn.EvaluateAsync(Context, script);
            await ReplyEmbedAsync(result);
        }
    }
}
