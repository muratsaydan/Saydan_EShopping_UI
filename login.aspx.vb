Imports System.Data

Partial Class login
	Inherits System.Web.UI.Page
	Dim saydanclass As New SaydanClass
	Dim GeriDonusSayfası As String = ""
	Dim kod As Integer = 0
	Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
		'GeriDonusSayfası = Request.QueryString("x")
		'kod = Request.QueryString("kod")
		'Select Case GeriDonusSayfası
		'	Case "sipdet"
		'		GeriDonusSayfası = "SiparisDetay.aspx"
		'	Case "sipdet2"
		'		GeriDonusSayfası = "SiparisDetay2.aspx?kod=" + kod.ToString
		'	Case "siplis"
		'		GeriDonusSayfası = "SiparisListe.aspx"
		'	Case "odemesayfasi"
		'		GeriDonusSayfası = "OdemeSayfasi?kod=" + kod.ToString
		'	Case Else
		'		GeriDonusSayfası = "SiparisDetay.aspx"
		'End Select

		GeriDonusSayfası = Session("LoginOncesiSayfa")
		If Not Session("UserID") Is Nothing Then Response.Redirect(GeriDonusSayfası)
	End Sub

	Protected Sub btnUyeGiris_Click(sender As Object, e As EventArgs) Handles btnUyeGiris.Click
		Dim cmdstr As String = "select * from [dbo].[MUSTERILER] where Email='" + inputEmail1.Text + "' and Password='" + inputPassword1.Text + "'"
		Dim dt As DataTable = SaydanClass._ConnDB(cmdstr)
		If dt.Rows.Count > 0 Then
			Dim dr As DataRow = dt.Rows(0)
			Session("UserID") = dr("MusteriKodu").ToString
			Session("UserName") = dr("Isim").ToString + " " + dr("Soyisim").ToString
			If IsDBNull(dr("YoneticiMi")) Then dr("YoneticiMi") = False
			Session("YoneticiMi") = dr("YoneticiMi")
			'Üye olmadan önce sepete yüklenen birşeyler varsa onların müşteri kodlarını güncelle ki müşterimizi uğraştırmayalım.
			cmdstr = "select * from SEPET where SessionID='" + Session("SessionID").ToString.ToString + "'"
			dt = SaydanClass._ConnDB(cmdstr)
			For Each dr In dt.Rows
				dr("MusteriKodu") = Session("UserID").ToString
			Next
			saydanclass._SaveDB(cmdstr, dt)
			Response.Redirect(GeriDonusSayfası)
		Else
		End If
	End Sub

	Protected Sub btnregister_Click(sender As Object, e As EventArgs) Handles btnregister.Click
		Response.Redirect("register.aspx")
	End Sub
End Class
