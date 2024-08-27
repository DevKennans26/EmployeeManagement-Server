namespace EmployeeManagementServer.Documentation.Common.Framework.Architecture;

public class Overview
{
    #region Domain-Driven Design (DDD): Stratejik Olarak Derinlemesine Inceleme

    /*
     * Yeni bir projeye başlanacağı vakit projenin toplam net süresini öngörebilmek için kah sezgisel, kah tahmine dayalı alt kırılımlar oluşturmaya çalışırız. Bu kırılımlar eşliğinde projenin anatomisini periyodik olarak daha hesaplanabilir hale getirerek izafi açıdan göz önüne sermiş oluruz. Tabi genel anlamda bu uğraşta ne ölçüde başarılı olduğumuz ayrı bir konu olsa da burada niyetimiz stratejik açıdan projenin cephesel haritasını tam olarak masaya yatırabilmektir. Özellikle büyük projelerde, projenin kapsamından dolayı kaynaklanan bilgi eksikliği nedeniyle bazen zamanı tahmin etmek neredeyse imkansız hale gelebilmektedir. Bu durumda gerçekte ne yapmamız gerektiğini ve nasıl yapacağımızı tam olarak bilemeyeceğimizden dolayı izafi olarak tahmin edilen zaman ya haddinden fazla uzun ya da kısa olabilmektedir. İşte bu öngörülebilirliği kuvvetlendirmek ve ilgili projenin kapsamındaki bilinmeyen terminolojik terimlerin ve bilgilerin tüm personeller tarafından anlaşılabilir hale getirebilmek için bir standart geliştirilmesi gerekmektedir.
     *
     * Hadi diyelim ki, üç aşağı beş yukarı öngörülerle temellendirilmiş ve belirli bir noktaya kadar getirilmiş bir projede bazen de hiç öngörülemeyen ekstra işlere ya da modüllere sonradan ihtiyaç duyulabilmektedir. İşte böyle bir durumda geliştiriciler açısından önceden yazılmış kodun ne yaptığını hatırlamak ve hatta tekrardan anlamak neredeyse imkansız hale gelebilmektedir. İşte bu durumda da yazılımcılar açısından kodu hızlı hatırlayabilmek ve bir standart haline getirebilmek için başlangıçta uygun kuralların getirilmesi ve o kurallar çerçevesinde kodun geliştirilmesi gerekmektedir.
     *
     * Yukarıda bahsedilen proje süreçlerinde yaşanan sıkıntıları özetlersek eğer: proje kapsamından kaynaklı olası terminoloji ve bilgi eksikliği yüzünden meydana gelen hesaplamadaki tutarsızlıklar yahut geliştiriciler açısından yaşanabilen olası kod karmaşası ve geriye dönük mimarisel ya da tasarımsal kopuşlar dikkat ederseniz hep projelerin etki alanıyla ilgili çözüm getirebileceğimiz noktalara temas etmektedir. İşte tüm projelerde, işin kavramsal mantığını ve etki alanını geliştirici olanlar için koda yansıtan, geliştirici olmayanlar için ise daha anlaşılır ve sürdürülebilir kılan en azından olayı bu kurumsal açıdan değerlendirmemizi savunan Domain Driven Design yaklaşımını baz alarak bu sorunlara kolayca çözüm bulabilir ve ortadan kaldırabiliriz, kaldırmayı amaçlayabiliriz.
     *
     * Burada Domain Driven Design (DDD)'nin stratejik açıdan ne olduğunu, neleri savunduğunu detaylıca inceleyecek, ve ardından taktiksel açıdan DDD'nin hangi terimleri günlük hayatımıza kazandırdığını ve mimarilerimize ne gibi şekiller verebildiğini tartışıyor olacağız. 
     */

    #region Domain Driven Design Nedir ?

