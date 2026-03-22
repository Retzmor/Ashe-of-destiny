using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] Image lastSkillIcon; // El componente Image en el Canvas del enemigo
    [SerializeField] GameObject iconContainer; // El objeto que contiene la imagen (para apagarlo/prenderlo)

    private string lastElement = "";
    private float comboTimer = 0f;
    [SerializeField] float maxComboTime = 4f; // Tiempo que tiene el jugador para hacer el combo

    void Start()
    {
        if (iconContainer != null) iconContainer.SetActive(false);
    }

    void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                ResetStatus();
            }
        }
    }

    // Esta función la llamará la bala/ataque al colisionar
    public void ApplyElement(string element, Sprite icon)
    {
        // 1. Verificar Combo KATON (Fuego + Aire)
        if ((lastElement == "Fire" && element == "Air") || (lastElement == "Air" && element == "Fire"))
        {
            ApplyKatonCombo();
            return;
        }

        // 2. Verificar Combo LODO (Agua + Tierra)
        if ((lastElement == "Water" && element == "Rock") || (lastElement == "Rock" && element == "Water"))
        {
            ApplyLodoCombo();
            return;
        }

        // 3. Si no es combo, actualizar el último elemento
        lastElement = element;
        comboTimer = maxComboTime;

        // Actualizar UI
        if (lastSkillIcon != null)
        {
            lastSkillIcon.sprite = icon;
            iconContainer.SetActive(true);
        }

        // Aplicar efectos base (Daño, Slow, etc.)
        ApplyBaseEffect(element);
    }

    private void ApplyBaseEffect(string element)
    {
        switch (element)
        {
            case "Fire": /* Daño + Quema 2s */ break;
            case "Air":  /* Pushback */ break;
            case "Water": /* Daño + Slow */ break;
            case "Rock": /* Stun */ break;
        }
    }

    private void ApplyKatonCombo()
    {
        Debug.Log("¡COMBO KATON APLICADO!");
        // Aquí disparas el empuje fuerte + fuego 5s
        ResetStatus();
    }

    private void ApplyLodoCombo()
    {
        Debug.Log("¡COMBO LODO APLICADO!");
        // Idea para Lodo: El enemigo se queda pegado al suelo (Root) y recibe más daño
        ResetStatus();
    }

    public void ResetStatus()
    {
        lastElement = "";
        comboTimer = 0;
        if (iconContainer != null) iconContainer.SetActive(false);
    }
}
