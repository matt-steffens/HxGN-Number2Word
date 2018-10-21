using HexMin.Financial.Services;

namespace HexMin.Financial.Tests
{
    public class FinancialTestsBase
    {
        protected NumberWordifier wrdSvc;

        public FinancialTestsBase()
        {
            wrdSvc = new NumberWordifier();
        }
    }
}