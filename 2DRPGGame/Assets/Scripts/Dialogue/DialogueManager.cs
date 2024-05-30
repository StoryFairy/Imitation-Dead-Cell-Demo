using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueType
{
    End, //结束
    Answer, //回答
    Choose //选择
}
[Serializable]
public struct DialogueData
{
    public int serialNumber; //序列号
    public DialogueType diaType;
    public int ID; //对话ID
    public string character;
    public string context;
    public int nextID;
}

[Serializable]
public class DialogueManager : MMSingleton<DialogueManager>
{
    public TextAsset textFile;
    public Text dialogueLabel;
    public Image characterImage;

    public List<Sprite> sprites = new List<Sprite>();
    public Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    public int dialogueIndex;

    private int textIndex;
    private float textSpeed;
    public int nextID = 0;
    public bool isSelect;
    public bool isEnd;

    public DialogueData data;
    public List<DialogueData> dialogueDatas = new List<DialogueData>();
    private List<DialogueData> currentDialogueDatas = new List<DialogueData>();

    protected override void Awake()
    {
        base.Awake();

        isSelect = false;
        isEnd = false;
        imageDic["观测者"] = sprites[0];
        imageDic["我"] = sprites[1];
        imageDic["主角"] = sprites[2];
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isSelect && !isEnd)
        {
            ShowDialogue(nextID);
        }
        else if (Input.GetKeyDown(KeyCode.R) && isEnd)
        {
            GameManager.Instance.UnPause(PauseMethods.NoPauseMenu);
            this.gameObject.SetActive(false);
        }
    }

    private void Initialize()
    {
        string[] rows = textFile.text.Split('\n');
        bool isData = false;
        foreach (var row in rows)
        {
            string[] cell = row.Split(',');
            if (cell.Length == 6)
            {
                if (isData)
                {
                    DialogueData data = new DialogueData();
                    data.serialNumber = int.Parse(cell[0]);
                    data.diaType = (DialogueType)int.Parse(cell[1]);
                    data.ID = int.Parse(cell[2]);
                    data.character = cell[3];
                    data.context = cell[4];
                    data.nextID = int.Parse(cell[5]);
                    dialogueDatas.Add(data);
                }

                isData = true;
            }
        }
    }

    private void UpdateText(string name, string content)
    {
        dialogueLabel.text = name + " : " + content;
    }

    private void UpdateImage(Sprite image)
    {
        characterImage.sprite = image;
    }

    public void InitCurrentDialogue(int serialNumber)
    {
        foreach (var data in dialogueDatas)
        {
            if (data.serialNumber == serialNumber)
            {
                currentDialogueDatas.Add(data);
            }
        }

        ShowDialogue(0);
    }

    public void ShowDialogue(int id)
    {
        data = currentDialogueDatas[id];
        switch (data.diaType)
        {
            case DialogueType.End:
                UpdateText(data.character, data.context);
                UpdateImage(imageDic[data.character]);
                isEnd = true;
                return;
            case DialogueType.Answer:
                UpdateText(data.character, data.context);
                UpdateImage(imageDic[data.character]);
                this.nextID = data.nextID;
                break;
            case DialogueType.Choose:
                isSelect = true;
                int i = id;
                if (i <= currentDialogueDatas.Count)
                    while (currentDialogueDatas[i].diaType == DialogueType.Choose)
                    {
                        GameObject gameObject = SelectionPool.Instance.GetFormPool();
                        gameObject.GetComponentInChildren<Text>().text = currentDialogueDatas[i].context;
                        gameObject.GetComponentInChildren<SelectionButton>().nextID = currentDialogueDatas[i].nextID;
                        i++;
                    }

                break;
        }
    }
}
