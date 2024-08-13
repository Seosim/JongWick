using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SequenceNode : INode
{
    private List<INode> mChildNodes;

    public SequenceNode(List<INode> childNodes)
    {
        mChildNodes = childNodes;
    }

    public INode.State GetState()
    {
        if (mChildNodes == null || mChildNodes.Count == 0)
            return INode.State.Failure;

        foreach (INode child in mChildNodes)
        {
            switch (child.GetState())
            {
                case INode.State.Running:
                    return INode.State.Running;
                case INode.State.Failure:
                    return INode.State.Failure;
            }
        }

        return INode.State.Success;
    }
}
