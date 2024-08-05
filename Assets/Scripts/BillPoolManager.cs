using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BillPoolManager : MonoBehaviour
{
    [SerializeField]
    private Bill billPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private Transform initialPosition;
    [SerializeField]
    private Transform maxHeightTransform;
    [SerializeField]
    private int initialPoolSize = 10;

    private ObjectPool<Bill> billPool;

    // Start is called before the first frame update
    void Start()
    {
        billPool = new ObjectPool<Bill>(
            CreateBill,
            bill => bill.gameObject.SetActive(true),
            bill => bill.gameObject.SetActive(false),
            bill => Destroy(bill),
            false,
            initialPoolSize, 100);
    }

    public void SpawnBill()
    {
        Bill bill = billPool.Get();
        bill.transform.position = initialPosition.position;

        Vector3 initialVelocity = new Vector3(
            Random.Range(-500f, 500f),
            Random.Range(500f, 1200f),
            0
        );

        bill.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        bill.GetComponent<Bill>().Initialize(initialVelocity, maxHeightTransform.position.y);
    }

    private Bill CreateBill()
    {
        Bill bill = Instantiate(billPrefab, canvasTransform);
        bill.ObjectPool = billPool;
        return bill;
    }
}
