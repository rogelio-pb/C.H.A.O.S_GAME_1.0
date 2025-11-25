using UnityEngine;

public class PlayerButtons : MonoBehaviour
{
    public static int moveDirection = 0;

    public void MoveLeft()
    {
        moveDirection = -1;
    }

    public void MoveRight()
    {
        moveDirection = 1;
    }

    public void StopMove()
    {
        moveDirection = 0;
    }
}
