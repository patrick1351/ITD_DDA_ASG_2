using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompleted {

    public bool rope;    
    public bool axe;    
    public bool chopTree;    
    public bool campfire;    
    public bool tent;    

    public TaskCompleted(bool rope, bool axe, bool tree, bool camp, bool tent)
    {
        this.rope = rope;
        this.axe = axe;
        this.chopTree = tree;
        this.campfire = camp;
        this.tent = tent;
    }
}
