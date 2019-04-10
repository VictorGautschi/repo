using UnityEngine;
using System.Collections;

public class WrapCam : MonoBehaviour
{
    //Assign these cameras in Inspector
    public Camera vertical;
    public Camera horizontal;
    public Camera diaganol;
    //Assign overall terrain width and depth in Inspector
    public float X;
    public float Z;
    // camera transform reference caches
    private Transform PCCamTransform;
    private Transform XCamTransform;
    private Transform ZCamTransform;
    private Transform XZCamTransform;

    // value caches
    private Vector2 Off = new Vector2();
    private Vector3 OffPos = new Vector3();
    private Vector3 Pos = new Vector3();
    private Quaternion Rot = new Quaternion();

    void Awake()
    {
        //cache all the camera transform references at the start
        PCCamTransform = this.transform;
        XCamTransform = horizontal.transform;
        ZCamTransform = vertical.transform;
        XZCamTransform = diaganol.transform;
    }
    void LateUpdate()
    {
        //get this camera transform
        Pos = PCCamTransform.position;
        Rot = PCCamTransform.rotation;

        //set the wraparound cameras to same rotation
        XCamTransform.rotation = Rot;
        ZCamTransform.rotation = Rot;
        XZCamTransform.rotation = Rot;

        //calculate the wraparound cameras offset coordinate
        Off.x = Pos.x + ((Pos.x < X / 2) ? X : -X);
        Off.y = Pos.z + ((Pos.z < Z / 2) ? Z : -Z);

        //set the wraparound cameras offset positions
        OffPos.x = Off.x; OffPos.y = Pos.y; OffPos.z = Pos.z;
        XCamTransform.position = OffPos;

        OffPos.z = Off.y;
        XZCamTransform.position = OffPos;

        OffPos.x = Pos.x;
        ZCamTransform.position = OffPos;
    }
}
