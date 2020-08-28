using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    private static Spinner instance;
    public static Spinner Instance
    {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<Spinner>();
            }
            return instance;
        }
    }

    public Transform wheel;

    public SpinItem[] spinItems;

    public float initRotateZ;
    /// <summary>
    /// State
    /// </summary>
    private Vector3 targetAngle;
    private int itemTarget;
    private float rotationSpeed = 700;

    /// <summary>
    /// value
    /// </summary>
    bool spin = false;
    public float speed;
    private float timeSlow;

    // ty le ra do
    ChanceTable<int> SpinItemsRatio = new ChanceTable<int>();

    SpinItem targetItem;

    private void Start()
    {
        Init();
        InitItem();
    }

    // init ty le roi item
    private void Init()
    {
        SpinItemsRatio.AddItem(0, 300);
        SpinItemsRatio.AddItem(1, 200);
        SpinItemsRatio.AddItem(2, 200);
        SpinItemsRatio.AddItem(3, 50);
        SpinItemsRatio.AddItem(4, 125);
        SpinItemsRatio.AddItem(5, 100);
        SpinItemsRatio.AddItem(6, 100);
        SpinItemsRatio.AddItem(7, 25);
    }

    //gan value vao tung item
    private void InitItem()
    {
        for (int i = 0; i < spinItems.Length; i++)
        {
            spinItems[i].SetItem(i);
        }
    }

    private void FixedUpdate()
    {
        if (spin)
        {
            if (Time.time > timeSlow)
            {
                wheel.localRotation = Quaternion.Euler(rotationSpeed * Vector3.forward * speed * Time.fixedDeltaTime + targetAngle);

                speed *= 0.97f;

                if (speed <= .1f)
                {
                    GetResult();
                }
            }
            else
            {
                wheel.localRotation = Quaternion.Euler(wheel.localRotation.eulerAngles + Vector3.back * rotationSpeed * speed / 30 * Time.fixedDeltaTime);
            }
        }
    }

    public void ButtonSpin()
    {
        speed = 1 / Time.fixedDeltaTime;
        spin = true;
        timeSlow = Time.time + 1.5f;

        SetTarget(SpinItemsRatio.GetRandomItem());
    }

    #region Item

    public void SetTarget(int _item)
    {
        itemTarget = _item;

        float deltaAngle = 360 / spinItems.Length;

        targetAngle = Vector3.forward * (initRotateZ - deltaAngle * _item);

        Debug.Log("target item : " + _item);
    }

    private void GetResult()
    {
        spin = false;

        Debug.Log("Spin Complete!!!");
        Debug.Log(spinItems[itemTarget].GetItem());
    }

    #endregion
}

