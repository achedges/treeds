using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TreeDS.Tests
{
    [TestClass]
    public class TreeMap
    {
        [TestMethod]
        public void TestTreeMapKeyValuePairs()
        {
            TreeMap<int, string> tree = new TreeMap<int, string>();
            foreach (KeyValuePair<int, string> pair in TestContext.TestKeyValues)
                tree.Add(pair.Key, pair.Value);

            Assert.AreEqual(4, tree.Size, "Incorrect list size");

            KeyValueNode<int, string>? node;
            foreach (KeyValuePair<int, string> pair in TestContext.TestKeyValues)
            {
                node = tree.Find(pair.Key);
                Assert.IsNotNull(node);
                Assert.AreEqual(pair.Value, node.Value, $"Unexpected value at node {pair.Key}: {node.Value}");
            }

            node = tree.Min();
            Assert.AreEqual(1, node?.Key ?? 0, $"Unexpected tree min: {node?.Key}");
            Assert.AreEqual("A", node?.Value ?? "", $"Unexpected tree min value: {node?.Value}");

            node = tree.Max();
            Assert.AreEqual(4, node?.Key ?? 0, $"Unexpected tree max: {node?.Key}");
            Assert.AreEqual("D", node?.Value ?? "", $"Unexpected tree max value: {node?.Value}");

            Assert.IsNull(tree.Find(0), "Unexpected value for absent key");

            tree.Add(1, "E");
            Assert.AreEqual("E", tree.Find(1)?.Value ?? "", "Unexpected value after replacement");
        }
    }
}