using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparelBehaviour : MonoBehaviour
{
    [SerializeField] private Transform wearer;

    private Animator animator;

    void Awake()
    {
        SetWearer(wearer);

        animator = gameObject.GetComponent<Animator>();
    }

/*deprecated
    void Update()
    {
        if(wearer != null) { gameObject.transform.position = wearer.position; }
    }
*/

    public void SetWearer(Transform wearer)
    {
        this.wearer = wearer;
        wearer.GetComponent<CharacterBehaviour>().EquipApparel(gameObject, UpdatePosition, UpdateAnimation);
    }

    public void UpdatePosition()
    {
        gameObject.transform.position = wearer.position;
    }

    public void UpdateAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
