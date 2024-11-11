using System;
using System.Windows.Controls;

public class BinarySearchTreeHandler
{
    private class TreeNode
    {
        public int Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private TreeNode root;

    public BinarySearchTreeHandler()
    {
        root = null;
    }

    // Вставка элемента в дерево
    public void Insert(int value, TextBlock outputTextBlock)
    {
        root = InsertRec(root, value, outputTextBlock);
    }

    private TreeNode InsertRec(TreeNode node, int value, TextBlock outputTextBlock)
    {
        if (node == null)
        {
            outputTextBlock.Text += $"Элемент {value} добавлен в дерево.\n";
            return new TreeNode(value);
        }

        if (value < node.Value)
        {
            node.Left = InsertRec(node.Left, value, outputTextBlock);
        }
        else if (value > node.Value)
        {
            node.Right = InsertRec(node.Right, value, outputTextBlock);
        }
        else
        {
            outputTextBlock.Text += $"Элемент {value} уже существует в дереве.\n";
        }

        return node;
    }

    // Поиск элемента в дереве
    public bool Search(int value, TextBlock outputTextBlock)
    {
        bool found = SearchRec(root, value);
        outputTextBlock.Text += found ? $"Элемент {value} найден в дереве.\n" : $"Элемент {value} не найден в дереве.\n";
        return found;
    }

    private bool SearchRec(TreeNode node, int value)
    {
        if (node == null)
        {
            return false;
        }

        if (node.Value == value)
        {
            return true;
        }

        if (value < node.Value)
        {
            return SearchRec(node.Left, value);
        }
        else
        {
            return SearchRec(node.Right, value);
        }
    }

    // Печать элементов дерева (обход в порядке возрастания)
    public void InOrderTraversal(TextBlock outputTextBlock)
    {
        InOrderTraversalRec(root, outputTextBlock);
        outputTextBlock.Text += "\n";
    }

    private void InOrderTraversalRec(TreeNode node, TextBlock outputTextBlock)
    {
        if (node != null)
        {
            InOrderTraversalRec(node.Left, outputTextBlock);
            outputTextBlock.Text += node.Value + " ";
            InOrderTraversalRec(node.Right, outputTextBlock);
        }
    }
}
