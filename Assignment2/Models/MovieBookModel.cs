namespace Assignment1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MovieBookModel : DbContext
    {
        public MovieBookModel()
            : base("name=DefaultConnection")
        {
        }

        //Sets Databases
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(e => e.Genre)
                .IsFixedLength();
        }
    }
}
