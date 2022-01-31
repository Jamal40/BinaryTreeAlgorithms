using BinaryTreeAlgorithms;
using BinaryTreeAlgorithms.Models;

#region constructing a char tree

var a = new Node<char>('a');
var b = new Node<char>('b');
var c = new Node<char>('c');
var d = new Node<char>('d');
var e = new Node<char>('e');
var f = new Node<char>('f');

a.Left = b;
a.Right = c;
b.Left = d;
b.Right = e;
c.Right = f;

#endregion

#region constructing a number tree

var one = new Node<int>(1);
var two = new Node<int>(2);
var three = new Node<int>(3);
var four = new Node<int>(4);
var five = new Node<int>(5);
var six = new Node<int>(6);

one.Left = two;
one.Right = three;
two.Left = four;
two.Right = five;
three.Right = six;
three.Left = new Node<int>(1);

#endregion

#region Running Tests

PrintCollection(BinaryTreeOperations<char>.DepthFirstValues(a));
PrintCollection(BinaryTreeOperations<char>.DepthFirstValuesRecursive(a));
PrintCollection(BinaryTreeOperations<char>.DepthFirstValuesFancyRecursive(a));
PrintCollection(BinaryTreeOperations<char>.BreadthFirstValues(a));
PrintCollection(BinaryTreeOperations<char>.BreadthFirstValuesRecursive(a));
PrintCollection(BinaryTreeOperations<int>.BreadthFirstValuesRecursive(one));
// Console.WriteLine("Invert");
// BinaryTreeOperations<char>.InvertBinaryTree(a);
PrintCollection(BinaryTreeOperations<char>.BreadthFirstValues(a));
Console.WriteLine(BinaryTreeOperations<char>.Contains(a, 'f'));
Console.WriteLine(BinaryTreeOperations<char>.ContainsRecursive(a, 'j'));
Console.WriteLine(BinaryTreeOperationsNumeric.SuRecursive(one));
Console.WriteLine(BinaryTreeOperationsNumeric.Sum(one));
Console.WriteLine(BinaryTreeOperationsNumeric.GetMinimumRecursive(one));
Console.WriteLine(BinaryTreeOperationsNumeric.GetMinimumDepthFirst(one));
Console.WriteLine(BinaryTreeOperationsNumeric.GetMinimumBreadthFirst(one));
Console.WriteLine(BinaryTreeOperationsNumeric.GetMaxRootToLeaf(one));
PrintCollection(BinaryTreeOperations<char>.FindPathToValue(a, 'c'));
PrintCollection(BinaryTreeOperations<char>.FindPathToValue(null, 'c'));
Console.WriteLine(BinaryTreeOperationsNumeric.ValueCountRecursive(one, 1));
Console.WriteLine(BinaryTreeOperationsNumeric.ValueCount(one, 1));
Console.WriteLine(BinaryTreeOperations<char>.GetTreeHeight(a));
Console.WriteLine(BinaryTreeOperations<char>.GetBottomRightRecursive(a));
Console.WriteLine(BinaryTreeOperations<char>.GetBottomRight(a));
Console.WriteLine("-----------------Tree All Paths----------------");
foreach (var path in BinaryTreeOperations<char>.GetAllPaths(a))
{
    PrintCollection(path);
}

Console.WriteLine("--------------Tree Levels Recursive---------------");
foreach (var level in BinaryTreeOperations<char>.GetAllTreeLevelsRecursive(a))
{
    PrintCollection(level);
}

Console.WriteLine("--------------Averages----------------");
PrintCollection(BinaryTreeOperationsNumeric.GetTreeLevelsAverages(one));

Console.WriteLine("--------------Leaves Recursive----------------");
PrintCollection(BinaryTreeOperationsNumeric.GetAllLeavesRecursive(one));
Console.WriteLine("--------------Leaves Iterative----------------");
PrintCollection(BinaryTreeOperationsNumeric.GetAllLeaves(one));

#endregion


static void PrintCollection<T>(IEnumerable<T> collection)
{
    if (collection is null)
        return;
    foreach (var item in collection)
    {
        Console.WriteLine(item.ToString());
    }

    Console.WriteLine("****");
}