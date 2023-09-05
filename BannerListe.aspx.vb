Imports System.Data
Partial Class BannerListe
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



		Dim dt As DataTable = Saydanclass._ConnDB("SELECT * from BANNER order by Siralama")


		Dim whrcnd As String = ""
		Dim SiraNo As Integer = 0
		hr = New HtmlTableRow
		hc = New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Resim" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Açıklama" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Link" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Detay" : hr.Cells.Add(hc)
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
			hc.InnerHtml = "<img src='" + dr("ResimURL").ToString + "' height='250px' />"
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.InnerHtml = dr("UrunAdi").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.InnerHtml = dr("BannerAciklamasi").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.InnerHtml = "<a href='" + dr("Link").ToString + "' target='_blank'>" + dr("Link").ToString + "</a>"
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.InnerHtml = "<a href='BannerDetay.aspx?kod=" + dr("BannerKodu").ToString + "' target='_blank'>Detay</a>"
			hr.Cells.Add(hc)

			tblKategoriler.Rows.Add(hr)
		Next


	End Sub



	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		'Saydanclass.LoginKontroluYap()
		'If Not Saydanclass.YoneticiMi Then Response.Redirect("index.aspx")
	End Sub


End Class
