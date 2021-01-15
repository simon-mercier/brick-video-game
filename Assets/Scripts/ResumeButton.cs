using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : SingleClickButton
{
    protected override void OnClick()
    {
        SceneManager.UnloadSceneAsync(Scenes.PauseScene);
        LevelManager.Instance.Brick.GetComponent<BrickManager>().CanMove = true;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OnClick();
        }
    }
}
