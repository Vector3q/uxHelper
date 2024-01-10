using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRcmdController : MonoBehaviour
{
    public Transform[] videos;
    public GameObject[] filterAreas;
    public Button[] panelButton;
    private int mainVideo = -1;
    private List<Vector3> initialPos;
    private List<Quaternion> initialRot;
    private Vector3 mainPos = new Vector3(0f, 0.5f, 0f);
    private Vector3 mainRot = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        initialPos = new List<Vector3>();
        initialRot = new List<Quaternion>();
        foreach (Transform transform in videos)
        {
            Vector3 Pos = transform.position;
            Quaternion Rot = transform.rotation;
            initialPos.Add(Pos);
            initialRot.Add(Rot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainVideoChange(int newMainVideo)
    {
        if(mainVideo != -1)
        {
            videos[mainVideo].transform.position = initialPos[mainVideo];
            videos[mainVideo].transform.rotation = initialRot[mainVideo];
            filterAreas[mainVideo].SetActive(false);
            panelButton[mainVideo].gameObject.SetActive(true);
        }
        mainVideo = newMainVideo;

        videos[mainVideo].transform.position = mainPos;
        videos[mainVideo].transform.rotation = new Quaternion(mainRot.x, mainRot.y, mainRot.z, videos[mainVideo].transform.rotation.w);
        filterAreas[mainVideo].SetActive(true);
        panelButton[mainVideo].gameObject.SetActive(false);

    }


}
