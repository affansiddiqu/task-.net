using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crud_Operations_in_Asp.Net_Core.Models;

public partial class TblProduct
{
    public int Pid { get; set; }

    public string Pname { get; set; } = null!;

    public string Pimage { get; set; } = null!;

    public double Price { get; set; }

    public string Status { get; set; } = null!;
}
