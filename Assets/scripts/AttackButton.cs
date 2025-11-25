using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public ArgelinoController argelino;

    public void OnAttackPressed()
    {
        argelino.Atacar();
    }
}
