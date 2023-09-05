Imports System.Data
Partial Class SiparisDetay
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim SiparisKodu As Integer = 0
	Dim KargoUcreti As Decimal = 6

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		SiparisKodu = Request.QueryString("kod")
		lblSozlesmeOnay.NavigateUrl = "sac.aspx?kod=" + SiparisKodu.ToString
		If Not IsPostBack Then
			If SiparisKodu = 0 Then
				MusteriBilgileriniDoldur()
			Else
				SiparisBilgileriniDoldur()
			End If
		End If
		If SiparisKodu = 0 Then
			SepettekiUrunler()
		Else
			StokHareketleri()
		End If
	End Sub

	Protected Sub MusteriBilgileriniDoldur()
		Dim cmdstr As String = "select * from MUSTERILER where MusteriKodu=" + Session("UserID").ToString
		Dim drMusteri As DataRow = Saydanclass._ConnDB(cmdstr).Rows(0)
		txtTeslimatAdresi.Text = drMusteri("TeslimatAdresi").ToString
		txtTeslimatSehri.Text = drMusteri("TeslimatSehri").ToString
		txtFaturaAdresi.Text = drMusteri("FaturaAdresi").ToString
		txtSirket.Text = drMusteri("Company").ToString
		txtVergiDairesi.Text = drMusteri("VergiDairesi").ToString
		txtVergiNo.Text = drMusteri("VergiNo").ToString
	End Sub

	Protected Sub SiparisBilgileriniDoldur()
		Dim cmdstr As String = "select * from SIPARISLER where SiparisKodu=" + SiparisKodu.ToString
		Dim drSiparis As DataRow = Saydanclass._ConnDB(cmdstr).Rows(0)
		If IsDBNull(drSiparis("OdemeYapildiMi")) Then drSiparis("OdemeYapildiMi") = False
		If drSiparis("OdemeYapildiMi") Then Response.Redirect("SiparisDetay2.aspx?kod=" + SiparisKodu.ToString)

		txtTeslimatAdresi.Text = drSiparis("TeslimatAdresi").ToString
		txtTeslimatSehri.Text = drSiparis("TeslimatSehri").ToString
		txtFaturaAdresi.Text = drSiparis("FaturaAdresi").ToString
		txtSirket.Text = drSiparis("FirmaUnvani").ToString
		txtVergiDairesi.Text = drSiparis("VergiDairesi").ToString
		txtVergiNo.Text = drSiparis("VergiNo").ToString
		txtEkBilgiler.Text = drSiparis("VergiNo").ToString
	End Sub

	Protected Sub SepettekiUrunler()
		Dim whrcnd As String = ""
		whrcnd = " MusteriKodu=" + Session("UserID").ToString
		Dim cmdstr As String = "SELECT SEPET.SepetKodu, SEPET.MusteriKodu, SEPET.UrunKodu, SEPET.SepeteEklenmeTarihi, SUM(SEPET.Miktar) AS Miktar, SEPET.SessionID, URUNLER.StokAdi1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.KDVOrani, URUNLER.Indirim FROM SEPET INNER JOIN URUNLER ON SEPET.UrunKodu = URUNLER.StokKodu  where " + whrcnd + " GROUP BY SEPET.SepetKodu, SEPET.MusteriKodu, SEPET.UrunKodu, SEPET.SepeteEklenmeTarihi, SEPET.SessionID, URUNLER.StokAdi1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.KDVOrani, URUNLER.Indirim"
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Adet" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Birim Fiyat" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Toplam" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 5
			hc.InnerHtml = "Sepetinizde ürün bulunmuyor."
			hr.Cells.Add(hc)
			tbl1.Rows.Add(hr)
			Exit Sub
		End If
		For Each dr As DataRow In dt.Rows
			SiraNo += 1
			hr = New HtmlTableRow
			hc = New HtmlTableCell : hc.InnerHtml = SiraNo.ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("StokAdi1").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("Miktar").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = CDec(dr("SatisFiyati")).ToString("n2") + " TL" : hr.Cells.Add(hc)
			Dim Toplam As Decimal = dr("Miktar") * dr("SatisFiyati")
			AraToplam += Toplam
			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = Toplam.ToString("n2") + " TL" : hr.Cells.Add(hc)
			tbl1.Rows.Add(hr)
		Next
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Ara Toplam"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = AraToplam.ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "KDV (%18)"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = "Dahil"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Kargo Ücreti"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = KargoUcreti.ToString + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Toplam"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = (AraToplam + KargoUcreti).ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)


	End Sub

	Protected Sub StokHareketleri()
		Dim whrcnd As String = ""
		whrcnd = " MusteriKodu=" + Session("UserID").ToString
		Dim cmdstr As String = "SELECT        STOK_HAREKET.SiparisKodu, STOK_HAREKET.UrunKodu, STOK_HAREKET.Miktar, URUNLER.StokAdi1, STOK_HAREKET.Tutar, URUNLER.Pic1 FROM            URUNLER INNER JOIN STOK_HAREKET ON URUNLER.StokKodu = STOK_HAREKET.UrunKodu where STOK_HAREKET.SiparisKodu=" + SiparisKodu.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Adet" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Birim Fiyat" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Toplam" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 5
			hc.InnerHtml = "Sepetinizde ürün bulunmuyor."
			hr.Cells.Add(hc)
			tbl1.Rows.Add(hr)
			Exit Sub
		End If
		For Each dr As DataRow In dt.Rows
			SiraNo += 1
			hr = New HtmlTableRow
			hc = New HtmlTableCell : hc.InnerHtml = SiraNo.ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("StokAdi1").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("Miktar").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = CDec(dr("Tutar")).ToString("n2") + " TL" : hr.Cells.Add(hc)
			Dim Toplam As Decimal = dr("Miktar") * dr("Tutar")
			AraToplam += Toplam
			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = Toplam.ToString("n2") + " TL" : hr.Cells.Add(hc)
			tbl1.Rows.Add(hr)
		Next
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Ara Toplam"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = AraToplam.ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "KDV (%18)"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = "Dahil"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Kargo Ücreti"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = KargoUcreti.ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Toplam"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = (AraToplam + KargoUcreti).ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)


	End Sub

	Protected Sub AlanKontrolu(ByVal cnt As TextBox, ByVal AlanAdi As String)
		If cnt.Text = "" Then
			lblUyari.Text += "<br />" + AlanAdi + " alanını doldurmanız gerekmektedir"
			KontrolOK = False
		End If
	End Sub

	Protected Sub AlanKontrolu(ByVal cnt As CheckBox)
		If cnt.Checked Then
			lblUyari.Text += "<br />Üyelik Sözleşmesini imzalamanız gerekmektedir"
			KontrolOK = False
		End If
	End Sub

	Protected Sub HataMesajiniGonder()
		divUyari.Visible = True
		divUyari.Style("display") = "block"
	End Sub

	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		'If Session("UserID") Is Nothing Then Response.Redirect("login.aspx?x=sipdet")
	End Sub

	Protected Sub btnRegister2_Click(sender As Object, e As EventArgs) Handles btnRegister2.Click
		lblSozlesmeOnay.Text = ""

		AlanKontrolu(txtTeslimatAdresi, "Teslimat Adresi")
		AlanKontrolu(txtTeslimatSehri, "Teslimat Şehri")
		AlanKontrolu(txtFaturaAdresi, "Fatura Adresi")
		If Not KontrolOK Then HataMesajiniGonder() : Exit Sub

		Dim cmdstr As String = "select top 1 * from SIPARISLER where SiparisKodu=" + SiparisKodu.ToString
		Dim dtSiparisler As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim drSiparis As DataRow
		If SiparisKodu = 0 Then
			drSiparis = dtSiparisler.NewRow
		Else
			drSiparis = dtSiparisler.Rows(0)
		End If

		drSiparis("MusteriKodu") = Session("UserID")
		Dim KayitTarihi As Date = Now
		drSiparis("SiparisVerilmeTarihi") = KayitTarihi
		drSiparis("TeslimatAdresi") = txtTeslimatAdresi.Text
		drSiparis("FaturaAdresi") = txtFaturaAdresi.Text
		drSiparis("TeslimatSehri") = txtTeslimatSehri.Text
		drSiparis("MusterininUyariNotu") = txtEkBilgiler.Text
		drSiparis("FirmaUnvani") = txtSirket.Text
		drSiparis("VergiDairesi") = txtVergiDairesi.Text
		drSiparis("VergiNo") = txtVergiNo.Text
		drSiparis("KargoUcreti") = KargoUcreti
		If SiparisKodu = 0 Then dtSiparisler.Rows.Add(drSiparis)
		Dim dbkayit As String = Saydanclass._SaveDB(cmdstr, dtSiparisler)
		If dbkayit <> "OK" Then
			lblUyari.Text = "Kayıt Sırasında Hata : " + dbkayit
			HataMesajiniGonder()
		Else
			If SiparisKodu = 0 Then
				drSiparis = Saydanclass._ConnDB("select top 1 * from SIPARISLER where MusteriKodu=" + Session("UserID").ToString + " order by SiparisKodu DESC").Rows(0)
				SiparisKodu = drSiparis("SiparisKodu")
				'Üye olmadan önce sepete yüklenen birşeyler varsa onların müşteri kodlarını güncelle ki müşterimizi uğraştırmayalım.
				cmdstr = "select * from SEPET where MusteriKodu=" + Session("UserID").ToString
				Dim dtSepet As DataTable = Saydanclass._ConnDB(cmdstr)

				Dim cmdSHAR As String = "select top 1 * from STOK_HAREKET"
				Dim dtSHAR As DataTable = Saydanclass._ConnDB(cmdSHAR)

				For Each drSepet As DataRow In dtSepet.Rows
					Dim drSHAR As DataRow = dtSHAR.NewRow
					drSHAR("SiparisKodu") = SiparisKodu
					drSHAR("UrunKodu") = drSepet("UrunKodu")
					drSHAR("Tutar") = Saydanclass._ConnDB("select SatisFiyati from URUNLER where StokKodu=" + drSepet("UrunKodu").ToString).Rows(0)(0)
					drSHAR("TutarPB") = 1
					drSHAR("Miktar") = drSepet("Miktar")
					dtSHAR.Rows.Add(drSHAR)
					drSepet.Delete()
				Next
				Saydanclass._SaveDB(cmdSHAR, dtSHAR)
				Saydanclass._SaveDB(cmdstr, dtSepet)
			End If
			Response.Redirect("OdemeSayfasi.aspx?kod=" + SiparisKodu.ToString)
		End If


	End Sub

	Protected Sub chkSozlesmeOnay_CheckedChanged(sender As Object, e As EventArgs) Handles chkSozlesmeOnay.CheckedChanged
		If chkSozlesmeOnay.Checked Then
			btnRegister2.ForeColor = Drawing.Color.Black
		Else
			btnRegister2.ForeColor = Drawing.Color.LightGray
		End If
		btnRegister2.Focus()
	End Sub
End Class
