using core.Model;

namespace core.Specifications.CustomSpecifications;

public class CustomerWithPortfolioElementSpecification : BaseSpecification<Customer>
{
    public CustomerWithPortfolioElementSpecification(int id) :
        base (x => x.Id == id)
    {
        AddInclude(x => x.PortfolioElements);
    }
}
