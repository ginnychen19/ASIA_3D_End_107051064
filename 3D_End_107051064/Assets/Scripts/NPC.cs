using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialog;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話時間間隔")]
    public float interval = 0.2f;
    [Header("對話者名稱")]
    public Text TextName;

    public enum NPCState
    {
        firstmeet,missioning,finish
    }
    public NPCState state = NPCState.firstmeet;

    /// <summary>
    /// 玩家是否進入感應區
    /// </summary>
    public bool playerInArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sapphiart")
        {
            playerInArea = true;
            StartCoroutine(Dialog());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Sapphiart")
        {
            playerInArea = false;
            StopDialog();

        }
    }

    private void StopDialog()
    {
        dialog.SetActive(false); //關閉對話框
        StopAllCoroutines(); //停止所有協同程序
    }

    private IEnumerator Dialog()  //表協同程序 IEnumerator
    {
        /**
        // print(data.dialogA);            // 取得字串全部資料
        // print(data.dialogA[0]);         // 取得字串局部資料：語法 [編號]

        // for 迴圈：重複處理相同程式
        //for (int i = 0; i < 10; i++)
        //{
        //    print("我是迴圈：" + i);
        //}

        //for (int apple = 1; apple < 100; apple++)
        //{
        //    print("迴圈：" + apple);
        //}

        // 字串的長度 dialogA.Length
        */
        dialog.SetActive(true);
        textContent.text = "";
        TextName.text = name;   //最後的name是表呼叫原物件在unity裡的名稱

        string dialogString = data.dialogB;

        switch (state)
        {
            case NPCState.firstmeet:
                dialogString = data.dialogA;
                break;
            case NPCState.missioning:
                dialogString = data.dialogB;
                break;
            case NPCState.finish:
                dialogString = data.dialogC;
                break;
            default:
                break;
        }

        for (int i = 0; i < dialogString.Length; i++)
        {
            textContent.text += dialogString[i] +"";
            yield return new WaitForSeconds(interval);
        }
    }
}
