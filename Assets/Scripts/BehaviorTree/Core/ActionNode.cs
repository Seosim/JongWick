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


    //fUpdate가 정의가 되어있다면 fUpdate를 호출, 정의되어있지 않다면 INode.State.Failure 반환
    public INode.State GetState() => fUpdate?.Invoke() ?? INode.State.Failure;
}
