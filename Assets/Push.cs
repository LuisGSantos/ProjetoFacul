using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
        public string _tagObjects = "Obj";
        public float forcaDeArremeco = 800;
        [Space(10)]
        public Sprite texturaMaoFechada;
        public Sprite texturaMaoAberta;
        public Sprite texturaCenter;

        bool canMove;
        bool isMoving;
        float distance;
        float rotXTemp;
        float rotYTemp;
        float tempDistance;
        RaycastHit tempHit;
        Rigidbody rbTemp;
        Vector3 rayEndPoint;
        Vector3 tempDirection;
        Vector3 tempSpeed;
        GameObject tempObject;

        Camera mainCamera;
        GameObject objClosedHand;
        GameObject objOpenHand;
        GameObject objCenter;

        void Awake()
        {
            distance = 6;
            mainCamera = Camera.main;

            //automatic set layer in player
            GameObject refTemp = transform.root.gameObject;
            refTemp.layer = 2;
            foreach (Transform trans in refTemp.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = 2;
            }
            //
            float tempDistance = 0.3f;
            float tempfloatNear = mainCamera.nearClipPlane;
            if (tempfloatNear >= tempDistance)
            {
                tempDistance = tempfloatNear + 0.05f;
            }
            if (texturaMaoFechada)
            {
                objClosedHand = new GameObject("objHandTextureClosed");
                objClosedHand.transform.parent = this.transform;
                objClosedHand.AddComponent<SpriteRenderer>().sprite = texturaMaoFechada;
                objClosedHand.transform.localPosition = new Vector3(0.0f, 0.0f, tempDistance);
                objClosedHand.transform.localScale = new Vector3(0.03f, 0.05f, 0.05f);
                objClosedHand.transform.localRotation = Quaternion.identity;
                objClosedHand.SetActive(false);
            }
            if (texturaMaoAberta)
            {
                objOpenHand = new GameObject("objHandTextureOpen");
                objOpenHand.transform.parent = this.transform;
                objOpenHand.AddComponent<SpriteRenderer>().sprite = texturaMaoAberta;
                objOpenHand.transform.localPosition = new Vector3(0.0f, 0.0f, tempDistance);
                objOpenHand.transform.localScale = new Vector3(0.03f, 0.05f, 0.05f);
                objOpenHand.transform.localRotation = Quaternion.identity;
                objOpenHand.SetActive(false);
            }
            if (texturaCenter)
            {
                objCenter = new GameObject("objCenter");
                objCenter.transform.parent = this.transform;
                objCenter.AddComponent<SpriteRenderer>().sprite = texturaCenter;
                objCenter.transform.localPosition = new Vector3(0.0f, 0.0f, tempDistance);
                objCenter.transform.localScale = new Vector3(0.01f, 0.01f, 0.05f);
                objCenter.transform.localRotation = Quaternion.identity;
                objCenter.SetActive(false);
            }
    }

        void Update()
        {
            //raycast camera forward
            rayEndPoint = transform.position + transform.forward * distance;
            if (Physics.Raycast(transform.position, transform.forward, out tempHit, 2))
            {
                if (Vector3.Distance(transform.position, tempHit.point) <= 6 && tempHit.transform.CompareTag(_tagObjects))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
                //
                if (Input.GetKeyDown(KeyCode.Mouse0) && canMove)
                {
                    if (tempHit.rigidbody)
                    {
                        tempHit.rigidbody.useGravity = true;
                        distance = Vector3.Distance(transform.position, tempHit.point);
                        tempObject = tempHit.transform.gameObject;
                        isMoving = true;
                    }
                }
            }
            else
            {
                canMove = false;
            }

            distance += Input.GetAxis("Mouse ScrollWheel") * 10.0f;
            distance = Mathf.Clamp(distance, 2.5f, 6);
            if (tempObject)
            {
                rbTemp = tempObject.GetComponent<Rigidbody>();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && tempObject)
            {
                rbTemp.useGravity = true;
                tempObject = null;
                rbTemp = null;
                isMoving = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && tempObject)
            {
                tempDirection = rayEndPoint - transform.position;
                tempDirection.Normalize();
                rbTemp.useGravity = true;
                rbTemp.AddForce(tempDirection * forcaDeArremeco);
                tempObject = null;
                rbTemp = null;
                isMoving = false;
            }
            if (tempObject)
            {
                if (Vector3.Distance(transform.position, tempObject.transform.position) > 6)
                {
                    rbTemp.useGravity = true;
                    tempObject = null;
                    rbTemp = null;
                    isMoving = false;
                }
            }

            if (tempObject && mainCamera)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    rotXTemp = Input.GetAxis("Mouse X") * 5.0f;
                    rotYTemp = Input.GetAxis("Mouse Y") * 5.0f;
                    tempObject.transform.Rotate(mainCamera.transform.up, -rotXTemp, Space.World);
                    tempObject.transform.Rotate(mainCamera.transform.right, rotYTemp, Space.World);
                }
            }
            //sprite elements
            if (canMove && !isMoving && texturaMaoAberta)
            {
                objClosedHand.SetActive(false);
                objOpenHand.SetActive(true);
            objCenter.SetActive(false);
        }
            else if (isMoving && texturaMaoFechada)
            {
                objClosedHand.SetActive(true);
                objOpenHand.SetActive(false);
             objCenter.SetActive(false);
            }
             else if (!isMoving)
            {
                objClosedHand.SetActive(false);
                objOpenHand.SetActive(false);
                objCenter.SetActive(true);
            }
            }

        void FixedUpdate()
        {
            if (tempObject)
            {
                rbTemp = tempObject.GetComponent<Rigidbody>();
                rbTemp.angularVelocity = new Vector3(0, 0, 0);
                tempSpeed = (rayEndPoint - rbTemp.transform.position);
                tempSpeed.Normalize();
                tempDistance = Vector3.Distance(rayEndPoint, rbTemp.transform.position);
                tempDistance = Mathf.Clamp(tempDistance, 0, 1);
                rbTemp.velocity = Vector3.Lerp(rbTemp.velocity, tempSpeed * 7.5f * tempDistance, Time.deltaTime * 12);
            }
        }
}
