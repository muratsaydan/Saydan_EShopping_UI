Imports System.Data
Imports Iyzipay
Imports Iyzipay.Model
Imports Iyzipay.Request
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Partial Class OdemeSayfasi
	Inherits System.Web.UI.Page

	Dim data As String

	Dim Saydanclass As New SaydanClass

	Dim SiparisKodu As Integer = 0
	Dim OdemeSonuc As String = ""
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		SiparisKodu = System.Web.HttpContext.Current.Request.QueryString("kod")

        Dim options As New Options()
		options.ApiKey = "fnx2qPCwpKcCVoQTDfQjWHZDBalPci9q"
		options.SecretKey = "JxMC5IbDAWuQdjtxGbDHgbl6qpdYHSwZ"
		options.BaseUrl = "https://api.iyzipay.com"


		data = System.Web.HttpContext.Current.Request.Params("token")

		Dim request As New RetrieveCheckoutFormAuthRequest()
		request.ConversationId = SiparisKodu.ToString
		request.Token = data

		Dim checkoutFormAuth As CheckoutFormAuth = checkoutFormAuth.Retrieve(request, options)

		Dim cmd2 As String = "select top 1 * from ODEME_ISLEMLERI"
		Dim dtOdemeIslemleri As DataTable = Saydanclass._ConnDB(cmd2)
		Dim drOdemeIslemleri As DataRow = dtOdemeIslemleri.NewRow

		drOdemeIslemleri("KayitTarihi") = Now
		drOdemeIslemleri("SiparisKodu") = SiparisKodu
		drOdemeIslemleri("Token") = checkoutFormAuth.Token
		drOdemeIslemleri("Status") = checkoutFormAuth.Status
		drOdemeIslemleri("PaymentStatus") = checkoutFormAuth.PaymentStatus
		drOdemeIslemleri("PaidPrice") = checkoutFormAuth.PaidPrice
		drOdemeIslemleri("ErrorMessages") = checkoutFormAuth.ErrorMessage
		drOdemeIslemleri("BasketID") = checkoutFormAuth.BasketId
		drOdemeIslemleri("BinNumber") = checkoutFormAuth.BinNumber
		drOdemeIslemleri("CardAssociation") = checkoutFormAuth.CardAssociation
		drOdemeIslemleri("CardFamily") = checkoutFormAuth.CardFamily
		drOdemeIslemleri("PaymentID") = checkoutFormAuth.PaymentId
		drOdemeIslemleri("SystemTime") = checkoutFormAuth.SystemTime.ToString
		dtOdemeIslemleri.Rows.Add(drOdemeIslemleri)
		Saydanclass._SaveDB(cmd2, dtOdemeIslemleri)

		Select checkoutFormAuth.PaymentStatus
			Case "SUCCESS"
				spnmesaj.InnerHtml = "<p>Ödeme işleminiz başarıyla sonuçlanmıştır. E-mail adresinize siparişinizle ilgili bir e-mail gönderilmiş ve işleme alınmıştır. Ekiplerimiz ürünlerinizi hazırlayıp en kısa süre içinde kargoya verecek ve sitemiz içindeki Sipariş Takip sayfasından konuyla ilgili güncel bilgiyi size ileteceklerdir. Siparişinizin durumunu oradan takip edebilirsiniz.</p>"
				Dim cmdstr As String = "UPDATE SIPARISLER SET OdemeYapildiMi=1 where SiparisKodu=" + SiparisKodu.ToString
				Dim a = Saydanclass.ExecSQL(cmdstr)

				Saydanclass.MailGonder("hakansencan7@gmail.com;murat@lojistikprogrami.net", "* * * Mersin Borsası YENİ SİPARİŞ * * * ", "Sipariş Listesini Kontrol ediniz")



			Case Else
				spnmesaj.InnerHtml = "<p>Ödeme işleminiz sırasında hatayla karşılaşılmıştır. E-mail adresinize siparişinizle ilgili bir e-mail gönderilmiş ve işleme alınmıştır. Ekiplerimiz ürünlerinizi hazırlayıp en kısa süre içinde kargoya verecek ve sitemiz içindeki Sipariş Takip sayfasından konuyla ilgili güncel bilgiyi size ileteceklerdir. Siparişinizin durumunu oradan takip edebilirsiniz.</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.Token : " + checkoutFormAuth.Token + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.Status : " + checkoutFormAuth.Status + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.PaymentStatus  : " + checkoutFormAuth.PaymentStatus + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.PaidPrice  : " + checkoutFormAuth.PaidPrice + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.ErrorMessage  : " + checkoutFormAuth.ErrorMessage + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.BasketId  : " + checkoutFormAuth.BasketId + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.BinNumber  : " + checkoutFormAuth.BinNumber + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.CardAssociation  : " + checkoutFormAuth.CardAssociation + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.CardFamily  : " + checkoutFormAuth.CardFamily + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.CardToken  : " + checkoutFormAuth.CardToken + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.CardUserKey  : " + checkoutFormAuth.CardUserKey + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.ConversationId  : " + checkoutFormAuth.ConversationId + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.ErrorCode  : " + checkoutFormAuth.ErrorCode + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.PaymentId  : " + checkoutFormAuth.PaymentId + "</p>"
				spnmesaj.InnerHtml += "<p>checkoutFormAuth.SystemTime  : " + checkoutFormAuth.SystemTime.ToString + "</p>"


				Saydanclass.MailGonder("hakansencan7@gmail.com;murat@lojistikprogrami.net", "* * * Mersin Borsası HATA * * * ", spnmesaj.InnerHtml)

		End Select

	End Sub


	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		'Saydanclass.LoginKontroluYap()
	End Sub

End Class
