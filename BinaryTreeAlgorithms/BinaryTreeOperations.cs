using BinaryTreeAlgorithms.Models;

namespace BinaryTreeAlgorithms;

public class BinaryTreeOperations<T> where T : IComparable<T>, IEquatable<T>
{
    #region Depth First Values

    public static List<T> DepthFirstValues(Node<T> root)
    {
        var result = new List<T>();
        var stack = new Stack<Node<T>>();
        if (root is not null)
            stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            result.Add(current.Value);

            if (current.Right is not null)
                stack.Push(current.Right);


            if (current.Left is not null)
                stack.Push(current.Left);
        }

        return result;
    }

    public static List<T> DepthFirstValuesRecursive(Node<T> root, Stack<Node<T>> stack = null, List<T> result = null)
    {
        result ??= new();

        if (root is null)
            return result;

        if (stack is null)
        {
            stack = new();
            stack.Push(root);
        }

        if (stack.Count == 0)
            return result;


        var current = stack.Pop();
        result.Add(current.Value);

        if (current.Right is not null)
            stack.Push(current.Right);

        if (current.Left is not null)
            stack.Push(current.Left);

        return DepthFirstValuesRecursive(root, stack, result);
    }

    public static List<T> DepthFirstValuesFancyRecursive(Node<T> root)
    {
        if (root is null)
            return new();

        var leftTree = DepthFirstValuesFancyRecursive(root.Left);
        var rightTree = DepthFirstValuesFancyRecursive(root.Right);

        var result = new List<T> {root.Value};
        result.AddRange(leftTree);
        result.AddRange(rightTree);
        return result;
    }

    #endregion

    #region Breadth First Values

    public static List<T> BreadthFirstValues(Node<T> root)
    {
        var result = new List<T>();

        if (root is null) return result;

        var queue = new Queue<Node<T>>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            result.Add(current.Value);

            if (current.Left is not null) queue.Enqueue(current.Left);
            if (current.Right is not null) queue.Enqueue(current.Right);
        }

        return result;
    }

    public static List<T> BreadthFirstValuesRecursive(Node<T> root, Queue<Node<T>> queue = null, List<T> result = null)
    {
        result ??= new();

        if (root is null)
            return result;

        if (queue is null)
        {
            queue = new();
            queue.Enqueue(root);
        }

        if (queue.Count == 0) return result;

        var current = queue.Dequeue();
        result.Add(current.Value);

        if (current.Left is not null) queue.Enqueue(current.Left);
        if (current.Right is not null) queue.Enqueue(current.Right);

        return BreadthFirstValuesRecursive(root, queue, result);
    }

    #endregion

    #region Include Value

    public static bool Contains(Node<T> root, T value)
    {
        if (root is null) return false;

        var queue = new Queue<Node<T>>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Value.Equals(value)) return true;

            if (current.Left is not null) queue.Enqueue(current.Left);
            if (current.Right is not null) queue.Enqueue(current.Right);
        }

        return false;
    }

    public static bool ContainsRecursive(Node<T> root, T value)
    {
        if (root is null) return false;

        if (root.Value.Equals(value)) return true;

        return Contains(root.Left, value) || Contains(root.Right, value);
    }

    #endregion

    #region Invert Binary Tree

    public static void InvertBinaryTree(Node<T> root)
    {
        if (root is null)
            return;

        (root.Left, root.Right) = (root.Right, root.Left);
        InvertBinaryTree(root.Left);
        InvertBinaryTree(root.Right);
    }

    #endregion

    #region Tree Path Finder

    public static List<T> FindPathToValue(Node<T> root, T value)
    {
        var resultList = FindPathToValueHelper(root, value);
        if (resultList is null)
            return null;
        resultList.Reverse();
        return resultList;
    }

    private static List<T> FindPathToValueHelper(Node<T> root, T value)
    {
        if (root is null)
        {
            return null;
        }

        if (value.Equals(root.Value))
        {
            return new List<T> {root.Value};
        }

        var leftSide = FindPathToValue(root.Left, value);
        var rightSide = FindPathToValue(root.Left, value);

        if (leftSide is not null)
        {
            leftSide.Add(root.Value);
            return leftSide;
        }

        if (rightSide is not null)
        {
            rightSide.Add(root.Value);
            return rightSide;
        }

        return null;
    }

    #endregion

    #region TreeCountValue

    public static int ValueCountRecursive(Node<T> root, T value)
    {
        if (root is null)
            return 0;

        return ValueCountRecursive(root.Left, value) + ValueCountRecursive(root.Right, value) +
               (root.Value.Equals(value) ? 1 : 0);
    }

    public static int ValueCount(Node<T> root, T value)
    {
        int counter = 0;
        if (root is null)
            return counter;

        var queue = new Queue<Node<T>>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Value.Equals(value))
                counter++;

            if (current.Right is not null)
                queue.Enqueue(current.Right);

            if (current.Left is not null)
                queue.Enqueue(current.Left);
        }

        return counter;
    }

    #endregion

    #region Tree Heihgt

    public static int GetTreeHeight(Node<T> root)
    {
        if (root is null)
            return -1;


        int leftHeight = GetTreeHeight(root.Left);
        int rightHeight = GetTreeHeight(root.Right);
        return 1 + Math.Max(leftHeight, rightHeight);
    }

    #endregion

    #region Bottom Right Value

    #region Recursive Version

    public static T GetBottomRightRecursive(Node<T> root)
    {
        return GetBottomRightRecursiveHelper(root).leafValue;
    }

    private static (T leafValue, int level) GetBottomRightRecursiveHelper(Node<T> root)
    {
        if (root is null)
            return (default, 0);

        if (root.Right is null && root.Left is null)
            return (root.Value, 0);

        var leftResult = GetBottomRightRecursiveHelper(root.Left);
        var rightResult = GetBottomRightRecursiveHelper(root.Right);


        if (leftResult.level > rightResult.level)
            return (leftResult.leafValue, leftResult.level + 1);

        return (rightResult.leafValue, rightResult.level + 1);
    }

    #endregion

    public static T GetBottomRight(Node<T> root)
    {
        if (root is null)
            return default;

        var queue = new Queue<Node<T>>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Left is not null)
                queue.Enqueue(current.Left);

            if (current.Right is not null)
                queue.Enqueue(current.Right);

            if (queue.Count == 0)
                return current.Value;
        }

        return default;
    }

    #endregion

    #region All Tree Paths

    public static List<List<T>> GetAllPaths(Node<T> root)
    {
        var allPaths = GetAllPathsHelper(root);

        foreach (var path in allPaths)
        {
            path.Reverse();
        }

        return allPaths;
    }

    private static List<List<T>> GetAllPathsHelper(Node<T> root)
    {
        if (root is null)
            return null;

        if (root.Left is null && root.Right is null)
        {
            var innerList = new List<T> {root.Value};
            var outerList = new List<List<T>> {innerList};
            return outerList;
        }

        var leftPaths = GetAllPathsHelper(root.Left);
        var rightPaths = GetAllPathsHelper(root.Right);

        var allPaths = new List<List<T>>();
        if (leftPaths is not null)
            allPaths.AddRange(leftPaths);
        if (rightPaths is not null)
            allPaths.AddRange(rightPaths);

        foreach (var path in allPaths)
        {
            path.Add(root.Value);
        }

        return allPaths;
    }

    #endregion

    #region Tree Levels

    #region Iterative Version

    public static List<List<T>> GetAllTreeLevels(Node<T> root)
    {
        var allLevels = new List<List<T>>();

        if (root is null)
            return null;


        var queue = new Queue<(Node<T> noode, int level )>();
        queue.Enqueue((root, 0));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            PushToList(allLevels, current);

            if (current.noode.Left is not null)
            {
                queue.Enqueue((current.noode.Left, current.level + 1));
            }

            if (current.noode.Right is not null)
            {
                queue.Enqueue((current.noode.Right, current.level + 1));
            }
        }

        return allLevels;
    }

    private static void PushToList(List<List<T>> list, (Node<T> noode, int level) current)
    {
        int i = current.level;
        if (list.Count >= i + 1)
        {
            list[i].Add(current.noode.Value);
        }
        else
        {
            list.Add(new List<T> {current.noode.Value});
        }
    }

    #endregion

    #region Recursive Version

    public static List<List<T>> GetAllTreeLevelsRecursive(Node<T> root)
    {
        if (root is null)
            return null;

        var allLevels = new List<List<T>>();

        GetAllTreeLevelsRecursiveHelper(root, allLevels);

        return allLevels;
    }

    private static int GetAllTreeLevelsRecursiveHelper(Node<T> root, List<List<T>> treeLevels)
    {
        if (root is null)
            return -1;

        if (root.Left is null && root.Right is null)
        {
            TryToAddToList(treeLevels, 0, root.Value);
            return 0;
        }

        int leftLevel = GetAllTreeLevelsRecursiveHelper(root.Left, treeLevels);
        int rightLevel = GetAllTreeLevelsRecursiveHelper(root.Right, treeLevels);
        int currentLevel = Math.Max(leftLevel, rightLevel) + 1;

        TryToAddToList(treeLevels, currentLevel, root.Value);
        return currentLevel;
    }

    private static void TryToAddToList(List<List<T>> outerList, int level, T value)
    {
        if (outerList.Count >= level + 1)
            outerList[level].Add(value);
        else
            outerList.Add(new List<T> {value});
    }

    #endregion

    #endregion

    #region Get All Leafs

    #region Recursive Version

    public static List<T> GetAllLeavesRecursive(Node<T> root)
    {
        if (root is null)
            return null;

        var leaves = new List<T>();
        FillLeavesRecursiveHelper(root, leaves);

        return leaves;
    }

    private static void FillLeavesRecursiveHelper(Node<T> root, List<T> leaves)
    {
        if (root is null)
            return;

        if (root.Left is null && root.Right is null)
        {
            leaves.Add(root.Value);
            return;
        }

        FillLeavesRecursiveHelper(root.Left, leaves);
        FillLeavesRecursiveHelper(root.Right, leaves);
    }

    #endregion

    #region Iterative Version

    public static List<T> GetAllLeaves(Node<T> root)
    {
        var leaves = new List<T>();

        if (root is null)
            return null;

        var stack = new Stack<Node<T>>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (current.Left is null && current.Right is null)
                leaves.Add(current.Value);

            if (current.Right is not null)
                stack.Push(current.Right);

            if (current.Left is not null)
                stack.Push(current.Left);
        }

        return leaves;
    }

    #endregion

    #endregion
}

