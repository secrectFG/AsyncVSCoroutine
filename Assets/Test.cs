using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public int count = 10000;
    public float waitSeconds = 3;
    Text text;
    private void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    }

    void print(string msg)
    {
        text.text += msg + "\n";
    }
    void ClearLog()
    {
        text.text = "";
    }

    // Start is called before the first frame update
    // async void Start()
    // {
    //     print("Start");
    //     // await Task.Delay(1000);
    //     await WaitForSecondsAsync(1);
    //     print("Hello World");
    // }

    int counter = 0;

    async Task WaitForSecondsAsync(float seconds)
    {
        var time = Time.time;
        while (Time.time - time < seconds)
        {
            await Task.Yield();
        }
        if (++counter == count) print("WaitForSecondsAsync end");
        
    }

    async Task WaitForSecondsAsync2(float seconds)
    {
        //不推荐使用
        await Task.Delay((int)(seconds * 1000));
        if (++counter == count) print("WaitForSecondsAsync2 end");
    }

    IEnumerator CoroutineCo(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (++counter == count) print("CoroutineCo end");
    }

    IEnumerator Coroutine2Co(float seconds)
    {
        var time = Time.time;
        while (Time.time - time < seconds)
        {
            yield return null;
        }
        if (++counter == count) print("WaitForSecondsCo end");
    }

    async UniTask WaitForSecondsUniTaskAsync(float seconds)
    {
        var time = Time.time;
        while (Time.time - time < seconds)
        {
            await UniTask.Yield();
        }
        if (++counter == count) print("WaitForSecondsUniTaskAsync end");
    }

    async UniTask WaitForSecondsUniTaskAsync2(float seconds)
    {
        await UniTask.Delay((int)(seconds * 1000));
        if (++counter == count) print("WaitForSecondsUniTaskAsync2 end");
    }



    private void OnGUI()
    {

        if (GUILayout.Button("Async"))
        {
            counter = 0;
            ClearLog();
            print("Async");
            for (int i = 0; i < count; i++)
            {
                _ = WaitForSecondsAsync(waitSeconds);
            }
        }
        if (GUILayout.Button("Async2"))
        {
            counter = 0;
            ClearLog();
            print("Async2");
            for (int i = 0; i < count; i++)
            {
                _ = WaitForSecondsAsync2(waitSeconds);
            }
        }
        if (GUILayout.Button("Coroutine"))
        {
            counter = 0;
            ClearLog();
            print("Coroutine");
            for (int i = 0; i < count; i++)
            {
                StartCoroutine(CoroutineCo(5));
            }
        }
        if (GUILayout.Button("Coroutine2"))
        {
            counter = 0;
            ClearLog();
            print("Coroutine2");
            for (int i = 0; i < count; i++)
            {
                StartCoroutine(Coroutine2Co(waitSeconds));
            }
        }
        if (GUILayout.Button("UniTask"))
        {
            counter = 0;
            ClearLog();
            print("UniTask");
            for (int i = 0; i < count; i++)
            {
                _ = WaitForSecondsUniTaskAsync(waitSeconds);
            }
        }
        if (GUILayout.Button("UniTask2"))
        {
            counter = 0;
            ClearLog();
            print("UniTask2");
            for (int i = 0; i < count; i++)
            {
                _ = WaitForSecondsUniTaskAsync2(waitSeconds);
            }
        }
    }
}
