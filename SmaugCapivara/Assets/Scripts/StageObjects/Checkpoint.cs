using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;
    public bool alreadyPassed;
    public int checkpointIndex;
    [SerializeField]private SpriteRenderer spriteRenderer;

    public void changeCheckpointColor(bool passed)
    {
        if (passed)
        {
            alreadyPassed = true;
            spriteRenderer.color = Color.red;
        }
        else
        {
            alreadyPassed = false;
            spriteRenderer.color = Color.green;
        }
    }
}
