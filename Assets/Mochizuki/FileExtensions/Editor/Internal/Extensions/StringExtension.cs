/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

#if UNITY_2017

namespace Mochizuki.FileExtensions.Editor.Internal.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrWhitespace(this string str)
        {
            return str == null || str.Trim().Length == 0;
        }
    }
}

#endif