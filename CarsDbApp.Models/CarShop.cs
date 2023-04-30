using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Models
{
    [Table("CarShop")]
    public class CarShop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShopId { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public int AvailableCarsCount { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [ForeignKey("ManagerId")]
        public int ManagerId { get; set; }
        [NotMapped]
        public virtual Manager Manager { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Cars> Cars { get; set; }
        [NotMapped]        
        public virtual ICollection<Customer> Customers { get; set; }
        public CarShop()
        {

        }
        public CarShop(string line)
        {
            string[] split = line.Split('#');
            ShopId = int.Parse(split[0]);
            Name = split[1];
            AvailableCarsCount = int.Parse(split[2]);
            Address = split[3];
            ManagerId = int.Parse(split[4]);
        }
    }
}
