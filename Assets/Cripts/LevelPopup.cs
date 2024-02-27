using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : MonoBehaviour
{
    public Button playButton;
    public int currentLevel = 1;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayClick);
    }

    private void OnPlayClick()
    {
        var level1 = LevelManager.Ins.levelAsset.levelConfigs[currentLevel - 1];
    }
}
