using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour
{
    [SerializeField] private DetectorType detectorType;

    private void OnTriggerEnter(Collider other)
    {
        
    }

}

public enum DetectorType { Lose, Win }
