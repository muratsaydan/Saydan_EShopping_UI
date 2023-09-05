<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="sac.aspx.vb" Inherits="sac" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<div class="container ">

		<h2>Satış Sözleşmesi</h2>


		<div class="clr5"></div>
		<p><strong>1. SATICI</strong></p>
		<div class="clr5"></div>
		<div class="left">cepport <span style="color: gray;">(Kullanıcının ad, soyad ve iletişim bilgileri satış işleminin tamamlanmasıyla beraber sunulacaktır.)</span></div>
		<div class="clr5"></div>

		<p><strong>2. SATIŞ BİLGİLERİ:</strong></p>
		<div class="clr5"></div>
		<p>İşbu Ön Bilgilendirme Formuna konu hizmet bilgileri aşağıdaki gibidir:</p>

		<table id="tbl1" runat="server" style="width: 100%; padding: 10px"></table>



		<div class="left" style="width: 250px;">Ödeme Şekli</div>
		<div class="left">Kredi kartı tek çekim</div>
		<div class="clr5"></div>
		<div class="mss_ayr" style="width: 410px;"></div>
		<div class="clr5"></div>

		<div class="left" style="width: 250px;">Kargo Ücretini Karşılayacak Taraf</div>
		<div class="left">Alıcı </div>
		<div class="clr5"></div>
		<div class="mss_ayr" style="width: 410px;"></div>
		<div class="clr20"></div>


		<p>Alıcı, Satıcılar tarafından listeleme sayfasında yer verilen ve işbu ön bilgilendirme formunda yer alan ürün/hizmetlerin temel nitelikleri, satış fiyatı, ödeme şekli, teslimatına ilişkin bilgileri dahil fakat bunlarla sınırlı olmamak üzere ürün/hizmet ile ilgili her türlü bilgiyi okuduğunu, anladığını ve onayladığını kabul ve beyan eder. </p>

		<p><strong>3. CAYMA HAKKI: </strong></p>
		<div class="clr5"></div>

		<p>Alıcı, Satıcı ile yapacağı mesafeli sözleşmelerde herhangi bir gerekçe göstermeksizin 14 gün içerisinde cayma hakkını kullanabilir. Cayma hakkı süresi malın teslimine ilişkin sözleşmelerde alıcının ürünü teslim almasından itibaren, ürün haricindeki hizmete ilişkin sözleşmelerde ise sözleşmenin yapıldığı tarihten itibaren başlar. Alıcının cayma hakkını Satıcıya yöneltmesi gerekmektedir. Alıcı işbu hakkını süresi içerisinde ve yazılı olarak Satıcının açık adresine yapacağı yazılı bildirim, e-posta veya fax yolu ile kullanabilir. Satıcı, Alıcının cayma talebinin kendisine ulaştığını Alıcıya yapacağı bildirimle teyit etmek zorundadır.</p>

		<p>Alıcı, cayma hakkının hukuka uygun olarak kullanıldığı durumlarda iade işlemini gerçekleştirirken Satıcı tarafından kendisine bildirilen taşıyıcıyı tercih edecektir. Alıcıya iade için herhangi bir taşıyıcı bilgisinin sunulmadığı veya adresinde söz konusu taşıyıcının şubesinin bulunmadığı haller dahil olmak üzere Satıcı, Alıcıdan hangi ad altında olursa olsun iade masrafı talep edemez. Satıcı ilave hiçbir masraf talep etmeksizin iade edilmek istenen ürünü Alıcıdan almakla yükümlüdür.</p>

		<p>Alıcı, Mesafeli Sözleşmeler Yönetmeliği md. 15/1  uyarınca cayma hakkını aşağıdaki hallerde kullanamayacağını kabul etmiştir:</p>
		<p>a) Fiyatı finansal piyasalardaki dalgalanmalara bağlı olarak değişen ve satıcı veya sağlayıcının kontrolünde olmayan mal veya hizmetler,</p>
		<p>b) Tüketicinin istekleri veya kişisel ihtiyaçları doğrultusunda hazırlanan mallar,</p>
		<p>c) Çabuk bozulabilen veya son kullanma tarihi geçebilecek mallar,</p>
		<p>ç) Tesliminden sonra ambalaj, bant, mühür, paket gibi koruyucu unsurları açılmış olan mallardan; iadesi sağlık ve hijyen açısından uygun olmayanlar,</p>
		<p>d) Tesliminden sonra başka ürünlerle karışan ve doğası gereği ayrıştırılması mümkün olmayan mallar,</p>
		<p>e) Malın tesliminden sonra ambalaj, bant, mühür, paket gibi koruyucu unsurları açılmış olması halinde maddi ortamda sunulan kitap, dijital içerik ve bilgisayar sarf malzemeleri,</p>
		<p>f) Abonelik sözleşmesi kapsamında sağlananlar dışında, gazete ve dergi gibi süreli yayınlar,</p>
		<p>g) Belirli bir tarihte veya dönemde yapılması gereken, konaklama, eşya taşıma, araba kiralama, yiyecek-içecek tedariki ve eğlence veya dinlenme amacıyla yapılan boş zamanın değerlendirilmesine ilişkin hizmetler,</p>
		<p>ğ) Elektronik ortamda anında ifa edilen hizmetler veya tüketiciye anında teslim edilen gayrimaddi mallar,</p>
		<p>h) Cayma hakkı süresi sona ermeden önce, tüketicinin onayı ile ifasına başlanan hizmetler.</p>
		<div class="clr5"></div>

		<p><strong>4.  İşbu ön bilgilendirme formu, elektronik ortamda Alıcı tarafından okunarak kabul edilmiştir.</strong></p>
		<div class="clr5"></div>

		<p>
			<strong>5. Satıcı, ürün veya hizmeti, alıcı ödeme yaptıktan sonra 3 (üç) iş günü + 24 saat içinde alıcının adresine gönderir ve gönderi bilgilerinin siteye girişini sağlar. Gönderilen ürün veya hizmeti teslim alan alıcı, ürün/hizmet kararlaştırılan niteliklere ve şartlara uygun ise, ürün ve/veya hizmet bedelinin satıcının banka hesabına transferi için www.mersinborsasi.com  üzerinden onay verir.<br>
				<br>
				Alıcı, Satıcının ürünün kargo bilgilerini veya hizmetin ifa edildiğine ilişkin bilgileri www.mersinborsasi.com sitesine  girmesini müteakiben 3 (üç) iş günü + 24 saat içinde onay veya iade bildiriminde bulunmazsa, ürün veya hizmet bedeli Satıcının hesabına transfer edilir. Bu amaçla, 3 (üç) işgününün tamamlanmasını takiben, alıcının e-posta hesabına, mersinborsasi tarafından bir hatırlatma mesajı atılacaktır. Alıcının bu e-posta mesajına istinaden, takip eden 24 saat içerisinde ürün ve/veya hizmete ilişkin şikâyetini site üzerinden iletmemesi halinde, ürün ve/veya hizmet bedeli Satıcıya transfer edilecektir.</strong>
		</p>
		<div class="clr5"></div>

		<p><strong>6. Alıcı ve Satıcı, Tüketicinin Korunması Hakkında Kanun ve Elektronik Ticaret Düzenlenmesi Hakkında Kanun kapsamında mersinborsasi'un mesafeli sözleşme kurulmasına aracılık eden Aracı Hizmet Sağlayıcı sıfatına haiz olduğunu ve Satıcı ve/veya Alıcılar tarafından sağlanan içerikleri  kontrol etmekle, bu içerik veya içeriğe konu mal ve hizmetle ilgili hukuka aykırı bir faaliyetin ya da durumun söz konusu olup olmadığını araştırmakla yükümlü ve sorumlu olmadığını kabul, beyan ve taahhüt ederler. Satıcı, mersinborsasi kullanıcı sözleşmesinde tanımlanan Sıfır Risk Sisteminin işleyişininin sağlanması amacıyla, satışa sunduğu ürün ve/veya hizmetlere ilişkin bedellerin kendi nam ve hesabına Alıcı'dan tahsil edilmesi konusunda, mersinborsasi'u münhasır olarak temsilci olarak atadığını kabul etmektedir. Temsil yetkisinin Satıcı tarafından iptal edilmesi halinde mersinborsasi, kullanıcı sözleşmesini feshetme ve Satıcı'nın üyeliğini iptal etme hakkına sahiptir. Alıcı, ürün ve/veya hizmetlere ilişkin ödemeyi Satıcı'nın temsilcisi sıfatıyla mersinborsasi'a yapmakla alım-satım akdi kapsamında ödeme yükümlülüğünü ifa etmiş olacaktır.</strong></p>
		<div class="clr5"></div>

		<p><strong>7. Alıcı satın almış olduğu ürün ve hizmetlere ilişkin varsa şikayetlerini yukarıda belirtilen iletişim bilgileri üzerinden Satıcıya iletebilir. Site üzerinden satın almış olduğu ürün ve hizmetlere ilişkin mersinborsasi'un herhangi bir yükümlülüğünün bulunmadığını kabul eden Alıcı, ana sayfada yer alan Müşteri Hizmetleri bölümünden mersinborsasi ile her zaman iletişim kurabilir. </strong></p>
		<div class="clr5"></div>

		<p><strong>8. Alıcı, satın alınan ürün ve hizmetlerle ilgili çıkacak ihtilaflara ilişkin Satıcıya yönelteceği her türlü talebini Tüketici Hakem Heyetleri ve Tüketici Mahkemeleri nezdinde ileri sürebilme hakkına sahiptir. </strong></p>
		<div class="clr5"></div>

		<p><strong>9. İşbu Ön Bilgilendirme Formuna konu ürün veya hizmetten kaynaklanan ihtilaflarda, Gümrük ve Ticaret Bakanlığınca ilan edilen değere kadar Alıcı'nın yerleşim yerindeki Tüketici Hakem Heyetleri ile Alıcı'nın yerleşim yerindeki Tüketici Mahkemeleri yetkilidir.</strong></p>
		<div class="clr5"></div>

		<p><strong>10. İşbu ön bilgilendirme formu Satıcının site üzerindeki ürün veya hizmetlerine yönelik sunduğu her çeşit bilgi/reklam/açıklama ile bir bütün olup Alıcı ve Satıcı arasında yapılan Mesafeli Satış Sözleşmesi'nin ayrılmaz bir parçasıdır. Alıcı, işbu ön bilgilendirme formu ve Satıcı ile akdedeceği Mesafeli Satış Sözleşmesi'ndeki ürün veya hizmete ilişkin bilgiler nedeniyle her türlü talebini tüketici mevzuatı kapsamında Satıcıya yöneltmesi gerektiğini kabul eder.</strong></p>
		<div class="clr5"></div>

		<p><strong>11. Alıcı işbu Ön Bilgilendirme Formu ve Mesafeli Satış Sözleşmesi'ni onaylamasını takiben sitedeki "Ödeme Yap" butonunu tıklamakla seçtiği ürün veya hizmete ilişkin ödeme yükümlülüğü altına girdiğini kabul eder.</strong></p>
		<div class="clr5"></div>

		<p>ALICI </p>
		<p>mersinborsasi <span style="color: gray;">(Kullanıcının ad, soyad ve iletişim bilgileri satış işleminin tamamlanmasıyla beraber sunulacaktır.)</span></p>
		<div class="clr20"></div>
		<p>E-POSTA: hakansencan7@gmail.com</p>
		<p>TARİH: 14 / 06 / 2016</p>



		<div class="sozlesme">
			<p><strong>MADDE 1-  TARAFLAR</strong></p>
			<div class="clr5"></div>
			<p><strong>1.1- SATICI</strong></p>
			<div class="clr5"></div>
			<div class="left">
				MersinBorsasi.com <span style="color: gray;">(Kullanıcının ad, soyad ve iletişim bilgileri satış işleminin tamamlanmasıyla beraber sunulacaktır.)</span>
			</div>
			<div class="clr5"></div>
			<p><strong>1.2- ALICI 	</strong></p>
			<div class="clr5"></div>
			<div class="left">
				mersinborsasi <span style="color: gray;">(Kullanıcının ad, soyad ve iletişim bilgileri satış işleminin tamamlanmasıyla beraber sunulacaktır.)</span>
			</div>
			<div class="clr5"></div>
			<p>Bundan böyle Alıcı ve Satıcı birlikte "Taraflar" olarak anılacaktır.</p>
			<p>Taraflar www.mersinborsasi.com sitesinde belirtilen kurallar dahilinde platform üzerinden birbirleri ile iletişim kurabileceklerdir.</p>

			<p><strong>MADDE 2-  SÖZLEŞMENİN KONUSU:</strong></p>
			<div class="clr5"></div>
			<p>İşbu Mesafeli Satış Sözleşmesi'nin (Bundan sonra Sözleşme olarak anılacaktır) konusu, aşağıdaki nitelikleri ve satış fiyatı belirlenen ürün ile ilgili olarak Tüketicinin Korunması Hakkında Kanun ve Mesafeli Sözleşmeler Yönetmeliği hükümleri gereğince tarafların hak ve yükümlülüklerinin tespitidir.</p>
			<div class="clr5"></div>

			<p><strong>MADDE 3- ÜRÜN BİLGİLERİ:</strong></p>
			<div class="clr5"></div>
			<p>İşbu Sözleşme konusu ürüne; ürünün satış bedeline, teslim ve ödeme şekillerine ait bilgiler aşağıdaki gibidir:</p>



		<table id="tbl5" runat="server" style="width: 100%; padding: 10px"></table>



























			<div class="left" style="width: 250px;">Ödeme Şekli</div>
			<div class="left">
				Kredi kartı tek çekim                                           
			</div>
			<div class="clr5"></div>
			<div class="mss_ayr" style="width: 410px;"></div>
			<div class="clr5"></div>

			<div class="left" style="width: 250px;">Kargo Ücretini Karşılayacak Taraf</div>
			<div class="left">Alıcı </div>
			<div class="clr5"></div>
			<div class="mss_ayr" style="width: 410px;"></div>
			<div class="clr20"></div>

			<p><strong>MADDE 4 - GENEL HÜKÜMLER</strong></p>
			<div class="clr5"></div>
			<p>4.1 Alıcı ve Satıcı, Sözleşme konusu ürününe/hizmetine ilişkin listeleme sayfasında yer verilen açıklamalardan, ürünün/hizmetin sağlam, eksiksiz, siparişte belirtilen niteliklere uygun ve varsa garanti belgeleri ve kullanım kılavuzları ile birlikte teslim edilmesinden Satıcı'nın sorumlu olduğunu kabul, beyan ve taahhüt eder.</p>

			<p>4.2 Alıcı, işbu Sözleşme öncesinde Satıcılar tarafından listeleme sayfasında yer verilen ürün/hizmetlerin temel nitelikleri, satış fiyatı, ödeme şekli, teslimatına ilişkin bilgiler dahil fakat bunlarla sınırlı olmamak üzere ürün/hizmet ile ilgili her türlü bilgiyi ve işbu Sözleşme'nin 3. maddesinde belirtilen ürün/hizmete ilişkin bilgileri okuduğunu, anladığını ve işbu Sözleşme akdedilmeden önce gerekli teyidi verdiğini kabul ve beyan eder.</p>

			<p>4.3 Alıcı, işbu Sözleşme öncesinde ürün/hizmete ilişkin ön bilgilendirme formunu teyit ettiğini kabul, beyan ve taahhüt eder.</p>

			<p>4.4 Taraflar işbu Sözleşme şartlarının yanı sıra Tüketicilerin Korunması Hakkındaki Kanun ve Mesafeli Sözleşmeler Yönetmeliği hükümlerini kabul ettiklerini ve bu hükümlere uygun hareket edeceklerini kabul, beyan ve taahhüt ederler.</p>

			<p>4.5 İşbu Sözleşme'nin tüm maddeleri Satıcı ve Alıcı tarafından okunmuş ve kabul edilmiş olup, işbu Sözleşme Alıcı tarafından elektronik olarak onaylandığı tarihte yürürlüğe girer.</p>

			<p>4.6 Alıcı ve Satıcı, Tüketicinin Korunması Hakkında Kanun ve Elektronik Ticaretin Düzenlenmesi Hakkında Kanun kapsamında mersinborsasi'un mesafeli sözleşme kurulmasına aracılık eden Aracı Hizmet Sağlayıcı sıfatına haiz olduğunu ve Satıcı ve Alıcı tarafından sağlanan içerikleri kontrol etmekle, bu içerik veya içeriğe konu mal ve hizmetle ilgili hukuka aykırı bir faaliyetin ya da durumun söz konusu olup olmadığını araştırmakla yükümlü ve sorumlu olmadığını kabul, beyan ve taahhüt ederler. Taraflar, işbu Sözleşmeye konu üründen/hizmetin sunulmasından kaynaklanan tüm sorumluluğun Satıcı'ya ait olduğunu; www.mersinborsasi.com sitesinin Alıcı ve Satıcı arasındaki işbu Sözleşmenin tarafı olmadığını ve işbu Sözleşmeden kaynaklanan yükümlülüğü bulunmadığını kabul, beyan ve taahhüt ederler. Satıcı,www.mersinborsasi.com kullanıcı sözleşmesinde tanımlanan Sıfır Risk Sisteminin işleyişinn sağlanması amacıyla satışa sunduğu ürün ve/veya hizmetlere ilişkin bedellerin kendi nam ve hesabına Alıcı'dan tahsil edilmesi konusuna münhasır olarak, mersinborsasi'u temsilci olarak atadığını kabul etmektedir. Temsil yetkisinin Satıcı tarafından iptal edilmesi halinde mersinborsasi, kullanıcı sözleşmesini feshetme ve Satıcı'nın üyeliğini iptal etme hakkına sahiptir. Alıcı, ürün ve/veya hizmetlere ilişkin ödemeyi Satıcı'nın temsilcisi sıfatıyla mersinborsasi'a yapmakla alım-satım akdi kapsamında ödeme yükümlülüğünü ifa etmiş olacaktır.</p>

			<p>4.7 İşbu Sözleşme, Alıcı ve Satıcı için www.mersinborasasi.com Kullanıcı Sözleşmesi'nden kaynaklanan yükümlülüklerini ortadan kaldırmaz. Taraflar, mersinborsasi Kullanıcı Sözleşmesi ve eklerinde düzenlenen kurallara uymakla yükümlüdür.</p>

			<p>4.8 Taraflar, aralarındaki işbu sözleşmeden kaynaklı doğacak ihtilaflardan dolayı mersinborsasi'un, idare ve yargı kararları dahil olmak üzere, herhangi bir zarara uğraması halinde onaylamış oldukları www.mersinborasasi.com  kullanıcı sözleşmesi ve sitedeki diğer kurallar kapsamında mersinborsasi'un söz konusu zararları kendilerine rücu etme hakkı olduğunu kabul ederler.</p>
			<div class="clr5"></div>

			<p><strong>MADDE - 5 CAYMA HAKKI: </strong></p>
			<div class="clr5"></div>

			<p style="font-weight: bold;">5.1 Alıcı, işbu Sözleşme konusunun ürün olması halinde; ürünün kendisine veya gösterdiği adresteki kişiye / kuruluşa tesliminden itibaren 14 (ondört)  gün içerisinde, işbu Sözleşme konusunun ürün dışında bir hizmetin sunulmasına ilişkin olması halinde, işbu Sözleşmenin kurulduğu tarihten itibaren 14 (ondört) gün içerisinde herhangi bir gerekçe göstermeksizin işbu Sözleşmeden cayma hakkına sahiptir. Alıcı'nın, cayma hakkını kullanabilmesi için, bu süre içinde Satıcı/Sağlayıcı'ya bildirimde bulunması şarttır.</p>

			<p>5.2 Taraflar, Mesafeli Sözleşmeler Yönetmeliği md. 15/1  uyarınca cayma hakkını aşağıdaki hallerde kullanamayacağını kabul etmiştir:</p>

			<p style="margin-left: 10px;">5.2.1 Fiyatı finansal piyasalardaki dalgalanmalara bağlı olarak değişen ve satıcı veya sağlayıcının kontrolünde olmayan mal veya hizmetler,</p>
			<p style="margin-left: 10px;">5.2.2 Tüketicinin istekleri veya kişisel ihtiyaçları doğrultusunda hazırlanan mallar,</p>
			<p style="margin-left: 10px;">5.2.3 Çabuk bozulabilen veya son kullanma tarihi geçebilecek mallar,</p>
			<p style="margin-left: 10px;">5.2.4 Tesliminden sonra ambalaj, bant, mühür, paket gibi koruyucu unsurları açılmış olan mallardan; iadesi sağlık ve hijyen açısından uygun olmayanlar,</p>
			<p style="margin-left: 10px;">5.2.5 Tesliminden sonra başka ürünlerle karışan ve doğası gereği ayrıştırılması mümkün olmayan mallar,</p>
			<p style="margin-left: 10px;">5.2.6 Malın tesliminden sonra ambalaj, bant, mühür, paket gibi koruyucu unsurları açılmış olması halinde maddi ortamda sunulan kitap, dijital içerik ve bilgisayar sarf malzemeleri,</p>
			<p style="margin-left: 10px;">5.2.7 Abonelik sözleşmesi kapsamında sağlananlar dışında, gazete ve dergi gibi süreli yayınlar,</p>
			<p style="margin-left: 10px;">5.2.8 Belirli bir tarihte veya dönemde yapılması gereken, konaklama, eşya taşıma, araba kiralama, yiyecek-içecek tedariki ve eğlence veya dinlenme amacıyla yapılan boş zamanın değerlendirilmesine ilişkin hizmetler,</p>
			<p style="margin-left: 10px;">5.2.9 Elektronik ortamda anında ifa edilen hizmetler veya tüketiciye anında teslim edilen gayrimaddi mallar,</p>
			<p style="margin-left: 10px;">5.2.10 Cayma hakkı süresi sona ermeden önce, tüketicinin onayı ile ifasına başlanan hizmetler.</p>

			<p>5.3 Alıcı'nın cayma hakkını kullanması hâlinde Satıcı, cayma bildiriminin kendisine ulaştığı tarihten itibaren en geç 14 (on dört) gün içerisinde almış olduğu toplam bedeli ve tüketiciyi borç altına sokan her türlü belgeyi tüketiciye hiçbir masraf yüklemeksizin iade etmekle yükümlüdür. Alıcı ise cayma hakkını kullandığına ilişkin bildirimi yönelttiği tarihten itiaren on (10) gün içerisinde malı satıcıya veya yetkilendirmiş olduğu kişiye geri göndermek zorundadır.</p>
			<p>5.4 Alıcı, cayma hakkının hukuka uygun olarak kullanıldığı durumlarda iade işlemini gerçekleştirirken Satıcı tarafından kendisine bildirilen taşıyıcıyı tercih edecektir. Alıcı'ya iade için herhangi bir taşıyıcı bilgisinin sunulmadığı veya adresinde söz konusu taşıyıcının şubesinin bulunmadığı haller dahil olmak üzere Satıcı, Alıcı'dan hangi ad altında olursa olsun iade masrafı talep edemez. Satıcı ilave hiçbir masraf talep etmeksizin iade edilmek istenen ürünü Alıcı'dan almakla yükümlüdür.</p>
			<p>5.5 Alıcı tarafından sipariş edilmeyen malın teslimi veya hizmetin sunulması durumunda, ürünün kullanılması veya hizmetin tüketilmesine ilişkin durumlar saklı kalmak kaydıyla, Satıcı, Alıcı'ya karşı herhangi bir talepte bulunamaz.</p>
			<div class="clr5"></div>

			<p><strong>MADDE - 6 YETKİLİ MAHKEME:</strong></p>
			<div class="clr5"></div>
			<p>İş bu Sözleşme ile ilgili çıkacak ihtilaflarda her yıl Gümrük ve Ticaret Bakanlığı tarafından ilan edilen değere kadar Alıcı'nın yerleşim yerindeki Tüketici Sorunları Hakem Heyetleri, söz konusu değerin üzerindeki ihtilaflarda ise Tüketici Mahkemeleri yetkilidir.</p>

			<div class="clr5"></div>
			<p>İşbu sözleşme 14 / 06 / 2016 tarihinde kurulmuştur. </p>
			<div class="clr20"></div>

			<p>SATICI</p>
			<p>
				mersinborsasi <span style="color: gray;">(Kullanıcının ad, soyad ve iletişim bilgileri satış işleminin tamamlanmasıyla beraber sunulacaktır.)</span>
			</p>
			<div class="clr20"></div>

			<p>ALICI </p>
			<p>
				Müşteri <span style="color: gray;">(Kullanıcının ad, soyad ve iletişim bilgileri satış işleminin tamamlanmasıyla beraber sunulacaktır.)</span>
			</p>
			<div class="clr20"></div>
		</div>

	</div>

</asp:Content>

