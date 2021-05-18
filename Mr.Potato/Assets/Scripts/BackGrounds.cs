using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] backGrounds;
    public float fparallax = 0.4f;
    public float fparallay = 0.4f;
    public float layerFraction = 5f;
    public float fSmooth = 5f;

    Transform cam;
    Vector3 previousCamPos;
    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        float fParallax = (previousCamPos.x - cam.position.x) * fparallax;
        float fParallay = (previousCamPos.y - cam.position.y) * fparallay;
        for (int i=0;i< backGrounds.Length; i++)
        {
            float fNewX = backGrounds[i].position.x + fParallax * (1 + i * layerFraction);
            Vector3 newPos = new Vector3(fNewX, backGrounds[i].position.y, backGrounds[i].position.z);
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, newPos, fSmooth * Time.deltaTime);
            //y方向
            float fNewY = backGrounds[i].position.y + fParallay * (1 + i * layerFraction);
            Vector3 newpos = new Vector3(backGrounds[i].position.x, fNewY, backGrounds[i].position.z);
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, newpos, fSmooth * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
