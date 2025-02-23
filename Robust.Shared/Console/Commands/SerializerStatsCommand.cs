﻿using Robust.Shared.Serialization;

namespace Robust.Shared.Console.Commands;

internal sealed class SerializeStatsCommand : IConsoleCommand
{
    public string Command => "szr_stats";
    public string Description => "Report serializer statistics.";
    public string Help => "szr_stats";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        shell.WriteLine($"serialized: {RobustSerializer.BytesSerialized} bytes, {RobustSerializer.ObjectsSerialized} objects");
        shell.WriteLine($"largest serialized: {RobustSerializer.LargestObjectSerializedBytes} bytes, {RobustSerializer.LargestObjectSerializedType} objects");
        shell.WriteLine($"deserialized: {RobustSerializer.BytesDeserialized} bytes, {RobustSerializer.ObjectsDeserialized} objects");
        shell.WriteLine($"largest serialized: {RobustSerializer.LargestObjectDeserializedBytes} bytes, {RobustSerializer.LargestObjectDeserializedType} objects");
    }
}

