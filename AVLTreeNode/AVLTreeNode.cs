using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AVLTreeNode
{
    public class AVLTreeNode<TNode> : IComparable<TNode> where TNode : IComparable
    {
        AVLTree<TNode> _tree;

        AVLTreeNode<TNode> _left;
        AVLTreeNode<TNode> _right;

        public AVLTreeNode(TNode value, AVLTreeNode<TNode> parent, AVLTree<TNode> tree)
        {
            Value = value;
            Parent = parent;
            _tree = tree;
        }

        public TNode Value
        {
            get;
            private set;
        }

        public AVLTreeNode<TNode> Parent
        {
            get;
            internal set;
        }
        public AVLTreeNode<TNode> Left
        {
            get
            {
                return _left;
            }
            internal set
            {
                _left = value;
                if(_left != null)
                {
                    _left.Parent = this;
                }
            }
        }
        public AVLTreeNode<TNode> Right
        {
            get
            {
                return _right;
            }
            internal set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

        internal void Balance()
        {
            if(State == TreeState.RightHeavy)
            {
                if(Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if(State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }
        private TreeState State
        {
            get
            {
                if(LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                } 
                
                if(RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }
                return TreeState.Balanced;
            }
        }
        private int LeftHeight
        {
            get
            {
                return MaxChildHeight(Left);
            }
        }
        private int RightHeight
        {
            get
            {
                return MaxChildHeight(Right);
            }
        }

        private int BalanceFactor
        {
            get
            {
                return RightHeight - LeftHeight;
            }
        }
        private int MaxChildHeight(AVLTreeNode<TNode> node)
        {
            if(node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }
            return 0;
        }
        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy
        }

        private void LeftRotation()
        {
            AVLTreeNode<TNode> newRoot = Right;
            ReplaceRoot(newRoot);
            Right = newRoot.Left;
            newRoot.Left = this;
        }

        private void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }
        private void RightRotation()
        {            
            AVLTreeNode<TNode> newRoot = Left;
            ReplaceRoot(newRoot);            
            Left = newRoot.Right;               
            newRoot.Right = this;
        }
        private void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }

        #region Replace root

        private void ReplaceRoot(AVLTreeNode<TNode> newRoot)
        {
            if(this.Parent != null)
            {
                if(this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if(this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else
            {
                _tree.Head = newRoot;
            }
            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
        }

        #endregion
    }



}

