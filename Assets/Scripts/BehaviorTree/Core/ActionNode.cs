using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ActionNode : INode
{
    Func<INode.State> fUpdate = null;

    public ActionNode(Func<INode.State> updateFunc)
    {
        fUpdate = updateFunc;
    }


    //fUpdate�� ���ǰ� �Ǿ��ִٸ� fUpdate�� ȣ��, ���ǵǾ����� �ʴٸ� INode.State.Failure ��ȯ
    public INode.State GetState() => fUpdate?.Invoke() ?? INode.State.Failure;
}
