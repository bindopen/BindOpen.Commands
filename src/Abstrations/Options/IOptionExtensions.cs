using BindOpen.Data.Meta;
using System;

namespace BindOpen.Commands;

/// <summary>
/// 
/// </summary>
public static class IOptionExtensions
{
    public static T Execute<T>(
        this T option,
        Action action)
        where T : IOption
        => option.Execute(null, action);

    public static T Execute<T>(
        this T option,
        Predicate<IBdoMetaData> predicate,
        Action action)
        where T : IOption
    {
        if (option != null)
        {
            option.Executions ??= new();
            option.Executions.Add((predicate, action));
        }

        return option;
    }
}