    /*
     * 'Domain Driven Design, gerçek dünyadaki iş modellerini herkesin anlayabileceği ortak bir dil (Ubiquitous Language) ile oluşturarak dijital dünyaya uyarlamak için yazılımların nasıl modellenmesi gerektiği konusunuda bir felsefeyi savunur.'
     *
     * Domain Driven Design'i anlayabilmek için öncelikle 'Domain' kelimesinin ne olduğunu anlamamız gerekmektedir. Mevzu bahis konumuzun yazarı olan Eric Evans'a göre Domain, her yazılımın kendine has olayları ve ilgi alanları vardır.
     *
     * İşte buradaki ilgi alanı Domain'e karşılık gelmektedir. Bunu şöyle örneklendirebiliriz: Bir market yönetim sistemi inşa edeceğimizi düşünelim. Bu işi “Market Yönetimi” şeklinde nitelendirmek sizce doğru mu? Bazılarınız evet diyebilir ama gerçekten öyle mi? Misal, kurulacak sistem bir süper market için ise bu marketten farklı olacaktır. Öyle değil mi? Bu yüzden ilgi alanımızı isimlendirirken öncelikle spesifik/özel olmaya dikkat etmeli ve yapacağımız işe uygun bir isimlendirmeyle başlamalıyız. Evet, eğer bu bir süper market yönetim sistemiyse o halde “Süper Market Yönetimi” şeklinde isim vermemiz “Market Yönetimi” isminden daha mantıklı, açıklayıcı ve anlamlı olacaktır. Böylece yazılımımızın Domain’i(yani ilgi/etki alanı) isimsel olarak net belirlenmiş olacaktır.
     *
     * Domain Driven Design‘ın, ‘Domain’ kelimesine açıklık getirdikten sonra ‘Driven Design’ kısmını da açarsak eğer; proje yaklaşımımızın “tasarım aklı kim” sorusunu soran kısmıdır. Bunu da örneklendirmek için Data Driven Design‘ı ele alalım. Adı geçen yaklaşım, mevcut problemi data/veri temelli bir şekilde analiz etmeye ve çözmeye çalışan bir stratejiye sahiptir. Benzer şekilde ilgili kalıbı -Ahmet Driven Design- olarak kurgularsak var olan problemi Ahmet temelli bir şekilde ele almayı önermektedir. Haliyle Domain Driven Design‘da var olan ve yaşanan problemlerin Domain esas alınarak analiz edilmesi ve çözülmesi gerektiğini savunan ve bunun için Domain’in net bir şekilde anlaşılmasının gerekli olduğunu söyleyen bir felsefedir.
     *
     * 'Domain Driven Design, var olan ve yaşanan problemlerin Domain esas alınarak analiz edilmesi ve çözülmesi gerektiğini savunan ve bunun için Domain’in net bir şekilde anlaşılmasının gerekli olduğunu söyleyen bir felsefedir.
     */

    #endregion

    #region Strategic Domain Driven Design

    /*
     * DDD, stratejik olarak temel kavranlar üretmekte ve her bir proje sürecinde bu kavramlara özen gösterilmesi gerektiğini ifade etmektedir. Şimdi gelin bu kavramları tek-tek inceleyelim.
     */

    #region Domain Model

    /*
     * Bir domain model, yapılacak yazılımın etki alanının (yani, domain'inin) toplam fikrini içermesi ve şöyle bakıldığı zaman herkesin ilgili operasyona dair neyin ne anlama geldiğini anlayacağı bir vaziyette olması gerekmektedir. Domain model içinde tüm özellik adlarına sahiptir ve genellikle, UML diyagramlarına nazaran resim ve görselle desteklenmektedir.
     *
     * Son olarak Eric Evans’a göre domain model, belirli bir diyagram değildir! Diyagramın iletmeyi amaçladığı fikirdir. Ve bu sadece alan uzmanının kafasındaki bilgi değil, bu bilginin titiz bir şekilde organize edilmiş bir soyutlamasıdır.
     */

    #endregion
    
    #region Domain Expert
    
