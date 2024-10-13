using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZone : MonoBehaviour
{
  private void OnCollisionEnter(Collision other)
  {
    Debug.Log("a");
    if (other.gameObject.CompareTag("target"))
    {
      Destroy(other.gameObject);
    }

  }
}
