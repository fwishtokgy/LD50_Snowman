using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelStageUI : MainPlayMonoBehaviour
{
    [SerializeField]
    protected TMP_Text StageNumber;

    [SerializeField]
    protected TMP_Text LevelName;

    [SerializeField]
    protected Image LevelColorBlock;

    [SerializeField]
    protected LevelHandler Level;

    protected override void OnInit()
    {
        Level.OnNewSeason += UpdateStageInfo;
        print("you should spruce up this transition later");
    }
    protected void UpdateStageInfo(LevelData data)
    {
        StageNumber.text = string.Format("STAGE {0}", (int)data.LevelSeason + 1);
        LevelName.text = data.LevelSeason.ToString();
        LevelColorBlock.color = data.PrimaryColor;
    }
}
