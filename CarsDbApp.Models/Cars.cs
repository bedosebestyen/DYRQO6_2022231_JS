using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Models
{
    [Table("Cars")]
    public class Cars
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        [StringLength(150)]
        public string CarType { get; set; }
        [StringLength(150)]
        public string CarColor { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Price { get; set; }
        [NotMapped]
        public virtual Customer Customer { get; set; }
        [NotMapped]
        public virtual CarShop Shop { get; set; }
        public Cars()
        {

        }
        public Cars(string line)
        {
            string[] split = line.Split('#');
            CarId = int.Parse(split[0]);
            CarType = split[1];
            CarColor = split[2];
            CustomerId = int.Parse(split[3]);
            ShopId = int.Parse(split[4]);
            PurchaseDate = DateTime.Parse(split[5].Replace('*', '.'));
            Price = int.Parse(split[6]);
        }
    }
}
