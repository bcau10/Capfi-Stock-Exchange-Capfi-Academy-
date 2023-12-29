using core.Model;
using core.Specifications;

namespace coreTests.TestUtils;

public class SomeSpecification : BaseSpecification<StockAction>
{
    public SomeSpecification(int id) : base(e => e.Id == id)
    {
    }
        
    public SomeSpecification(int take, int skip)
    {
        ApplyPaging(skip, take);
    }
        
    public SomeSpecification()
    {
            
    }
}