using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 20f;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown; // Zıplama sonrası bekleme süresi
    [SerializeField] private bool _canJump = true;

    [Header("Physics Settings")]
    [SerializeField] private float _normalGravity = -9.81f; // Normal gravity
    [SerializeField] private float _jumpGravity = -20f; // Zıplama gravity'si (Daha gerçekçi zıplama için)


    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask _groundLayer; // Yere basılıp basılmadığını kontrol etmek için kullanacağımız layer mask
    [SerializeField] private float _playerHeight = 2f; // Player'ın boyu. Yani player'ın yüksekliği. Bu değeri player modeline göre ayarlayabilirsin.


    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private UnityEngine.Vector3 _movementDirection;
   
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

        if (Input.GetKeyDown(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false; // Zıpladıktan sonra bir süre zıplayamaz. Bu süreyi ayarlamak için bir coroutine kullanabilirsin.
            Physics.gravity = new Vector3(0, _jumpGravity, 0); // Zıplama gravity'si
            setPlayerJumping();
            // Yani bir zıplama yaptıktan sonra cooldown kadar bekleyip, canJump'ı true yapan methodu çağırıp; tekrar zıplayabilir hale getiriyoruz bu if'e tekrar girebiliyor.
            // Invoke methodu şu işe yarar: Belirli bir süre sonra bir methodu çağırmak için kullanılır. Yani burada zıpladıktan sonra belirli bir süre bekleyip, canJump'ı true yapıyoruz.
            Invoke(nameof(ResetJumping), _jumpCooldown); 

        }

    }

    #region Player Movement
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

    #endregion Player Movement

    #region Player Jumping
    private void setPlayerJumping()
    {

        // Zıplamadan önce y ekseninde hızımızı sıfırlıyoruz ki zıplamamız bozulmasın.
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        // Yere basılıp basılmadığını kontrol etmeliyiz. Yani player zıplayabilir mi? Zıplamak için yere basılı olması gerekiyor.
        // Eğer player zıplıyorsa, yani yukarı doğru bir kuvvet uygulamak istiyorsak, Rigidbody'ye yukarı doğru bir kuvvet uyguluyoruz.
        // ForceMode.Impulse ile uyguladığımız kuvvet anlık bir kuvvet uygular. Yani zıplama anında bir kuvvet uygular.
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }


    private void ResetJumping()
    {
        _canJump = true; // Zıpladıktan sonra bir süre zıplayamaz. Bu süreyi ayarlamak için bir coroutine kullanabilirsin.
        Physics.gravity = new Vector3(0, _normalGravity, 0); // Normal gravity'ye geri dön
    }


    // Yere basılıp basılmadığını kontrol etmek için bir method. Karakter eğer yerdeyse zıplayabilir! kontrolünü yapıyoruz.
    // Mesela jetpack joyride yada flappy bird yazacaksın bunu yapmaman lazım, havada tuşa bastıkça zıplamasını istiyorsun orada. Ama burada sadece yerdeyken zıplaması gerektiğinden raycast yapıyoruz.
    private bool IsGrounded()
    {
        // Yere basılıp basılmadığını kontrol etmek için Raycast kullanıyoruz. Pozisyonumuzdan aşağıya doğru bir ışın gönderiyoruz. 
        // Raycast, bir noktadan bir yönde bir ışın gönderir ve bu ışının bir collider ile çarpışıp çarpışmadığını kontrol eder.
        // Olduığumuz pozisyondan aşağıyada doğru bir ışın gönderiyoruz. Yani player'ın altına doğru bir ışın gönderiyoruz.
        // _playerHeight, player'ın boyunu temsil eder. Yani playerın boyunun yarısı kadar aşağıya doğru bir ışın gönderiyoruz.
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);

        //_groundLayer, yere basılıp basılmadığını kontrol etmek için kullanacağımız layer mask. Yani hangi layer'da yere basılıp basılmadığını kontrol edeceğimizi belirtiyoruz.
        //Plane'imizin ismini Ground olarak ayarlamıştık. Onun içine girip sağ üstteki Layer kısmından Ground layer'ını seçiyoruz.
        // Add layer --> Ground adında bir şey ekledik. Onu seçebiliyoruz.
        // Ardından yukarıdaki değişkeni de sağlamak için 4ncü parametredeki _groundLayer'ı da ground olarak seçebiliyoruz.

        //Böylece sadece benim o plane'imdeki layer'a sahip olanlarda bu raycast çalışacak. Örneğin aynı layer'a sahip olan başka bir nesne varsa, o da yere basılıp basılmadığını kontrol edebilecek.
        //Bir teknenin sadece suda gitmesi gibi. Sadece su layer'ına sahip yerlerde git demiş oluyorsun aslında.
    }
    #endregion Player Jumping




}
