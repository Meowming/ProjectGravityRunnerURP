using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class WeaponGravity : MonoBehaviour
{
    Camera mainCam;
    public float rayDistance = 100f;
    public float grabDistance = 2.5f;
    public LayerMask hitLayers;
    public GameObject currentHoldingItem;
    Rigidbody holdingRB;
    Prop holdingProp;
    public float relativeHoldingDistance = 2f;
    public GameObject hitGoodEffect;
    public GameObject hitBadEffect;
    public AudioClip hitGoodSound;
    public AudioClip hitGoodSoundExtra;
    public AudioClip hitBadSoundExtra;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, hitLayers,QueryTriggerInteraction.Ignore))
            {
                GameObject hitGO = hit.collider.gameObject;
                if (hitGO.tag == "Prop")
                {
                    Prop prop = hitGO.GetComponent<Prop>();
                    if (prop.canReversed)
                    {
                        prop.ReverseGravity();
                        Instantiate(hitGoodEffect, hit.point, Quaternion.identity);
                        AudioSource.PlayClipAtPoint(hitGoodSound, transform.position);
                        AudioSource.PlayClipAtPoint(hitGoodSoundExtra, transform.position);
                    }
                    else
                    {
                        //TODO: revise boiler plate code
                        Instantiate(hitBadEffect, hit.point, Quaternion.identity);
                        AudioSource.PlayClipAtPoint(hitGoodSound, transform.position);
                        AudioSource.PlayClipAtPoint(hitBadSoundExtra, transform.position);
                    }
                }
                else
                {
                    Instantiate(hitBadEffect, hit.point, Quaternion.identity);
                    AudioSource.PlayClipAtPoint(hitGoodSound, transform.position);
                    AudioSource.PlayClipAtPoint(hitBadSoundExtra, transform.position);
                }

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (currentHoldingItem == null) //pick up
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, grabDistance, hitLayers))
                {
                    GameObject hitGO = hit.collider.gameObject;
                    if (hitGO.tag == "Prop")
                    {
                        holdingProp = hitGO.GetComponent<Prop>();
                        if (holdingProp.canHeld)
                        {
                            currentHoldingItem = hitGO;
                            holdingRB = currentHoldingItem.GetComponent<Rigidbody>();
                            holdingRB.isKinematic = true;
                            holdingProp.isHeld = true;
                        }
                    }
                }
            }
            else //drop
            {
                currentHoldingItem = null;
                holdingRB.isKinematic = false;
                holdingRB = null;
                holdingProp.isHeld = false;
            }
        }
        //Updating holding object
        if (holdingRB != null)
        {
            holdingRB.position = mainCam.transform.position + mainCam.transform.forward * relativeHoldingDistance;
        }
        //Button Interaction
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, hitLayers))
            {
                GameObject hitGO = hit.collider.gameObject;
                if (hitGO.tag == "Button")
                {
                    ButtonBase button = hitGO.GetComponent<ButtonBase>();
                    button.OnButtonPressed();
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(mainCam.transform.position + mainCam.transform.forward * relativeHoldingDistance, 0.1f);
        }
    }
}
