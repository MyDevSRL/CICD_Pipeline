using Xunit;

namespace TestApi
{
    public class TestEquals
    {


        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public void Test2()
        {
            Assert.True(true); 
        }

        [Fact]
        public void Test3()
        {
            Assert.True("ciao".Equals("ciao"));
        }
    }
}