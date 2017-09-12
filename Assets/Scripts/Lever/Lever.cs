using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, I_Interactable
{
    // レバー
    public GameObject lever;
    // レバーフラグ
    [SerializeField]
    private bool isLeverSwitch;
    // レバーの動き制限
    [SerializeField]
    private float moveRestriction;
    // レバーの動く速度
    [SerializeField]
    private float moveSpeed;
    // 近くにいるか
    public bool isNear;

    // スイッチに対応したオブジェクト
    public GameObject[] correspondenceObject;

    // Use this for initialization
    void Start()
    {
        isLeverSwitch = false;
        isNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        LeverAnimation();

        Interact();
    }

    // レバーのアニメーション
    private void LeverAnimation()
    {
        if (isLeverSwitch)
        {
            if (lever.transform.rotation.z > -0.25f)
            {
                lever.transform.Rotate(0, 0, -moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(lever.transform.rotation.z < 0.25f)
            {
                lever.transform.Rotate(0, 0, moveSpeed * Time.deltaTime);
            }
        }
    }

    // インタラクト
    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E) && isNear)
        {
            isLeverSwitch = !isLeverSwitch;

            CorrespondenceInteract();
        }
    }

    public void CorrespondenceInteract()
    {
        foreach(GameObject interactObject in correspondenceObject)
        {
            interactObject.gameObject.GetComponent<I_Triggerable>().triggerable();
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isNear = false;
    }
}
