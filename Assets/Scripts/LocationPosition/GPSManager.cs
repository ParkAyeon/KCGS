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
        // 사용자 위치를 코드에서 직접 설정
        latitude = getLocation.f_Lat; // 예시 위도
        longitude = getLocation.f_Long; // 예시 경도

        // 위도와 경도가 null인지 확인하고 로그를 찍음
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
