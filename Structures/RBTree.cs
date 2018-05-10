using System;

namespace Structures
{
	

	public class RBTree<T> where T : IComparable
	{
		RBNode<T> root;

		public RBTree ()
		{
			root = new RBLeaf<T> ();
		}

		void Insert(RBBranch<T> node) {
			root.Insert (node);
			// insert_repair_tree
			while (root.parent != null)
				root = root.parent;
		}
	}
}

