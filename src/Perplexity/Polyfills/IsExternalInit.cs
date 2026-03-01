// Polyfill for init-only properties (C# 9) when targeting netstandard2.0/netstandard2.1
#if NETSTANDARD2_0 || NETSTANDARD2_1

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    // ReSharper disable once UnusedType.Global
    internal static class IsExternalInit;
}

#endif
