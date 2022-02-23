using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public Image fader;
    public Animator animator;
    string targetSceneName;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public string TargetSceneName
    {
        set
        {
            targetSceneName = value;
        }
    }
    AsyncOperation asyncOperation;

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.2f);
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while(!asyncOperation.isDone)
        {
            float p = asyncOperation.progress*100 + 10f;
            if(p>100) p = 100;
            text.text = p + "%";

            slider.value = Mathf.Lerp(slider.value, asyncOperation.progress, Time.time);

            yield return null;
        }
        while(asyncOperation.isDone)
        {
            slider.value = 1;
            Invoke("FadeOut", 2);
            Invoke("Destroy", 6);
            break;
        }
    }

    void FadeOut()
    {
        animator.Play("LoadAnimFadeOut");
    }
    void Destroy()
    {
        Object.Destroy(this.gameObject);
    }
}
