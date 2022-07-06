namespace TreeDS
{
    public class TreeSet<K> : TreeBase<K> where K : IComparable
    {
        public TreeSet() : base() {}

        public void Add(K key)
        {
            KeyNode<K> node = new KeyNode<K>(key);
            Root = InsertNode(Root, node);
            if (Root != null) Root.Parent = null;
        }

        public void Delete(K key)
        {
            DeleteNode(Find(key));
        }

        public KeyNode<K>? Find(K key)
        {
            TreeNode<K>? node = FindNode(key);
            return node != null ? (KeyNode<K>)node : null;
        }

        public KeyNode<K>? Min()
        {
            TreeNode<K>? min = Root?.GetSubtreeMin();
            return min != null ? (KeyNode<K>)min : null;
        }

        public TreeNode<K>? Max()
        {
            TreeNode<K>? max = Root?.GetSubtreeMax();
            return max != null ? (KeyNode<K>)max : null;
        }

        public KeyNode<K>? Next(TreeNode<K>? node)
        {
            if (node == null)
                return null;
                
            if (node.Right != null)
                return (KeyNode<K>)node.Right.GetSubtreeMin();

            TreeNode<K>? parent = node.Parent;
            while (parent?.Right != null && node.Key.CompareTo(parent.Right.Key) == 0)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent != null ? (KeyNode<K>)parent : null;
        }

        public KeyNode<K>? Prev(TreeNode<K>? node)
        {
            if (node == null)
                return null;

            if (node.Left != null)
                return (KeyNode<K>)node.Left.GetSubtreeMax();

            TreeNode<K>? parent = node.Parent;
            while (parent?.Left != null && node.Key.CompareTo(parent.Left.Key) == 0)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent != null ? (KeyNode<K>)parent : null;
        }
    }
}