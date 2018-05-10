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
			return parent.right;
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
			RBBranch<T> newLocalRoot = (RBBranch<T>)right;
			if (newLocalRoot == null) 
				throw new InvalidOperationException ("Can't rotate left a branch with a right leaf");
			RBNode<T> middle = newLocalRoot.left;
			newLocalRoot.parent = this.parent;
			this.parent = newLocalRoot;
			newLocalRoot.left = this;
			this.right = middle;
			middle.parent = this; 
		}

		void RotateRight() {
			RBBranch<T> newLocalRoot = (RBBranch<T>)left;
			if (newLocalRoot == null) 
				throw new InvalidOperationException ("Can't rotate right a branch with a left leaf");
			RBNode<T> middle = newLocalRoot.right;
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

		void InsertRepair() {
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
			if (parent == Grandparent ().left && this == parent.right) {
				parent.RotateLeft ();
				return parent;
			} else if (parent == Grandparent().right && this == parent.left) {
				parent.RotateRight ();
				return parent;
			}
			return this;
		}

		void RepairRotateParentToGrandparent() {
			if (this == parent.left)
				Grandparent ().RotateRight ();
			else
				Grandparent ().RotateLeft ();
			parent.red = false;
			Grandparent ().red = true;
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
			if (parent.left == this)
				parent.left = newNode;
			else
				parent.right = newNode;
			newNode.parent = parent;
		}
	}
}

