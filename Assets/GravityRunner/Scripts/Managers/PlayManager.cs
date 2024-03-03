using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public string PlayerGOTag = "Player";
    static GameObject PlayerGO;
    static ChangeOfGravity playerGravityController;
    static ButtonGravityFlipper currentGravityFlipper;
    static public UIGravityIndicator gravityIndicator;

    // Start is called before the first frame update
    void Start()
    {
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        if (PlayerGO != null)
        {
            playerGravityController = PlayerGO.GetComponent<ChangeOfGravity>();
        }

    }

    public static void FlipPlayerGravity()
    {
        if (playerGravityController != null)
        {
            playerGravityController.OnChangeGravity();
        }
        gravityIndicator.OnGravityFlip();
    }

    public static GameObject GetPlayerGameObject()
    {
        return PlayerGO;
    }

    public static void ReplaceCurrentGravityFlipper(ButtonGravityFlipper flipper)
    {
        if (currentGravityFlipper != null) //the current Gravity Flipper needs to be released
        {
            currentGravityFlipper.DisconnectHeldingProp();
            currentGravityFlipper.Cooldown(); // use Cooldown to release button with delay     
        }
        currentGravityFlipper = flipper; //assign the new global solo Gravity Flipper
        currentGravityFlipper.Cooldown();
    }
}