    /*
     * Bir yazılımcıya uçak trafiği yönlendirme ile ilgili bir uygulama geliştirmesi söylense, ilgili yazılımcının konuya dair herhangi bir bilgisi olmaksızın (yani domain bilgisi olmadan) uygulama geliştirmesi pek mümkün olmayacaktır. Yazılımcının bu işi gerçekleştirebilmesi için; uçak, uçak trafiği, nereye yönlendirileceği, ne şekilde yönlendirileceği, işin mevzuatı vs. gibi türlü bilgilerin aydınlatılması gerekmektedir. Peki bu konulara hakim olan kim var? Filanca kişi… Artık her kimse… İşte o filanca kişi işin uzmanı ve yazılımın geliştirilmesi için gerekli tüm teorik bilgilere sahiptir. Haliyle bu kişi olmaksızın yazılımın geliştirilmesi pek mümkün değildir. Bu kişi artık bizim için Domain Expert’tir.
     */
    
    #endregion

    #region Ubiquitous Language
    
    /*
     * Telafuzu zor olan bu terim (yubikutıs ya da öyle bişey) yazılım ekibiyle, domain expert arasındaki ortak iletişimi sağlamakta, sağlanması gerektiğini ifade etmektedir. Domain expert’ler, alanlarına dair her ne kadar derin ve yeterli bilgiye sahip olsalar da yazılım geliştirme hakkında hiçbir şey bilmiyor olabilirler. Aynı şey yukarıda gördüğümüz gibi bir yazılım geliştirici için de geçerli olabilir ve çalışılacak alana dair herhangi bir bilgi söz konusu olmayabilir. İşte böyle bir durumda DDD yazılım geliştiricisi ile domain expert’ler arasında her iki tarafında rahat anlaşabilmesi için ortak dil bulunmasını önermektedir. Hatta bu bir tek domain expert ile geliştiricilerden ziyade projedeki diğer tüm çalışanlar içinde geçerli olabilecek şekilde anlaşılabilir bir dil olmasını savunmaktadır. Evet, bu dili geliştirmek kolay değildir. Zaman alabilir amma velakin son derece gerekli bir uğraş olduğu aşikardır. Zaten sizler onlarca kez bu şekilde ortak bir dil geliştirilmediği durumlar için projelerde anlaşmazlıkların çıktığına ve hatta aynı şeyin farklı kelimelerle anlatılmaya çalışıldığından tartışmaların lüzumsuz yere yükseldiğine onlarca kez şahit olmuşsunuzdur kanaatindeyim. Haliyle bu doğruyu illa ki DDD’nin söylemesine gerek olmasa da aklın yolu bir olduğu için DDD’de bunu kayıtsız şartsız olması gereken bir durum olarak ortaya koymaktadır.
     */

    #region Peki Uniquitous Language’i Koda Nasıl Yansıtacağız?

    /*
     * Ubiquitous language’in önerdiği ortak dilin geliştirilmesi ister-istemez domain model’e yansıyacaktır. Çünkü domain model, yapılacak işin omurgasıdır. Haliyle otomatik olarak bu ortak dil koda yansımış olacaktır. Zaten ortak dil kullanmanın en büyük avantajı bu dilin tekrarlı kullanımında dilin zayıf yönlerini belirlememizi sağlamasıdır. Bu durumda domain model hızlı bir şekilde düzeltme yapabilmemizi sağlayacak ve böylece ortak dilde bir şey değişirse bu domain model aracılığıyla doğrudan kod üzerinde de değişikliğe sebebiyet vermiş olacaktır.
     *
     * Son olarak ortak dilin neden elzem olduğunu ortaya koyabilmek için Barış Velioğlu’nun makalesindeki örneği aşağıda alıntılıyorum: '…dolayısıyla ekip ya da firma içerisinde isimlendirme konusunda tutarlılık göstermek bir yana, yazılımcıların kendileriyle bile çeliştikleri durumlar olur. Bazen Delete sözcüğünü kullanırken, bazen Remove sözcüğünü kullanmaya karar verirler ve sorduğunuzda nedenini kendileri bile bilmez ya da hatırlamazlar…'
     */

    #endregion
    
    #endregion

    #region Bounded Context

