using System;
using System.Collections.Generic;

namespace Crud_Operations_in_Asp.Net_Core.Models;

public partial class TblUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte Role { get; set; }

    public string? Gender { get; set; }
}
