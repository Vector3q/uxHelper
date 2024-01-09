using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkNode
{
    public string tag;
    public List<GameObject> attachedObject;

    public MarkNode(string tag, GameObject Gobject)
    {
        this.tag = tag;
        attachedObject = new List<GameObject>();
        attachedObject.Add(Gobject);
    }
}
public class RcmdNetwork : MonoBehaviour
{
    public List<MarkNode> MarkNetwork;
    // Start is called before the first frame update
    void Start()
    {
        MarkNetwork = new List<MarkNode>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMarkNode(string tag, GameObject gameObject)
    {
        bool found = false;
        //GameObject gameObject = transform.gameObject;
        foreach(MarkNode node in MarkNetwork)
        {
            if(node.tag == tag)
            {
                node.attachedObject.Add(gameObject);
            }
        }
        if (!found)
        {
            MarkNode node = new MarkNode(tag, gameObject);
            MarkNetwork.Add(node);
        }
    }

    public void FindMarkNode(Transform[] transforms)
    {
    }
    public void test(GameObject gameObject)
    {

    }
    public void ControlMarkNetwork(string text, bool status)
    {
        Debug.Log($"[RcmdNetwork] {text}  --  {status}" );
    }
    public void PrintMarkNetwork()
    {
        foreach(MarkNode node in MarkNetwork)
        {
            Debug.Log($"[RcmdNetwork Display] {node.tag}: {node.attachedObject.Count}");
        }
    }
}
