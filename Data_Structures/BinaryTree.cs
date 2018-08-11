using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    #region Binary_Tree Node
    class Node<T> : IComparable<T> where T : IComparable<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public T Value { get; private set; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
    #endregion

    #region Binary_Tree

    class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> head;

        private int count;
        private int Count
        {
            get
            {
                return count;
            }
        }

        public void Add(T value)
        {
            if (head == null)
            {
                head = new Node<T>(value);
            }

            else
            {
                AddTo(head, value);
            }
            count++;
        }

        private void AddTo(Node<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node<T>(value);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }

            else
            {
                if (node.Right == null)
                {
                    node.Right = new Node<T>(value);
                }

                else
                {
                    AddTo(node.Right, value);
                }
            }
        }

        public bool Contains(T value)
        {
            Node<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        private Node<T> FindWithParent(T value, out Node<T> parent)
        {
            Node<T> current = head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        public bool Remove(T value)
        {
            Node<T> current;
            Node<T> parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }

            count--;

            if (current.Right == null)
            {
                if (parent == null)
                {
                    head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }

                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }

            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    head = current.Right;
                }

                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }

                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }

            else
            {
                Node<T> leftmost = current.Right.Left;
                Node<T> leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                leftmostParent.Left = leftmost.Right;

                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    head = leftmost;
                }

                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }

                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }

            return true;
        }

        public IEnumerator<T> InOrderTraversal()
        {
            if (head != null)
            {
                Stack<Node<T>> stack = new Stack<Node<T>>();
                Node<T> current = head;

                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count() > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }

                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }

            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
    #endregion
}
