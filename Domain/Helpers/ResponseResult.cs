﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers;
public class ResponseResult
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public object? Obj { get; set; }
}