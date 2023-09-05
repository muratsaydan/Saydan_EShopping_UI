Imports System.Data
Partial Class register
    Inherits System.Web.UI.Page
	Dim SaydanClass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim GeriDonusSayfası As String
	Dim kod As Integer

	Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
		SaydanClass.LoginKontroluYap()
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		GeriDonusSayfası = Request.QueryString("x")
		kod = Request.QueryString("kod")
		Select Case GeriDonusSayfası
			Case "sipdet"
				GeriDonusSayfası = "SiparisDetay.aspx"
			Case "sipdet2"
				GeriDonusSayfası = "SiparisDetay2.aspx?kod=" + kod.ToString
			Case "siplis"
				GeriDonusSayfası = "SiparisListe.aspx"
			Case "odemesayfasi"
				GeriDonusSayfası = "OdemeSayfasi?kod=" + kod.ToString
			Case Else
				GeriDonusSayfası = "SiparisDetay.aspx"
		End Select
		If Not IsPostBack Then
			VarOlanKaydinBilgileriniDoldur()
		End If
	End Sub
	Protected Sub VarOlanKaydinBilgileriniDoldur()
		Dim cmdstr As String = "select top 1 * from MUSTERILER where MusteriKodu=" + Session("UserID").ToString
		Dim dtMusteriler As DataTable = SaydanClass._ConnDB(cmdstr)
		Dim drMusteriler As DataRow = dtMusteriler.Rows(0)
		txtIsim.Text = drMusteriler("Isim").ToString
		txtSoyisim.Text = drMusteriler("Soyisim").ToString
		txtemail.Text = drMusteriler("Email").ToString
		txtphone.Text = drMusteriler("Telefon").ToString
		txtSirket.Text = drMusteriler("Company").ToString
		txtTeslimatAdresi.Text = drMusteriler("TeslimatAdresi").ToString
		txtTeslimatSehri.Text = drMusteriler("TeslimatSehri").ToString
		txtFaturaAdresi.Text = drMusteriler("FaturaAdresi").ToString
		txtVergiDairesi.Text = drMusteriler("VergiDairesi").ToString
		txtVergiNo.Text = drMusteriler("VergiNo").ToString
		txtTCKimlikNo.Text = drMusteriler("TCKimlikNo").ToString


	End Sub

	Protected Sub chkFaturaAdresiAyni_CheckedChanged(sender As Object, e As EventArgs) Handles chkFaturaAdresiAyni.CheckedChanged
		txtFaturaAdresi.Text = txtTeslimatAdresi.Text
		chkFaturaAdresiAyni.Checked = False
	End Sub

	Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
		'Zorunlu alanların girişleri doğru yapılmış mı, kontrol et
		KontrolOK = True
		lblUyari.Text = ""
		AlanKontrolu(txtemail, "E-mail")
		AlanKontrolu(txtIsim, "İsim")
		AlanKontrolu(txtSoyisim, "Soyisim")
		If txtPassword.Text <> "" Then AlanKontrolu(txtPassword, "Şifre")
		AlanKontrolu(txtTCKimlikNo, "TC Kimlik No")
		AlanKontrolu(txtphone, "Telefon")
		AlanKontrolu(txtTeslimatAdresi, "Teslimat Adresi")
		AlanKontrolu(txtTeslimatSehri, "Teslimat Şehri")
		AlanKontrolu(txtFaturaAdresi, "Fatura Adresi")
		'Üyelik Sözleşmesini imzaladığını kontrol et
		'Şifre bilgisinin doğruluğunu kontrol et
		If txtPassword.Text <> "" And txtPassword.Text <> txtPassword2.Text Then lblUyari.Text += "<br />Şifreyi yanlış girdiniz. Lütfen kontrol ediniz." : KontrolOK = False
		'Bu e-mail adresi ve TC kimlik No başka bir üye için kullanılmış mı, kontrol et
		If SaydanClass._ConnDB("select * from MUSTERILER where MusteriKodu <>" + Session("UserID").ToString + " and Email='" + txtemail.Text + "'").Rows.Count > 0 Then lblUyari.Text = "<br />Bu e-mail adresi kullanılmaktadır. Farklı bir e-mail adresi girebilirsiniz." : KontrolOK = False
		'If SaydanClass._ConnDB("select * from MUSTERILER where TCKimlikNo='" + txtTCKimlikNo.Text + "'").Rows.Count > 0 Then lblUyari.Text = "<br />Daha önce sitemize kayıt olduğunuz anlaşılıyor. Aksi bir durumda bizimle irtibata geçebilirsiniz." : KontrolOK = False

		If Not KontrolOK Then HataMesajiniGonder() : Exit Sub

		Dim cmdstr As String = "select top 1 * from MUSTERILER where MusteriKodu=" + Session("UserID").ToString
		Dim dtMusteriler As DataTable = SaydanClass._ConnDB(cmdstr)
		Dim drMusteriler As DataRow = dtMusteriler.Rows(0)
		drMusteriler("Isim") = txtIsim.Text
		drMusteriler("Soyisim") = txtSoyisim.Text
		drMusteriler("Email") = txtemail.Text
		drMusteriler("Telefon") = txtphone.Text
		drMusteriler("Company") = txtSirket.Text
		drMusteriler("TeslimatAdresi") = txtTeslimatAdresi.Text
		drMusteriler("TeslimatSehri") = txtTeslimatSehri.Text
		If txtPassword.Text <> "" Then drMusteriler("Password") = txtPassword.Text
		drMusteriler("FaturaAdresi") = txtFaturaAdresi.Text
		drMusteriler("VergiDairesi") = txtVergiDairesi.Text
		drMusteriler("VergiNo") = txtVergiNo.Text
		drMusteriler("TCKimlikNo") = txtTCKimlikNo.Text

		Dim DBKayit As String = SaydanClass._SaveDB(cmdstr, dtMusteriler)
		If DBKayit <> "OK" Then
			lblUyari.Text = "Kayıt Sırasında Hata : " + DBKayit
		Else
			lblUyari.Text = "Üyelik bilgileriniz başarıyla değiştirilmiştir"
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

End Class
