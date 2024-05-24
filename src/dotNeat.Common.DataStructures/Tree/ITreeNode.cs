namespace dotNeat.Common.DataStructures.Tree
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines an interface of a data that can be arranged in a tree-like structure.
    /// </summary>
    public interface ITreeNode
    {
        /// <summary>
        /// Adds the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns>This ITreeNode instance</returns>
        ITreeNode Add(ITreeNode child);

        /// <summary>
        /// Removes the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns>This ITreeNode instance</returns>
        ITreeNode Remove(ITreeNode child);

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        IReadOnlyCollection<ITreeNode> Children { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        ITreeNode? Parent { get; }
    }

    /// <summary>
    /// Defines an interface of a tree node with a data attached to it.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public interface ITreeNode<TData>
        where TData : IComparable<TData>
    {
        /// <summary>
        /// Adds the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns>This ITreeNode instance</returns>
        ITreeNode<TData> Add(ITreeNode<TData> child);

        /// <summary>
        /// Removes the specified child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns>This ITreeNode instance</returns>
        ITreeNode<TData> Remove(ITreeNode<TData> child);

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        IReadOnlyCollection<ITreeNode<TData>> Children { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        ITreeNode<TData>? Parent { get; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        TData Data { get; set; }
    }
}

