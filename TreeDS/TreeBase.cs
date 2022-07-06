namespace TreeDS;

public enum TreeWalkOrder
{
    InOrder,
    PreOrder,
    PostOrder,
    BreadthFirst
}

public abstract class TreeBase<K> where K : IComparable
{
    public int Size { get; set; }
    public TreeNode<K>? Root { get; set; }

    public static TreeNode<K>? RotateLeft(TreeNode<K> node)
    {
        TreeNode<K>? newRoot = node.Right;
        TreeNode<K>? tmp = newRoot?.Left;
        if (newRoot != null) newRoot.Left = node;
        node.Right = tmp;

        if (tmp != null) tmp.Parent = node;

        node.Height = node.GetMaxSubtreeHeight() + 1;
        if (newRoot != null) newRoot.Height = newRoot.GetMaxSubtreeHeight() + 1;

        return newRoot;
    }

    public static TreeNode<K>? RotateRight(TreeNode<K> node)
    {
        TreeNode<K>? newRoot = node.Left;
        TreeNode<K>? tmp = newRoot?.Right;
        if (newRoot != null) newRoot.Right = node;
        node.Left = tmp;

        if (tmp != null) tmp.Parent = node;

        node.Height = node.GetMaxSubtreeHeight() + 1;
        if (newRoot != null) newRoot.Height = newRoot.GetMaxSubtreeHeight() + 1;

        return newRoot;
    }

    public static TreeNode<K> ReplaceNode(TreeNode<K> oldNode, TreeNode<K> newNode)
    {
        newNode.Key = oldNode.Key;
        newNode.Height = oldNode.Height;
        newNode.Left = oldNode.Left;
        newNode.Right = oldNode.Right;
        newNode.Parent = oldNode.Parent;
        return newNode;
    }

    public bool Contains(K key)
    {
        return FindNode(key) != null;
    }

    protected TreeNode<K>? InsertNode(TreeNode<K>? root, TreeNode<K> node)
    {
        if (root == null)
        {
            root = node;
            Size += 1;
            return root;
        }

        if (node.Key.CompareTo(root.Key) < 0)
        {
            root.Left = InsertNode(root.Left, node);
            if (root.Left != null) root.Left.Parent = root;
        } else if (node.Key.CompareTo(root.Key) > 0)
        {
            root.Right = InsertNode(root.Right, node);
            if (root.Right != null) root.Right.Parent = root;
        } else {
            root = ReplaceNode(root, node);
        }

        int lheight = root.Left?.Height ?? 0;
        int rheight = root.Right?.Height ?? 0;
        root.Height = Math.Max(lheight, rheight) + 1;
        int balance = lheight - rheight;

        if (balance > 1 && root.Left != null && node.Key.CompareTo(root.Left.Key) < 0)
        {
            root = RotateRight(root);
        } 
        else if (balance < -1 && root.Right != null && node.Key.CompareTo(root.Right.Key) > 0)
        {
            root = RotateLeft(root);
        } 
        else if (balance > 1 && root.Left != null && node.Key.CompareTo(root.Left.Key) > 0)
        {
            root.Left = RotateLeft(root.Left);
            root = RotateRight(root);
        } 
        else if (balance < -1 && root.Right != null && node.Key.CompareTo(root.Right.Key) < 0)
        {
            root.Right = RotateRight(root.Right);
            root = RotateLeft(root);
        }

        if (root?.Left != null) root.Left.Parent = root;
        if (root?.Right != null) root.Right.Parent = root;

        return root;
    }

    public void TransplantNode(TreeNode<K> oldNode, TreeNode<K>? newNode)
    {
        if (oldNode.Parent == null)
        {
            Root = newNode;
        } 
        else if (oldNode.Parent?.Left != null && oldNode.Key.CompareTo(oldNode.Parent.Left.Key) == 0)
        {
            oldNode.Parent.Left = newNode;
        }
        else if (oldNode.Parent?.Right != null)
        {
            oldNode.Parent.Right = newNode;
        }

        if (newNode != null)
        {
            newNode.Parent = oldNode.Parent;
        }
    }

    protected void DeleteNode(TreeNode<K>? node)
    {
        if (node == null) return;

        if (node.Left == null)
        {
            TransplantNode(node, node.Right);
        }
        else if (node.Right == null)
        {
            TransplantNode(node, node.Left);
        }
        else 
        {
            TreeNode<K> y = node.Right.GetSubtreeMin();
            if (y.Parent != null && y.Parent.Key.CompareTo(node.Key) != 0)
            {
                TransplantNode(y, y.Right);
                y.Right = node.Right;
                y.Right.Parent = y;
            }
            TransplantNode(node, y);
            y.Left = node.Left;
            y.Left.Parent = y;
        }

        Size -= 1;
    }

    protected TreeNode<K>? FindNode(K key)
    {
        TreeNode<K>? node = Root;
        while (node != null)
        {
            int comp = key.CompareTo(node.Key);
            if (comp == 0)
            {
                break;
            } 
            else if (comp < 0)
            {
                node = node.Left;
            }
            else
            {
                node = node.Right;
            }
        }
        return node;
    }

    protected void WalkKeys(TreeNode<K> node, List<K> keys, TreeWalkOrder walkOrder)
    {
        switch (walkOrder)
        {
            case TreeWalkOrder.InOrder:
                if (node.Left != null) WalkKeys(node.Left, keys, walkOrder);
                keys.Add(node.Key);
                if (node.Right != null) WalkKeys(node.Right, keys, walkOrder);
                break;
            case TreeWalkOrder.PreOrder:
                keys.Add(node.Key);
                if (node.Left != null) WalkKeys(node.Left, keys, walkOrder);
                if (node.Right != null) WalkKeys(node.Right, keys, walkOrder);
                break;
            case TreeWalkOrder.PostOrder:
                if (node.Left != null) WalkKeys(node.Left, keys, walkOrder);
                if (node.Right != null) WalkKeys(node.Right, keys, walkOrder);
                keys.Add(node.Key);
                break;
        }
    }

    protected void BFS(TreeNode<K> node, Dictionary<int, List<K>> depthMap, int depth)
    {
        if (!depthMap.ContainsKey(depth))
            depthMap[depth] = new List<K>();

        depthMap[depth].Add(node.Key);
        if (node.Left != null)
            BFS(node.Left, depthMap, depth + 1);
        if (node.Right != null)
            BFS(node.Right, depthMap, depth + 1);
    }

    public List<K> GetKeys(TreeWalkOrder order)
    {
        List<K> keys = new List<K>();
        if (Root == null)
            return keys;

        if (order == TreeWalkOrder.BreadthFirst)
        {
            Dictionary<int, List<K>> map = new Dictionary<int, List<K>>();
            BFS(Root, map, 0);
            for (int i = 0; i < map.Count; i++)
            {
                keys.AddRange(map[i]);
            }
        }
        else
        {
            WalkKeys(Root, keys, order);
        }

        return keys;
    }
}