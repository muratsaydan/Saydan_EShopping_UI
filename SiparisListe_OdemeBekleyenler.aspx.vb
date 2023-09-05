Imports System.Data
Partial Class SiparisListe_OdemeBekleyenler
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim SiparisKodu As Integer = 0

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		StokHareketleri()
	End Sub

	Protected Sub StokHareketleri()
		Dim cmdstr As String = "SELECT [SiparisKodu],[MusteriKodu],OdemeYapildiMi,[SiparisVerilmeTarihi],[KargoyaTeslimTarihi],[MusteriyeTeslimTarihi],[TeslimatSehri] FROM [SIPARISLER] where (OdemeYapildiMi is null or OdemeYapildiMi=0 ) and MusteriKodu=" + Session("UserID").ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim whrcnd As String = ""
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sipariş No" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sipariş Tarihi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürünler" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Tutar" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 7
			hc.InnerHtml = "Ödemesi bekleyen bir siparişiniz bulunmuyor."
			hr.Cells.Add(hc)
			tbl1.Rows.Add(hr)
			Exit Sub
		End If

		For Each dr As DataRow In dt.Rows
			whrcnd += "," + dr("SiparisKodu").ToString
		Next
		Dim dvSHAR As DataView = Saydanclass._ConnDB("SELECT STOK_HAREKET.*, URUNLER.StokAdi1 FROM STOK_HAREKET INNER JOIN URUNLER ON STOK_HAREKET.UrunKodu = URUNLER.StokKodu where STOK_HAREKET.SiparisKodu in (" + whrcnd.Substring(1) + ")").DefaultView

		For Each dr As DataRow In dt.Rows
			SiraNo += 1
			hr = New HtmlTableRow
			hc = New HtmlTableCell : hc.InnerHtml = SiraNo.ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = CDate(dr("SiparisVerilmeTarihi")).ToString("dd.MM.yyyy") : hr.Cells.Add(hc)
			hc = New HtmlTableCell
			dvSHAR.RowFilter = "SiparisKodu=" + dr("SiparisKodu").ToString
			Dim cmdUrunler As String = ""
			Dim tutar As Decimal = 0
			For Each drshar As DataRowView In dvSHAR
				cmdUrunler += "<br />" + drshar("StokAdi1").ToString
				tutar += drshar("Tutar")
			Next
			If cmdUrunler <> "" Then hc.InnerHtml = cmdUrunler.Substring(6)
			hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = tutar.ToString("n2") + " TL" : hr.Cells.Add(hc)

			hc = New HtmlTableCell
			hc.ColSpan = 3
			hc.InnerHtml = "<a href=""OdemeSayfasi.aspx?kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Ödemeyi Yap</a>"
			hr.Cells.Add(hc)

			AraToplam += tutar
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
		hc = New HtmlTableCell
		'hc.ColSpan = 3
		hc.InnerHtml = ""
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)

	End Sub


	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		'If Session("UserID") Is Nothing Then Response.Redirect("login.aspx?x=siplis")
	End Sub


End Class
