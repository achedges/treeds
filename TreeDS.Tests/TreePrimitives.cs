using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TreeDS.Tests
{
    [TestClass]
    public class TreePrimitives
    {
        [TestMethod]
        public void TestTreeSize()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            Assert.AreEqual(TestContext.ListSize, tree.Size, $"Unexpected tree size: {tree.Size}");
        }

        [TestMethod]
        public void TestTreeMinMax()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            Assert.IsNotNull(tree.Min(), "Null tree min");
            Assert.AreEqual(0, tree.Min()?.Key ?? -1, $"Unexpected tree min: {tree.Min()?.Key}");
            Assert.IsNotNull(tree.Max(), "Null tree max");
            Assert.AreEqual(9, tree.Max()?.Key ?? -1, $"Unexpected tree max: ${tree.Max()?.Key}");
            Assert.IsNull(tree.Prev(tree.Min()), "Previous node from tree min should be null");
            Assert.IsNull(tree.Next(tree.Max()), "Next node from tree max should be null");
        }

        [TestMethod]
        public void TestTreeNextPrevNodes()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();

            TreeNode<int>? node = tree.Min();
            TreeNode<int>? next = tree.Next(node);

            Assert.IsNotNull(node, "Initial node is null");
            Assert.IsNotNull(next, "Initial next node is null");

            while (next != null)
            {
                int nodeKey = node.Key;
                int nextKey = next.Key;
                Assert.AreEqual(nodeKey + 1, nextKey, $"Unexpected next key: {nextKey}");
                node = next;
                next = tree.Next(next);
            }

            node = tree.Max();
            TreeNode<int>? prev = tree.Prev(node);

            Assert.IsNotNull(node, "Initial node is null");
            Assert.IsNotNull(prev, "Initial previous node is null");

            while (prev != null)
            {
                int nodeKey = node.Key;
                int prevKey = prev.Key;
                Assert.AreEqual(nodeKey - 1, prevKey, $"Unexpected previous key: {prevKey}");
                node = prev;
                prev = tree.Prev(prev);
            }
        }

        [TestMethod]
        public void TestTreeInOrderTraversal()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            int[] preOrderKeys = { 3, 1, 0, 2, 7, 5, 4, 6, 8, 9 };
            int[] postOrderKeys = { 0, 2, 1, 4, 6, 5, 9, 8, 7, 3 };
            int[] bfsKeys = { 3, 1, 7, 0, 2, 5, 8, 4, 6, 9 };
            TestContext.TraversalTestHelper(tree, preOrderKeys, postOrderKeys, bfsKeys);
        }

        [TestMethod]
        public void TestTreeReversedTraversal()
        {
            TreeSet<int> tree = TestContext.GetTreeSetReversed();
            int[] preOrderKeys = { 6, 2, 1, 0, 4, 3, 5, 8, 7, 9 };
            int[] postOrderKeys = { 0, 1, 3, 5, 4, 2, 7, 9, 8, 6 };
            int[] bfsKeys = { 6, 2, 8, 1, 4, 7, 9, 0, 3, 5 };
            TestContext.TraversalTestHelper(tree, preOrderKeys, postOrderKeys, bfsKeys);
        }

        [TestMethod]
        public void TestTreeScrambledTraversal()
        {
            TreeSet<int> tree = TestContext.GetTreeSetScrambled();
            int[] preOrderKeys = { 4, 1, 0, 2, 3, 6, 5, 8, 7, 9 };
            int[] postOrderKeys = { 0, 3, 2, 1, 5, 7, 9, 8, 6, 4 };
            int[] bfsKeys = { 4, 1, 6, 0, 2, 5, 8, 3, 7, 9 };
            TestContext.TraversalTestHelper(tree, preOrderKeys, postOrderKeys, bfsKeys);
        }

        [TestMethod]
        public void TestTreeDeleteInnerNode()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            tree.Delete(5);
            Assert.AreEqual(9, tree.Size, "Unexpected tree size after delete");
            TreeNode<int>? node = tree.Find(5);
            Assert.IsNull(node, "Node found after delete");
        }

        [TestMethod]
        public void TestTreeDeleteMinNode()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            TreeNode<int>? deleteNode = tree.Min();
            Assert.IsNotNull(deleteNode, "Delete node not found");
            tree.Delete(deleteNode.Key);
            TreeNode<int>? minNode = tree.Min();
            Assert.IsNotNull(minNode, "Tree minimum node is null after delete");
            Assert.AreEqual(1, minNode.Key, $"Unexpected min node after delete: {minNode.Key}");
        }

        [TestMethod]
        public void TestTreeDeleteMaxNode()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            TreeNode<int>? deleteNode = tree.Max();
            Assert.IsNotNull(deleteNode, "Delete node not found");
            tree.Delete(deleteNode.Key);
            TreeNode<int>? maxNode = tree.Max();
            Assert.IsNotNull(maxNode, "Tree maximum is null after delete");
            Assert.AreEqual(8, maxNode.Key, $"Unexpected max node after delete: {maxNode.Key}");
        }

        [TestMethod]
        public void TestTreeDeleteRootNode()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            TreeNode<int>? deleteNode = tree.Root;
            Assert.IsNotNull(deleteNode, "Delete node not found");
            tree.Delete(deleteNode.Key);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(4, tree.Root.Key, $"Unexpected root node after delete: {tree.Root.Key}");
        }

        [TestMethod]
        public void TestTreeDeleteAllNodes()
        {
            TreeSet<int> tree = TestContext.GetTreeSetInOrder();
            foreach (int key in TestContext.TestKeys)
                tree.Delete(key);

            Assert.AreEqual(0, tree.Size, "Tree is not empty after deleting all nodes");
            Assert.IsNull(tree.Root, "Tree root is not null after deleting all nodes");
        }
    }
}