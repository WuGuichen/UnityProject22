using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets;

public class RobSceneLoad : MonoBehaviour
{
    // 事件
    public delegate void RobDelegate();
    public event RobDelegate CallLoadCompleted;

    public Text text;
    public Slider slider;
    public Image fader;

    public Animator animator;
    string targetSceneName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public string TargetSceneName
    {
        set
        {
            targetSceneName = value;
            StartCoroutine(LoadScene(targetSceneName));
            //暗
            animator.Play("LoadAnimFadeIn");
        }
    }

    AsyncOperation asyncOperation;

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.2f);
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            float p = asyncOperation.progress * 100 + 10f;
            if (p > 100) p = 100;
            text.text = p + "%";

            slider.value = Mathf.Lerp(slider.value, asyncOperation.progress, Time.time);

            yield return null;
        }

        while (asyncOperation.isDone)
        {
            slider.value = 1;

            // Invoke("FadeOut", 1);
            StartCoroutine(LoadAndAssociateResultWithLable(new string[] { "Enemys", "Others", "Players" }));
            Invoke("Destory", 6);
            break;
        }

    }

    void FadeOut()
    {
        // 通知加载完毕
        CallLoadCompleted();
        //亮
        animator.Play("LoadAnimFadeOut");
    }

    void Destory()
    {
        Object.Destroy(gameObject);
    }
    IEnumerator LoadAndAssociateResultWithLable(string[] lables)
    {
        if (IFactory.GetAssetFactory().assetsLoaded == false)
        {

            for (int i = 0; i < lables.Length; i++)
            {
                string lable = lables[i];
                AsyncOperationHandle<IList<IResourceLocation>> locations = Addressables.LoadResourceLocationsAsync(lable);
                // Debug.Log("加载中...." + lable);
                yield return locations;

                Dictionary<string, GameObject> associationDoesMatter = new Dictionary<string, GameObject>();
                foreach (IResourceLocation location in locations.Result)
                {
                    AsyncOperationHandle<GameObject> handle =
                        Addressables.LoadAssetAsync<GameObject>(location);
                    handle.Completed += obj => IFactory.GetAssetFactory().SaveAsset(location.PrimaryKey, obj.Result, lable);
                    yield return handle;
                }
            }
            Debug.Log("加载完成");
            IFactory.GetAssetFactory().AssetLoaded();
        }
        Invoke("FadeOut", 1);
    }
}
