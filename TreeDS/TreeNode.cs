namespace TreeDS;

public abstract class TreeNode<K> where K: IComparable
{
    public K Key { get; set; }
    public TreeNode<K>? Left { get; set; }
    public TreeNode<K>? Right { get; set; }
    public TreeNode<K>? Parent { get; set; }
    public int Height { get; set; }

    public TreeNode(K key)
    {
        Key = key;
        Left = null;
        Right = null;
        Parent = null;
        Height = 1;
    }

    public int GetMaxSubtreeHeight()
    {
        return Math.Max(Left?.Height ?? 0, Right?.Height ?? 0);
    }

    public TreeNode<K> GetSubtreeMin()
    {
        TreeNode<K> node = this;
        while (node.Left != null) node = node.Left;
        return node;
    }

    public TreeNode<K> GetSubtreeMax()
    {
        TreeNode<K> node = this;
        while (node.Right != null) node = node.Right;
        return node;
    }
}

public class KeyNode<K> : TreeNode<K> where K : IComparable
{
    public KeyNode(K key) : base(key) {}
}

public class KeyValueNode<K, V> : TreeNode<K> where K : IComparable
{
    public V Value { get; set; }

    public KeyValueNode(K key, V value) : base(key)
    {
        Value = value;
    }
}
