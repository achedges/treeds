using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TreeDS.Tests
{
    [TestClass]
    public class TreeSet
    {
        [TestMethod]
        public void TestTreeSetLookup()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            foreach (int key in TestContext.TestKeys)
            {
                KeyNode<int>? node = tree.Find(key);
                Assert.IsNotNull(node);
                Assert.AreEqual(key, node.Key, $"Unexpected lookup key: {node.Key}");
            }
        }
    }
}