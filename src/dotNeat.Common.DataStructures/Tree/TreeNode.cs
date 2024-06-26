﻿namespace dotNeat.Common.DataStructures.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Generic tree node class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T> : ITreeNode<T>
        where T : IComparable<T>
    {
        private T _data;// = default(T);
        private TreeNode<T>? _parent = null;
        private readonly ObservableCollection<TreeNode<T>> _children;
        private readonly ReadOnlyObservableCollection<TreeNode<T>> _readOnlyChildren;

        /// <summary>
        /// Prevents a default instance of the <see cref="TreeNode{T}"/> class from being created.
        /// </summary>
        //private TreeNode()
        //{
        //    _children = new ObservableCollection<TreeNode<T>>();
        //    _readOnlyChildren = new ReadOnlyObservableCollection<TreeNode<T>>(_children);
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public TreeNode(T data)
            : this(data, Array.Empty<T>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="childData">The child data.</param>
        public TreeNode(T data, IEnumerable<T> childData)
        {
            _children = new ObservableCollection<TreeNode<T>>();
            _readOnlyChildren = new ReadOnlyObservableCollection<TreeNode<T>>(_children);

            _data = data;
            if (childData != null)
            {
                foreach (T child in childData)
                {
                    this.Add(child);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="childNodes">The child nodes.</param>
        public TreeNode(T data, IEnumerable<TreeNode<T>> childNodes)
        {
            _children = new ObservableCollection<TreeNode<T>>();
            _readOnlyChildren = new ReadOnlyObservableCollection<TreeNode<T>>(_children);

            _data = data;
            if (childNodes != null)
            {
                foreach (var node in childNodes)
                {
                    _children.Add(node);
                    node.Parent = this;
                }
            }

        }

        /// <summary>
        /// Adds the specified child data.
        /// </summary>
        /// <param name="childData">The child data.</param>
        /// <returns>New TreeNode instance</returns>
        public virtual TreeNode<T> Add(T childData)
        {
            var newNode = new TreeNode<T>(childData);
            _children.Add(newNode);
            newNode.Parent = this;
            return newNode;
        }

        /// <summary>
        /// Adds the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns>This TreeNode instance</returns>
        public virtual TreeNode<T> Add(TreeNode<T> child)
        {
            _children.Add(child);
            child.Parent = this;
            return this;
        }

        public virtual TreeNode<T>? Remove(T childData)
        {
            TreeNode<T>? removedNode = null;
            foreach (var treeNode in _children)
            {
                if (treeNode.Data.CompareTo(childData) == 0)
                {
                    removedNode = treeNode;
                    _children.Remove(removedNode);
                    removedNode.Parent = null;
                    break;
                }
            }
            return removedNode;
        }

        public virtual TreeNode<T> Remove(TreeNode<T> child)
        {
            //TreeNode<T> removedNode = null;
            foreach (var treeNode in _children)
            {
                if (treeNode == child)
                {
                    Debug.Assert(treeNode.Data.CompareTo(child.Data) == 0, "data loads of the nodes do not match!");
                    _children.Remove(treeNode);
                    treeNode.Parent = null;
                    return treeNode;
                }
            }
            return this;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public virtual T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public TreeNode<T>? Parent
        {
            get { return _parent; }
            private set { _parent = value; }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public ReadOnlyObservableCollection<TreeNode<T>> Children
        {
            get { return _readOnlyChildren; }
        }

        /// <summary>
        /// Recursively displays node and its children 
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="indentation">The indentation.</param>
        public static void Display(ITreeNode<T> node, int indentation)
        {
            string line = new String(node.Children.Count > 0 ? '+' : '-', indentation);
            Trace.WriteLine(line + " " + node.Data);

            foreach (var child in node.Children)
                Display(child, indentation + 1);
        }

        private static void TraceSubtree(StringBuilder traceBuilder, ITreeNode<T> node, int indentation)
        {
            traceBuilder.Append(new String(node.Children.Count > 0 ? '+' : '-', indentation));
            traceBuilder.Append(" ");
            traceBuilder.AppendLine(node.Data.ToString());

            foreach (var child in node.Children)
                TraceSubtree(traceBuilder, child, indentation + 1);

            return;
        }

        /// <summary>
        /// Gets the trace string.
        /// </summary>
        /// <returns></returns>
        public string GetTraceString()
        {
            StringBuilder traceBuilder = new StringBuilder();
            TraceSubtree(traceBuilder, this, 1);
            return traceBuilder.ToString();
        }

        #region ITreeNode<T>

        /// <summary>
        /// Adds the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        ITreeNode<T> ITreeNode<T>.Add(ITreeNode<T> child)
        {
            return this.Add((TreeNode<T>) child);
        }

        /// <summary>
        /// Removes the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        ITreeNode<T> ITreeNode<T>.Remove(ITreeNode<T> child)
        {
            return this.Remove((TreeNode<T>) child);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        IReadOnlyCollection<ITreeNode<T>> ITreeNode<T>.Children
        {
            get { return this.Children; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        ITreeNode<T>? ITreeNode<T>.Parent
        {
            get { return this.Parent; }
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        T ITreeNode<T>.Data
        {
            get
            {
                return this.Data;
            }
            set
            {
                this.Data = value;
            }
        }

        #endregion ITreeNode<T>

    }
}

