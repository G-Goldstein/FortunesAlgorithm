using System;

namespace Structures
{
	public abstract class RBNode<T> where T : IComparable
	{
		public bool red;
		public RBBranch<T> parent;

		public abstract void Insert (RBBranch<T> newNode);
		public abstract void Remove (T t);

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

		public void DeleteRepair() {
			if (parent == null) {
				return;
			}
			
			if (Sibling ().red) {
				parent.red = true;
				Sibling ().red = false;
				if (this == parent.left) 
					parent.RotateLeft ();
				else
					parent.RotateRight ();
			}

			// Sibling is a branch (not a leaf) because it has more black children than we do.
			RBBranch<T> sibling = (RBBranch<T>)Sibling();

			if ((!parent.red) &&
			    (!sibling.red) &&
			    (!sibling.left.red) &&
			    (!sibling.right.red)) {
				sibling.red = true;
				parent.DeleteRepair ();
				return;
			}

			if ((parent.red) &&
			    (!sibling.red) &&
			    (!sibling.left.red) &&
			    (!sibling.right.red)) {
				sibling.red = true;
				parent.red = false;
				return;
			}

			if ((this == parent.left) &&
			    (!sibling.right.red)) {
				Console.WriteLine ("Step 5 hit");
				sibling.left.red = false;
				sibling.red = true;
				sibling.RotateRight ();
			} else if ((this == parent.right) &&
				(!sibling.left.red)) {
				Console.WriteLine ("Step 5 hit");
				sibling.left.red = false;
				sibling.red = true;
				sibling.RotateLeft ();
			}

			// Sibling is still a branch either from the earlier reasoning, or because our 
			// earlier sibling was rotated and a rotation must put a branch at the root.
			sibling = (RBBranch<T>)Sibling();

			sibling.red = parent.red;
			parent.red = false;

			if (this == parent.left) {
				sibling.right.red = false;
				parent.RotateLeft ();
			} else {
				sibling.left.red = false;
				parent.RotateRight ();
			}
		}
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

		public override void Insert (RBBranch<T> newNode)
		{
			T t = newNode.value;
			int compareResult = t.CompareTo (value);
			if (compareResult == 0)
				throw new ArgumentException (string.Format("Duplicate value {0} specified", t));
			else if (compareResult > 0)
				right.Insert (newNode);
			else
				left.Insert (newNode);
		}

		public override void Remove (T t) {
			int compareResult = t.CompareTo (value);
			if (compareResult == 0)
				Delete ();
			else if (compareResult > 0)
				right.Remove (t);
			else
				left.Remove (t);
		}

		public void RotateLeft() {
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

		public void RotateRight() {
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

		RBBranch<T> MinBranch() {
			if (!(left is RBBranch<T>))
				return this;
			RBBranch<T> branch = (RBBranch<T>)left;
			return branch;
		}

		public void Delete() {
			RBBranch<T> minBranch = this;
			if (right is RBBranch<T>) {
				RBBranch<T> rightBranch = (RBBranch<T>)right;
				minBranch = rightBranch.MinBranch ();
				value = minBranch.value;
			}
			minBranch.DeleteBranchWithNoMoreThanOneChildBranch ();
		}

		void ReplaceNode(RBNode<T> child) {
			child.parent = parent;
			if (parent != null) {
				if (this == parent.left)
					parent.left = child;
				else
					parent.right = child;
			}
		}

		void DeleteBranchWithNoMoreThanOneChildBranch() {
			RBNode<T> child;
			if (!(left is RBBranch<T>))
				child = right;
			else
				child = left;

			ReplaceNode (child);
			if (!red) {
				if (child.red)
					child.red = false;
				else
					child.DeleteRepair ();
			}
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

		public override void Remove (T t) {
			throw new ArgumentException (string.Format("Value {0} to remove not found", t));
		}
	}
}

