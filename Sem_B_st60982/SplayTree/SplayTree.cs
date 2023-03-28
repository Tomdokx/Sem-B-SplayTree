using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sem_B_st60982.SplayTree
{
	public class SplayTree<V> where V : IComparable<V>
	{
		class Node<V>
		{
			public V Value { get; set; }
			public Node<V>? Left { get; set; }
			public Node<V>? Right { get; set; }
			public Node<V>? Parent { get; set; }
		}

		private Node<V>? root;
		public int Count { get; private set; } = 0;
		public void Insert(V value)
		{
			if (root == null)
			{
				root = new Node<V> { Value = value};
				return;
			}

			// Perform standard binary search tree insertion
			Node<V>? parent = null;
			Node<V> node = root;
			while (node != null)
			{
				parent = node;
				if (value.CompareTo(node.Value) > 0)
				{
					node = node.Left;
				}
				else if (value.CompareTo(node.Value) < 0)
				{
					node = node.Right;
				}
				else
				{
					// Key already exists in tree, update its value
					node.Value = value;
					return;
				}
			}

			// Create new node with given key and value
			var new_node = new Node<V> { Value = value };

			// Attach new node to parent
			new_node.Parent = parent;
			if (value.CompareTo(parent.Value) > 0)
			{
				parent.Left = new_node;
			}
			else
			{
				parent.Right = new_node;
			}

			// Apply splay operation to bring new node to root
			Splay(new_node);
			Count++;
		}

		private void Splay(Node<V> node)
		{
			while (node != root)
			{
				var parent = node.Parent;

				if (parent == root)
				{
					if (node == parent.Left)
					{
						RotateRight(parent);
					}
					else
					{
						RotateLeft(parent);
					}
				}
				else
				{
					var grandparent = parent.Parent;

					if (node == parent.Left && parent == grandparent.Left)
					{
						RotateRight(grandparent);
						RotateRight(parent);
					}
					else if (node == parent.Right && parent == grandparent.Right)
					{
						RotateLeft(grandparent);
						RotateLeft(parent);
					}
					else if (node == parent.Right && parent == grandparent.Left)
					{
						RotateLeft(parent);
						RotateRight(grandparent);
					}
					else
					{
						RotateRight(parent);
						RotateLeft(grandparent);
					}
				}
			}
		}

		private void RotateLeft(Node<V> node)
		{
			var parent = node.Parent;
			var right_child = node.Right;
			node.Right = right_child.Left;
			right_child.Left = node;
			node.Parent = right_child;

			if (parent == null)
			{
				root = right_child;
			}
			else if (node == parent.Left)
			{
				parent.Left = right_child;
			}
			else
			{
				parent.Right = right_child;
			}
			right_child.Parent = parent;
		}

		private void RotateRight(Node<V> node)
		{
			var parent = node.Parent;
			var left_child = node.Left;
			node.Left = left_child.Right;
			left_child.Right = node;
			node.Parent = left_child;

			if (parent == null)
			{
				root = left_child;
			}
			else if (node == parent.Left)
			{
				parent.Left = left_child;
			}
			else
			{
				parent.Right = left_child;
			}
			left_child.Parent = parent;
		}

		public void Delete(V key)
		{
			Node<V> node = FindNode(key);
			if (node == null)
			{
				return; // key not found
			}

			Splay(node); // bring the node to the root without move-to-front heuristic

			if (node.Left == null)
			{
				ReplaceNode(node, node.Right);
			}
			else if (node.Right == null)
			{
				ReplaceNode(node, node.Left);
			}
			else
			{
				Node<V> minRight = FindMin(node.Right);
				if (minRight.Parent != node)
				{
					ReplaceNode(minRight, minRight.Right);
					minRight.Right = node.Right;
					minRight.Right.Parent = minRight;
				}
				ReplaceNode(node, minRight);
				minRight.Left = node.Left;
				minRight.Left.Parent = minRight;
			}
			Count--;
		}

		private Node<V> FindMin(Node<V> node)
		{
			while (node.Left != null)
			{
				node = node.Left;
			}
			return node;
		}

		private Node<V> FindMax(Node<V> node)
		{
			while (node.Right != null)
			{
				node = node.Right;
			}
			return node;
		}

		public V? Search(V key)
		{
			Node<V> node = FindNode(key);
			if (node != null)
			{
				return node.Value;
			}
			throw new Exception("There is nothing with this Key");
		}

		private void ReplaceNode(Node<V> node, Node<V>? newNode)
		{
			if (node.Parent == null)
			{
				root = newNode;
			}
			else if (node == node.Parent.Left)
			{
				node.Parent.Left = newNode;
			}
			else
			{
				node.Parent.Right = newNode;
			}
			if (newNode != null)
			{
				newNode.Parent = node.Parent;
			}
		}

		private Node<V> FindNode(V key)
		{
			Node<V> node = root;
			while (node != null)
			{
				if (node.Value.CompareTo(key) == 0)
				{
					Splay(node);
					return node;
				}
				else if (node.Value.CompareTo(key) < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}
			return null; // key not found
		}

		private int GetHeight(Node<V> node)
		{ 
				if (node == null)
				{
					return 0;
				}

				int leftHeight = GetHeight(node.Left);
				int rightHeight = GetHeight(node.Right);

				return 1 + Math.Max(leftHeight, rightHeight);
		}
		public int GetHeightOfTheTree()
		{
			return GetHeight(root);
		}

		public void Destroy()
		{
			root = null;
			Count = 0;
			GC.Collect();
		}
	}
}
