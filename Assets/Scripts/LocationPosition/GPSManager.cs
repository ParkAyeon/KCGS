using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    public double? latitude;
    public double? longitude;

    public GpsModular getLocation;

    void Start()
    {
        // ����� ��ġ�� �ڵ忡�� ���� ����
        latitude = getLocation.f_Lat; // ���� ����
        longitude = getLocation.f_Long; // ���� �浵

        // ������ �浵�� null���� Ȯ���ϰ� �α׸� ����
        if (!latitude.HasValue)
        {
            Debug.Log("Latitude is null.");
        }

        if (!longitude.HasValue)
        {
            Debug.Log("Longitude is null.");
        }
    }
}
