Imports System.Data
Partial Class sac
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim SiparisKodu As Integer
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		SiparisKodu = Request.QueryString("kod")
		'SiparisKodu = 9
		'Session("UserID") = "2"

		StokHareketleri()
		StokHareketleri2()

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
		hc.InnerHtml = "6 TL"
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
		hc.InnerHtml = (AraToplam + 6).ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)


	End Sub
	Protected Sub StokHareketleri2()
		Dim whrcnd As String = ""
		whrcnd = " MusteriKodu=" + Session("UserID").ToString
		Dim cmdstr As String = "SELECT STOK_HAREKET.SiparisKodu, STOK_HAREKET.UrunKodu, STOK_HAREKET.Miktar, URUNLER.StokAdi1, STOK_HAREKET.Tutar, URUNLER.Pic1 FROM URUNLER INNER JOIN STOK_HAREKET ON URUNLER.StokKodu = STOK_HAREKET.UrunKodu where STOK_HAREKET.SiparisKodu=" + SiparisKodu.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Adet" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Birim Fiyat" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Toplam" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		tbl5.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 5
			hc.InnerHtml = "Sepetinizde ürün bulunmuyor."
			hr.Cells.Add(hc)
			tbl5.Rows.Add(hr)
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
			tbl5.Rows.Add(hr)
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
		tbl5.Rows.Add(hr)
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
		tbl5.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Kargo Ücreti"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = "6 TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl5.Rows.Add(hr)
		hr = New HtmlTableRow
		hc = New HtmlTableCell
		hc.ColSpan = 4
		hc.InnerHtml = "Toplam"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		hc = New HtmlTableCell
		hc.InnerHtml = (AraToplam + 6).ToString("n2") + " TL"
		hc.Style.Add("text-align", "right")
		hr.Cells.Add(hc)
		tbl5.Rows.Add(hr)


	End Sub

End Class
