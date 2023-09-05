Imports System.Data
Partial Class register
    Inherits System.Web.UI.Page
	Dim SaydanClass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim GeriDonusSayfası As String
	Dim kod As Integer

	Protected Sub chkSozlesmeOnay_CheckedChanged(sender As CheckBox, e As EventArgs) Handles chkSozlesmeOnay.CheckedChanged
		If sender.Checked Then
			btnRegister.Enabled = True
			btnRegister.CssClass = "btn btn-large btn-success"
			btnRegister.ToolTip = ""
			btnRegister.Focus()
		Else
			btnRegister.Enabled = False
			btnRegister.CssClass = "btn btn-large btn-default"
			btnRegister.ToolTip = "Önce Sözleşmeyi Onaylayınız"
			chkSozlesmeOnay.Focus()
		End If
	End Sub

	Protected Sub chkFaturaAdresiAyni_CheckedChanged(sender As Object, e As EventArgs) Handles chkFaturaAdresiAyni.CheckedChanged
		txtFaturaAdresi.Text = txtTeslimatAdresi.Text
		chkFaturaAdresiAyni.Checked = False
		txtSirket.Focus()
	End Sub

	Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
		'Zorunlu alanların girişleri doğru yapılmış mı, kontrol et
		KontrolOK = True
		lblUyari.Text = ""
		AlanKontrolu(txtemail, "E-mail")
		AlanKontrolu(txtIsim, "İsim")
		AlanKontrolu(txtSoyisim, "Soyisim")
		AlanKontrolu(txtPassword, "Şifre")
		AlanKontrolu(txtTCKimlikNo, "TC Kimlik No")
		AlanKontrolu(txtphone, "Telefon")
		AlanKontrolu(txtTeslimatAdresi, "Teslimat Adresi")
		AlanKontrolu(txtTeslimatSehri, "Teslimat Şehri")
		AlanKontrolu(txtFaturaAdresi, "Fatura Adresi")
		'Üyelik Sözleşmesini imzaladığını kontrol et
		AlanKontrolu(chkSozlesmeOnay)
		'Şifre bilgisinin doğruluğunu kontrol et
		If txtPassword.Text <> txtPassword2.Text Then lblUyari.Text += "<br />Şifreyi yanlış girdiniz. Lütfen kontrol ediniz." : KontrolOK = False
		'Bu e-mail adresi ve TC kimlik No başka bir üye için kullanılmış mı, kontrol et
		If SaydanClass._ConnDB("select * from MUSTERILER where Email='" + txtemail.Text + "'").Rows.Count > 0 Then lblUyari.Text = "<br />Bu e-mail adresi kullanılmaktadır. Farklı bir e-mail adresi girebilirsiniz." : KontrolOK = False
		If SaydanClass._ConnDB("select * from MUSTERILER where TCKimlikNo='" + txtTCKimlikNo.Text + "'").Rows.Count > 0 Then lblUyari.Text = "<br />Daha önce sitemize kayıt olduğunuz anlaşılıyor. Aksi bir durumda bizimle irtibata geçebilirsiniz." : KontrolOK = False

		If Not KontrolOK Then HataMesajiniGonder() : Exit Sub

		Dim cmdstr As String = "select top 1 * from MUSTERILER"
		Dim dtMusteriler As DataTable = SaydanClass._ConnDB(cmdstr)
		Dim drMusteriler As DataRow = dtMusteriler.NewRow
		drMusteriler("Isim") = txtIsim.Text
		drMusteriler("Soyisim") = txtSoyisim.Text
		drMusteriler("Email") = txtemail.Text
		drMusteriler("Telefon") = txtphone.Text
		drMusteriler("Company") = txtSirket.Text
		drMusteriler("TeslimatAdresi") = txtTeslimatAdresi.Text
		drMusteriler("TeslimatSehri") = txtTeslimatSehri.Text
		drMusteriler("Password") = txtPassword.Text
		drMusteriler("FaturaAdresi") = txtFaturaAdresi.Text
		drMusteriler("VergiDairesi") = txtVergiDairesi.Text
		drMusteriler("VergiNo") = txtVergiNo.Text
		drMusteriler("TCKimlikNo") = txtTCKimlikNo.Text
		dtMusteriler.Rows.Add(drMusteriler)

		Dim DBKayit As String = SaydanClass._SaveDB(cmdstr, dtMusteriler)
		If DBKayit <> "OK" Then
			lblUyari.Text = "Kayıt Sırasında Hata : " + DBKayit
		Else
			drMusteriler = SaydanClass._ConnDB("select * from MUSTERILER where TCKimlikNo='" + txtTCKimlikNo.Text + "'").Rows(0)
			Session("UserID") = drMusteriler("MusteriKodu").ToString
			Session("UserName") = drMusteriler("Isim").ToString + " " + drMusteriler("Soyisim").ToString
			Session("YoneticiMi") = False

			'Üye olmadan önce sepete yüklenen birşeyler varsa onların müşteri kodlarını güncelle ki müşterimizi uğraştırmayalım.
			cmdstr = "select * from SEPET where SessionID='" + Session("SessionID").ToString.ToString + "'"
			Dim dtSepet As DataTable = SaydanClass._ConnDB(cmdstr)
			For Each dr In dtSepet.Rows
				dr("MusteriKodu") = Session("UserID").ToString
			Next
			SaydanClass._SaveDB(cmdstr, dtSepet)
			Response.Redirect(GeriDonusSayfası)
		End If
	End Sub

	Protected Sub AlanKontrolu(ByVal cnt As TextBox, ByVal AlanAdi As String)
		If cnt.Text = "" Then
			lblUyari.Text += "<br />" + AlanAdi + " alanını doldurmanız gerekmektedir"
			KontrolOK = False
		End If
	End Sub

	Protected Sub AlanKontrolu(ByVal cnt As CheckBox)
		If Not cnt.Checked Then
			lblUyari.Text += "<br />Üyelik Sözleşmesini imzalamanız gerekmektedir"
			KontrolOK = False
		End If
	End Sub

	Protected Sub HataMesajiniGonder()
		divUyari.Visible = True
		divUyari.Style("display") = "block"
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
End Class
