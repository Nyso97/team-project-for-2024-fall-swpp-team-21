using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_Turret : VirusBehaviour
{
    [SerializeField] private GameObject upperBody;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimToBeamPeriod;
    [SerializeField] private float beamToAimPeriod;
    [SerializeField] private float flickerToBeamPeriod;
    [SerializeField] private float flickerSwapPeriod;
    [SerializeField] private float beamArriveTime; // time to reach the player
    [SerializeField] private int beamDamage;
    [SerializeField] private float upSpeed;
    [SerializeField] private float targetYOffset;

    private LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(GoUp());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // 밑에서 지상으로 올라옴
    private IEnumerator GoUp()
    {
        while (true)
        {
            float newY = Mathf.Min(transform.position.y + Time.deltaTime * upSpeed, 0);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            if (newY >= 0)
            {
                break;
            }
            yield return null;
        }
        StartCoroutine(Shoot());
    }


    private IEnumerator Shoot()
    {
        while (true)
        {
            // 조준
            float elapsedTime = 0f;
            Vector3 targetPos = player.transform.position + new Vector3(0, targetYOffset, 0);
            lineRenderer.enabled = true;
            Coroutine flickerCoroutine = null;
            while (elapsedTime < aimToBeamPeriod)
            {
                // 일시정지 시에도 조준선이 깜빡거리는 현상 방지
                if (Time.deltaTime > 0)
                {
                    // muzzle에서 player 방향으로 조준선 그리기
                    targetPos = player.transform.position + new Vector3(0, targetYOffset, 0);
                    Vector3 lookDir = (player.transform.position - transform.position).normalized;
                    upperBody.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);

                    lineRenderer.SetPosition(0, muzzle.position);
                    lineRenderer.SetPosition(1, targetPos);

                    if (elapsedTime >= aimToBeamPeriod - flickerToBeamPeriod && flickerCoroutine == null)
                    {
                        flickerCoroutine = StartCoroutine(Flicker());
                    }

                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            // 빔 발사
            StopCoroutine(flickerCoroutine);
            lineRenderer.enabled = false;
            float beamSpeed = Vector3.Distance(muzzle.position, targetPos) / beamArriveTime;
            GameObject beam = PoolManager.instance.GetObject(PoolType.VProj_Beam, muzzle.position, Quaternion.identity);
            beam.GetComponent<VP_Beam>().Initialize(targetPos, beamSpeed, beamDamage);

            yield return new WaitForSeconds(beamToAimPeriod);
        }
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            lineRenderer.enabled = !lineRenderer.enabled;
            yield return new WaitForSeconds(flickerSwapPeriod);
        }
    }
}