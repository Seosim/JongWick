using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    enum State {
        Success,
        Failure,
        Running
    }

    public State GetState();
}
