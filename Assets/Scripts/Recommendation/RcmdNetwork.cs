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
    public void ControlMarkNetwork(string text, bool isClick)
    {
        MarkNode targetnode;
        foreach (MarkNode node in MarkNetwork)
        {
            if (node.tag == text)
            {
                targetnode = node;
                foreach(GameObject gameObject in targetnode.attachedObject)
                {
                    if (isClick)
                    {
                        gameObject.SetActive(false);
                        //Debug.Log($"[RcmdNetwork] {gameObject.name}  --  {gameObject.activeSelf}");
                    }
                    else
                    {
                        gameObject.SetActive(true);
                        //Debug.Log($"[RcmdNetwork] {gameObject.name}  --  {gameObject.activeSelf}");
                    }
                }
                break;
            }
            
        }
        //Debug.Log($"[RcmdNetwork] {text}  --  {isClick}" );
    }
    public void PrintMarkNetwork()
    {
        foreach(MarkNode node in MarkNetwork)
        {
            Debug.Log($"[RcmdNetwork Display] {node.tag}: {node.attachedObject.Count}");
        }
    }
}
