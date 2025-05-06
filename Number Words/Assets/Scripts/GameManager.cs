using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    [SerializeField] DropArea[] dropAble, wordDrop;
    [SerializeField] Camera MainCam;
    [SerializeField] SelectArea[] selectArea, wordSelect;
    [SerializeField] PhysicsRaycaster raycaster;

    private GameObject pickUpanswercard;
    public GameObject GetPickUpAnswerCard => pickUpanswercard;

    public bool SetPickUp(GameObject _picked)
    {
        if ( pickUpanswercard == null )
        {
            pickUpanswercard = _picked;
            return true;
        }
        return false;
    }

    public void DropPickup()
    {
        pickUpanswercard = null;
    }

    void Awake()
    {
        if ( !Instance )
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (!MainCam) MainCam = Camera.main; 
    }
}
