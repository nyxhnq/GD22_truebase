using UnityEngine;

/// <summary>
/// Простой снаряд: летит вперёд и уничтожается при столкновении или по достижении дальности.
/// Пока просто логирует попадания.
/// </summary>
public class Projectile : MonoBehaviour
{
    [Tooltip("Скорость полёта снаряда (единиц в секунду).")]
    [SerializeField] float speed = 20f;

    [Tooltip("Максимальная дистанция, после которой снаряд уничтожается.")]
    [SerializeField] float maxDistance = 20f;

    [Tooltip("Урон, который этот снаряд должен нанести при попадании.")]
    [SerializeField] float damage = 10f;

    [Tooltip("Слои, по которым может быть нанесён урон.")]
    [SerializeField] LayerMask hitLayers;

    public float Damage => damage;
    public float MaxDistance => maxDistance;
    public LayerMask HitLayers => hitLayers;
    public float Speed => speed;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        // Движемся вперёд по локальному forward
        transform.position += transform.forward * (speed * Time.deltaTime);

        // Проверяем пройденную дистанцию
        float traveled = Vector3.Distance(_startPosition, transform.position);
        if (traveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, попадает ли объект под маску слоёв
        if ((hitLayers.value & (1 << other.gameObject.layer)) == 0)
            return;

        Debug.Log($"Снаряд попал в {other.name}, потенциальный урон: {damage}");

        // TODO: логика нанесения урона

        // var damageable = other.GetComponent<IDamageable>();
        // if (damageable != null)
        // {
        //     damageable.TakeDamage(damage);
        // }

        Destroy(gameObject);
    }
}