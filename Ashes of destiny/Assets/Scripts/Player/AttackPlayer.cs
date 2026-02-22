using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] float radiusAttackMelee;
    [SerializeField] float radiusAttackRange;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject _currentWeapon;
    [SerializeField] Transform targetAttack;
    [SerializeField] WorldCrossHairController crosshairController;

    AbilitiesPlayer abilitiesPlayer;
    DiContainer _container;


    bool canAttackMelee = false;
    bool coolDownAttack = true;
    bool coolDown = false;
    private bool meleeMode = false;


    Dictionary<Ashes, Coroutine> cooldowns = new();
    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    [Inject]
    void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
    }


    private void FixedUpdate()
    {
        Collider[] zoneAttackMelee = Physics.OverlapSphere(transform.position, radiusAttackMelee, layer);

        if (zoneAttackMelee.Length > 0)
        {
            canAttackMelee = true;
        }

        else
        {
            canAttackMelee = false;
        }
    }

    public void Attack(Ashes ashes)
    {
        if (ashes == null)
        {
            Debug.Log("Golpe melee");
            //aqui llamariamos la funcion de atacar
        }
            

        if (ashes.ElementAttack == null)
            return;

        if (!cooldowns.ContainsKey(ashes))
            cooldowns.Add(ashes, null);

        if (cooldowns[ashes] == null)
        {
            Particulas particula = abilitiesPlayer.particulaActual;
            cooldowns[ashes] = StartCoroutine(CooldownAttack(ashes, particula));
            Vector3 direction = (crosshairController.CurrentAimPoint - targetAttack.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            _container.InstantiatePrefab(
                ashes.ElementAttack,
                targetAttack.position,
                rotation,
                null
            );

            ashes.DesactiveRock();
            StartCoroutine(abilitiesPlayer.CooldownVisual(abilitiesPlayer.CurrentButton, 5f));
            abilitiesPlayer.particulaActual.DesactiveParticule();
        }
    }

    IEnumerator CooldownAttack(Ashes ashes, Particulas particula)
    {
        float currentTime = 0;

        while (currentTime < 5)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        if (particula != null)
            particula.ActivasParticulasLoop();

        cooldowns[ashes] = null;
    }

    public bool IsOnCooldown(Ashes ashes)
    {
        if (ashes == null)
            return false;

        if (!cooldowns.ContainsKey(ashes))
            return false;

        return cooldowns[ashes] != null;
    }

    public void ToggleMeleeMode()
    {
        meleeMode = !meleeMode;

        if (meleeMode)
        {
            ActivateMelee();
        }
        else
        {
            DeactivateMelee();
        }
    }

    void ActivateMelee()
    {
        Debug.Log("Modo melee activado");

        if (_currentWeapon != null)
            _currentWeapon.SetActive(true);

        abilitiesPlayer.ClearSelection();
    }

    void DeactivateMelee()
    {
        Debug.Log("Modo habilidades activado");

        if (_currentWeapon != null)
            _currentWeapon.SetActive(false);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttackMelee);
    }
}
