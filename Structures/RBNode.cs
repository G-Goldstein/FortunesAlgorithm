using System;

namespace Structures
{
	public abstract class RBNode<T> where T : IComparable
	{
		public bool red;
		public RBBranch<T> parent;

		protected RBBranch<T> Grandparent() {
			if (parent == null)
				return null;
			return parent.parent;
		}

		protected RBNode<T> Sibling() {
			if (parent == null)
				return null;
			if (parent.left == this)
				return parent.right;
			return parent.left;
		}

		protected RBNode<T> Uncle() {
			if (parent == null)
				return null;
			return parent.Sibling ();
		}

		public abstract void Insert (RBBranch<T> newNode);
	}

	public class RBBranch<T> : RBNode<T> where T : IComparable {
		public T value;
		public RBNode<T> left;
		public RBNode<T> right;

		public RBBranch(T value, RBBranch<T> parent) {
			this.parent = parent;
			this.value = value;
			red = true;
			left = new RBLeaf<T> (this);
			right = new RBLeaf<T> (this);
		}

		public RBBranch(T value) : this(value, null) {}

		void RotateLeft() {
			if (!(right is RBBranch<T>))
				throw new InvalidOperationException ("Can't rotate left a branch with a right leaf");
			RBBranch<T> newLocalRoot = (RBBranch<T>)right;
			RBNode<T> middle = newLocalRoot.left;
			if (parent != null) {
				if (this == parent.left)
					parent.left = newLocalRoot;
				else
					parent.right = newLocalRoot;
			}
			newLocalRoot.parent = this.parent;
			this.parent = newLocalRoot;
			newLocalRoot.left = this;
			this.right = middle;
			middle.parent = this;
		}

		void RotateRight() {
			if (!(left is RBBranch<T>))
				throw new InvalidOperationException ("Can't rotate right a branch with a left leaf");
			RBBranch<T> newLocalRoot = (RBBranch<T>)left;
			RBNode<T> middle = newLocalRoot.right;
			if (parent != null) {
				if (this == parent.left)
					parent.left = newLocalRoot;
				else
					parent.right = newLocalRoot;
			}
			newLocalRoot.parent = this.parent;
			this.parent = newLocalRoot;
			newLocalRoot.right = this;
			this.left = middle;
			middle.parent = this; 
		}

		public override void Insert (RBBranch<T> newNode)
		{
			if (newNode.value.CompareTo (this.value) > 0)
				right.Insert (newNode);
			else
				left.Insert (newNode);
		}

		public void InsertRepair() {
			if (parent == null) {
				red = false;
			} else if (!parent.red) {
				return;
			} else if (Uncle ().red) {
				RepaintEldersAndPercolateInsertRepair ();
			} else {
				RotateParentIntoGrandparentPosition ();
			}
		}

		void RepaintEldersAndPercolateInsertRepair() {
			parent.red = false;
			Uncle ().red = false;
			Grandparent ().red = true;
			Grandparent ().InsertRepair ();
		}

		void RotateParentIntoGrandparentPosition () {
			RepairRotateToOutside ().RepairRotateParentToGrandparent ();
		}

		RBBranch<T> RepairRotateToOutside() {
			RBBranch<T> originalParent = parent;
			if (parent == Grandparent ().left && this == parent.right) {
				parent.RotateLeft ();
				return originalParent;
			} else if (parent == Grandparent().right && this == parent.left) {
				parent.RotateRight ();
				return originalParent;
			}
			return this;
		}

		void RepairRotateParentToGrandparent() {
			parent.red = false;
			Grandparent().red = true;
			if (this == parent.left)
				Grandparent().RotateRight ();
			else
				Grandparent().RotateLeft ();
		}
	}

	public class RBLeaf<T> : RBNode<T> where T : IComparable {
		public RBLeaf(RBBranch<T> parent) {
			this.parent = parent;
			red = false;
		}

		public RBLeaf() : this(null) {}

		public override void Insert (RBBranch<T> newNode)
		{
			if (parent != null) {
				if (parent.left == this)
					parent.left = newNode;
				else
					parent.right = newNode;
			}
			newNode.parent = parent;
		}
	}
}

