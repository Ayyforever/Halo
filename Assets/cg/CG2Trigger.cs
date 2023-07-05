using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CG2Trigger : MonoBehaviour
{
    public GameObject cg2Object;
    public GameObject playerObject;
    private PlayableDirector timeline;
    private bool hasTriggered = false;

    private void Start()
    {
        timeline = cg2Object.GetComponent<PlayableDirector>();
        cg2Object.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            ActivateCG2();
        }
    }

    private void ActivateCG2()
    {
        hasTriggered = true;
        playerObject.SetActive(false); // ����Player����
        playerObject.transform.position = new Vector3(-146, -14, -66); // �޸�Playerλ��Ϊ (0, 0, 0)
        playerObject.transform.rotation = Quaternion.Euler(0, -90, 0); // �޸�Player��ת�Ƕ�Ϊ (0, 180, 0)
        cg2Object.SetActive(true);
        timeline.Play();
        timeline.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        DeactivateCG2();
        playerObject.SetActive(true); // ��timeline���Ž����󼤻�Player����
    }

    private void DeactivateCG2()
    {
        cg2Object.SetActive(false);
        timeline.stopped -= OnTimelineStopped;
    }
}


