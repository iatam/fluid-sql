﻿// <copyright company="TTRider, L.L.C.">
// Copyright (c) 2014-2015 All Rights Reserved
// </copyright>

using System;

namespace TTRider.FluidSql
{
    public class WaitforTimeStatement : WaitforStatement
    {
        public DateTime Time { get; set; }
    }
}