class BinaryTreeOperationsNumeric : BinaryTreeOperations<int>
{
    #region Sum

    public static int SuRecursive(Node<int> root)
    {
        if (root is null) return 0;
        return SuRecursive(root.Left) + SuRecursive(root.Right) + root.Value;
    }

    public static int Sum(Node<int> root)
    {
        var sum = 0;
        if (root is null) return sum;

        var queue = new Queue<Node<int>>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            sum += current.Value;

            if (current.Left is not null) queue.Enqueue(current.Left);
            if (current.Right is not null) queue.Enqueue(current.Right);
        }

        return sum;
    }

    #endregion

    #region Minimum Value

    public static int GetMinimumRecursive(Node<int> root)
    {
        if (root is null)
            return int.MaxValue;

        var leftValue = GetMinimumRecursive(root.Left);
        var rightValue = GetMinimumRecursive(root.Right);
        var minChild = leftValue < rightValue ? leftValue : rightValue;

        return minChild < root.Value ? minChild : root.Value;
    }

    public static int GetMinimumDepthFirst(Node<int> root)
    {
        if (root is null)
            return int.MaxValue;

        var stack = new Stack<Node<int>>();
        stack.Push(root);

        int min = root.Value;

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (current.Value < min)
                min = current.Value;

            if (current.Right is not null)
                stack.Push(current.Right);

            if (current.Left is not null)
                stack.Push(current.Left);
        }

        return min;
    }

    public static int GetMinimumBreadthFirst(Node<int> root)
    {
        if (root is null)
            return int.MaxValue;

        var queue = new Queue<Node<int>>();
        queue.Enqueue(root);

        int min = root.Value;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Value < min)
                min = current.Value;

            if (current.Right is not null)
                queue.Enqueue(current.Right);

            if (current.Left is not null)
                queue.Enqueue(current.Left);
        }

        return min;
    }

    #endregion

    #region Max Root To Leaf

    public static int GetMaxRootToLeaf(Node<int> root)
    {
        if (root is null)
            return int.MinValue;

        int leftMax = GetMaxRootToLeaf(root.Left);
        int rightMax = GetMaxRootToLeaf(root.Right);
        if (leftMax == rightMax && rightMax == int.MinValue)
        {
            return root.Value;
        }

        return root.Value + (leftMax > rightMax ? leftMax : rightMax);
    }

    #endregion

    #region Tree Level Averages

    public static List<double> GetTreeLevelsAverages(Node<int> root)
    {
        var result = new List<double>();
        if (root is null)
            return null;

        var treeLevels = new List<List<int>>();
        FillTreeLevelsRecursive(root, treeLevels, 0);

        treeLevels.ForEach(list => { result.Add(list.Average()); });

        return result;
    }

    private static void FillTreeLevelsRecursive(Node<int> root, List<List<int>> levels, int currentLevel)
    {
        if (root is null)
            return;

        AddNumberToList(root, levels, currentLevel);

        FillTreeLevelsRecursive(root.Left, levels, currentLevel + 1);
        FillTreeLevelsRecursive(root.Right, levels, currentLevel + 1);
    }

    private static void AddNumberToList(Node<int> root, List<List<int>> levels, int currentLevel)
    {
        if (levels.Count > currentLevel)
            levels[currentLevel].Add(root.Value);
        else
            levels.Add(new List<int> {root.Value});
    }

    #endregion
}