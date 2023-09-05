Imports System.Data
Imports Iyzipay
Imports Iyzipay.Model
Imports Iyzipay.Request
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Partial Class OdemeSayfasi
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass

	Dim drSiparis As DataRow
	Dim SiparisKodu As String = 0
	Dim dtSHAR As DataTable
	Dim ToplamTutar As Decimal

	Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		SiparisKodu = Request.QueryString("kod")
		If Not IsPostBack Then SiparisBilgileriniDoldur()
		'options
		Dim options As New Options()
		options.ApiKey = "fnx2qPCwpKcCVoQTDfQjWHZDBalPci9q"
		options.SecretKey = "JxMC5IbDAWuQdjtxGbDHgbl6qpdYHSwZ"
		options.BaseUrl = "https://api.iyzipay.com"
		'request
		Should_Initialize_Checkout_Form(options)
	End Sub
	Protected Sub Should_Initialize_Checkout_Form(ByVal options As Options)
		Dim request As New CreateCheckoutFormInitializeRequest()
		request.Locale = Locale.TR.GetName()
		request.ConversationId = drSiparis("SiparisKodu").ToString
		request.Price = ToplamTutar.ToString.Replace(",", ".")
		request.PaidPrice = (ToplamTutar + CDec(drSiparis("KargoUcreti"))).ToString.Replace(",", ".")
		request.BasketId = drSiparis("SiparisKodu").ToString

		request.PaymentGroup = PaymentGroup.PRODUCT.ToString()
		request.Buyer = NewBuyer()
		request.ShippingAddress = newShippingAddress()
		request.BillingAddress = newBillingAddress()
		request.BasketItems = newBasketItems()
		request.CallbackUrl = "http://www.mersinborsasi.com/OdemeSonucSayfasi.aspx?kod=" + SiparisKodu.ToString

		Dim checkoutFormInitialize As CheckoutFormInitialize = checkoutFormInitialize.Create(request, options)

		Response.Write(checkoutFormInitialize.ErrorMessage)
		Response.Write(checkoutFormInitialize.Token)
		Response.Write(checkoutFormInitialize.CheckoutFormContent)

	End Sub

	Protected Sub Should_Retrieve_Checkout_Form_Auth(ByVal options As Options)
		Dim request As RetrieveCheckoutFormAuthRequest = New RetrieveCheckoutFormAuthRequest()
		request.ConversationId = drSiparis("SiparisKodu").ToString
		request.Token = "myToken"
		Dim checkoutformAuth As CheckoutFormAuth = checkoutformAuth.Retrieve(request, options)
		Response.Write(checkoutformAuth)
	End Sub

	Private Function NewBuyer() As Buyer
		Dim buyer As New Buyer()
		buyer.Id = drSiparis("MusteriKodu").ToString
		buyer.Name = drSiparis("Isim").ToString
		buyer.Surname = drSiparis("Soyisim").ToString
		buyer.IdentityNumber = drSiparis("TCKimlikNo").ToString
		buyer.Email = drSiparis("Email").ToString
		buyer.GsmNumber = drSiparis("Telefon").ToString
		buyer.RegistrationAddress = drSiparis("TeslimatAdresi").ToString
		buyer.City = drSiparis("TeslimatSehri").ToString
		buyer.Country = "Turkiye"
		buyer.Ip = Request.UserHostAddress
		Return buyer

	End Function

	Private Function newShippingAddress() As Address
		Dim address As New Address()
		address.Description = drSiparis("TeslimatAdresi").ToString
		address.ContactName = drSiparis("Isim").ToString + " " + drSiparis("Soyisim").ToString
		address.City = drSiparis("TeslimatSehri").ToString
		address.Country = "Turkiye"
		Return address
	End Function

	Private Function newBillingAddress() As Address
		Dim address As New Address()
		address.Description = drSiparis("TeslimatAdresi").ToString
		address.ContactName = drSiparis("Isim").ToString + " " + drSiparis("Soyisim").ToString
		address.City = drSiparis("TeslimatSehri").ToString
		address.Country = "Turkiye"
		Return address
	End Function

	Private Function newBasketItems() As List(Of BasketItem)
		Dim paymentBasketItemDtoList As New List(Of BasketItem)()
		For Each drSHAR As DataRow In dtSHAR.Rows
			Dim firstBasketItem As New BasketItem()
			firstBasketItem.Id = drSHAR("SUrunKodu").ToString
			firstBasketItem.Name = drSHAR("StokAdi1").ToString
			firstBasketItem.Category1 = "Muhtelif Ürün"
			firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString()
			firstBasketItem.Price = drSHAR("Tutar").ToString.Replace(",", ".")
			paymentBasketItemDtoList.Add(firstBasketItem)
		Next
		Return paymentBasketItemDtoList
	End Function

	Protected Sub SiparisBilgileriniDoldur()

		Dim cmdstr As String = "SELECT SIPARISLER.*, MUSTERILER.Isim, MUSTERILER.Soyisim, MUSTERILER.Email, MUSTERILER.Telefon, MUSTERILER.Company, MUSTERILER.TCKimlikNo FROM SIPARISLER INNER JOIN MUSTERILER ON SIPARISLER.MusteriKodu = MUSTERILER.MusteriKodu where SIPARISLER.SiparisKodu=" + SiparisKodu.ToString
		drSiparis = Saydanclass._ConnDB(cmdstr).Rows(0)

		If IsDBNull(drSiparis("OdemeYapildiMi")) Then drSiparis("OdemeYapildiMi") = False
		If drSiparis("OdemeYapildiMi") Then Response.Redirect("SiparisDetay2.aspx?kod=" + SiparisKodu.ToString)

		dtSHAR = Saydanclass._ConnDB("SELECT STOK_HAREKET.SUrunKodu, STOK_HAREKET.SiparisKodu, STOK_HAREKET.UrunKodu, STOK_HAREKET.Tutar, STOK_HAREKET.TutarPB, STOK_HAREKET.Vergi, STOK_HAREKET.Miktar, STOK_HAREKET.PBDK, STOK_HAREKET.TedarikciSiparisNo, STOK_HAREKET.TedarikciKargoyaVerdiMi, URUNLER.StokAdi1, URUNLER.TedarikciFirma FROM STOK_HAREKET INNER JOIN URUNLER ON STOK_HAREKET.UrunKodu = URUNLER.StokKodu where SiparisKodu=" + SiparisKodu.ToString)
		ToplamTutar = 0
		For Each drSHAR As DataRow In dtSHAR.Rows
			ToplamTutar += drSHAR("Tutar")
		Next
	End Sub

	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		'If Session("UserID") Is Nothing Then Response.Redirect("login.aspx?x=odemesayfasi&kod=" + SiparisKodu.ToString)
	End Sub

End Class
