using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public string sceneToLoad = "Cutscene"; //Change to the desired Scene name.
    public Slider progressBar;
    public float smoothSpeed = 2f; //Adjust the speed value to be appropriate.

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; //Prevent immediate scene changes

        float targetProgress = 0;

        while (!operation.isDone)
        {
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            //Gradually smooth out the Progress Ba
            progressBar.value = Mathf.Lerp(progressBar.value, targetProgress, Time.deltaTime * 2f);

            yield return null;

            //Check if the ProgressBar has reached 100%.
            if (progressBar.value >= 0.99f)
            {
                yield return new WaitForSeconds(1f); //Wait 1 second before changing Scene
                operation.allowSceneActivation = true; //Change Scene immediately
            }
        }
        if (progressBar.value >= 0.99f)
        {
            Debug.Log("Loading Completed, Changing Scene...");
            yield return new WaitForSeconds(1f);
            operation.allowSceneActivation = true;
        }
    }


}
