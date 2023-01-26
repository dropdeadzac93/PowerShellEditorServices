// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace Microsoft.PowerShell.EditorServices.Services.Symbols
{
    /// <summary>
    /// A way to define symbols on a higher level
    /// </summary>
    internal enum SymbolType
    {
        /// <summary>
        /// The symbol type is unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The symbol is a variable
        /// </summary>
        Variable,

        /// <summary>
        /// The symbol is a function
        /// </summary>
        Function,

        /// <summary>
        /// The symbol is a parameter
        /// </summary>
        Parameter,

        /// <summary>
        /// The symbol is a DSC configuration
        /// </summary>
        Configuration,

        /// <summary>
        /// The symbol is a workflow
        /// </summary>
        Workflow,

        /// <summary>
        /// The symbol is a hashtable key
        /// </summary>
        HashtableKey,

        /// <summary>
        /// The symbol is a class
        /// </summary>
        Class,

        /// <summary>
        /// The symbol is a enum
        /// </summary>
        Enum,

        /// <summary>
        /// The symbol is a enum member/value
        /// </summary>
        EnumMember,

        /// <summary>
        /// The symbol is a class property
        /// </summary>
        Property,

        /// <summary>
        /// The symbol is a class method
        /// </summary>
        Method,

        /// <summary>
        /// The symbol is a class constructor
        /// </summary>
        Constructor,

        /// <summary>
        /// The symbol is a type reference
        /// </summary>
        Type,
    }

    internal static class SymbolTypeUtils
    {
        internal static SymbolKind GetSymbolKind(SymbolType symbolType)
        {
            return symbolType switch
            {
                SymbolType.Function or SymbolType.Configuration or SymbolType.Workflow => SymbolKind.Function,
                SymbolType.Enum => SymbolKind.Enum,
                SymbolType.Class => SymbolKind.Class,
                SymbolType.Constructor => SymbolKind.Constructor,
                SymbolType.Method => SymbolKind.Method,
                SymbolType.Property => SymbolKind.Property,
                SymbolType.EnumMember => SymbolKind.EnumMember,
                SymbolType.Variable => SymbolKind.Variable,
                // TODO: More delicately handle the other symbol types.
                _ => SymbolKind.Variable,
            };
        }

        // Provides a partial equivalence between type constraints and custom types.
        internal static bool SymbolTypeMatches(SymbolType left, SymbolType right)
        {
            return left == right
                || (left is SymbolType.Class or SymbolType.Enum or SymbolType.Type
                && right is SymbolType.Class or SymbolType.Enum or SymbolType.Type)
                || (left is SymbolType.EnumMember or SymbolType.Property
                && right is SymbolType.EnumMember or SymbolType.Property);
        }
    }
}
