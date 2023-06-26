using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieRecs.Models;

namespace MovieRecs.Data
{
    public class MovieRecsContext : DbContext
    {
        public MovieRecsContext (DbContextOptions<MovieRecsContext> options)
            : base(options)
        {
        }

        public DbSet<MovieRecs.Models.User> User { get; set; } = default!;
    }
}
