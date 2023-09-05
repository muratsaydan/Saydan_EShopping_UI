Imports System.Data
Partial Class product_summary
    Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		SepettekiUrunler()
	End Sub

	'Protected Sub btnGiris_Click(sender As Object, e As EventArgs) Handles btnGiris.Click
	'	Dim cmdstr As String = "select * from [dbo].[MUSTERILER] where Email='" + inputUsername.Text + "' and Password='" + inputPassword1.Text + "'"
	'	Dim dt As DataTable = SaydanClass._ConnDB(cmdstr)
	'	If dt.Rows.Count > 0 Then
	'		Dim dr As DataRow = dt.Rows(0)
	'		Session("UserID") = dr("MusteriKodu").ToString
	'		Session("UserName") = dr("Isim").ToString + " " + dr("Soyisim").ToString
	'		'Üye olmadan önce sepete yüklenen birşeyler varsa onların müşteri kodlarını güncelle ki müşterimizi uğraştırmayalım.
	'		cmdstr = "select * from SEPET where SessionID='" + Session("SessionID").ToString.ToString + "'"
	'		dt = SaydanClass._ConnDB(cmdstr)
	'		For Each dr In dt.Rows
	'			dr("MusteriKodu") = Session("UserID").ToString
	'		Next
	'		Saydanclass._SaveDB(cmdstr, dt)
	'	End If
	'	Response.Redirect("product_summary.aspx")
	'End Sub
	Protected Sub SepettekiUrunler()
		Dim whrcnd As String = ""
		If Session("UserID") Is Nothing Then
			whrcnd = " SessionID='" + Session("SessionID").ToString + "'"
		Else
			whrcnd = " MusteriKodu=" + Session("UserID").ToString
		End If
		Dim cmdstr As String = "SELECT SEPET.SepetKodu, SEPET.MusteriKodu, SEPET.UrunKodu, SEPET.SepeteEklenmeTarihi, SUM(SEPET.Miktar) AS Miktar, SEPET.SessionID, URUNLER.StokAdi1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.KDVOrani, URUNLER.Indirim FROM SEPET INNER JOIN URUNLER ON SEPET.UrunKodu = URUNLER.StokKodu  where " + whrcnd + " GROUP BY SEPET.SepetKodu, SEPET.MusteriKodu, SEPET.UrunKodu, SEPET.SepeteEklenmeTarihi, SEPET.SessionID, URUNLER.StokAdi1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.KDVOrani, URUNLER.Indirim"
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim cmdhtml As String = ""
		Dim SiraNo As Integer = 0
		cmdhtml += vbCrLf + " <table class=""table table-bordered"">"
		cmdhtml += vbCrLf + " <thead><tr><th>#</th><th>Ürün</th><th>Açıklama</th><th>Adet</th><th>Birim Fiyat</th><th>Toplam</th></tr></thead><tbody>"
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Adet" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Birim Fiyat" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Toplam" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		tbl1.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 6
			hc.InnerHtml = "Sepetinizde ürün bulunmuyor."
			hr.Cells.Add(hc)
			tbl1.Rows.Add(hr)
			Exit Sub
		End If
		For Each dr As DataRow In dt.Rows
			SiraNo += 1
			hr = New HtmlTableRow
			hc = New HtmlTableCell : hc.InnerHtml = SiraNo.ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = " <img width=""60"" src=""" + dr("Pic1").ToString + """ alt=""" + dr("StokAdi1").ToString + """/>" : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("StokAdi1").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell
			Dim txt As New TextBox
			txt.Width = 75
			txt.AutoPostBack = True
			AddHandler txt.TextChanged, AddressOf txt_changed
			txt.Style.Add("margin-top", "10px")
			txt.ID = "txt_" + dr("SepetKodu").ToString
			If Not IsPostBack Then txt.Text = dr("Miktar")
			hc.Controls.Add(txt)
			Dim btn As New LinkButton
			btn.ID = "btnminus_" + dr("SepetKodu").ToString
			btn.Attributes.Add("class", "btn")
			btn.Text = "<i class=""icon-minus""></i>"
			AddHandler btn.Click, AddressOf btnminus_Click
			hc.Controls.Add(btn)
			btn = New LinkButton
			btn.ID = "btnplus_" + dr("SepetKodu").ToString
			btn.Attributes.Add("class", "btn")
			btn.Text = "<i class=""icon-plus""></i>"
			AddHandler btn.Click, AddressOf btnplus_Click
			hc.Controls.Add(btn)
			btn = New LinkButton
			btn.ID = "btndelete_" + dr("SepetKodu").ToString
			btn.Attributes.Add("class", "btn btn-danger")
			btn.Text = "<i class=""icon-remove icon-white""></i>"
			AddHandler btn.Click, AddressOf btndelete_Click
			hc.Controls.Add(btn)
			hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.Style.Add("text-align", "right") : hc.InnerHtml = CDec(dr("SatisFiyati")).ToString("n2") + " TL" : hr.Cells.Add(hc)
			Dim Toplam As Decimal = dr("Miktar") * dr("SatisFiyati")
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
	Protected Sub txt_changed(sender As TextBox, e As EventArgs)
		If Not IsNumeric(sender.Text) Then sender.Text = 1
	End Sub
	Protected Sub btnminus_Click(sender As Object, e As EventArgs)
		Dim SepetKodu As Integer = Mid(sender.id, Len("btnminus_") + 1)
		Dim txt As TextBox = tbl1.FindControl("txt_" + SepetKodu.ToString)
		txt.Text = CInt(txt.Text) - 1
		Saydanclass.ExecSQL("UPDATE SEPET set Miktar=" + txt.Text + " where SepetKodu=" + SepetKodu.ToString)
		Response.Redirect(Me.Page.AppRelativeVirtualPath + "?" + Page.ClientQueryString)
	End Sub
	Protected Sub btnplus_Click(sender As Object, e As EventArgs)
		Dim SepetKodu As Integer = Mid(sender.id, Len("btnplus_") + 1)
		Dim txt As TextBox = tbl1.FindControl("txt_" + SepetKodu.ToString)
		txt.Text = CInt(txt.Text) + 1
		Saydanclass.ExecSQL("UPDATE SEPET set Miktar=" + txt.Text + " where SepetKodu=" + SepetKodu.ToString)
		Response.Redirect(Me.Page.AppRelativeVirtualPath + "?" + Page.ClientQueryString)
	End Sub
	Protected Sub btndelete_Click(sender As Object, e As EventArgs)
		Dim SepetKodu As Integer = Mid(sender.id, Len("btndelete_") + 1)
		Saydanclass.ExecSQL("delete from SEPET where SepetKodu=" + SepetKodu.ToString)
		Response.Redirect(Me.Page.AppRelativeVirtualPath + "?" + Page.ClientQueryString)
	End Sub
End Class
