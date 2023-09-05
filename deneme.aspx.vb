
Partial Class deneme
    Inherits System.Web.UI.Page

	Dim saydanclass As New SaydanClass
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim a = saydanclass.MailGonder("Murat@saydan.net;hakansencan7@gmail.com", "Mersin Borsası - Yeni Sipariş", "Siparişler Listesini Kontrol ediniz.")

	End Sub
End Class
