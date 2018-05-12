using System;

namespace Structures
{
	
	public class RBTree<T> where T : IComparable
	{
		public RBNode<T> root;

		public RBTree ()
		{
			root = new RBLeaf<T> ();
		}

		void Insert(RBBranch<T> node) {
			root.Insert (node);
			node.InsertRepair ();
			root = node;
			while (root.parent != null)
				root = root.parent;
		}

		public void Add(T value) {
			Insert (new RBBranch<T> (value));
		}

		public void Remove(T value) {
			root.Remove (value);

			RBBranch<T> rootBranch = (RBBranch<T>)root;
			if (rootBranch.left.parent == null) {
				root = rootBranch.left;
			}
			else if (rootBranch.right.parent == null) {
				root = rootBranch.right;
			}
			while (root.parent != null)
				root = root.parent;
		}
	}
}

