using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scafolled.Models;

[Table("logs")]
public partial class Log
{
    [Key]
    [Column("log_id")]
    public int LogId { get; set; }

    [Column("message")]
    [StringLength(255)]
    public string? Message { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
