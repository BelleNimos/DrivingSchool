using System.Collections.Generic;
using UnityEngine;

public class Utilizer : MonoBehaviour
{
    public void DestroyCone(Cone cone)
    {
        Destroy(cone.gameObject);
    }
}
