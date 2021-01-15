using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : Button
{
    protected override void OnClick()
    {
        LevelManager.Instance.Brick.GetComponent<BrickManager>().CanMove = false;
        SceneManager.LoadScene(Scenes.PauseScene, LoadSceneMode.Additive);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            OnClick();
        }
    }
}
