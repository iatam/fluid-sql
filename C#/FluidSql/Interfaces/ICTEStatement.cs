﻿// <license>
//     The MIT License (MIT)
// </license>
// <copyright company="TTRider, L.L.C.">
//     Copyright (c) 2014-2016 All Rights Reserved
// </copyright>

using System.Collections.Generic;

namespace TTRider.FluidSql
{
    interface ICTEStatement
    {
        List<CTEDefinition> CommonTableExpressions { get; set; }
    }
}