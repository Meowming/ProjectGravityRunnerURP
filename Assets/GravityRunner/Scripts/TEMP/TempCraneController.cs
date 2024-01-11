using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCraneController : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 shovelDirection = new Vector3(0f, 0f, 1f);
    public Vector3 beamDirection = new Vector3(1f, 0f, 0f);
    public GameObject craneShovel;
    public GameObject craneBeam;

    public InputRedirector moveUp;
    public InputRedirector moveDown;
    public InputRedirector moveLeft;
    public InputRedirector moveRight;
    // Start is called before the first frame update
    void Start()
    {
        moveUp.BindInput(MoveUp);
        moveDown.BindInput(MoveDown);
        moveLeft.BindInput(MoveLeft);
        moveRight.BindInput(MoveRight);
    }

    public void MoveUp()
    {
        craneShovel.transform.position += shovelDirection * speed * Time.deltaTime;
    }
    public void MoveDown()
    {
        craneShovel.transform.position -= shovelDirection * speed * Time.deltaTime;
    }
    public void MoveLeft()
    {
        craneBeam.transform.position += beamDirection * speed * Time.deltaTime;
    }
    public void MoveRight()
    {
        craneBeam.transform.position -= beamDirection * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Shovel
        if (Input.GetKey(KeyCode.Comma))
        {
            craneShovel.transform.position += shovelDirection * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Period))
        {
            craneShovel.transform.position -= shovelDirection * speed * Time.deltaTime;
        }
        //Beam
        if (Input.GetKey(KeyCode.LeftBracket))
        {
            craneBeam.transform.position += beamDirection * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightBracket))
        {
            craneBeam.transform.position -= beamDirection * speed * Time.deltaTime;
        }
    }
}
