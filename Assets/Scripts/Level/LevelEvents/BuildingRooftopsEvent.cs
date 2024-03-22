using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRooftopsEvent : LevelEvent
{
    [SerializeField] private GameObject buildingObject;

    [Header("Building Variables")]
    [SerializeField] float buildingHeight;
    [SerializeField] float buildingSpeed;

    float buildingTime;


    public override void OnEvent()
    {
        base.OnEvent();
        StartCoroutine(RiseBuilding());
    }

    IEnumerator RiseBuilding()
    {
        GameManager.instance.currentController.ChangeState(PlayerState.Idle);
        GameManager.instance.currentCameraManager.SetToLookAt();
        GameManager.instance.currentController.transform.parent = buildingObject.transform;
        GameManager.instance.currentController.playerRigid.isKinematic = true;
        yield return new WaitForSeconds(0.5f);

        while (buildingTime <= 3)
        {
            float value = Mathf.Lerp(buildingObject.transform.localPosition.y, buildingHeight, buildingTime / buildingSpeed);
            buildingTime += Time.deltaTime;

            buildingObject.transform.localPosition = new Vector3(buildingObject.transform.localPosition.x, value, buildingObject.transform.localPosition.z);
            Debug.Log(buildingTime);
            yield return null;
        }

        buildingObject.transform.localPosition = new Vector3(buildingObject.transform.localPosition.x, buildingHeight, buildingObject.transform.localPosition.z);

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.currentController.transform.parent = null;
        GameManager.instance.currentController.playerRigid.isKinematic = false;
        GameManager.instance.currentCameraManager.ResetLookAt();

    }
}
