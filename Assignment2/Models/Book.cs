namespace Assignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string BookTitle { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Author { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Genre { get; set; }
    }
}
