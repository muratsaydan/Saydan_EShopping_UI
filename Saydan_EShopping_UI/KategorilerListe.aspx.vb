Imports System.Data
Partial Class KategorilerListe
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim SiparisKodu As Integer = 0

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		KategorilerListesi()
	End Sub

	Protected Sub KategorilerListesi()
		Dim hr As HtmlTableRow
		Dim hc As HtmlTableCell

		Dim cmdstr As String = "SELECT * FROM [KATEGORILER] order by Siralama ASC"
		Dim dtRaw As DataTable = Saydanclass._ConnDB(cmdstr)
		If dtRaw.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 9
			hc.InnerHtml = "Sistemde tanımlı bir kategori bulunmuyor."
			hr.Cells.Add(hc)
			tblKategoriler.Rows.Add(hr)
			Exit Sub
		End If

		Dim dt As DataTable = Saydanclass.Hiyerarsik_Tablo_Olusturma(dtRaw, "KategoriKodu", "EbeveynKategoriKodu")


		Dim dvUrunAdedi As DataView = Saydanclass._ConnDB("SELECT KATEGORILER.KategoriKodu, COUNT(KATEGORILER_URUNLER.UrunKodu) AS UrunSayisi FROM KATEGORILER INNER JOIN KATEGORILER_URUNLER ON KATEGORILER.KategoriKodu = KATEGORILER_URUNLER.KategoriKodu INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu GROUP BY KATEGORILER.KategoriKodu").DefaultView


		Dim whrcnd As String = ""
		Dim SiraNo As Integer = 0
		hr = New HtmlTableRow
		hc = New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ekle" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kategori Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kategori Kodu" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Description" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Keywords" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Detay" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adedi" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sıralama" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
		tblKategoriler.Rows.Add(hr)
		'lblAdet.Text = dt.Rows.Count.ToString

		Dim Urunler As String = ""
		Dim SatisFiyati As Decimal = 0
		Dim AlisFiyati As Decimal = 0
		For SiraNo = 0 To dt.Rows.Count - 1
			Dim dr As DataRow = dt.Rows(SiraNo)

			hr = New HtmlTableRow
			hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.Style.Add(HtmlTextWriterStyle.PaddingLeft, ((dr("__Level") - 1) * 30).ToString + "px")
			hc.Style.Add(HtmlTextWriterStyle.TextAlign, "center")
			Dim hyp As New HyperLink
			hyp.NavigateUrl = "KategorilerDetay.aspx?kod=-1&ustkod=" + dr("KategoriKodu").ToString
			hyp.Text = "<i class=""icon-chevron-right"" aria-hidden=""true""></i>"
			hyp.Target = "_blank"
			hc.Controls.Add(hyp)
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.Style.Add(HtmlTextWriterStyle.PaddingLeft, (dr("__Level") * 30).ToString + "px")
			hc.InnerHtml = dr("StokGrupAdi1").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.Style.Add(HtmlTextWriterStyle.PaddingLeft, (dr("__Level") * 30).ToString + "px")
			hc.InnerHtml = dr("KategoriKodu").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			Dim lbl As New Label
			lbl.Text = Mid(dr("Description").ToString, 1, 20)
			lbl.ToolTip = dr("Description").ToString
			hc.Controls.Add(lbl)
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			lbl = New Label
			lbl.Text = Mid(dr("Keywords").ToString, 1, 20)
			lbl.ToolTip = dr("Keywords").ToString
			hc.Controls.Add(lbl)
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			lbl = New Label
			lbl.Text = Mid(dr("Detay").ToString, 1, 20)
			lbl.ToolTip = dr("Detay").ToString
			hc.Controls.Add(lbl)
			hr.Cells.Add(hc)

			dvUrunAdedi.RowFilter = "KategoriKodu=" + dr("KategoriKodu").ToString
			Dim UrunAdedi As Integer = 0
			If dvUrunAdedi.Count > 0 Then UrunAdedi = dvUrunAdedi(0)("UrunSayisi").ToString
			hc = New HtmlTableCell : hc.InnerHtml = UrunAdedi : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("Siralama").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = "<a href=""KategorilerDetay.aspx?kod=" + dr("KategoriKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
			tblKategoriler.Rows.Add(hr)
		Next


	End Sub



	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		If Not Saydanclass.YoneticiMi Then Response.Redirect("index.aspx")
	End Sub


End Class
