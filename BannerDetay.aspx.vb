Imports System.Data
Partial Class BannerDetay
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim Kod As Integer = 0
	Dim UrunKodu As Integer = 0

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Kod = Request.QueryString("kod")
		UrunKodu = Request.QueryString("uk")

		'Kod = 28

		If Not IsPostBack Then
			If Kod > 0 Then
				VarOlanKaydinBilgisiniDoldur(Kod)
			ElseIf UrunKodu > 0 Then
				UrunBilgilerineGoreBanneriOlustur(UrunKodu)
			End If
		End If
	End Sub

	Protected Sub UrunBilgilerineGoreBanneriOlustur(ByVal UrunKodu As Integer)
		Dim cmdstr As String = "select * from URUNLER where StokKodu=" + UrunKodu.ToString()
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow = dt.Rows(0)

		lblBannerKodu.Text = ""
		txtUrunAdi.Text = dr("StokAdi1").ToString
		lblDetay2.Text = dr("DetayAciklama1").ToString
		txtSiralama.Text = ""
		txtBannerAciklamasi.Text = dr("KisaAciklama1").ToString
		txtLink.Text = "./product_details.aspx?kod=" + UrunKodu.ToString()
		txtResimURL.Text = dr("Pic1").ToString
		txtAlt.Text = dr("StokAdi1").ToString

	End Sub
	Protected Sub VarOlanKaydinBilgisiniDoldur(ByVal Ref As Integer)
		Dim cmdstr As String = "SELECT * FROM BANNER where BannerKodu=" + Ref.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow = dt.Rows(0)
		lblBannerKodu.Text = dr("BannerKodu").ToString
		btnSil.Visible = True
		txtUrunAdi.Text = dr("UrunAdi").ToString
		lblDetay2.Text = ""
		txtSiralama.Text = dr("Siralama").ToString
		txtBannerAciklamasi.Text = dr("BannerAciklamasi").ToString
		txtLink.Text = dr("Link").ToString
		txtResimURL.Text = dr("ResimURL").ToString
		txtAlt.Text = dr("ResimAciklamasi").ToString


	End Sub

	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		If Not Saydanclass.YoneticiMi Then Response.Redirect("index.aspx")
	End Sub

	Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click

		Dim kod As String = "-1"
		If lblBannerKodu.Text <> "" Then kod = lblBannerKodu.Text
		Dim cmdstr As String = "SELECT * FROM BANNER where BannerKodu=" + kod

		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow
		If lblBannerKodu.Text = "" Then
			dr = dt.NewRow
		Else
			dr = dt.Rows(0)
		End If
		dr("UrunAdi") = txtUrunAdi.Text
		If txtSiralama.Text = "" Then txtSiralama.Text = 1
		dr("Siralama") = txtSiralama.Text
		dr("BannerAciklamasi") = txtBannerAciklamasi.Text
		dr("Link") = txtLink.Text
		dr("ResimURL") = txtResimURL.Text
		dr("ResimAciklamasi") = txtAlt.Text
		If lblBannerKodu.Text = "" Then dt.Rows.Add(dr)
		Saydanclass._SaveDB(cmdstr, dt)

		Response.Redirect("BannerListe.aspx")
	End Sub



	'Protected Sub UrunlerListesi()
	'	Dim hr As HtmlTableRow
	'	Dim hc As HtmlTableCell

	'	Dim cmdstr As String = "SELECT KATEGORILER_URUNLER.KategoriKodu, KATEGORILER_URUNLER.SiraNo, URUNLER.StokKodu, URUNLER.StokAdi1, URUNLER.KisaAciklama1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.AlisFiyati, URUNLER.Miktar, URUNLER.TedarikciFirma FROM KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu where KATEGORILER_URUNLER.KategoriKodu=" + Kod.ToString
	'	Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
	'	If dt.Rows.Count = 0 Then
	'		hr = New HtmlTableRow
	'		hc = New HtmlTableCell
	'		hc.ColSpan = 8
	'		hc.InnerHtml = "Bu kategoriye ait ürün bulunmuyor."
	'		hr.Cells.Add(hc)
	'		tblUrunler.Rows.Add(hr)
	'		Exit Sub
	'	End If

	'	Dim SiraNo As Integer = 0
	'	hr = New HtmlTableRow
	'	hc = New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Seç" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Resim" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Miktar" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "<nobr>Alış Fiyatı<nobr><br><nobr>Satış Fiyatı<nobr>" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
	'	tblUrunler.Rows.Add(hr)
	'	'lblAdet.Text = dt.Rows.Count.ToString

	'	Dim Urunler As String = ""
	'	Dim SatisFiyati As Decimal = 0
	'	Dim AlisFiyati As Decimal = 0
	'	For SiraNo = 0 To dt.Rows.Count - 1
	'		Dim dr As DataRow = dt.Rows(SiraNo)

	'		hr = New HtmlTableRow
	'		hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = "" : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = "<img src=""" + dr("Pic1").ToString + """ width=""75"" />" : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = dr("StokAdi1").ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = dr("Miktar").ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = CDec(dr("AlisFiyati")).ToString("n2") + "<br>" + CDec(dr("SatisFiyati")).ToString("n2") : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)


	'		hc = New HtmlTableCell : hc.InnerHtml = "<a href=""UrunlerDetay.aspx?kod=" + dr("StokKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
	'		tblUrunler.Rows.Add(hr)
	'	Next


	'End Sub

	Protected Sub btnSil_Click(sender As Object, e As EventArgs) Handles btnSil.Click
		If lblBannerKodu.Text = "" Then Return

		Kod = lblBannerKodu.Text

		Dim cmdSQL As String = "Delete from BANNER where BannerKodu=" + Kod.ToString()
		Saydanclass.ExecSQL(cmdSQL)
		Response.Redirect("BannerListe.aspx")

	End Sub
End Class
