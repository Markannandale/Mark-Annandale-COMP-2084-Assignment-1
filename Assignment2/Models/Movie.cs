namespace Assignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Movie")]
    public partial class Movie
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string MovieTitle { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Director { get; set; }

        [Key]
        [Column("Theatrical Release Year", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Theatrical_Release_Year { get; set; }
    }
}
