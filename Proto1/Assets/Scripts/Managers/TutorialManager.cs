﻿using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //public GameObject tutorialArrow;

    //private GameObject arrow;
    //private GameObject arrow2;

    [SerializeField]
    private GameObject fingerTap;

    private GameObject fingerTapLocal;

    private bool rollingFinished;
    public bool changeBallTutorial;

    public bool RollingFinished
    {
        get
        {
            return rollingFinished;
        }
        set
        {
            rollingFinished = value;
            if (rollingFinished)
            {
                PlayFingerTapAnimation();
            }
            else
            {
                StopFingerTapAnimation();
            }
        }
    }

    public void TurnTutorialMaskOff()
    {
        if (!changeBallTutorial)
        {
            GameState.Instance.UIManager.canvas.GetComponentsInChildren<TutorialMask>(true)[0].gameObject.SetActive(false);
        }
        else
        {
            GameState.Instance.UIManager.canvas.GetComponentsInChildren<TutorialMask>(true)[1].gameObject.SetActive(false);
            GameState.Instance.UIManager.instantiatedInventoryButtons[0].gameObject.SetActive(true);
            GameState.Instance.UIManager.instantiatedInventoryButtons[1].gameObject.SetActive(true);
        }
    }

    public void StartTutorial()
    {
        StartCoroutine(SetTutorialActiveAsSoonAsPossible());
        StartCoroutine(SpawnTutorialMaskAfterSecond());
    }

    IEnumerator SetTutorialActiveAsSoonAsPossible()
    {
        bool done = false;
        while (!done)
        {
            if (PlayerDataController.instance.PreviousScene == 1 || PlayerDataController.instance.PreviousScene == 5)
            {
                if (GameState.Instance.UIManager.canvas != null)
                {
                    GameState.Instance.UIManager.canvas.gameObject.transform.Find("StartButton").GetComponent<ButtonManager>().tutorialActive = true;
                    GameState.Instance.UIManager.canvas.gameObject.transform.Find("BallKnop").GetComponent<ButtonManager>().tutorialActive = true;

                    if (GameState.Instance.UIManager.instantiatedInventoryButtons.Length > 0)
                    {
                        if (GameState.Instance.UIManager.instantiatedInventoryButtons[0] != null)
                        {
                            GameState.Instance.UIManager.instantiatedInventoryButtons[0].gameObject.SetActive(false);
                            done = true;
                        }
                        if (changeBallTutorial)
                        {
                            GameState.Instance.UIManager.instantiatedInventoryButtons[1].gameObject.SetActive(false);
                            done = true;
                        }
                    }
                }
            }
            else
            {
                done = true;
            }
            yield return null;
        }
    }

    IEnumerator SpawnTutorialMaskAfterSecond()
    {
        yield return new WaitForSeconds(0.2f);
        if (PlayerDataController.instance.PreviousScene == 1 || PlayerDataController.instance.PreviousScene == 5)
        {
            if (!changeBallTutorial)
            {
                GameState.Instance.UIManager.canvas.GetComponentsInChildren<TutorialMask>(true)[0].gameObject.SetActive(true);
            }
            else
            {
                GameState.Instance.UIManager.canvas.GetComponentsInChildren<TutorialMask>(true)[1].gameObject.SetActive(true);
            }
        }
    }

    private void PlayFingerTapAnimation()
    {
        if (fingerTapLocal == null)
        {
            fingerTapLocal = Instantiate(fingerTap);
            fingerTapLocal.transform.SetParent(GameState.Instance.UIManager.canvas.transform);
        }

        //if (arrow == null)
        //{
        //    arrow = Instantiate(tutorialArrow, new Vector3(3.5f, -4.3f, -4), Quaternion.Euler(0, 0, 45));
        //}
        //if (arrow2 == null)
        //{
        //    arrow2 = Instantiate(tutorialArrow, new Vector3(3.5f, -3.3f, -4), Quaternion.Euler(0, 0, 20));
        //}
    }

    public void StopFingerTapAnimation()
    {
        if (fingerTapLocal != null)
        {
            Destroy(fingerTapLocal);
        }
        //if (arrow != null)
        //{
        //    Destroy(arrow);
        //}
        //if (arrow2 != null)
        //{
        //    Destroy(arrow2);
        //}
    }

}
