Imports System.Data
Partial Class UrunlerListe
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim SiparisKodu As Integer = 0
	Dim SayfaNo As Integer = 1
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		SayfaNo = Request.QueryString("s")

		KategorilerListesi()
	End Sub


	Protected Sub KategorilerListesi()
		Dim hr As HtmlTableRow
		Dim hc As HtmlTableCell

		Dim Filtre As String = ""
		If txtBarkodNo.Text <> "" Then Filtre += " and BarkodNo like '%" + txtBarkodNo.Text + "%'"
		If txtStokAdi.Text <> "" Then Filtre += " and StokAdi1 like '%" + txtStokAdi.Text + "%'"
		If txtStokKodu.Text <> "" Then Filtre += " and StokKodu=" + txtStokKodu.Text
		If txtTedarikci.Text <> "" Then Filtre += " and TedarikciFirma like '%" + txtTedarikci.Text + "%'"
		If Filtre <> "" Then Filtre = " WHERE " + Filtre.Substring(5)

		Dim ToplamUrunAdedi As Integer = Saydanclass._ConnDB("SELECT count(*) as UrunAdedi FROM URUNLER " + Filtre).Rows(0)(0).ToString
		lblUrunAdedi.Text = ToplamUrunAdedi
		Dim SayfadaGosterilecekKayitAdedi As Integer = txtKayitAdedi.Text

		If SayfaNo = 0 Then SayfaNo = 1
		txtSayfaNo.Text = SayfaNo
		hypPrev.NavigateUrl = "UrunlerListe.aspx?s=" + (SayfaNo - 1).ToString
		hypNext.NavigateUrl = "UrunlerListe.aspx?s=" + (SayfaNo + 1).ToString
		If SayfaNo = 1 Then hypPrev.Enabled = False
		Dim ToplamSayfaSayisi As Integer = ToplamUrunAdedi / SayfadaGosterilecekKayitAdedi
		If ToplamUrunAdedi Mod SayfadaGosterilecekKayitAdedi > 0 Then ToplamSayfaSayisi += 1
		If SayfaNo = ToplamSayfaSayisi Then hypNext.Enabled = False


		Dim BaslangicKayitNo As Integer = (SayfaNo - 1) * SayfadaGosterilecekKayitAdedi

		Dim cmdstr As String = "SELECT * FROM [URUNLER] " + Filtre + " order by StokKodu  desc  OFFSET  " + BaslangicKayitNo.ToString + " rows FETCH NEXT " + SayfadaGosterilecekKayitAdedi.ToString + " ROWS ONLY"
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 9
			hc.InnerHtml = "Sistemde tanımlı bir ürün bulunmuyor."
			hr.Cells.Add(hc)
			tblKategoriler.Rows.Add(hr)
			Exit Sub
		End If


		Dim dvKategoriler As DataView = Saydanclass._ConnDB("SELECT KATEGORILER.StokGrupAdi1, KATEGORILER_URUNLER.UrunKodu, KATEGORILER_URUNLER.SiraNo FROM KATEGORILER_URUNLER INNER JOIN KATEGORILER ON KATEGORILER_URUNLER.KategoriKodu = KATEGORILER.KategoriKodu ORDER BY KATEGORILER_URUNLER.SiraNo").DefaultView


		Dim whrcnd As String = ""
		Dim SiraNo As Integer = BaslangicKayitNo
		hr = New HtmlTableRow
		hc = New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Resim" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Kodu" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kategorisi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adedi" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Alış Fiyatı<br />Satış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Keywords" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Description" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Aktif Mi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
		tblKategoriler.Rows.Add(hr)
		'lblAdet.Text = dt.Rows.Count.ToString

		Dim Urunler As String = ""
		Dim SatisFiyati As Decimal = 0
		Dim AlisFiyati As Decimal = 0
		For Each dr As DataRow In dt.Rows
			'Dim dr As DataRow = dt.Rows(SiraNo)
			SiraNo += 1
			hr = New HtmlTableRow
			hc = New HtmlTableCell : hc.InnerHtml = (SiraNo).ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.InnerHtml = "<img src=""" + dr("Pic1").ToString + """ width=""75"">"
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.InnerHtml = dr("StokAdi1").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.InnerHtml = dr("StokKodu").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell

			dvKategoriler.RowFilter = "UrunKodu=" + dr("StokKodu").ToString
			For Each drvKategoriler As DataRowView In dvKategoriler
				hc.InnerHtml += "<br />" + drvKategoriler("StokGrupAdi1").ToString
			Next
			If hc.InnerHtml <> "" Then hc.InnerHtml = hc.InnerHtml.Substring(6)
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.Style.Add(HtmlTextWriterStyle.TextAlign, "right")
			hc.InnerHtml = dr("Miktar").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			hc.Style.Add(HtmlTextWriterStyle.TextAlign, "right")
			hc.InnerHtml = dr("AlisFiyati").ToString + "<br />" + dr("SatisFiyati").ToString
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			Dim lbl As New Label
			lbl.Text = Mid(dr("TedarikciFirma").ToString, 1, 20)
			lbl.ToolTip = dr("TedarikciFirma").ToString
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
			lbl.Text = Mid(dr("Description").ToString, 1, 20)
			lbl.ToolTip = dr("Description").ToString
			hc.Controls.Add(lbl)
			hr.Cells.Add(hc)
			hc = New HtmlTableCell
			lbl = New Label
			If IsDBNull(dr("AktifMi")) Then dr("AktifMi") = True
			If dr("AktifMi") = True Then
				lbl.Text = "A"
			Else
				lbl.Text = "P"
			End If
			hc.Controls.Add(lbl)
			hr.Cells.Add(hc)


			hc = New HtmlTableCell : hc.InnerHtml = "<a href=""UrunlerDetay.aspx?kod=" + dr("StokKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
			tblKategoriler.Rows.Add(hr)
		Next



	End Sub



	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		If Not Saydanclass.YoneticiMi Then Response.Redirect("index.aspx")
		'If Session("UserID") Is Nothing Then Response.Redirect("login.aspx")
		'If Session("YoneticiMi") Is Nothing Then Response.Redirect("index.aspx")
		'If Session("YoneticiMi") <> True Then Response.Redirect("index.aspx")
	End Sub


End Class
