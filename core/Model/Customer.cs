using System.ComponentModel.DataAnnotations;

namespace core.Model
{
    public class Customer : BaseEntity
    {
        [Required]
        public double AccountValue { get; set; }
        public ICollection<PortfolioElement> PortfolioElements { get; set; }
    }
}
