using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Robust.Shared.Console;
using Robust.Shared.IoC;
using Robust.Shared.Network;

namespace Robust.Shared.ViewVariables.Commands;

public abstract class ViewVariablesBaseCommand : IConsoleCommand
{
    [Dependency] protected readonly INetManager _netMan = default!;
    [Dependency] protected readonly IViewVariablesManager _vvm = default!;

    public abstract string Command { get; }
    public abstract string Description { get; }
    public abstract string Help { get; }
    public abstract void Execute(IConsoleShell shell, string argStr, string[] args);

    public virtual async ValueTask<CompletionResult> GetCompletionAsync(IConsoleShell shell, string[] args, CancellationToken cancel)
    {
        if (args.Length is 0 or > 1)
            return CompletionResult.Empty;

        var path = args[0];

        if(_netMan.IsClient)
        {
            if(path.StartsWith("/c"))
                return CompletionResult.FromOptions(
                    _vvm.ListPath(path[2..], new())
                        .Select(p => new CompletionOption($"/c{p}", null, CompletionOptionFlags.PartialCompletion)));

            return CompletionResult.FromOptions((await _vvm.ListRemotePath(path, new()))
                .Select(p => new CompletionOption(p, null, CompletionOptionFlags.PartialCompletion))
                .Append(new CompletionOption("/c", "Client-side paths", CompletionOptionFlags.PartialCompletion)));
        }

        return CompletionResult.FromOptions(
            _vvm.ListPath(path, new())
                .Select(p => new CompletionOption(p, null, CompletionOptionFlags.PartialCompletion)));
    }
}
