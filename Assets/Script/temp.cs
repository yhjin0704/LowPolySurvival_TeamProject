using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class temp : MonoBehaviour
{

    /*void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        CameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        Player.eulerAngles += new Vector3(0,     mouseDelta.x     * lookSensitivity, 0);*/

    /*private List<IDamagable> things = new List<IDamagable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
    }
}

private void OnTriggerEnter(Collider other)
{
    if (other.TryGetComponent(out IDamagable target))
    {
        things.Add(target);
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.TryGetComponent(out IDamagable target))
    {
        things.Remove(target);
    }
}*/

    /*void SetSkillUI()
    {
        remainText.text = remainSkillCnt + "/" + maxSkillCnt;
        remainGaugeBar.fillAmount = remainSkillCnt / maxSkillCnt;
    }*/


    /*private Ray returnInteractionRay()
    {
        Ray ray;
        if (nowFirstPerson)
        {
            //TODO
            //camera를 활용할 것
            ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        }
        else
        {
            //TODO
            //interactionRayPointTransform를 활용할 것
            ray = new Ray(interactionRayPointTransform.transform.position,Vector3.forward);
        }
        return ray;
    }*/

    private Coroutine myCoroutine;
    private void Start()
    {
        StartTestCoroutine();
        Invoke("StartTestCoroutine", 1);
    }

    void StartTestCoroutine()
    {
        if (myCoroutine != null) StopCoroutine(myCoroutine);
        myCoroutine = StartCoroutine(TestCoroutine());
    }
    IEnumerator TestCoroutine()
    {
        Debug.Log("a");
        yield return null;
        Debug.Log("b");
        yield return new WaitForSeconds(3);
        Debug.Log("c");
    }




}
