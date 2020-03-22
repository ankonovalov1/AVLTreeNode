using System;

namespace AVLTreeNode
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> avlTree = new AVLTree<int>();

            avlTree.Add(10);
            avlTree.Add(3);
            avlTree.Add(2);
            avlTree.Add(4);
            avlTree.Add(12);
            avlTree.Add(15);
            avlTree.Add(11);

            foreach(var item in avlTree)
            {
                Console.WriteLine(item);
            }
        }
    }
}
