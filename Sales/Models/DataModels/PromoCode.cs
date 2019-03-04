using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Models.DataModels
{
    public class PromoCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Value { get; set; }

    }
}
