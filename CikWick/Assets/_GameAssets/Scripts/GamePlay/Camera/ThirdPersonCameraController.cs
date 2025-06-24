using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _playerVisualTransform; // karakterin görüntü mesh kısmındaki değeri

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        // Kamera kontrolleri update veya late update içinde yapılabilir
        // Bu yaklaşım 3. şahıs kameralarda yaygındır çünkü: Kamera oyuncudan yüksekte olabilir,Ama oyuncunun yatay hareket yönü önemli,Dikey fark göz ardı edilerek düz bakış açısı elde edilir
        // Karakterin baktığı yönün directionunu açısını pozisyonunu görmemiz lazım :

        // KAÇIRDIĞIN NOKTA : BU script şu an kamerada çalışıyor yani transform.postionu kameranın pozisyonunu veriyor.
        Vector3 viewDirection =
         _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z); // Y eksenindeki pozisyonu sabit tutarak bakış yönünü hesaplıyoruz

        _orientationTransform.forward = viewDirection.normalized; // Yönü normalize ederek yön vektörünü ayarlıyoruz // Yani oryantasonum artık kameranın baktığı yere bakması gerekiyor.
        //orientasyonum baktığım yöne bakmalı o yüzden viewdriection'a eşitliyoruz.

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizontalInput; // Yönü hesaplıyoruz

        if (inputDirection != Vector3.zero) // Eğer karakter hareket ediyorsa...Karakter duruyorsa sürekli bu slerp işlemi yapılırsa karakter sürekli döner.
        {
         //Karakterin görsel parçasının transformu; karakteri görsel olarak döndüreceğiz. Görsel parça da playervisual diye ayrı bir şey yapmıştık hatırlarsan. Onu döndürsek yeterli oluyor yani
        // geri döndüğünde görsel geri dönüyordu ya o olay yani. 
        _playerVisualTransform
            .forward = Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed); // Karakterin görsel parçasını input yönüne doğru döndürüyoruz
                                                                                                                                  // Bu işlem, karakterin görsel parçasının input yönüne doğru yumuşak bir şekilde dönmesini sağlar. Slerp, sferik lineer interpolasyon anlamına gelir ve iki vektör arasında yumuşak bir geçiş sağlar.
                                                                                                                                  // Bu şekilde karakterin görsel parçası, oyuncunun girdiği yönü takip eder ve yumuşak bir dönüş sağlar.
                                                                                                                                  // Lerp pozisyonlar için, Slerp rotasyonlar için kullanılır. Yani karakterin görsel parçasını input yönüne doğru yumuşak bir şekilde döndürüyoruz.

        // Time.deltaTime kullanımı :
        // Time.deltaTime, bir frame'in ne kadar sürdüğünü gösterir. Yani
        // bu değeri kullanarak hareket hızını FPS'e bağımlı hale getirebilir
        // ve oyunun her platformda aynı hızda çalışmasını sağlayabiliriz.
            // Update her bir frame'de çağrıldığı için Önceki frame ile şu anki frame arasındaki saniye değerini veriyor Time.deltatime.

        //Bilgisayardan bilgisayara FPS değiştiği için burada bir patlar ve pc'den pc'ye değişir. 
        // Burada fixedUptade de kullanamayız çünkü fiziksel bir işlem de yapmıyoruz. Bunu update'te nasıl optimize ederiz dersek? 
        }
        
        



    }
    
}
