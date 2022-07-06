using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TreeDS.Tests
{
    public static class TestContext
    {
        public const int ListSize = 10;
        public static List<int> TestKeys = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static Dictionary<int, string> TestKeyValues = new Dictionary<int, string>()
        {
            { 1, "A" },
            { 2, "B" },
            { 3, "C" },
            { 4, "D" }
        };

        public static TreeSet<int> GetTreeSetInOrder()
        {
            TreeSet<int> tree = new TreeSet<int>();
            foreach (int k in TestKeys)
                tree.Add(k);
            return tree;
        }

        public static TreeSet<int> GetTreeSetReversed()
        {
            TreeSet<int> tree = new TreeSet<int>();
            for (int i = TestKeys.Count; i > 0; i--)
                tree.Add(TestKeys[i - 1]);
            return tree;
        }

        public static TreeSet<int> GetTreeSetScrambled()
        {
            TreeSet<int> tree = new TreeSet<int>();
            int[] insertionOrder = new int[] { 0, 2, 1, 6, 4, 5, 3, 9, 7, 8 };
            foreach (int i in insertionOrder)
                tree.Add(TestKeys[i]);
            return tree;
        }

        public static void TraversalTestHelper(TreeSet<int> tree, int[] preOrderKeys, int[] postOrderKeys, int[] bfsKeys)
        {
            List<int> keys = tree.GetKeys(TreeWalkOrder.InOrder);
            for (int i = 0; i < keys.Count; i++)
            {
                Assert.AreEqual(TestKeys[i], keys[i], "Incorrect in-order traversal");
            }

            keys = tree.GetKeys(TreeWalkOrder.PreOrder);
            for (int i = 0; i < keys.Count; i++)
            {
                Assert.AreEqual(preOrderKeys[i], keys[i], "Incorrect pre-order traversal");
            }

            keys = tree.GetKeys(TreeWalkOrder.PostOrder);
            for (int i = 0; i < keys.Count; i++)
            {
                Assert.AreEqual(postOrderKeys[i], keys[i], "Incorrect post-order traversal");
            }

            keys = tree.GetKeys(TreeWalkOrder.BreadthFirst);
            for (int i = 0; i < keys.Count; i++)
            {
                Assert.AreEqual(bfsKeys[i], keys[i], "Incorrect breadth-first traversal");
            }
        }
    }
}