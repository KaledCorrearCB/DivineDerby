using UnityEngine;

public class CleanBox : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        other.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        other.transform.localScale = new Vector3(2, 2, 2);
    }
}
