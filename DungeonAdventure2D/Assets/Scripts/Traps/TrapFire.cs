using System.Collections;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] private float offDuration;

    private Animator anim;
    private CapsuleCollider2D fireCollider;
    private bool isActive = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        fireCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        SetFire(true);
    }

    public void SwitchOffFire()
    {
        if (isActive == false) return;
        StartCoroutine(FireCouroutine());
    }
    private IEnumerator FireCouroutine()
    {
        SetFire(false);
        yield return new WaitForSeconds(offDuration);
        SetFire(true);
    }

    private void SetFire(bool activate)
    {
        anim.SetBool("activate", activate);
        fireCollider.enabled = activate;
        isActive = activate;
    }
}
