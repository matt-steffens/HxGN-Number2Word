using HxGN.Financial.Services;

namespace HxGN.Financial.Tests
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