using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerationLogic : MonoBehaviour
{
    public static BoxGenerationLogic Instance;

    public int maxBasicNb = 20;
    public int maxBlockNb = 5;
    public int maxMouvementBonusNb = 10;
    public int maxBombNb = 7;
    public int maxPushNb = 9;

    public int currentBasicNb = 0;
    public int currentBlockNb = 0;
    public int currentMouvementBonusNb = 0;
    public int currentBombNb = 0;
    public int currentPushNb = 0;

    private void Awake()
    {
        Instance = this;
    }

    public bool checkBoxAvalability(cubeTypeController.CubeType cubeIndex)
    {
        switch (cubeIndex)
        {
            case cubeTypeController.CubeType.Normal:
                if (currentBasicNb < maxBasicNb)
                {
                    return true;
                }
                break;
            case cubeTypeController.CubeType.Block:
                if (currentBlockNb < maxBlockNb)
                {
                    return true;
                }
                break;
            case cubeTypeController.CubeType.MouvementBonus:
                if (currentBombNb < maxMouvementBonusNb)
                {
                    return true;
                }
                break;
            case cubeTypeController.CubeType.Bomb:
                if (currentMouvementBonusNb < maxBombNb)
                {
                    return true;
                }
                break;
            case cubeTypeController.CubeType.Push:
                if (currentPushNb < maxMouvementBonusNb)
                {
                    return true;
                }
                break;
            default:


                break;
        }

        return false;
    }

    public void removeOldBoxType(cubeTypeController.CubeType cubeIndex)
    {
        switch (cubeIndex)
        {
            case cubeTypeController.CubeType.Normal:
                if (currentBasicNb > 0)
                {
                    currentBasicNb--;
                }
                break;
            case cubeTypeController.CubeType.Block:
                if (currentBlockNb > 0)
                {
                    currentBlockNb--;
                }
                break;
            case cubeTypeController.CubeType.MouvementBonus:
                if (currentBombNb > 0)
                {
                    currentBombNb--;
                }
                break;
            case cubeTypeController.CubeType.Bomb:
                if (currentMouvementBonusNb > 0)
                {
                    currentMouvementBonusNb--;
                }
                break;
            case cubeTypeController.CubeType.Push:
                if (currentPushNb > 0)
                {
                    currentPushNb--;
                }
                break;
            default:

                break;
        }
    }

    public void AddNewBoxType(cubeTypeController.CubeType cubeIndex)
    {
        switch (cubeIndex)
        {
            case cubeTypeController.CubeType.Normal:

                currentBasicNb++;

                break;
            case cubeTypeController.CubeType.Block:

                currentBlockNb++;

                break;
            case cubeTypeController.CubeType.MouvementBonus:

                currentBombNb++;

                break;
            case cubeTypeController.CubeType.Bomb:

                currentMouvementBonusNb++;

                break;
            case cubeTypeController.CubeType.Push:

                currentPushNb++;

                break;
            default:

                break;
        }
    }

}
