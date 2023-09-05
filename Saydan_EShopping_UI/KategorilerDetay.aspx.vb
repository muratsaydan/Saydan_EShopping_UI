Imports System.Data
Partial Class KategorilerDetay
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim Kod As Integer = 0
	Dim UstKod As Integer = 0

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Kod = Request.QueryString("kod")
		UstKod = Request.QueryString("ustkod")

		'Kod = 20

		If Not IsPostBack Then
			Saydanclass._HDrpDoldur(drpEbeveyn, "KATEGORILER", "KategoriKodu", "EbeveynKategoriKodu", "StokGrupAdi1", , , , "Siralama")
			drpEbeveyn.Items.Insert(0, "...boş...")
			If Kod = -1 And UstKod > 0 Then drpEbeveyn.SelectedValue = UstKod

			If Kod > 0 Then VarOlanKaydinBilgisiniDoldur(Kod)
		End If
		If Kod = -1 Then
			spnUrunler.Visible = False
		Else
			UrunlerListesi()
		End If

	End Sub

	Protected Sub VarOlanKaydinBilgisiniDoldur(ByVal Ref As Integer)
		Dim cmdstr As String = "SELECT * FROM [KATEGORILER] where KategoriKodu=" + Ref.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow = dt.Rows(0)
		lblKategoriKodu.Text = dr("KategoriKodu").ToString
		txtKategoriAdi.Text = dr("StokGrupAdi1").ToString
		lblKategoriAdi.Text = dr("StokGrupAdi1").ToString
		drpEbeveyn.SelectedValue = dr("EbeveynKategoriKodu").ToString
		txtKeywords.Text = dr("Keywords").ToString
		txtDescription.Text = dr("Description").ToString
		txtDetay.Text = dr("Detay").ToString
		txtSiralama.Text = dr("Siralama").ToString
	End Sub

	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		If Not Saydanclass.YoneticiMi Then Response.Redirect("Index.aspx")
	End Sub


	Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
		Dim cmdstr As String = "SELECT * FROM [KATEGORILER] where KategoriKodu=" + Kod.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow
		If Kod = -1 Then
			dr = dt.NewRow
		Else
			dr = dt.Rows(0)
		End If
		dr("StokGrupAdi1") = txtKategoriAdi.Text
		If IsNumeric(drpEbeveyn.SelectedValue) Then
			dr("EbeveynKategoriKodu") = drpEbeveyn.SelectedValue
		Else
			dr("EbeveynKategoriKodu") = Convert.DBNull
		End If
		dr("Keywords") = txtKeywords.Text
		dr("Description") = txtDescription.Text
		dr("Detay") = txtDetay.Text
		If IsNumeric(txtSiralama.Text) Then
			dr("Siralama") = txtSiralama.Text
		Else
			dr("Siralama") = 1
		End If
		If Kod = -1 Then dt.Rows.Add(dr)
		Saydanclass._SaveDB(cmdstr, dt)
		lblUyari.Text = "Kayıt işlemi tamamlanmıştır."
		divUyari.Visible = True
	End Sub



	Protected Sub UrunlerListesi()
		Dim hr As HtmlTableRow
		Dim hc As HtmlTableCell

		Dim cmdstr As String = "SELECT KATEGORILER_URUNLER.KategoriKodu, KATEGORILER_URUNLER.SiraNo, URUNLER.StokKodu, URUNLER.StokAdi1, URUNLER.KisaAciklama1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.AlisFiyati, URUNLER.Miktar, URUNLER.TedarikciFirma FROM KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu where KATEGORILER_URUNLER.KategoriKodu=" + Kod.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 8
			hc.InnerHtml = "Bu kategoriye ait ürün bulunmuyor."
			hr.Cells.Add(hc)
			tblUrunler.Rows.Add(hr)
			Exit Sub
		End If

		Dim SiraNo As Integer = 0
		hr = New HtmlTableRow
		hc = New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Seç" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Resim" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Miktar" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "<nobr>Alış Fiyatı<nobr><br><nobr>Satış Fiyatı<nobr>" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ziyaretçi Sayfası" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürün Detay Sayfası" : hr.Cells.Add(hc)
		tblUrunler.Rows.Add(hr)
		'lblAdet.Text = dt.Rows.Count.ToString

		Dim Urunler As String = ""
		Dim SatisFiyati As Decimal = 0
		Dim AlisFiyati As Decimal = 0
		For SiraNo = 0 To dt.Rows.Count - 1
			Dim dr As DataRow = dt.Rows(SiraNo)

			hr = New HtmlTableRow
			hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = "" : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = "<img src=""" + dr("Pic1").ToString + """ width=""75"" />" : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("StokAdi1").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = dr("Miktar").ToString : hr.Cells.Add(hc)
			hc = New HtmlTableCell : hc.InnerHtml = CDec(dr("AlisFiyati")).ToString("n2") + "<br>" + CDec(dr("SatisFiyati")).ToString("n2") : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)

			hc = New HtmlTableCell : hc.InnerHtml = "<a href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """ target=""_blank"">İzle</a>" : hr.Cells.Add(hc)

			hc = New HtmlTableCell : hc.InnerHtml = "<a href=""UrunlerDetay.aspx?kod=" + dr("StokKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
			tblUrunler.Rows.Add(hr)
		Next


	End Sub

	Protected Sub btnSil_Click(sender As Object, e As EventArgs) Handles btnSil.Click
		'Dim cmdstr As String = "SELECT * FROM [KATEGORILER] where KategoriKodu=" + Kod.ToString
		'Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		'Dim dr As DataRow
		'If Kod = -1 Then
		'	dr = dt.NewRow
		'Else
		'	dr = dt.Rows(0)
		'End If
		'dr("StokGrupAdi1") = txtKategoriAdi.Text
		'If IsNumeric(drpEbeveyn.SelectedValue) Then
		'	dr("EbeveynKategoriKodu") = drpEbeveyn.SelectedValue
		'Else
		'	dr("EbeveynKategoriKodu") = Convert.DBNull
		'End If
		'dr("Keywords") = txtKeywords.Text
		'dr("Description") = txtDescription.Text
		'dr("Detay") = txtDetay.Text
		'If IsNumeric(txtSiralama.Text) Then
		'	dr("Siralama") = txtSiralama.Text
		'Else
		'	dr("Siralama") = 1
		'End If
		'If Kod = -1 Then dt.Rows.Add(dr)
		'Saydanclass._SaveDB(cmdstr, dt)
		'lblUyari.Text = "Kayıt işlemi tamamlanmıştır."
		'divUyari.Visible = True
		Dim dt As DataTable = Saydanclass._ConnDB("SELECT        KATEGORILER_URUNLER.KategoriKodu, URUNLER.StokAdi1 FROM            KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu where KATEGORILER_URUNLER.KategoriKodu=" + Kod.ToString())
		If dt.Rows.Count > 0 Then
			lblUyari.Text = "Bu kategori altında ürün bulunmaktadır. Bu ürünleri başka kategorilere aktarmanız, sonra bu kategoriyi sil"
		End If

		Dim cmdstr = "Delete from KATEGORILER where KategoriKodu=" + Kod.ToString()
		Saydanclass.ExecSQL(cmdstr)
		Response.Redirect("KategorilerListe.aspx")

	End Sub
End Class
