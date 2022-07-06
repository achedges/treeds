namespace TreeDS
{
    public class TreeMap<K, V> : TreeBase<K> where K : IComparable
    {
        public TreeMap() : base() {}

        public void Add(K key, V value)
        {
            KeyValueNode<K, V> node = new KeyValueNode<K, V>(key, value);
            Root = InsertNode(Root, node);
            if (Root != null) Root.Parent = null;
        }

        public void Delete(K key)
        {
            DeleteNode(FindNode(key));
        }

        public KeyValueNode<K, V>? Find(K key)
        {
            TreeNode<K>? node = FindNode(key);
            return node != null ? (KeyValueNode<K, V>)node : null;
        }

        public KeyValueNode<K, V>? Min()
        {
            TreeNode<K>? min = Root?.GetSubtreeMin();
            return min != null ? (KeyValueNode<K, V>)min : null;
        }

        public KeyValueNode<K, V>? Max()
        {
            TreeNode<K>? max = Root?.GetSubtreeMax();
            return max != null ? (KeyValueNode<K, V>)max : null;
        }

        public KeyValueNode<K, V>? Next(TreeNode<K> node)
        {
            if (node.Right != null)
                return (KeyValueNode<K, V>)node.Right.GetSubtreeMin();

            TreeNode<K>? parent = node.Parent;
            while (parent?.Right != null && node.Key.CompareTo(parent.Right.Key) == 0)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent != null ? (KeyValueNode<K, V>)parent : null;
        }

        public KeyValueNode<K, V>? Prev(TreeNode<K> node)
        {
            if (node.Left != null)
                return (KeyValueNode<K, V>)node.Left.GetSubtreeMax();

            TreeNode<K>? parent = node.Parent;
            while (parent?.Left != null && node.Key.CompareTo(parent.Left.Key) == 0)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent != null ? (KeyValueNode<K, V>)parent : null;
        }
    }
}