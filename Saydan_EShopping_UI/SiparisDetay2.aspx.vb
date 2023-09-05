Imports System.Data
Partial Class SiparisDetay2
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim SiparisKodu As Integer = 0

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		SiparisKodu = Request.QueryString("kod")

		If Not IsPostBack Then
			SiparisBilgileriniDoldur()
		End If
		StokHareketleri()
	End Sub

	Protected Sub SiparisBilgileriniDoldur()
		Dim cmdstr As String = "select * from SIPARISLER where SiparisKodu=" + SiparisKodu.ToString
		Dim drSiparis As DataRow = Saydanclass._ConnDB(cmdstr).Rows(0)
		If IsDBNull(drSiparis("OdemeYapildiMi")) Then drSiparis("OdemeYapildiMi") = False
		If Not drSiparis("OdemeYapildiMi") Then Response.Redirect("SiparisDetay.aspx?kod=" + SiparisKodu.ToString)

		txtTeslimatAdresi.Text = drSiparis("TeslimatAdresi").ToString
		txtTeslimatSehri.Text = drSiparis("TeslimatSehri").ToString
		txtFaturaAdresi.Text = drSiparis("FaturaAdresi").ToString
		txtSirket.Text = drSiparis("FirmaUnvani").ToString
		txtVergiDairesi.Text = drSiparis("VergiDairesi").ToString
		txtVergiNo.Text = drSiparis("VergiNo").ToString
		txtEkBilgiler.Text = drSiparis("VergiNo").ToString

		If Not IsPostBack Then
			txtTedarikciSiparisKodu.Text = ""
			txtKargoFirmasi.Text = drSiparis("KargoFirmasi").ToString
			txtKargoNo.Text = drSiparis("KargoNo").ToString
		End If


	End Sub

	Protected Sub StokHareketleri()
		Dim whrcnd As String = ""
		whrcnd = " MusteriKodu=" + Session("UserID").ToString
		Dim cmdstr As String = "SELECT        STOK_HAREKET.SUrunKodu,STOK_HAREKET.SiparisKodu, STOK_HAREKET.UrunKodu, STOK_HAREKET.Miktar, URUNLER.StokAdi1, STOK_HAREKET.Tutar, URUNLER.Pic1 FROM            URUNLER INNER JOIN STOK_HAREKET ON URUNLER.StokKodu = STOK_HAREKET.UrunKodu where STOK_HAREKET.SiparisKodu=" + SiparisKodu.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Adet" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
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
			hc = New HtmlTableCell

			Dim lnk As New LinkButton
			lnk.ToolTip = "İade"
			AddHandler lnk.Click, AddressOf btniade_click
			lnk.Attributes.Add("class", "btn btn-warning")
			lnk.ID = "btniade_" + dr("SUrunKodu").ToString
			lnk.Text = "<i class=""icon-undo icon-white""></i>"
			hc.Controls.Add(lnk)

			lnk = New LinkButton
			lnk.ToolTip = "iptal"
			lnk.Attributes.Add("class", "btn btn-danger")
			AddHandler lnk.Click, AddressOf btniptal_click
			lnk.ID = "btniptal_" + dr("SUrunKodu").ToString
			lnk.Text = "<i class=""icon-remove icon-white""></i>"
			hc.Controls.Add(lnk)

			hr.Cells.Add(hc)

			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = CDec(dr("Tutar")).ToString("n2") + " TL" : hr.Cells.Add(hc)
			Dim Toplam As Decimal = dr("Miktar") * dr("Tutar")
			AraToplam += Toplam
			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = Toplam.ToString("n2") + " TL" : hr.Cells.Add(hc)
			tbl1.Rows.Add(hr)
		Next
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 5
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
		hc.ColSpan = 5
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
		hc.ColSpan = 5
		hc.InnerHtml = "Kargo Ücreti"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = "6 TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 5
		hc.InnerHtml = "Toplam"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = (AraToplam + 6).ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
	End Sub

	Protected Sub btniptal_click(sender As Object, e As EventArgs)

	End Sub

	Protected Sub btniade_click(sender As Object, e As EventArgs)

	End Sub

	Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
		Saydanclass.LoginKontroluYap()
		'If Session("UserID") Is Nothing Then Response.Redirect("login.aspx?x=sipdet2&kod=" + SiparisKodu.ToString)
	End Sub


End Class
