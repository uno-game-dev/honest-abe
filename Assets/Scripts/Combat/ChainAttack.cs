using UnityEngine;

public class ChainAttack : MonoBehaviour
{
    public int numberOfChainAttacks = 0;
    public float chainAttackTimer = 0;
    public float chainAttackResetsIn = 1;
    private bool previousAttackLight;

    private void Update()
    {
        if (chainAttackTimer < chainAttackResetsIn)
            chainAttackTimer += Time.deltaTime;
        else
        {
            numberOfChainAttacks = 0;
            ChainUI.SetChainNumber(numberOfChainAttacks);
        }
    }

    public void LightAttackChain()
    {
        if (numberOfChainAttacks >= 3)
            numberOfChainAttacks = 0;

        previousAttackLight = true;
        chainAttackTimer = 0;
        numberOfChainAttacks++;
        ChainUI.SetChainNumber(numberOfChainAttacks);
    }

    public void Miss()
    {
        chainAttackTimer = float.PositiveInfinity;
        numberOfChainAttacks = 0;
        ChainUI.SetChainNumber(numberOfChainAttacks);
    }

    public void HeavyAttackChain()
    {
        if (previousAttackLight)
            chainAttackTimer = 0;

        previousAttackLight = false;
        ChainUI.AddHeavy();
        numberOfChainAttacks = 0;
    }
}