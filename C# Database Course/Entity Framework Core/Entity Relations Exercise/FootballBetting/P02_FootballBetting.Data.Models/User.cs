﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;

        [Unicode]
        public string Email { get; set; } = null!;
        public decimal Balance { get; set; }
        public virtual ICollection<Bet> Bets { get; set; } = null!;

    }
}
