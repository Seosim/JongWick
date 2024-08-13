using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEnemy
{
    private INode mRoot;

    public BTEnemy(INode root)
    {
        mRoot = root;
    }

    public void Operate()
    {
        mRoot.GetState();
    }
}
