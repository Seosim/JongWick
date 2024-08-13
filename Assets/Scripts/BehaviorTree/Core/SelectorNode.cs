using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SelectorNode : INode
{
    private List<INode> mChildNodes;

    public SelectorNode(List<INode> childNodes)
    {
        mChildNodes = childNodes;
    }

    public INode.State GetState()
    {
        if (mChildNodes == null)
            return INode.State.Failure;


        foreach (INode child in mChildNodes)
        {
            if (child.GetState() == INode.State.Running)
                return INode.State.Running;
            else if (child.GetState() == INode.State.Success)
                return INode.State.Success;
        }

        return INode.State.Failure;
    }
}
