using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")] 
    [SerializeField] private float _moveSpeed = 20f;
    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true; // Rigidbody'nin dönüşünü donduruyoruz. Yani player dönmeyecek.
    }

    private void Update()
    {
            // Update'de input işlemlerini yapıyoruz. Yani kullanıcının girdiği tuşları alıyoruz.
        setInput();
    }

    private void FixedUpdate()
    {
        // FixedUpdate'de fiziksel işlemler yapıyoruz. Yani Rigidbody ile ilgili işlemler.
        setPlayerMovement();
    }

    private void setInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void setPlayerMovement()
    {
        //öne bakma ve sağa bakma + olduğu için böyle yapıyoruz. Diğer türlü eksi işaretli olacaktı. Orentation'da ileri bakmayı simüle eden bir gameobject'ti.
        // karakter hareket ettikçe öne baktığı yer transform'un yönü olacak. Yani karakterin yönü. Ve transform değişiyor, sürekli yeni olduğu yeri alıyoruz.
        _movementDirection = _orientationTransform.forward * _verticalInput
        + _orientationTransform.right * _horizontalInput;

        // direkt transform değişikliği yaparsan çok statik ve çirkin durur. Onun yerine Rigidbody kullanarak hareket ettiriyoruz. Ve force uygulayarak hareket ettiriyoruz.
        // ForceMode.Force ile uyguladığımız kuvvet sürekli olarak uygulanır. Yani sürekli bir hız kazandırır.
        // normalized ile hareket yönünü birim vektör haline getiriyoruz. Yani yönü koruyoruz ama hızını 1 birim yapıyoruz. Böylece hareket hızı sabit kalır.
        // 20f değeri ise hareket hızını belirler. Bu değeri istediğin gibi değiştirebilirsin.

        _playerRigidbody.AddForce(_movementDirection.normalized * _moveSpeed, ForceMode.Force);

    }

    

}