    /*
     * 'Domain Driven Design tasarımındaki en merkezi prensip Bounded Context’tir.'
     *
     * Bounded Context, birbirlerinden ayrılmış ve sınırları belirlenmiş yapılanmalardır. Esasında bu içeriğimizin başlangıç paragrafında belirtilen alt kırılımlardaki her bir ana kırılım bir Bounded Context’e karşılık gelmektedir. Bounded Context’leri mikro servis mimarilerdeki her bir servise karşılık gelen projeler/microservis olarak da düşenebilirsiniz. Diğer servisler olmadan, onlara bağlı olmaksızın geliştirilebilen, çalıştırılabilen, bağımsız otonom birimlerdir. Örnek vermemiz gerekirse; Amazon’da ürünlerin aranıp listelendiği yer, alışveriş sepeti, satın alma işlevleri vs. ayrı ayrı bounded context’tir. Bu işlevlerin her biri diğeri olmadan tek başına geliştirilebilir.
     */

    #region Peki Bounded Context’ler neye göre belirlenir?

    /*
     * Bounded context’leri belirleyebilmek sanıldığı kadar kolay bir iş değildir. Bunun için sezgisel olarak aşağıdaki durumlara dikkat edilmesi önerilmektedir:
     * Domain hakkında konuşurken, konuşulan dile pürdikkat kesilmek. Zaman zaman benzer, hatta aynı kelimeler farklı anlamlar ifade etmeye başlayabilir. Dilin tam bu farklılaşmaya başladığı noktalar, bize farklı sınırlar içerisinde bulunduğumuzu işaret ediyor olabilir.
     * Hangi seviyede transactional tutarlılığa ihtiyaç olduğunu gözlemlemek.
     * Yapılacak işin altyapısına göre değil, iş mantığına göre belirlenmelidir.
     *
     * 'Domain Driven Design ile çalışmak heuristic (sezgisel) olabilmeyi gerektirir.'
     *
     * Bounded context’ler;
     * Kendi içlerinde tutarlı ve bütündür.
     * Pek istenmese de belirli kurallar çerçevesinde birbirleriyle iletişim kurabilirler.
     */

    #endregion

    #endregion

    #region Context Mapping

    /*
     * Bounded context’lerin kendi aralarındaki iletişim mimarisine dayalı birbirleriyle olan kesişim noktalarını izah edebilmeye context mapping denmektedir. Yani bir başka deyişle context mapping, bounded context’ler ile bunlardan sorumlu ekipler arasındaki ilişkiyi belirlemenize olanak sağlayan araçtır. Buna bir örnek vermemiz gerekirse eğer; siparişler tablosunda (bounded context) müşteri numarasının olması amma velakin müşteri tablosunda (bounded context) siparişe dair herhangi birşeyin olmaması yani bu sınırların ayarlanması bir Context Mapping’dir.
     */

    #endregion

    #region Clean - Readable Code

    /*
     * Kod inşa edilirken domainin amacını yansıtacak şekilde inşa edilmelidir. Burada dikkat edilmesi gereken yapılara verilen isimlerin ne kadar pratik ya da standart olduğu değil, uzun dahi olsa ne kadar yaptığı işi iyi anlatıyor olmasıdır. Bu DDD’nin önerisidir. Temiz koda gelirsek eğer, zaten bu iyi bir yazılımın temel gereksinimidir.
     */

    #endregion
    
    #endregion

    #region Layered Architecture

    /*
     * Domain Driven Design, geliştirme süreçleri için herhangi bir teknolojik veya mimarisel seçim yapmaktan ziyade problemin net bir şekilde tanımlanabilmesine yardımcı olmakta ve uygulamayı geliştiren tüm paydaşların arasında ortak bir dil (ubiquitous language) geliştirilmesini öncelik olarak sunmaktadır. Bu mantıkla yola çıkarsak eğer DDD, projeye herhangi bir mimarisel yaklaşımı dayatmamakta, sadece yukarıda gördüğümüz gibi teknik olarak domain katmanının nasıl tasarlanacağını anlatmaktadır. Bu kabulden sonra hangi yaklaşımın ve mimarinin kullanılacağı biz geliştiricilere ve projenin paydaşlarının ortak kararına kalmıştır.
     */

    #endregion

    #endregion
}